using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LitePlacer {
    public class CalibrationStatus {

        public bool NeedleHeight;
        public bool Wobble; //at power on
        public bool UpCameraMMPerPixel; //when focus height is changed


        // these do not require recalibrating every time
        public bool Backoff;
        public bool PCBThickness; //if using different PCB
        public bool CameraOffset; //depends on UpCamera Position and
                                  //upcamera positition should be set right after this is measured
        public bool UpCameraZero; //cameraoffset is changed
        public bool NeedleToCameraOffset;


        public void NeedleChanged() {
            NeedleHeight = false;
            Wobble = false;
        }

        public void PowerCycle() {
            Wobble = false;
        }

        public void FocusHeightChanged() {
            UpCameraMMPerPixel = false;
            Wobble = false;
            UpCameraZero = false;
            CameraOffset = false;
        }

        public void NewPCB() {
            PCBThickness = false;
        }

        public void UpCameraPositionChanged() {
            NeedleToCameraOffset = false;
            UpCameraZero = false;
        }


    }
}
