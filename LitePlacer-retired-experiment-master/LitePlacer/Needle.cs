using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using LitePlacer.Properties;
using Newtonsoft.Json.Linq;

namespace LitePlacer {
    public class NeedlePoint : PartLocation {
        public override double A { get; set; }
    }

    public class NeedleClass {
        public List<NeedlePoint> CalibrationPoints = new List<NeedlePoint>();

        private static FormMain MainForm;

        public bool NeedleAlwaysCalibrated = false;

        public NeedleClass(FormMain MainF) {
            MainForm = MainF;
            Calibrated = false;
            CalibrationPoints.Clear();
        }

        // private bool probingMode;
        public void ProbingMode(bool set) {
            if (set) {
                CNC_Write("{\"zsn\",0}");
                Thread.Sleep(150);
                CNC_Write("{\"zsx\",1}");
                Thread.Sleep(150);
                CNC_Write("{\"zzb\",0}");
                Thread.Sleep(150);
            } else {
                CNC_Write("{\"zsn\",3}");
                Thread.Sleep(50);
                CNC_Write("{\"zsx\",2}");
                Thread.Sleep(50);
                CNC_Write("{\"zzb\",2}");
                Thread.Sleep(50);
            }
        }

        public bool ProbeDown() {
            MainForm.DisplayText("ProbeDown()", Color.PapayaWhip);
            ProbingMode(true);
            MainForm.Cnc.DisableZAdjust();
            if (!CNC_Write("{\"gc\":\"G28.4 Z0\"}", 10000)) {
                ProbingMode(false);
                return false;
            }
            ProbingMode(false);
            return true;
        }

        /// <summary>
        /// Move the needle down, measure when it hits, then move the needle back
        /// </summary>
        /// <returns>z distance where switch triggers</returns>
        public double ProbeDistance() {
            var orig_z = Global.Instance.cnc.CurrentZ;
            if (!ProbeDown()) return -1;
            var z = Global.Instance.cnc.CurrentZ;
            Global.Instance.cnc.CNC_Z(orig_z);
            return z;
        }


        public bool Calibrated { get; set; }

        public bool CorrectedPosition_m(double angle, out double X, out double Y) {
            if (!Calibrated) {
                MainForm.DisplayText("Needle not calibrated - calibrating now", Color.Red);
                MainForm.CalibrateNeedle_m();
                if (!Calibrated) {
                    MainForm.ShowSimpleMessageBox("Needle not calibrated and calibration attempt failed");
                    X = 0; Y = 0;
                    return false;
                }
            }

            while (angle < 0) angle = angle + 360.0;
            while (angle > 360.0) angle = angle - 360.0;

            // since we are not going to check the last point (which is the cal. value for 360)
            // in the for loop,we check that now
            if (angle > 359.98) {
                X = CalibrationPoints[0].X;
                Y = CalibrationPoints[0].Y;
                return true;
            };

            for (int i = 0; i < CalibrationPoints.Count-1; i++) {
                if (Math.Abs(angle - CalibrationPoints[i].A) < 1.0) {
                    X = CalibrationPoints[i].X;
                    Y = CalibrationPoints[i].Y;
                    return true;
                }
                if ((angle > CalibrationPoints[i].A) && (angle < CalibrationPoints[i + 1].A)) {
                    // angle is between CalibrationPoints[i] and CalibrationPoints[i+1], and is not == CalibrationPoints[i+1]
                    double fract = (angle - CalibrationPoints[i + 1].A) / (CalibrationPoints[i + 1].A - CalibrationPoints[i].A);                    
                    X = CalibrationPoints[i].X + fract * (CalibrationPoints[i + 1].X - CalibrationPoints[i].X);
                    Y = CalibrationPoints[i].Y + fract * (CalibrationPoints[i + 1].Y - CalibrationPoints[i].Y);
                    return true;
                }
            }
            MainForm.ShowMessageBox(
                "Needle Calibration value read: value not found",
                "Sloppy programmer error",
                MessageBoxButtons.OK);
            X = 0;
            Y = 0;
            return false;
        }


        public bool Calibrate(double Tolerance) {
            if (NeedleAlwaysCalibrated)
            {
                for (int i = 0; i <= 3600; i = i + 225)
                {
                    NeedlePoint Point = new NeedlePoint();
                    Point.A = i / 10.0;
                    Point.X = 0;
                    Point.Y = 0;
                    CalibrationPoints.Add(Point);
                }
                Calibrated = true;
                return true;
            }

            //setup camera
            MainForm.cameraView.UpCameraReset();
            MainForm.cameraView.SetUpCameraFunctionSet("Needle");
            MainForm.cameraView.downSettings.FindCircles = true;

            // we are already @ upcamera position
            MainForm.Cnc.Zup();
            Global.GoTo("Up Camera");

            MainForm.Cnc.ZGuardOn();

            MainForm.Cnc.Zdown(Settings.Default.focus_height, true); //1 mm above PCB
            
            CalibrationPoints.Clear();   // Presumably user changed the needle, and calibration is void no matter if we succeed here
            Calibrated = false;

            for (int i = 0; i <= 3600; i = i + 225) {
                NeedlePoint Point = new NeedlePoint();
                Point.A = i / 10.0;

                if (!MainForm.Cnc.CNC_A(Point.A)) return false;
                //detect average of 3 measurements
                var circle = VideoDetection.GetClosestAverageCircle(MainForm.cameraView.upVideoProcessing, Tolerance, 3);

                if (circle == null) {
                    MainForm.ShowSimpleMessageBox("Needle Calibration: Can't see needle at angle " + Point.A + " - aborting");
                    return false;
                }

                circle.ToMMResolution();
                Point.X = circle.X;
                Point.Y = circle.Y;

                // display point
                var pt = circle.Clone().ToScreenResolution().ToPartLocation().ToPointF();
                MainForm.DisplayText("circle @ " + circle + "\tx @ " + pt.X + "," + pt.Y);
                MainForm.cameraView.upVideoProcessing.MarkA.Add(pt);

                CalibrationPoints.Add(Point);
            }

            // Calculate average midpoint
            double AverageX = 0, AverageY = 0;
            for (int i = 0; i < CalibrationPoints.Count; i = i + 1)
            {
                AverageX += CalibrationPoints[i].X;
                AverageY += CalibrationPoints[i].Y;
            }
            AverageX = AverageX / (CalibrationPoints.Count);
            AverageY = AverageY / (CalibrationPoints.Count);

            // Subtract average midpoint from all calibrationpoints
            for (int i = 0; i < CalibrationPoints.Count; i = i + 1)
            {
                CalibrationPoints[i].X -= AverageX;
                CalibrationPoints[i].Y -= AverageY;
            }
            

            Calibrated = true;
            MainForm.Cnc.Zup();
            MainForm.cameraView.SetUpCameraDefaults();

            return true;
        }

        public bool Move_m(PartLocation p) {
            return Move_m(p.X, p.Y, p.A);
        }

        public PartLocation NeedleOffset {
            get { return new PartLocation(Settings.Default.DownCam_NeedleOffsetX, Settings.Default.DownCam_NeedleOffsetY); }
            set {
                Settings.Default.DownCam_NeedleOffsetY = value.Y;
                Settings.Default.DownCam_NeedleOffsetX = value.X;
            }
        }

        public bool Move_m(double X, double Y, double A) {
            double dX;
            double dY;
            if (!CorrectedPosition_m(A, out dX, out dY)) return false;


            var loc = new PartLocation(X, Y, A);
            var wobble = new PartLocation(dX, dY, 0);

            var dest = loc - wobble + NeedleOffset;

            MainForm.DisplayText("== NEEDLE ==" + String.Format("pos {0} + offset {1} - wobble {2} = {3}", loc, NeedleOffset, wobble, dest), Color.ForestGreen);

            return Global.Instance.cnc.CNC_XYA(dest);
        }



        // =================================================================================
        // CNC interface functions
        // =================================================================================
        private bool CNC_Write(string s) { return Global.Instance.cnc.CNC_Write_m(s);}
        private bool CNC_Write(string s, int timeout) { return Global.Instance.cnc.CNC_Write_m(s,timeout); }
    }
}
