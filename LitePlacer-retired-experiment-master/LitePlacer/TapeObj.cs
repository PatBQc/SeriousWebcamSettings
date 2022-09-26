using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Xml.Serialization;

// http://www.yageo.com/exep/pages/download/literatures/UPY-C_GEN_15.pdf

namespace LitePlacer {
    public enum ComponentType { T0402, T0603, T0805, QFN, Other }
    public enum TapeCalibrationType {TapeHoles,ImageBasedTape,Tray}
    
    [Serializable]
    public class TapeObj : INotifyPropertyChanged {
        public PartLocation FirstHole;
        public PartLocation FirstPart;
        private List<NamedLocation> availableParts;
        public static readonly string[] Orientation = {"0deg","90deg","180deg","270deg","PosX","PosY","NegX","NegY"};

        public Shapes.Rectangle GetComponentSize() {
            switch (GetComponentTypeEnumFromString(PartType)) {
                case ComponentType.QFN: return null;
                case ComponentType.T0402: return new Shapes.Rectangle(0,0) {Height = .5, Width = 1};
                case ComponentType.T0603: return new Shapes.Rectangle(0, 0) { Height = .8, Width = 1.6 };
                case ComponentType.T0805: return new Shapes.Rectangle(0, 0) { Height = 1.25, Width = 2 };
            }
            return null;
        }

        public int NumberPartsAvailable {
            get { return AvailableParts.Count; }
        }

        public List<NamedLocation> AvailableParts { 
            get { if (availableParts == null) availableParts = new List<NamedLocation>();  return availableParts; }
            set { availableParts = value; Notify("AvailableParts"); Notify("NumberPartsAvailable"); } 
        }

        private string tapeType = "Paper", partType = "T0402", id, templateFilename, traySizeX, traySizeY;

        public string TraySizeX { get { return traySizeX; } set { traySizeX = value; Notify("TraySizeX"); }}
        public string TraySizeY { get { return traySizeY; } set { traySizeY = value; Notify("TraySizeY"); } }
        public string TapeType { get { return tapeType; } set { tapeType = value; Notify("TapeType");  } } //tapetype
        public string PartType { get { return partType; } set { partType = value; ToDefaults(GetComponentTypeEnumFromString(value)); Notify("PartType"); } } //tapedefault
        public string ID { get { return id; } set { id = value; Notify("ID"); } } // name 
        public string TemplateFilename { get { return templateFilename; } set { templateFilename = value; Notify("TemplateFilename"); Notify("TemplateBased"); } }


        private double holePitch, partPitch, holeToPartSpacingX, holeToPartSpacyingY, pickupZ, placeZ, tapeWidth;

        public double HoleDiameter { get; set; } //Do
        public double HolePitch { get { return holePitch; } set { holePitch = value; Notify("HolePitch"); } } // P0
        public double PartPitch { get { return partPitch; } set { partPitch = value; Notify("PartPitch"); } }
        public double HoleToPartSpacingX { get { return holeToPartSpacingX; } set { holeToPartSpacingX = value; Notify("HoleToPartSpacingX"); } }
        public double HoleToPartSpacyingY { get { return holeToPartSpacyingY; } set { holeToPartSpacyingY = value; Notify("HoleToPartSpacyingY"); } }
        public double PickupZ { get { return pickupZ; } set { pickupZ = value; Notify("PickupZ"); } }
        public double PlaceZ { get { return placeZ; } set { placeZ = value; Notify("PlaceZ"); } }
        public double TapeWidth { get { return tapeWidth; } set { tapeWidth = value; Notify("TapeWidth"); Notify("HoleToPartSpacingY"); } }
        

        public double HoleToPartSpacingY { get { return TapeWidth / 2 - .5; } } // F
        public bool IsPickupZSet { get { return (pickupZ == -1) ? false : true; } }
        public bool IsPlaceZSet { get { return (placeZ == -1) ? false : true; } }
        public bool IsFullyCalibrated = false;

        private int currentPart;
        public int CurrentPart { get { return currentPart; } set { currentPart = value; Notify("NextPart"); } }

        public void Reset() {
            TapeType = "Paper";
            CurrentPart = 1;
            PickupZ = -1;
            PlaceZ = -1;
            OriginalTapeOrientation = "0deg";
            OriginalPartOrientation = "0deg";
            Slope = 0;
            PartType = "T0402"; //will reset defaults
            PartPitch = 4d;
            TapeWidth = 8d;
            HolePitch = 4d;
            HoleToPartSpacingX = -2d; // first part is to the "left" of the hole
            HoleDiameter = 1.5d;
        }

        public void ToDefaults(ComponentType x) {
            switch (x) {
                case ComponentType.T0402:
                    PartPitch = 2d;
                    break;
                case ComponentType.T0603:
                case ComponentType.T0805:
                    PartPitch = 4d;
                    break;
            }
        }

        //************* Angle Computation ***********************

        private void AnglesChanged() {
            Notify("TapeAngle");
            Notify("DeltaAngle");
            Notify("PartAngle");
            Notify("Slope");
            Notify("TapeOrientation");
        }

        private double slope;
        private PartLocation tapeOrientation;
        private string originalTapeOrientation;
        private string originalPartOrientation;

        // Settable
        public double a; //coeffients of equation defining location of tape a + b*x though mostly ignored
        public double Slope { set { slope = value; tapeOrientation = new PartLocation(1, value); AnglesChanged(); } get { return slope; } }
        public String OriginalTapeOrientation { get { return originalTapeOrientation; } set { originalTapeOrientation = value; AnglesChanged(); } }
        public String OriginalPartOrientation { get { return originalPartOrientation; } set { originalPartOrientation = value; AnglesChanged(); } }

        // Read Only
        [XmlIgnore]public bool TemplateBased { get { return (FirstPart != null); } }
        [XmlIgnore]public double TapeAngle { get { return TapeOrientation.ToDegrees(); } }
        [XmlIgnore]public double DeltaAngle { get { return TapeAngle - OriginalTapeOrientationVector.ToDegrees(); } }
        [XmlIgnore]public double PartAngle { get { return OriginalPartOrientationVector.ToDegrees() + DeltaAngle; } }
        public PartLocation TapeOrientation { get { if (tapeOrientation == null) return OriginalTapeOrientationVector; return tapeOrientation; } }
        public PartLocation OriginalTapeOrientationVector { get { return OrientationToVector(OriginalTapeOrientation); } }
        public PartLocation OriginalPartOrientationVector { get { return OrientationToVector(OriginalPartOrientation); } }



        /// <summary>
        /// If given a reference to a DataGridViewCellCollection, it will
        /// auto-populate itself with what's in there and throw an exception 
        /// if it can't parse something
        /// </summary>
        public TapeObj() : this(ComponentType.T0402) {
        }

        public TapeObj(ComponentType x) { Reset();  ToDefaults(x); }

        public int CurrentPartIndex() { return CurrentPart; }
        public void PickupFailed() { SetPart(CurrentPart - 1); }
        public void IncrementPartNumber() { SetPart(CurrentPart + 1); }
        public void SetPart(int part) { CurrentPart = part; }

        public PartLocation GetCurrentPartLocation() { return GetPartLocation(CurrentPart); }

        // Part Orientation = orientation if tape is perfectly oriented the way it is oriented
        // deltaOrientation = how far offf the tape is from it's stated orientation
        public PartLocation GetPartLocation(int componentNumber) {
            PartLocation part;
            if (FirstPart != null) {
                part = GetPartBasedLocation(componentNumber);
            } else {
                var offset = new PartLocation(componentNumber * PartPitch + HoleToPartSpacingX, HoleToPartSpacingY);
                // add the vector to the part rotated by the tape orientation
                part = new PartLocation(FirstHole) + offset.Rotate(TapeOrientation.ToRadians());
            }
            // add deviation from expected orientation to part orientation
            var deltaOrientation = tapeOrientation.ToRadians() - OriginalTapeOrientationVector.ToRadians();
            part.A = (OriginalPartOrientationVector.ToRadians() + deltaOrientation) * 180d / Math.PI;

            return part;
        }


        // HOLES //
        public PartLocation GetHoleLocation(int holeNumber) {
            return new PartLocation(FirstHole) + new PartLocation(holeNumber * HolePitch, 0).Rotate(TapeOrientation.ToRadians());
        }

        public PartLocation GetPartBasedLocation(int partNumber) {
            return new PartLocation(FirstPart) + new PartLocation(partNumber * PartPitch, 0).Rotate(TapeOrientation.ToRadians());
        }

        public PartLocation GetNearestCurrentPartHole() {
            double distanceX = CurrentPart * PartPitch + HoleToPartSpacingX;
            int holeNumber = (int)(distanceX / HolePitch);
            return GetHoleLocation(holeNumber);
        }


        private ComponentType GetComponentTypeEnumFromString(string s) {
            return (ComponentType)Enum.Parse(typeof(ComponentType), s);
        }

        private PartLocation OrientationToVector(string orientation) {
            switch (orientation) {
                case "0deg":
                case "PosX": return new PartLocation(1, 0);
                case "90deg":
                case "PosY": return new PartLocation(0, 1);
                case "180deg":
                case "NegX": return new PartLocation(-1, 0);
                case "270deg":
                case "NegY": return new PartLocation(0, -1);
                default: throw new Exception("Invalid Orientation");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public override string ToString() {
            return ID;
        }

        public PartLocation LastHole;
        public bool IsLocationBased = false;

        public bool SetTapeOrientation(double X, double Y) {
            TapeOrientation.X = X;
            TapeOrientation.Y = Y;
            return true;
        }
    }
}

