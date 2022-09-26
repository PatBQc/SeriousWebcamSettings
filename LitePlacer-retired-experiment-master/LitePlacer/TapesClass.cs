using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using MathNet.Numerics.LinearRegression;

namespace LitePlacer {
    public class TapesClass {
        private DataGridView Grid;
        private NeedleClass Needle;
        private FormMain MainForm;
        private CNC Cnc;
        public SortableBindingList<TapeObj> tapeObjs = new SortableBindingList<TapeObj>();
        public List<string> TapeTypes;
        private const string TapesSaveName = "Tapes.xml";



        private string TapeFilename { get { return Global.BaseDirectory + @"\" + TapesSaveName; } }

        public TapesClass(DataGridView gr, NeedleClass ndl, CNC c, FormMain MainF) {
            Grid = gr;
            Needle = ndl;
            MainForm = MainF;
            Cnc = c;
            TapeTypes = AForgeFunctionSet.GetTapeTypes();
            if (File.Exists(Global.BaseDirectory + @"\" + TapesSaveName))
                tapeObjs = Global.DeSerialization<SortableBindingList<TapeObj>>(TapeFilename);
        }



        public void ReLoad(string filename=null) {
            filename = filename ?? TapeFilename;
            if (!File.Exists(filename)) {
                Global.Instance.mainForm.ShowSimpleMessageBox("Tape file misssing (" + filename + ")");
                return;
            }
            var newlist = Global.DeSerialization<SortableBindingList<TapeObj>>(filename);
            tapeObjs.Clear();
            tapeObjs.AddRange(newlist);
        }

        public void SaveAll(string filename=null) {
            filename = filename ?? TapeFilename;
            Global.Serialization(tapeObjs, filename);
        }

        // ========================================================================================
        // ClearAll(): Resets Tape positions and pickup/place Z's.
        public void ClearAll() { for (int tape = 0; tape < Grid.Rows.Count; tape++) Reset(tape); }

        // ========================================================================================
        // Reset(): Resets one tape position and pickup/place Z's.
        public void Reset(int tape) {
            foreach (var t in tapeObjs) {
                t.Reset();
            }
        }

        public string[] GetListOfTapeIDs() { return tapeObjs.Select(x => x.ID).ToArray(); }

        public TapeObj GetTapeObjByID(string id) {
            if (id == null) return null;
            foreach (var x in tapeObjs) {
                if (x.ID != null && x.ID.Equals(id)) return x;
            }
            return null;
        }

        public TapeObj FindClosestMatch(string name) {
            List<TapeObj> to = new List<TapeObj>();
            var words = name.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var t in tapeObjs) {
                foreach (var w in words) {
                    if (t.ID.ToLower().Equals(w.ToLower())) to.Add(t);
                }
            }
            if (to.Count == 0) return null;
            // return most occurant one - will be a random one if there are more than two with an equal number of matchs
            return to.GroupBy(x => x).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
        }


        public TapeObj GetTapeObjByIndex(int id) {
            if (tapeObjs.Count > id)
                return tapeObjs[id];
            return null;
        }

        public TapeObj AddTapeObject(int row) {
            TapeObj t = new TapeObj();
            tapeObjs.Insert(row, t);
            return t;
        }

        // will go to the nearest hole to the next part and display what it will pick up gooz
        public bool GoToNextComponent(TapeObj to) {
            SetCurrentTapeMeasurement(to.TapeType); //setup tape type to measure

            if (to.FirstPart != null) {
                Cnc.CNC_XY(to.GetPartBasedLocation(to.CurrentPartIndex()));
                return true;
            }
            if (to.FirstHole == null) {
                MainForm.ShowSimpleMessageBox("First hole not set and part not set - calibrate this first");
                return false;
            }

            if (to.IsLocationBased)
            {
                Cnc.CNC_XY(GetLocationBasedComponent(to));
            }
            else
            {
                MainForm.cameraView.downSettings.FindCircles = true;
                // move to closest hole to the part we are looking for 
                Cnc.CNC_XY(to.GetNearestCurrentPartHole());
                var hole = MainForm.FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 1.5, .05);
                if (hole == null)
                {
                    //MainForm.ShowSimpleMessageBox("Unable to center on nearest hole");
                    return false;
                }
                MainForm.cameraView.DownCameraReset();

                // move camera on top of the part, and then move from there to the part to pick up
                var offset = to.GetCurrentPartLocation() - to.GetNearestCurrentPartHole();
                Cnc.CNC_XY(hole + offset);
            }
            var c = MainForm.cameraView.downVideoProcessing.FrameCenter;
            MainForm.cameraView.downVideoProcessing.Arrows.Add(new Shapes.Arrow(c.X,c.Y, to.PartAngle, 100) );
            return true;

        }




        // ========================================================================================
        // Get/Set CurrentPickupZ/CurrentPlaceZ:
        // At the start of the job, we don't know the height of the component. Therefore, Main() probes 
        // the pickup and placement heights on the first part and sets them to the resulting values.
        // For speed, these results are used on the next parts.

        public bool ClearHeights_m(string Id) {
            var tape = GetTapeObjByID(Id);
            if (tape == null) return false;
            //tape.PickupZ = -1;
            //tape.PlaceZ = -1;
            return true;
        }


        public void PickupFailed(string id) {
            var tapeObj = GetTapeObjByID(id);
            if (tapeObj == null) return;
            tapeObj.PickupFailed();
        }



        public PartLocation GetNextComponentPartLocation(TapeObj tapeObj) {
            if (tapeObj == null) return null;
            MainForm.DisplayText("GotoNextPart_m(), tape id: " + tapeObj.ID);

            //Load & Parse Data
            if (!tapeObj.IsFullyCalibrated) {
                MainForm.ShowSimpleMessageBox("Tape " + tapeObj.ID + " is not yet calibrated.  Please do so and retry");
                return null;
            }

            PartLocation targetLocation = tapeObj.GetCurrentPartLocation();
            MainForm.DisplayText("Part " + tapeObj.CurrentPartIndex() + "  Source Location = " + targetLocation, Color.Blue);

            // see if part exists for part based detection
            if (tapeObj.TemplateBased) {
                Global.Instance.mainForm.cameraView.DownCameraReset();
                if (!MainForm.Cnc.CNC_XY(targetLocation)) return null;
                var thing = MainForm.GoToClosestThing(Shapes.ShapeTypes.Fiducial, 1, .2, null, tapeObj.TemplateFilename, .75);
                if (thing == null) {
                    MainForm.DisplayText("No Part Detected At This Location", Color.Red);
                    return null;
                } 
                MainForm.DisplayText("Part Detected : " + thing);
            }


            if (tapeObj.IsLocationBased) {
                targetLocation = GetLocationBasedComponent(tapeObj);
            } else
            // enhanced part detection
            if (tapeObj.FirstHole != null) {
                SetCurrentTapeMeasurement(tapeObj.TapeType);
                Cnc.CNC_XY(tapeObj.GetNearestCurrentPartHole());
                var hole = MainForm.GoToClosestThing(Shapes.ShapeTypes.Circle, 1.5, .1);
                if (hole == null) {
                    MainForm.DisplayText("Unable to detect part hole, aborting");
                    return null;
                }
                var offset = tapeObj.GetCurrentPartLocation() - tapeObj.GetNearestCurrentPartHole();
                targetLocation = hole.ToPartLocation() + offset;
                MainForm.cameraView.DownCameraReset();
            }

            //------------------- PART SPECIFIC LOGIC GOES HERE --------------------//
            if (tapeObj.PartType == "QFN") {
                MainForm.DisplayText("USING ENHANCE PART PICKUP", Color.HotPink);
                if (!MainForm.Cnc.CNC_XY(targetLocation)) return null;
                // setup view
                SetCurrentTapeMeasurement(tapeObj.TapeType);
                MainForm.cameraView.downSettings.FindRectangles = true;

                // MainForm.cameraView.downVideoProcessing.FindRectangles = true;
                // move closer and get exact coordinates plus rotation
                var rect = (Shapes.Rectangle) MainForm.GoToClosestThing(Shapes.ShapeTypes.Rectangle, 1.5, .025);

                if (rect == null) {
                    MainForm.cameraView.DownCameraReset();
                    return null;
                }
                Global.DoBackgroundWork(500);
                var rectAngle = rect.AngleOffsetFrom90();

                targetLocation = rect.ToPartLocation();
                targetLocation.A = tapeObj.OriginalPartOrientationVector.ToDegrees() + rectAngle;

                MainForm.cameraView.DownCameraReset();
            }

            return targetLocation;
        }
    

    // ========================================================================================
        // NeedleToNextPart_m(): Takes needle to exact location of the part, tape rotation taken in to account.
        // The position is measured using tape holes and knowledge about tape width and pitch (see EIA-481 standard).
        // Id tells the tape name. 
        public bool NeedleToNextPart_m(TapeObj to) {
            var loc = GetNextComponentPartLocation(to);
            if (loc == null) return false;
            if (!Needle.Move_m(loc)) return false; 
            // Increment Part 
            to.IncrementPartNumber();
            return true;
        }


        public bool SetCurrentTapeMeasurement(string type) {
            MainForm.cameraView.SetDownCameraFunctionSet(type+"Tape");
            Global.DoBackgroundWork(100);
            return true;
        }

        public bool PopulateAvailableParts(TapeObj x) {
            if (x.FirstHole == null) return false; // currently only handling hole-calibrated harts
            if (x.TemplateFilename == null || !File.Exists(x.TemplateFilename) ) return false;
            MainForm.Cnc.SlackCompensation = false;
            SetCurrentTapeMeasurement(x.TapeType);

            //find out the max number of parts
            int i = 0;
            while (true) {
                if (!MainForm.Cnc.CNC_XY(x.GetHoleLocation(i))) { MainForm.Cnc.SlackCompensation = true; return false; }
                var thing = VideoDetection.FindClosest(MainForm.cameraView.downVideoProcessing, Shapes.ShapeTypes.Circle, 1, 3);
                if (thing == null) break;
                if (Cnc.AbortPlacement) { MainForm.Cnc.SlackCompensation = true; return false; }
                i++;
            }

            MainForm.cameraView.SetDownCameraFunctionSet("ComponentPhoto");

            double len = i * x.HolePitch;
            double max_components = len / x.PartPitch + 2;
            var list = x.AvailableParts;
            list.Clear();
            if (!MainForm.Cnc.CNC_XY(x.GetPartLocation(0))) { MainForm.Cnc.SlackCompensation = true; return false; } //start off with first part
            for (i = 0; i < max_components; i++) {
                if (Cnc.AbortPlacement) { MainForm.Cnc.SlackCompensation = true; return false; }
                var location = MainForm.FindPositionAndMoveToClosest(Shapes.ShapeTypes.Fiducial, 1, .1, x.TemplateFilename, .5);
                if (location != null) {
                    list.Add(new NamedLocation(location, i.ToString()));
                    if (!MainForm.Cnc.CNC_XY(location + new PartLocation(x.PartPitch, 0).Rotate(x.TapeAngle * Math.PI / 180))) {
                        MainForm.Cnc.SlackCompensation = true;
                        return false;
                    }
                } else {
                    if (!MainForm.Cnc.CNC_XY(Cnc.XYLocation + new PartLocation(x.PartPitch, 0).Rotate(x.TapeAngle * Math.PI / 180))) {
                        MainForm.Cnc.SlackCompensation = true;
                        return false;
                    }
                }
            }
            Global.Instance.mainForm.DisplayText("Detected "+list.Count+" parts for "+x.ID+" ("+String.Join(" ",list)+")");
            MainForm.Cnc.SlackCompensation = true; 
            return true;
        }

        public bool CalibrateTape(TapeObj x) {
            // Setup Camera
            
            if (x.FirstHole == null && x.FirstPart == null) x.FirstHole = Cnc.XYLocation; //defaults to hole based calibration
            if (x.FirstHole != null) SetCurrentTapeMeasurement(x.TapeType);

            List<PartLocation> holes = new List<PartLocation>();
            List<int> holeIndex = new List<int>();
            if (x.FirstHole != null) {
                //1 - ensure first hole is correct
                MainForm.DisplayText("Moving to first hole @ " + x.FirstHole, Color.Purple);
                if (!MainForm.Cnc.CNC_XY(x.FirstHole)) return false;
                var holepos = MainForm.FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 1.8, 0.1); //find this hole with high precision
                if (holepos == null) return false;
                x.FirstHole = holepos;
                MainForm.DisplayText("Found new hole locaiton @ " + x.FirstHole, Color.Purple);

                // move to first hole for shits & giggles
                if (!MainForm.Cnc.CNC_XY(x.FirstHole)) return false;
                
                holes.Add(x.FirstHole);
                holeIndex.Add(0);
            }

            //2 - Look for for a few more holes 
            //    XXX-should be adjsuted to acocomodate smaller strips
            if (x.FirstHole != null) {
                for (int i = 2; i < 8; i += 2 ) {
                        /* - hole based detection - */
                        /****************************/
                        if (!MainForm.Cnc.CNC_XY(x.GetHoleLocation(i))) break;
                        Thread.Sleep(500);
                        var loc = MainForm.FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 1.8, 0.2);
                        if (loc == null) break;
                        holes.Add(loc);
                        holeIndex.Add(i);
                }
            } else {
                for (int i = 0; i < 8; i += 1)  {
                    /* - part based detection - */
                    /****************************/
                    if (x.FirstPart != null) {
                        if (!MainForm.Cnc.CNC_XY(x.GetPartBasedLocation(i))) break;
                        Thread.Sleep(500);
                        var loc = MainForm.FindPositionAndMoveToClosest(Shapes.ShapeTypes.Fiducial, 1.8, 0.2, x.TemplateFilename, .8);
                        if (loc == null) break;
                        holes.Add(loc);
                        holeIndex.Add(i);
                    }
                    
                }               
            }
            if (holes.Count < 2) return false; // didn't get enough points to calibrate this one


            //3 - Do Linear Regression on data if we have 2+ points
            // Fit circle to linear regression // y:x->a+b*x
            Double[] Xs = holes.Select(xx => xx.X - ((x.FirstHole != null) ? x.FirstHole.X : x.FirstPart.X )).ToArray();
            Double[] Ys = holes.Select(xx => xx.Y - ((x.FirstHole != null) ? x.FirstHole.Y : x.FirstPart.Y)).ToArray();
            Tuple<double, double> result = SimpleRegression.Fit(Xs, Ys);
            x.a = result.Item1; //this should be as close to zero as possible if things worked correctly
            x.Slope = result.Item2; //this represents the slope of the tape
            MainForm.DisplayText(String.Format("Linear Regression: {0} + (0,{1})+(0,{2})x", x.FirstHole, x.a, x.Slope), Color.Brown);

            //4 - Determine Avg Hole Spacing
            double spacing = 0;
            for (int i = 0; i < holes.Count - 1; i++) {
                spacing += holes[i].DistanceTo(holes[i + 1]) / 2; //distance one hole to the next - /2 because we skip every other hole (step 2)
            }
            if (x.FirstHole != null) x.HolePitch = spacing / (holes.Count - 1); //compute average for holes
            else x.PartPitch = 2 * spacing / (holes.Count - 1); 

            //5 - Done, specify that this is fully calibrated
            x.IsFullyCalibrated = true;
            
            MainForm.DisplayText("Tape " + x.ID + " Calibrated", Color.Brown);
            //MainForm.DisplayText(String.Format("\tEquation = {3} + (0,{0}) + {1} * ({2} * holeNumber)", x.a, x.b, x.HolePitch), System.Drawing.Color.Brown);

            MainForm.cameraView.DownCameraReset();
            if (!MainForm.Cnc.CNC_XY(x.FirstHole ?? x.FirstPart)) return false;

            return true;

        }


        public void CalibrateTapes() {
            foreach (TapeObj x in tapeObjs) {
                if (x.IsFullyCalibrated) continue; //skip if calibrated already
                CalibrateTape(x);
                MainForm.cameraView.DownCameraReset();
            }
        }

        public void CalibrateWithTemplate(TapeObj t) {
            if (string.IsNullOrEmpty(t.TemplateFilename)) {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "jpg (*.jpg)|*.jpg|png (*.png)|*.png|All Files (*.*)|*.*";
                ofd.Title = "Select Template To Match The Part";
                if (ofd.ShowDialog() != DialogResult.OK) return;
                t.TemplateFilename = ofd.FileName;
            }

            //now find the part
            var loc = MainForm.GoToClosestThing(Shapes.ShapeTypes.Fiducial, 5, .1, null, t.TemplateFilename, .75);
            if (loc == null) return;
            t.FirstPart = loc.ToPartLocation();
            t.FirstHole = null;
            // and calibrate
            CalibrateTape(t);
            MainForm.cameraView.DownCameraReset();
        }

        public void CalibrateWithHole(TapeObj t) {
            t.FirstHole = Cnc.XYLocation;
            CalibrateTape(t);
            MainForm.cameraView.DownCameraReset();
        }

        public void setNextOptically(TapeObj to) {
            for (int i = 0; i < 1000; i++) {
                var pos = to.GetPartLocation(i);
                if (pos.DistanceTo(Cnc.XYLocation) < 1) {
                    to.SetPart(i);
                    MainForm.DisplayText("Setting " + to.ID + " next part # " + i, Color.Green);
                    return;
                }
            }
            MainForm.ShowSimpleMessageBox("Couldn't Find corresponding part");
        }

        public void TakePhotoOfPartAtCurrentLocation(TapeObj t) {
            if (t == null) return;
            var dir = Global.BaseDirectory + @"\images\";
            Directory.CreateDirectory(dir);

            var size = t.GetComponentSize();
            if (size == null) return;
            var s = new PartLocation(size.Width, size.Height); //assumes 0 degree rotation
            s = (1.5 * s) / MainForm.cameraView.downVideoProcessing.mmPerPixel; //convert to pixels and ad extra 25% 
            s.Rotate(t.PartAngle * Math.PI / 180d); //and rotate to final position
            s.X = Math.Abs(s.X); //correct for sign changes
            s.Y = Math.Abs(s.Y);

            MainForm.cameraView.SetDownCameraFunctionSet("ComponentPhoto");
            Global.DoBackgroundWork(); //let new images be processed

            var topleft = MainForm.cameraView.downVideoProcessing.FrameCenter - (.5 * s);
            var rect = new Rectangle(topleft.ToPoint(), s.ToSize());
            var filter = new Crop(rect);

            using (var image = MainForm.cameraView.downVideoProcessing.GetMeasurementFrame()) {
                using (var cropped = filter.Apply(image)) {
                    var filename = dir + t.ID.Replace(" ", "_") + ".jpg";
                    if (File.Exists(filename)) File.Delete(filename);
                    cropped.Save(filename, ImageFormat.Jpeg);
                    t.TemplateFilename = filename;
                }
            }
        }
               


        public void TakePhotosOfAllComponents() {         
            foreach (var t in tapeObjs) {
                    if (!GoToNextComponent(t)) continue;
                    TakePhotoOfPartAtCurrentLocation(t);
                    SaveAll();
            }            
        }

        public bool SetFirstHole(TapeObj t)
        {
            var originalPos = Cnc.XYLocation;

            MainForm.cameraView.SetDownCameraFunctionSet(t.TapeType);
            
            var holepos = MainForm.FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 1.8, 0.1); //find this hole with high precision
            MainForm.cameraView.SetDownCameraFunctionSet("");
            if (holepos == null)
            {
                switch (MainForm.ShowMessageBox("Cannot find hole, use manual position?", "Hole not found", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:
                        return false;
                }
                holepos = originalPos;
            }
            t.FirstHole = holepos;
            return true;
        }

        public bool SetLastHole(TapeObj t)
        {
            if (t.FirstHole == null) {
                MainForm.DisplayText("First hole not set", Color.Red);
                return false;
            }

            var originalPos = Cnc.XYLocation;
            
            MainForm.cameraView.SetDownCameraFunctionSet(t.TapeType);
            
            var holepos = MainForm.FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 1.8, 0.1); //find this hole with high precision

            MainForm.cameraView.SetDownCameraFunctionSet("");
            
            if (holepos == null)
            {
                //MainForm.DisplayText("Cannot find hole", Color.Red);
                //return false;
                switch (MainForm.ShowMessageBox("Cannot find hole, use manual position?", "Hole not found", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:
                        return false;
                }
                holepos = originalPos;
            }
            
            double distance = t.FirstHole.DistanceTo(holepos);
   
            if ((Math.Abs(distance % t.PartPitch) > 0.4) && ((t.PartPitch - Math.Abs(distance % t.PartPitch)) > 0.4)) {
//                MainForm.DisplayText("Part pitch and hole position does not make sense, please redo", Color.Red);
                switch (MainForm.ShowMessageBox("Part pitch and hole position seems inconsistent, continue?", "Inconsistency", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:
                        return false;
                }
            }
            t.LastHole = holepos;
            
            /* Calculate tape angle given the first and last hole
             * the first hole is always considered to be the furthest away from the reel
             */
            double Xd = t.LastHole.X - t.FirstHole.X;
            double Yd = t.LastHole.Y - t.FirstHole.Y;
            t.SetTapeOrientation(Xd, Yd);
            double TapeAngle = t.TapeOrientation.ToRadians();

            /* Calculate the X and Y movements required to get to the next hole */
            double XMove = Math.Abs(t.PartPitch) * Math.Cos(TapeAngle);
            double YMove = Math.Abs(t.PartPitch) * Math.Sin(TapeAngle);

            /* Calculate the distance to the part from the hole given the angle */
            double XHoleToSpacing = Math.Abs(t.HoleToPartSpacingX) * Math.Cos(-TapeAngle) + Math.Abs(t.HoleToPartSpacingY) * Math.Sin(-TapeAngle);
            double YHoleToSpacing = Math.Abs(t.HoleToPartSpacingY) * Math.Cos(TapeAngle) + Math.Abs(t.HoleToPartSpacingX) * Math.Sin(TapeAngle);

            t.AvailableParts.Clear();

            NamedLocation ReturnLocation;
            for (int i = 0; i < (Math.Round(t.LastHole.DistanceTo(t.FirstHole) / t.PartPitch)); i++)
            {
                ReturnLocation = new NamedLocation();

                ReturnLocation.X = t.FirstHole.X + i * XMove + XHoleToSpacing;
                ReturnLocation.Y = t.FirstHole.Y + i * YMove + YHoleToSpacing;
                ReturnLocation.A = t.OriginalPartOrientationVector.ToDegrees();

                ReturnLocation.Name = t.ID + "_" + i.ToString();
                t.AvailableParts.Add(ReturnLocation);

            }

            t.IsFullyCalibrated = true;
            t.IsLocationBased = true;
            return true;
        }

        public bool SetAsFeeder(TapeObj t)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Number of components on reel", "Reel size", "Default", -1, -1);
            int reelSize = int.Parse(input);

            if (reelSize < 1) { return false; };

            t.AvailableParts.Clear();

            NamedLocation ReturnLocation;
            for (int i = 1; i < reelSize+1; i++)
            {
                ReturnLocation = new NamedLocation();

                ReturnLocation.X = Cnc.CurrentX;
                ReturnLocation.Y = Cnc.CurrentY;
                ReturnLocation.A = t.OriginalPartOrientationVector.ToDegrees();

                ReturnLocation.Name = t.ID + "_" + i.ToString();
                t.AvailableParts.Add(ReturnLocation);

            }

            t.IsFullyCalibrated = true;
            t.IsLocationBased = true;

            return true;
        }

        private PartLocation GetLocationBasedComponent(TapeObj t) {
            if (!t.IsFullyCalibrated || !t.IsLocationBased) { return null;  }
            if (t.CurrentPart >= t.NumberPartsAvailable) { return null;  }
            return t.AvailableParts.ElementAt(t.CurrentPart);
        }
    }
}
