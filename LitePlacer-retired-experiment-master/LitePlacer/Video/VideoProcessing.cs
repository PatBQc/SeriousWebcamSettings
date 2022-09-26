using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using LitePlacer.Properties;
using Point = System.Drawing.Point;

namespace LitePlacer {
    public class VideoProcessing {
        private BindingList<AForgeFunction> FunctionList = new BindingList<AForgeFunction>();
        private BindingList<AForgeFunction> NewFunctionList;
        public List<PointF> MarkA = new List<PointF>();
        public List<PointF> MarkB = new List<PointF>();
        public List<Shapes.Arrow> Arrows = new List<Shapes.Arrow>();
        public List<VideoTextMarkup> MarkupText = new List<VideoTextMarkup>();
        private VideoCapture videoCapture;
        public VideoOverlaySettings s;

        public bool ApplyVideoMarkup = true;

        public VideoProcessing(VideoCapture vc, VideoOverlaySettings settings) {
            videoCapture = vc;
            s = settings;
        }

        // transitional helper functions - while code is in flux
        public bool IsUpCamera() { return videoCapture.IsUp(); }
        public PartLocation FrameCenter { get { return videoCapture.FrameCenter; } }
        public System.Drawing.Size FrameSize { get { return videoCapture.FrameSize; } }

        public PartLocation mmPerPixel{get { return new PartLocation(XmmPerPixel, YmmPerPixel); }}

        public double XmmPerPixel {
            get { return (IsUpCamera()) ? Settings.Default.UpCam_XmmPerPixel : Settings.Default.DownCam_XmmPerPixel; }
        }
        public double YmmPerPixel {
            get { return (IsUpCamera()) ? Settings.Default.UpCam_YmmPerPixel : Settings.Default.DownCam_YmmPerPixel; }
        }

        public bool PauseProcessing { get { return !ApplyVideoMarkup; } set { ApplyVideoMarkup = !value; } }   // Drawing the video slows everything down. This can pause it for measurements.

        public Shapes.Rectangle box;

        // And calls xx_measure() funtion. (Any function doing measurement from video frames.)
        // The xxx_measure funtion calls GetMeasurementFrame() function, that takes a frame form the stream, 
        // processes it with the MeasurementFunctions list and returns the processed frame:
        public bool IsNewFunctionListPending { get { return (NewFunctionList != null); } }

        public Bitmap ProcessFrame(Bitmap measurementFrame) {
            // if we have an updated list, then apply it the next itteration
            if (NewFunctionList != null) {
                FunctionList = NewFunctionList;
                NewFunctionList = null;
            }

            // apply video processing functions
            if (FunctionList != null) {
                var count = FunctionList.Count;
                for (int i=0; i<count; i++) {
                    var f = FunctionList[i];                
                    if (!f.Enabled) continue;
                    switch (f.Method) {
                        case AForgeMethod.Grayscale:
                            GrayscaleFunc(ref measurementFrame);
                            break;
                        case AForgeMethod.ContrastStretch:
                            Contrast_scretchFunc(ref measurementFrame);
                            break;
                        case AForgeMethod.KillColor:
                            KillColor_Func(ref measurementFrame, f.parameter_double, f.R, f.G, f.B);
                            break;
                        case AForgeMethod.KeepColor:
                            KeepColor_Func(ref measurementFrame, f.parameter_double, f.R, f.G, f.B);
                            break;
                        case AForgeMethod.Invert:
                            InvertFunct(ref measurementFrame);
                            break;
                        case AForgeMethod.Zoom:
                            Meas_ZoomFunc(ref measurementFrame, f.parameter_double);
                            break;
                        case AForgeMethod.EdgeDetect1:
                            Edge_detectFunc(ref measurementFrame, 1); 
                            break;
                        case AForgeMethod.EdgeDetect2:
                            Edge_detectFunc(ref measurementFrame, 2); 
                            break;
                        case AForgeMethod.EdgeDetect3:
                            Edge_detectFunc(ref measurementFrame, 3); 
                            break;
                        case AForgeMethod.EdgeDetect4:
                            Edge_detectFunc(ref measurementFrame, 4); 
                            break;
                        case AForgeMethod.NoiseReduction1:
                            NoiseReduction_Funct(ref measurementFrame, 1);
                            break;
                        case AForgeMethod.NoiseReduciton2:
                            NoiseReduction_Funct(ref measurementFrame, 2);
                            break;
                        case AForgeMethod.NoiseReduction3:
                            NoiseReduction_Funct(ref measurementFrame, 3);
                            break;
                         case AForgeMethod.Threshold:
                            ThresholdFunct(ref measurementFrame, f.parameter_double);
                            break;
                        case AForgeMethod.Histogram:
                            HistogramFunct(ref measurementFrame);
                            break;
                        case AForgeMethod.CameraExposure:
                            videoCapture.SetExposure((int)f.parameter_double);
                            break;
                        case AForgeMethod.VideoBrightness:
                            videoCapture.SetBrightness((int)f.parameter_double);
                            break;
                    }
                }
            }


            // flip the measurement displayFrame so it looks the same as the display displayFrame XXX not sure why this is (?)
            if (videoCapture.IsUp()) new Mirror(false, true).ApplyInPlace(measurementFrame);
            return measurementFrame;
        }


        public Bitmap ApplyMarkup(Bitmap displayFrame) {
            //further modify the image
            if (ApplyVideoMarkup) {
                if (s.FindCircles) DrawCirclesFunct(displayFrame, this);
                if (s.FindRectangles) displayFrame = DrawRectanglesFunct(displayFrame, this);
                if (s.FindFiducial) DrawFiducialFunct(ref displayFrame, this);
                if (s.FindComponent) displayFrame = DrawComponentsFunct(displayFrame, this);
              
                if (s.DrawBox) DrawBoxFunct(displayFrame);
                if (MarkA.Count > 0) DrawMarks(ref displayFrame, MarkA, Color.Blue, 20);
                if (MarkB.Count > 0) DrawMarks(ref displayFrame, MarkB, Color.Red, 20);
                if (Arrows.Count > 0) DrawArrow(ref displayFrame, Arrows);
                
                if (s.Draw1mmGrid) DrawGridFunct(ref displayFrame);
                if (s.DrawCross) DrawCrossFunct(ref displayFrame);
                if (s.DrawDashedCross) DrawDashedCrossFunct(displayFrame);
                if (MarkupText.Count > 0) DrawMarkupText(ref displayFrame, MarkupText);
            }

            return displayFrame;
        } 




        // =========================================================
        // Snapshot handling
        // =========================================================

        // repeated rotations destroy the image. We'll store the original here and rotate only once.
        Bitmap SnapshotOriginalImage = new Bitmap(640, 480);

        Bitmap SnapshotImage = new Bitmap(640, 480);

        private void TakeSnapshot_funct(Bitmap img) {
            Bitmap image = Grayscale.CommonAlgorithms.RMY.Apply(img);

            // find edges
            SobelEdgeDetector EdgeFilter = new SobelEdgeDetector();
            EdgeFilter.ApplyInPlace(image);
            // back to color format
            GrayscaleToRGB RGBfilter = new GrayscaleToRGB();
            image = RGBfilter.Apply(image);
            // get rid of grays
            EuclideanColorFiltering filter = new EuclideanColorFiltering();
            filter.CenterColor.Red = 20;
            filter.CenterColor.Green = 20;
            filter.CenterColor.Blue = 20;
            filter.FillOutside = false;
            filter.Radius = 200;
            filter.ApplyInPlace(image);

            Color peek;
            for (int y = 0; y < image.Height; y++) {
                for (int x = 0; x < image.Width; x++) {
                    peek = image.GetPixel(x, y);
                    if (peek.R != 0) {
                        image.SetPixel(x, y, Color.Blue);
                    }
                }
            }

            image.MakeTransparent(Color.Black);
            SnapshotImage = image;
            SnapshotOriginalImage = image;
        }




        // Since the measured image might be zoomed in, we need the value, so that we can convert to real measurements (public for debug)
        public double GetZoom() {
            double zoom = 1.0;
            foreach (var x in FunctionList.Where(x => x.Method == AForgeMethod.Zoom).Select(x => x.parameter_double).ToList()) {
                zoom *= x;
            }
            return zoom; 
        }

  //      public void ClearFunctionsList() {
  //          NewFunctionList = new BindingList<AForgeFunction>();
  //      }

        public void SetFunctionsList(string name) {
            if (name == null) NewFunctionList = new BindingList<AForgeFunction>();
            else              NewFunctionList = AForgeFunctionSet.GetFunctionsFromDisk(name);
        }


        public void SetFunctionsList(BindingList<AForgeFunction> list) {
            if (list == null) NewFunctionList = new BindingList<AForgeFunction>();
            else              NewFunctionList = list;
        }
        

        // ==========================================================================================================
        // Functions compatible with lists:
        // ==========================================================================================================
        // Note, that each function needs to keep the image in RGB, otherwise drawing fill fail 

        // ========================================================= 

        private void NoiseReduction_Funct(ref Bitmap frame, int par_int) {
            frame = Grayscale.CommonAlgorithms.RMY.Apply(frame);	// Make gray
            switch (par_int) {
                case 1:
                    BilateralSmoothing Bil_filter = new BilateralSmoothing();
                    Bil_filter.KernelSize = 7;
                    Bil_filter.SpatialFactor = 10;
                    Bil_filter.ColorFactor = 30;
                    Bil_filter.ColorPower = 0.5;
                    Bil_filter.ApplyInPlace(frame);
                    break;

                case 2:
                    Median M_filter = new Median();
                    M_filter.ApplyInPlace(frame);
                    break;

                case 3:
                    Mean Meanfilter = new Mean();
                    // apply the filter
                    Meanfilter.ApplyInPlace(frame);
                    break;

                default:
                    Median Median_filter = new Median();
                    Median_filter.ApplyInPlace(frame);
                    break;
            }
            GrayscaleToRGB RGBfilter = new GrayscaleToRGB();	// back to color format
            frame = RGBfilter.Apply(frame);
        }

        // =========================================================
        private void Edge_detectFunc(ref Bitmap frame, int par_int) {
            frame = Grayscale.CommonAlgorithms.RMY.Apply(frame);	// Make gray
            switch (par_int) {
                case 1:
                    SobelEdgeDetector SobelFilter = new SobelEdgeDetector();
                    SobelFilter.ApplyInPlace(frame);
                    break;

                case 2:
                    DifferenceEdgeDetector DifferenceFilter = new DifferenceEdgeDetector();
                    DifferenceFilter.ApplyInPlace(frame);
                    break;

                case 3:
                    HomogenityEdgeDetector HomogenityFilter = new HomogenityEdgeDetector();
                    HomogenityFilter.ApplyInPlace(frame);
                    break;

                case 4:
                    // can we not have references to canny in the code. gives me ptsd flashbacks
                    CannyEdgeDetector Nightmare = new CannyEdgeDetector();
                    // apply the filter
                    Nightmare.ApplyInPlace(frame);
                    break;

                default:
                    HomogenityEdgeDetector filter = new HomogenityEdgeDetector();
                    filter.ApplyInPlace(frame);
                    break;
            }
            GrayscaleToRGB RGBfilter = new GrayscaleToRGB();	// back to color format
            frame = RGBfilter.Apply(frame);
        }

        // =========================================================
        private void InvertFunct(ref Bitmap frame) {
            Invert filter = new Invert();
            filter.ApplyInPlace(frame);
        }

        // =========================================================
        private void HistogramFunct(ref Bitmap frame) {
            // create filter
            HistogramEqualization filter = new HistogramEqualization();
            // process image
            filter.ApplyInPlace(frame);
        }


        // =========================================================
        private void KillColor_Func(ref Bitmap frame, double par_d, int par_R, int par_G, int par_B) {
            // create filter
            EuclideanColorFiltering filter = new EuclideanColorFiltering();
            // set center colol and radius
            filter.CenterColor = new RGB((byte)par_R, (byte)par_G, (byte)par_B);
            filter.Radius = (short)par_d;
            filter.FillOutside = false;
            // apply the filter
            filter.ApplyInPlace(frame);
        }

        // =========================================================
        private void KeepColor_Func(ref Bitmap frame, double par_d, int par_R, int par_G, int par_B) {
            // create filter
            EuclideanColorFiltering filter = new EuclideanColorFiltering();
            // set center colol and radius
            filter.CenterColor = new RGB((byte)par_R, (byte)par_G, (byte)par_B);
            filter.Radius = (short)par_d;
            filter.FillOutside = true;
            // apply the filter
            filter.ApplyInPlace(frame);
        }

        // =========================================================
        private void ThresholdFunct(ref Bitmap frame,  double par_d) {
            try {
                frame = Grayscale.CommonAlgorithms.RMY.Apply(frame);
                Threshold filter = new Threshold((int)par_d);
                filter.ApplyInPlace(frame);
                GrayscaleToRGB toColFilter = new GrayscaleToRGB();
                frame = toColFilter.Apply(frame);
            } catch {
            }
        }


        // ========================================================= Contrast_scretchFunc
        private void GrayscaleFunc(ref Bitmap frame) {
            Grayscale toGrFilter = new Grayscale(0.2125, 0.7154, 0.0721);       // create grayscale filter (BT709)
            Bitmap fr = toGrFilter.Apply(frame);
            GrayscaleToRGB toColFilter = new GrayscaleToRGB();
            frame = toColFilter.Apply(fr);
        }

        private void Contrast_scretchFunc(ref Bitmap frame) {
            ContrastStretch filter = new ContrastStretch();
            // process image
            filter.ApplyInPlace(frame);
        }

        // =========================================================
        private void Meas_ZoomFunc(ref Bitmap frame,double par_d) {
            ZoomFunct(ref frame, par_d);
        }

        private Bitmap DrawComponentsFunct(Bitmap bitmap, VideoProcessing vp) {
            List<Shapes.Component> Components = VideoDetection.FindComponents(vp,bitmap);

            Graphics g = Graphics.FromImage(bitmap);
            Pen OrangePen = new Pen(Color.DarkOrange, 1);
            Pen RedPen = new Pen(Color.DarkRed, 2);
            Pen BluePen = new Pen(Color.Blue, 2);
            Shapes.Component Component;
            Point p1 = new Point();
            Point p2 = new Point();

            for (int i = 0, n = Components.Count; i < n; i++) {
                Component = Components[i];

                // move Component.Longest start to ComponentCenter, draw it
                float dx = Component.Center.X - Component.Longest.Start.X;
                float dy = Component.Center.Y - Component.Longest.Start.Y;
                p1.X = (int)Math.Round(Component.Longest.Start.X + dx);
                p1.Y = (int)Math.Round(Component.Longest.Start.Y + dy);
                p2.X = (int)Math.Round(Component.Longest.End.X + dx);
                p2.Y = (int)Math.Round(Component.Longest.End.Y + dy);
                g.DrawLine(RedPen, p1, p2);

                // move Component.Longest end to ComponentCenter, draw Component.Longest
                dx = Component.Center.X - Component.Longest.End.X;
                dy = Component.Center.Y - Component.Longest.End.Y;
                p1.X = (int)Math.Round(Component.Longest.Start.X + dx);
                p1.Y = (int)Math.Round(Component.Longest.Start.Y + dy);
                p2.X = (int)Math.Round(Component.Longest.End.X + dx);
                p2.Y = (int)Math.Round(Component.Longest.End.Y + dy);
                g.DrawLine(RedPen, p1, p2);

                //  move Normal start to ComponentCenter, draw it
                dx = Component.Center.X - Component.NormalStart.X;
                dy = Component.Center.Y - Component.NormalStart.Y;
                p1.X = (int)Math.Round(Component.NormalStart.X + dx);
                p1.Y = (int)Math.Round(Component.NormalStart.Y + dy);
                p2.X = (int)Math.Round(Component.NormalEnd.X + dx);
                p2.Y = (int)Math.Round(Component.NormalEnd.Y + dy);
                g.DrawLine(RedPen, p1, p2);

                //  move Component.Normal end to ComponentCenter, draw it
                dx = Component.Center.X - Component.NormalEnd.X;
                dy = Component.Center.Y - Component.NormalEnd.Y;
                p1.X = (int)Math.Round(Component.NormalStart.X + dx);
                p1.Y = (int)Math.Round(Component.NormalStart.Y + dy);
                p2.X = (int)Math.Round(Component.NormalEnd.X + dx);
                p2.Y = (int)Math.Round(Component.NormalEnd.Y + dy);
                g.DrawLine(RedPen, p1, p2);

                // draw outline
                g.DrawPolygon(OrangePen, ToPointsArray(Component.Outline));

                // draw Component.Longest
                p1.X = (int)Math.Round(Component.Longest.Start.X);
                p1.Y = (int)Math.Round(Component.Longest.Start.Y);
                p2.X = (int)Math.Round(Component.Longest.End.X);
                p2.Y = (int)Math.Round(Component.Longest.End.Y);
                g.DrawLine(BluePen, p1, p2);

            }
            return (bitmap);
        }


        // =========================================================
        private void DrawCirclesFunct(Bitmap bitmap, VideoProcessing vp) {
            List<Shapes.Circle> Circles = VideoDetection.FindCircles(vp, bitmap);
            using (Graphics g = Graphics.FromImage(bitmap)) 
                using (Pen pen = new Pen(Color.DarkOrange, 2)) 
                    for (int i = 0, n = Circles.Count; i < n; i++) {
                        Circles[i].ToRawResolution();
                        g.DrawEllipse(pen,
                            (float)(Circles[i].X - Circles[i].Radius), (float)(Circles[i].Y - Circles[i].Radius),
                            (float)(Circles[i].Radius * 2), (float)(Circles[i].Radius * 2));
                    }
        }


        // ==========================================================================================================

        // flip Y
        private Bitmap MirrorFunct(Bitmap frame) {
            new Mirror(false, true).ApplyInPlace(frame);
            return frame;
        }

        // =========================================================
        private void ZoomFunct(ref Bitmap frame, double Factor) {
            if (Factor < 0.1) return;

            int centerX = frame.Width / 2;
            int centerY = frame.Height / 2;
            int OrgSizeX = frame.Width;
            int OrgSizeY = frame.Height;

            int fromX = centerX - (int)(centerX / Factor);
            int fromY = centerY - (int)(centerY / Factor);
            int SizeX = (int)(OrgSizeX / Factor);
            int SizeY = (int)(OrgSizeY / Factor);
            try {
                Crop CrFilter = new Crop(new Rectangle(fromX, fromY, SizeX, SizeY));
                frame = CrFilter.Apply(frame);
                ResizeBilinear RBfilter = new ResizeBilinear(OrgSizeX, OrgSizeY);
                frame = RBfilter.Apply(frame);
            } catch {
            }
        }

        public void DrawMarkupText(ref Bitmap image, List<VideoTextMarkup> texts) {
            lock(texts) {
                foreach (var x in texts) {
                    x.Draw(ref image);
                }
            } 
        }
        


        // =========================================================
        /// <summary>
        /// This function will display markings at specified locations on the image offset from the center of the image
        /// </summary>
        public void DrawMarks(ref Bitmap image, List<PointF> points, Color color, int size) {
            try {
                using (Image<Bgr, Byte> img = new Image<Bgr, byte>(image)) {
                    foreach (var pt in points) {
                        PointF p = new PointF(pt.X + videoCapture.FrameCenterX, videoCapture.FrameCenterY-pt.Y);
                        var cross = new Cross2DF(p, size, size);
                        img.Draw(cross, new Bgr(color), 2);
                    }
                    image = img.ToBitmap();
                }
            } catch {

            }
        }

        // =========================================================
        private void DrawFiducialFunct(ref Bitmap image, VideoProcessing vp) {
            MarkA.Clear();
            var fids = VideoDetection.FindTemplates(vp, image);
            foreach (var f in fids) f.ToScreenResolution();
            MarkA.AddRange(fids.Select(x => x.ToPartLocation().ToPointF()).ToArray());
        }


        public static Point[] PointFToPoint(PointF[] list) {
            var l = new Point[list.Length];
            for (int i = 0; i < list.Length; i++) {
                l[i].X = (int)list[i].X;
                l[i].Y = (int)list[i].Y;
            }
            return l;
        }
            

        public static PointF[] ToRotatedRectangle(Shapes.Rectangle s) {
            s.ToRawResolution();
            PointF[] p = new PointF[5];
            p[0].X = (float)s.Left; p[0].Y = (float)s.Top;
            p[1].X = (float)s.Right; p[1].Y = (float)s.Top;
            p[2].X = (float)s.Right; p[2].Y = (float)s.Bottom;
            p[3].X = (float)s.Left; p[3].Y = (float)s.Bottom;
            p[4].X = (float)s.Left; p[4].Y = (float)s.Top;

            // roate about center
            double angle = s.A * Math.PI / 180d;
            PartLocation center = new PartLocation(s);
            for (int i = 0; i < p.Length; i++) {
                PartLocation pp = new PartLocation(p[i]) - center; //shift to zero
                pp = pp.Rotate(angle) + center;
                p[i] = pp.ToPointF();
            }

            return p;
        }
        
        private static void DrawRectangle(ref Bitmap image, Shapes.Rectangle rectangle) {
            using (Graphics g = Graphics.FromImage(image)) {
                g.DrawLines(new Pen(Color.Red), ToRotatedRectangle(rectangle));
            }
        }

        private static void DrawArrow(ref Bitmap image, List<Shapes.Arrow> arrows) {
            using (Image<Bgr, Byte> img = new Image<Bgr, byte>(image)) {
                for (int j = 0; j < arrows.Count; j++) {
                    var arrow = arrows[j];
                    var length = arrow.Length;
                    var loc = arrow.ToPartLocation();
                    var p = new Point[5];
                    int i = 0;
                    p[i++] = new PartLocation(0, 0).RotateDegrees(loc.A).FlipY().OffsetBy(loc).ToPoint();
                    p[i++] = new PartLocation(length, 0).RotateDegrees(loc.A).FlipY().OffsetBy(loc).ToPoint();
                    p[i++] = new PartLocation(length * .8f, length * .2f).RotateDegrees(loc.A).FlipY().OffsetBy(loc).ToPoint();
                    p[i++] = new PartLocation(length, 0).RotateDegrees(loc.A).FlipY().OffsetBy(loc).ToPoint();
                    p[i++] = new PartLocation(length * .8f, length * -.2f).RotateDegrees(loc.A).FlipY().OffsetBy(loc).ToPoint();
       
                    img.DrawPolyline(p, false, new Bgr(Color.OrangeRed), 2);
                }
                image = img.ToBitmap();
            }
        }


        private Bitmap DrawRectanglesFunct(Bitmap image, VideoProcessing vp ) {
            var rects = VideoDetection.FindRectangles(vp, image);
            if (rects.Count == 0) return image;
            var rect2 = VideoDetection.GetSmallestCenteredRectangle(rects);

            try {
                using (Image<Bgr, Byte> img = new Image<Bgr, byte>(image)) {
                    foreach (var rect in rects) {
                        img.DrawPolyline(PointFToPoint(ToRotatedRectangle(rect)), true, new Bgr(Color.Red), 2);
                    }

                    if (rect2 != null) {
                        img.DrawPolyline(PointFToPoint(ToRotatedRectangle(rect2)), true, new Bgr(Color.Green), 5);
                        img.Draw(new Cross2DF(rect2.ToPartLocation().ToPointF(), 20, 20), new Bgr(Color.Blue), 2);
                    }
                    image = img.ToBitmap();
                }
                
            } catch {

            }

            return image;
        }

        // =========================================================
        private Bitmap DrawRectanglesFunct_OLD(Bitmap image) {

            // step 1 - turn background to black (done)

            // step 2 - locating objects
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = 3;
            blobCounter.MinWidth = 3;
            blobCounter.ProcessImage(image);
            Blob[] blobs = blobCounter.GetObjectsInformation();

            // step 3 - check objects' type and do what you do:
            Graphics g = Graphics.FromImage(image);
            Pen pen = new Pen(Color.DarkOrange, 2);

            for (int i = 0, n = blobs.Length; i < n; i++) {
                SimpleShapeChecker ShapeChecker = new SimpleShapeChecker();
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);
                List<IntPoint> cornerPoints;

                // fine tune ShapeChecker
                ShapeChecker.AngleError = 15;  // default 7
                ShapeChecker.LengthError = 0.3F;  // default 0.1 (10%)
                ShapeChecker.MinAcceptableDistortion = 0.9F;  // in pixels, default 0.5 
                ShapeChecker.RelativeDistortionLimit = 0.2F;  // default 0.03 (3%)

                // use the Outline checker to extract the corner points
                if (ShapeChecker.IsQuadrilateral(edgePoints, out cornerPoints)) {
                    // only do things if the corners form a rectangle
                    if (ShapeChecker.CheckPolygonSubType(cornerPoints) == PolygonSubType.Rectangle) {
                        List<IntPoint> corners = PointsCloud.FindQuadrilateralCorners(edgePoints);
                        g.DrawPolygon(pen, ToPointsArray(corners));
                    }
                }
            }
            return (image);
        }


        // =========================================================
        private void DrawDashedCrossFunct(Bitmap img) {
            Pen pen = new Pen(Color.SlateGray, 1);
            Graphics g = Graphics.FromImage(img);
            int step = videoCapture.FrameSizeY / 40;
            int i = step / 2;
            while (i < videoCapture.FrameSizeY)
            {
                g.DrawLine(pen, videoCapture.FrameCenterX, i, videoCapture.FrameCenterX, i + step);
                i = i + 2 * step;
            }
            step = videoCapture.FrameSizeX / 40;
            i = step / 2;
            while (i < videoCapture.FrameSizeX) {
                g.DrawLine(pen, i, videoCapture.FrameCenterY, i + step, videoCapture.FrameCenterY);
                i = i + 2 * step;
            }
        }

        // =========================================================

        private void DrawGridFunct(ref Bitmap img) {  // i get out of memory errors here all the time - not sure why 
            // so protecting execution this way.
            try {
                Pen pen = new Pen(Color.Green, 1);
                Graphics g = Graphics.FromImage(img);

                var xscale = Settings.Default.DownCam_XmmPerPixel / GetZoom();

                var yscale = Settings.Default.DownCam_YmmPerPixel / GetZoom();
                var xlines = videoCapture.FrameSizeX / 2 * xscale;
                var ylines = videoCapture.FrameSizeY / 2 * yscale;

                for (int i = 0; i < xlines; i++) {
                    g.DrawLine(pen, videoCapture.FrameCenterX + (int)(i / xscale), videoCapture.FrameSizeY, videoCapture.FrameCenterX + (int)(i / xscale), 0);
                    g.DrawLine(pen, videoCapture.FrameCenterX + (int)(-i / xscale), videoCapture.FrameSizeY, videoCapture.FrameCenterX + (int)(-i / xscale), 0);
                }
                for (int i = 0; i < ylines; i++) {
                    g.DrawLine(pen, 0, videoCapture.FrameCenterY + (int)(i / yscale), videoCapture.FrameSizeX, videoCapture.FrameCenterY + (int)(i / yscale));
                    g.DrawLine(pen, 0, videoCapture.FrameCenterY + (int)(-i / yscale), videoCapture.FrameSizeX, videoCapture.FrameCenterY + (int)(-i / yscale));
                }
            } catch (Exception e) {
                Console.WriteLine("DrawGridFunct Error: " + e);
            }
        }
        // =========================================================

        public void DrawCrossFunct(ref Bitmap img) {  // i get out of memory errors here all the time - not sure why 
            // so protecting execution this way.
            try {
                Pen pen = new Pen(Color.Red, 1);
                Graphics g = Graphics.FromImage(img);

                g.DrawLine(pen, videoCapture.FrameCenterX , 0, videoCapture.FrameCenterX, videoCapture.FrameSizeY);
                g.DrawLine(pen, 0, videoCapture.FrameCenterY, videoCapture.FrameSizeX, videoCapture.FrameCenterY);
            } catch (Exception e) {
                Console.WriteLine("DrawCrossFunct Error: " + e);
            }
        }

        // =========================================================

 


        // =========================================================
        private void DrawBoxFunct(Bitmap img) {
            if (box != null) {
                box.videoProcessing = this;
                DrawRectangle(ref img, box);
            }
        }

        // =========================================================
        // Convert list of AForge.NET's points to array of .NET points
        private Point[] ToPointsArray(List<IntPoint> points) {
            return points.Select(x => new Point(x.X, x.Y)).ToArray();
        }

        public Bitmap GetMeasurementFrame() {
            Global.DoBackgroundWork();
            var frame = videoCapture.GetFrame();
            frame = ProcessFrame(frame);
            return frame;
        }


        public void Reset() {            
            //default box size
            box = new Shapes.Rectangle(0, 0, 0) { 
                Width = 200, Height = 200, 
                videoProcessing=this, 
                pointMode = Shapes.PointMode.ScreenUnzoomed
            };
            // Draws
            s.DrawCross = (IsUpCamera()) ? false : true;
            s.DrawDashedCross = false;
            // Finds:
            s.FindCircles = false;
            s.FindRectangles = false;
            s.FindComponent = false;
            s.TakeSnapshot = false;
            s.TestAlgorithm = false;
            s.Draw_Snapshot = false;
            s.DrawBox = false;
            s.FindFiducial = false;
            s.Draw1mmGrid = false;
            MarkA.Clear();
            MarkB.Clear();
            Arrows.Clear();
        }

        /*
        // moved functions
        public List<Shapes.Circle> FindAndDrawCircles(Bitmap frame) {
            //camera.MainForm.DisplayText("FindCircles()", System.Drawing.Color.Orange);

            var circles = VideoDetection.FindCircles(frame);
            List<PointF> t = new List<PointF>();
            foreach (var circle in circles) {;
                circle.ToScreenResolution();
                t.Add(circle.ToPartLocation().ToPointF());
            }
            DrawMarks(ref frame, t, Color.Green, 30);
            DrawCrossFunct(ref frame);
            frame.Save(@"c:\findCircles.bmp");
            return circles;
        }
        */


    }



}
