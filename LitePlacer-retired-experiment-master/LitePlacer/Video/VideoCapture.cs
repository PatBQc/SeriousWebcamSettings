using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

// implemetned https://code.google.com/p/aforge/issues/detail?id=357#makechanges
// rather than the stock aforge code to enable setting brightness,etc..

namespace LitePlacer {
    public delegate void NewFrameCaptureDelegate(Bitmap bitmap);
    public enum CameraType {
        UpCamera, DownCamera
    };


    public class VideoCapture {
        VideoCaptureDevice _videoSource;
        MyAsyncVideoSource VideoSource;
        public CameraType cameraType;
        private int videoCapabilityIndex = 0;

        int FrameNumber;
        string MonikerString = "";

        public bool VirtualBox = false;

        public Size FrameSize { get { return new Size(FrameSizeX, FrameSizeY); } }
        public int FrameSizeX { get { return (VideoSource != null) ? _videoSource.VideoCapabilities[videoCapabilityIndex].FrameSize.Width  : 0; } }
        public int FrameSizeY { get { return (VideoSource != null) ? _videoSource.VideoCapabilities[videoCapabilityIndex].FrameSize.Height : 0; } }
        public int FrameCenterX { get { return FrameSizeX / 2; } }
        public int FrameCenterY { get { return FrameSizeY / 2; } }
        public PartLocation FrameCenter { get { return new PartLocation(FrameCenterX, FrameCenterY); } }

        public List<NewFrameCaptureDelegate> FrameCaptureDelegates = new List<NewFrameCaptureDelegate>();

        public VideoCapture(CameraType type) {
            cameraType = type;
        }

        public bool IsDown() { return (cameraType == CameraType.DownCamera); }
        public bool IsUp() { return (cameraType == CameraType.UpCamera); }

        public bool IsRunning() {
            if (VideoSource != null) return (VideoSource.IsRunning);
            return false;
        }

        // Asks nicely
        public void SignalToStop() {
            timer.Stop();
            VideoSource.SignalToStop();
        }

        // Tries to force it (but still doesn't always work, just like with children)
        public void NakedStop() {
            timer.Stop();
            VideoSource.Stop();
        }

        public void NoWaitClose() {
            if (VideoSource == null || !VideoSource.IsRunning) return;
            timer.Stop();
            VideoSource.NewFrame -= NewFrame;
            VideoSource.SignalToStop();
            //VideoSource = null;
            MonikerString = "";
        }

        public void Close() {
            if (VideoSource == null || !VideoSource.IsRunning) return;
            timer.Stop();
            VideoSource.SignalToStop();
            VideoSource.WaitForStop();  // problem?
            VideoSource.NewFrame -= NewFrame;
            VideoSource.Stop();
            VideoSource = null;
            MonikerString = "";
        }

        public void DisplayPropertyPage() {
            _videoSource.DisplayPropertyPage(IntPtr.Zero);
        }

        public void SetMaxResolution() {
            videoCapabilityIndex = _videoSource.VideoCapabilities.Length - 1; //the last one is the highest resolution for my cameras. not sure it's generalizable
            _videoSource.VideoResolution = _videoSource.VideoCapabilities[videoCapabilityIndex];
            Global.Instance.DisplayText("Setting Camera " + cameraType + " to resolution " + _videoSource.VideoCapabilities[videoCapabilityIndex].FrameSize);
        }

        System.Timers.Timer timer = new System.Timers.Timer(1000);




        public string BrightnessRange { get; private set; }
        public string ExposureRange   { get; private set; }

        public bool Start(int DeviceNo) {
            try {
                FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                MonikerString = videoDevices[DeviceNo].MonikerString;
                _videoSource = new VideoCaptureDevice(MonikerString);
                VideoSource = new MyAsyncVideoSource(_videoSource, true);
                //SetMaxResolution();
                VideoSource.NewFrame += NewFrame;
                VideoSource.Start();
                FrameNumber = 0;

                // measure range
                int min,max,step,def;
                CameraControlFlags ccflags;
                VideoProcAmpFlags  vpflags;
                _videoSource.GetCameraPropertyRange(CameraControlProperty.Exposure, out min, out max, out step, out def, out ccflags);
                ExposureRange = String.Format("{0} to {1})", min, max, step);
                _videoSource.GetVideoPropertyRange(VideoProcAmpProperty.Brightness, out min, out max, out step, out def, out vpflags);
                BrightnessRange = String.Format("{0} to {1})", min, max, step);

                timer.Elapsed += delegate {
                    FPSReceived = VideoSource.FramesReceived;
                    FPSProcessed = VideoSource.FramesProcessed;
                };
                timer.Start();              

                return VideoSource.IsRunning;
            } catch {
                return false;
            }
        }

        int _fps_received, _fps_processed;
        public int FPSReceived {
            get { return _fps_received; }
            private set { _fps_received = value; }
        }
        public int FPSProcessed {
            get { return _fps_processed; }
            private set { _fps_processed = value; }
        }
        public int FPSDropped {
            get { return FPSReceived - FPSProcessed; }
        }


        /* a bunch of BS to help ensure we avoid race conditions */
        /* still untested but was seeing problems before */
        object NewFrameLock = new object();
        Bitmap lastFrame;
        public Bitmap GetFrame() {
            Global.DoBackgroundWork(); //make sure we get an updated image
            bool acquiredLock = false;
            if (lastFrame == null) return null;
            Bitmap ret = null;
            while (ret == null) {
                try {
                    Monitor.TryEnter(NewFrameLock, ref acquiredLock);
                    if (acquiredLock) {
                        ret = (Bitmap)lastFrame.Clone();
                    } else {
                        Thread.Sleep(1); //wait for someone else to give up the lock?
                    }
                } finally {
                    if (acquiredLock) {
                        Monitor.Exit(NewFrameLock);
                    }
                }
            }
            return ret;
        }



  
        private void NewFrame(object sender, NewFrameEventArgs eventArgs) {
          //  VideoSource.NewFrame -= NewFrame;
            bool acquiredLock = false;

            // (try to) set the last frame
            try {
                Monitor.TryEnter(NewFrameLock, ref acquiredLock);
                if (acquiredLock) {
                    //Console.WriteLine("Setting lastframe with thread " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                    if (lastFrame != null) lastFrame.Dispose();
                    lastFrame = (Bitmap)eventArgs.Frame.Clone();
                    FrameNumber++;
                } else {
                    Console.WriteLine("Dropping Frame due to lock");
                }
            } catch {
                Console.WriteLine("dropping frame with thread " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            } finally {
                if (acquiredLock) Monitor.Exit(NewFrameLock);
            }

            //forward copies of the bitmap to any delegates
            foreach (var x in FrameCaptureDelegates) {
               // Console.WriteLine("Forwarding Frame From Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                x((Bitmap)eventArgs.Frame.Clone());
            }
            //if (VideoSource != null) VideoSource.NewFrame += NewFrame;
        }


        /* STATIC FUNCTIONS */
        public static List<string> GetVideoDeviceList() {
            List<string> Devices = new List<string>();

            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices) {
                Devices.Add(device.Name);
            }
            return (Devices);
        }


        // ---------------------------------------------------------------//
        // -----------Camera setting functions from aforge        --------//
        // ---------------------------------------------------------------//
        private Dictionary<string, int> _values = new Dictionary<string,int>() { 
            {"Exposure",10000},
            {"Brightness", 10000}};

        
        public int GetExposure() {
            if (VirtualBox) { return -1;  }

            int val;
            CameraControlFlags flags;
            try {
                if (_videoSource.GetCameraProperty(CameraControlProperty.Exposure, out val, out flags)) return val;
            } catch {
                // ignored
            }
            return -1;
        }
        
        public void SetExposure(int val, CameraControlFlags flags = CameraControlFlags.Manual) {
            if (_values["Exposure"] != val) {
                _videoSource.SetCameraProperty(CameraControlProperty.Exposure, val, flags);
                _values["Exposure"] = val;
            }
        }

        public void UnlockExposure() {
            int val = GetExposure();
            if (val != -1) SetExposure(val, CameraControlFlags.Auto);
            _values["Exposure"] = 10000;
        }

        public int GetBrightness() {
            if (VirtualBox) { return -1; }
            
            int val;
            VideoProcAmpFlags flags;
            try {
                if (_videoSource.GetVideoProperty(VideoProcAmpProperty.Brightness, out val, out flags)) return val;
            } catch {
                //ignore
            }
            return -1;
        }

        public void SetBrightness(int val, VideoProcAmpFlags flags = VideoProcAmpFlags.Manual) {
            if (_values["Brightness"] != val) {
                _videoSource.SetVideoProperty(VideoProcAmpProperty.Brightness, val, flags);
                _values["Brightness"] = val;
            }
        }
        
        public void UnlockBrightness() {
            int val = GetBrightness();
            if (val != -1) SetBrightness(val, VideoProcAmpFlags.Auto);
            _values["Brightness"] = 10000;
        }


        public void test() {
            int val;
            VideoProcAmpFlags flags;
            _videoSource.GetVideoProperty(VideoProcAmpProperty.BacklightCompensation, out val, out flags);
            
        }


    }
}
