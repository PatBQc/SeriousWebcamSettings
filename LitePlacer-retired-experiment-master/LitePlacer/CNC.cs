using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LitePlacer {
    public class CNC {
        private static FormMain MainForm;
        private SerialComm Com;
        public bool JoggingBusy;
        public bool AbortPlacement;

        public bool Simulation = false;

        private bool _Zguard = true;
        public void ZGuardOn() { _Zguard = true; }
        public void ZGuardOff() { _Zguard = false; }

        public bool Connected { get { if (Simulation) { return true; } else { return Com.IsOpen; } } }
        
        private readonly SlackCompensationManager SlackCompensationX = new SlackCompensationManager() { RequiredDistance = .4 };
        private readonly SlackCompensationManager SlackCompensationY = new SlackCompensationManager() { RequiredDistance = .4 };
        private readonly SlackCompensationManager SlackCompensationZ = new SlackCompensationManager() { RequiredDistance = 1 };
        private readonly SlackCompensationManager SlackCompensationA = new SlackCompensationManager() { RequiredDistance = 30 };

        static ManualResetEventSlim _readyEvent = new ManualResetEventSlim(false);

        public void ShowSimpleMessageBox(string msg) {
            Global.Instance.mainForm.ShowSimpleMessageBox(msg);
        }

        public CNC(FormMain mf) {
            MainForm = mf;
            Com = new SerialComm { serialDelegate = InterpretLine };
            Connect(Properties.Settings.Default.CNC_SerialPort);

        }

        public void Close() {
            if (Connected) Com.Close();
            _readyEvent.Set();
            MainForm.UpdateCncConnectionStatus();
        }

        public bool Connect(String name) {
            // For now, just see that the port opens. 
            // TODO: check that there isTinyG, not just any comm port.
            // TODO: check/set default values
            if (Simulation) { return true; }

            if (Connected) Com.Close();
            Com.Open(name);
            _readyEvent.Set();
            return Com.IsOpen;
        }


        public bool RawWrite(string command) {
            if (Simulation) { return true; }

            if (!Com.IsOpen) {
                MainForm.DisplayText("###" + command + " discarded, com not open");
                return false;
            }
            Com.Write(command);
            return true;
        }

        // Square compensation:
        // The machine will be only approximately square. Fortunately, the squareness is easy to measure with camera.
        // User measures correction value, that we apply to movements and reads.
        // For example, correction value is +0.002, meaning that for every unit of +Y movement, 
        // the machine actually also unintentionally moves 0.002 units to +X. 
        // Therefore, for each movement when the user wants to go to (X, Y),
        // we really go to (X - 0.002*Y, Y)

        // CurrentX/Y is the corrected value that user sees and uses, and reflects a square machine
        // TrueX/Y is what the TinyG actually uses.

        public static double SquareCorrection { get; set; }
        private static double CurrX;
        private static double _trueX;
        private static double _trueY;

        private static bool LastMoveZAdjusted = true;
        public void DisableZAdjust()
        {
            LastMoveZAdjusted = false;
        }

        public PartLocation XYLocation {
            get { return new PartLocation(CurrentX, CurrentY); }
        }

        public PartLocation XYALocation {
            get { return new PartLocation(CurrentX, CurrentY, CurrentA); }
        }

        public double TrueX {
            get { return (_trueX); }
            set { _trueX = value; }
        }

        public double CurrentX {
            get { return (CurrX); }
            set { CurrX = value; }
        }

        public static void setCurrX(double x) {
            _trueX = x;
            CurrX = x - CurrY * SquareCorrection;

            if (MainForm.Cnc.Simulation) { Global.Instance.mainForm.UpdatePositionDisplay(); }
        }

        private static double CurrY;
        public double CurrentY {
            get { return (CurrY); }
            set { CurrY = value; }
        }

        public static void setCurrY(double y) {
            _trueY = y;
            CurrY = y;
            CurrX = _trueX - CurrY * SquareCorrection;

            if (MainForm.Cnc.Simulation) { Global.Instance.mainForm.UpdatePositionDisplay(); }
        }

        private static double CurrZ;
        public double CurrentZ {
            get { return (CurrZ); }
            set { CurrZ = value; }
        }
        public static void setCurrZ(double z) {
            CurrZ = z;
            CurrX = _trueX - CurrY * SquareCorrection;
            CurrY = _trueY;

            if (MainForm.Cnc.Simulation) { Global.Instance.mainForm.UpdatePositionDisplay(); }
        }


        private static double CurrA;
        public double CurrentA { get { var x = CurrA % 360; if (x < 0)x += 360; return (x); } }
        public double CurrentAMachine { get { return CurrA; } }
        public static void setCurrA(double a) { CurrA = a; }

        public bool SlackCompensation {
            get { return Properties.Settings.Default.CNC_SlackCompensation; }
            set {
                Properties.Settings.Default.CNC_SlackCompensation = value;
                Properties.Settings.Default.Save();
            }
        }

        public bool DoesJObjectExist(JObject o, string a, string b) {
            if (o[a] != null && o[a][b] != null) return true;
            return false;
        }


        //  public bool IgnoreError { get; set; }
        public int LastStatusCode { get; private set; } //not sure what the relevance of this is but it's the only thing interesting in the f array returned
        public int CNC_LastWriteStatus;

        //  f: (1) revision number, (2) status code, (3) the number of bytes pulled from RX buffer for this command (including the terminating LF), and (4) checksum (more details provided later).
        public void InterpretLine(string line) {
            JObject o = JObject.Parse(line);
            if (o["f"] != null) {
                LastStatusCode = (int)o["f"][1];
           //     if (LastStatusCode == 0) _readyEvent.Set();
            }

            // This is called from SerialComm dataReceived, and runs in a separate thread than UI            
            MainForm.DisplayText(line, Color.Gray);

            if (line.Contains("SYSTEM READY")) {
                Close();
                MainForm.ShowMessageBox(
                    "TinyG Reset.",
                    "System Reset",
                    MessageBoxButtons.OK);
                // ishomes should be set to false here - though cross threading issue and i'm lazy so i'll ignore for now
                MainForm.UpdateCncConnectionStatus();
                return;
            }
            if (line.StartsWith("tinyg [mm] ok>")) {
                MainForm.DisplayText("ReadyEvent ok");
                _readyEvent.Set();
                return;
            }

            // ERROR
            if (o["er"] != null) {
                if (o["er"]["msg"].ToString().Equals("File not open")) {
                  //  MainForm.DisplayText("### Ignored file not open error - implies eeprom update failed due to machine state ###");
                    return;
                }
                // Close();
                MainForm.UpdateCncConnectionStatus();
                MainForm.ShowSimpleMessageBox("TinyG Error : " + o["er"]["msg"]);
                return;
            }

            // STATUS Update
            if (o["sr"] != null || DoesJObjectExist(o,"r","sr")) {
                var x = (o["sr"] != null) ? o["sr"] : o["r"]["sr"];
                if (x["posx"] != null) CNC.setCurrX((double)x["posx"]);
                if (x["posy"] != null) CNC.setCurrY((double)x["posy"]);
                if (x["posz"] != null) CNC.setCurrZ((double)x["posz"]);
                if (x["posa"] != null) CNC.setCurrA((double)x["posa"]);
                Global.Instance.mainForm.UpdatePositionDisplay();
                if (DoesJObjectExist(o, "r","sr") ||
                   (DoesJObjectExist(o, "sr","stat") && (int)(o["sr"]["stat"]) == 3)) {
                    _readyEvent.Set();
                    return;
                }
            }


            // RESULT
            if (o["r"] != null) {
                var r = o["r"];
                if (!r.HasValues) return; //got empty block
                var newstring = r.ToString(Formatting.None); //get rid of wrapping code

                if (r["adc0"] != null) { //get adc value
                    _ADC_RESULT = (int)r["adc0"];
                    _readyEvent.Set();
                    return;
                }

                if (r["me"] != null || r["md"] != null) {// response to motor power on/off commands
                    _readyEvent.Set();
                    return;
                }
                if (r["msg"] != null) {
                    if (r["msg"] != null) MainForm.ShowSimpleMessageBox("TinyG Message: " + r["msg"]);
                    return;
                }                
                if (r["sys"] != null) {
                    Properties.Settings.Default.TinyG_sys = newstring;
                    _readyEvent.Set();
                    MainForm.DisplayText("ReadyEvent sys group");
                    return;
                }
                if (r["1"] != null) {
                    Properties.Settings.Default.TinyG_m1 = newstring;
                    _readyEvent.Set();
                    MainForm.DisplayText("ReadyEvent m1 group");
                    return;
                }
                if (r["2"] != null) {
                    Properties.Settings.Default.TinyG_m2 = newstring;
                    _readyEvent.Set();
                    MainForm.DisplayText("ReadyEvent m2 group");
                    return;
                }
                if (r["3"] != null) {
                    Properties.Settings.Default.TinyG_m3 = newstring;
                    _readyEvent.Set();
                    MainForm.DisplayText("ReadyEvent m3 group");
                    return;
                }
                if (r["4"] != null) {
                    Properties.Settings.Default.TinyG_m4 = newstring;
                    _readyEvent.Set();
                    MainForm.DisplayText("ReadyEvent m4 group");
                    return;
                }
                if (r["x"] != null) {
                    Properties.Settings.Default.TinyG_x = newstring;
                    _readyEvent.Set();
                    MainForm.DisplayText("ReadyEvent x group");
                    return;
                }
                if (r["y"] != null) {
                    Properties.Settings.Default.TinyG_y = newstring;
                    _readyEvent.Set();
                    MainForm.DisplayText("ReadyEvent y group");
                    return;
                }
                if (r["z"] != null) {
                    Properties.Settings.Default.TinyG_z = newstring;
                    _readyEvent.Set();
                    MainForm.DisplayText("ReadyEvent z group");
                    return;
                }
                if (r["a"] != null) {
                    Properties.Settings.Default.TinyG_a = newstring;
                    _readyEvent.Set();
                    MainForm.DisplayText("ReadyEvent a group");
                    return;
                }
              
                foreach (var token in r.Children()) {
                    if (token is JProperty) {
                        var prop = (JProperty)token;
                        MainForm.ValueUpdater(prop.Name, (string)prop.Value);
                    }
                }
                _readyEvent.Set();

            }
            
            if ((o["f"] != null) && ((string) o["f"][1] == "0"))
            {
                _readyEvent.Set();
            }
        }  // end InterpretLine()

        // =================================================================================
        // TinyG JSON stuff
        // =================================================================================

        // ADDITIONAL COMMANDS 
        public bool Zdown(bool AllowMovementWhileDown = false) {
            if (AllowMovementWhileDown) ZGuardOff();
            return CNC_Z(Properties.Settings.Default.ZDistanceToTable);
        }

        public bool Zdown(double offsetFromPCB, bool AllowMovementWhileDown = false) {
            if (AllowMovementWhileDown) ZGuardOff();
            return CNC_Z(Properties.Settings.Default.ZDistanceToTable - offsetFromPCB);
        }

        public bool Zup() {
            ZGuardOn();
            return CNC_Z(z_offset); 
        }



        public double _z_offset;  // this is how far from zero the z-head should be to speed-up movements
        public double z_offset {
            get { return _z_offset; }
            set {
                if (value < 0) value = 0;
                if (value > 20) {
                    ShowSimpleMessageBox("Attempted to set z_offset > 20mm - too dangerous, setting to 20mm");
                    value = 20;
                }
                _z_offset = value;
            }
        }


        public bool CNC_Home_m(string axis) {
            if (Simulation) {
                CurrX = 0;
                CurrY = 0;
                CurrZ = 0;
                CurrA = 0;
                Global.Instance.mainForm.UpdatePositionDisplay();
                return true;  
            }

            if (!CNC_Write_m("{\"gc\":\"G28.2 " + axis + "0\"}", 10000)) {
                ShowSimpleMessageBox("Homing operation mechanical step failed, CNC issue");
                return false;
            }
            Global.Instance.DisplayText("Homing " + axis + " done.", Color.DarkSeaGreen);
            return true;
        }

        //-------- ADC ----------------
        public int _ADC_RESULT;
        public int GetADC() {
            if (!CNC_Write_m("{\"adc0\":\"\"}")) return -1;
            return _ADC_RESULT;
        }

        // =================================================================================
        // CNC_Write_m
        // Sends a command to CNC, doesn't return until the response is handled
        // by the CNC class. (See _readyEvent )
        // =================================================================================
        private const int CNC_MoveTimeout = 3000; // timeout for X,Y,Z,A movements; 2x ms. (3000= 6s timeout)

        public void CNC_RawWrite(string s) {
            // This for operations that cause conflicts with event firings. Caller does waiting, if needed.
            RawWrite(s);
        }

        bool CNC_BlockingWriteDone;
        private void CNC_BlockingWrite_thread(string cmd) {
            CNC_LastWriteStatus = -1;
            _readyEvent.Reset();
            Com.Write(cmd);
            _readyEvent.Wait();
            CNC_BlockingWriteDone = true;
        }

        //public bool CNC_IsBusy() { return !CNC_BlockingWriteDone;  }


        public bool CNC_SetValue(string para, int value) {
            return CNC_Write_m("{\""+para+"\":" + value + "}");
        }

        public bool CNC_Write_m(string s, int Timeout = 250) {
            if (!Com.IsOpen) {
                MainForm.DisplayText("** PORT CLOSED ** Ignoring command " + s, Color.Red);
                return false;
            }
            CNC_BlockingWriteDone = false;
            Thread t = new Thread(() => CNC_BlockingWrite_thread(s));
            t.Name = "CNC_BlockingWrite";
            t.IsBackground = true;
            t.Start();

            int i = 0;
            while (!CNC_BlockingWriteDone) {
                Thread.Sleep(2);
                Application.DoEvents();
                i++;
                if (i > Timeout) {
                    _readyEvent.Set();  // terminates the CNC_BlockingWrite_thread
                    Global.Instance.mainForm.ShowMessageBox(
                        "Debug: CNC_BlockingWrite: Timeout on command " + s,
                        "Timeout",
                        MessageBoxButtons.OK);
                    CNC_BlockingWriteDone = true;
                    JoggingBusy = false;
                    return false;
                }
            }
            return true;
        }

        public bool IsNeedleDown() {
            return (CurrentZ > z_offset + 1);
        }

        private bool CNC_MoveIsSafe_m(PartLocation p) {
            var m = Global.Instance.Locations.GetLocation("max machine");
            if ((p.X < -3.0) || (p.X > m.X) || (p.Y < -3.0) || (p.Y > m.Y)) {
                ShowSimpleMessageBox("Attempt to move outside safe limits " + p);
                return false;
            }
            if (_Zguard && IsNeedleDown()) {
                //needle is down - instead of bitching, just move it up first
                CNC_Z(0);
            }
            return true;
        }


        private void CNC_BlockingMove_thread(double? X = null, double? Y = null, double? Z = null, double? A = null) {
            if (!SlackCompensation) Move(X, Y, Z, A);
            else {
                double? x1 = (X == null) ? null : (SlackCompensationX.NeedsSlackCompensation(CurrentX, (double)X)) ? X - SlackCompensationX.RequiredDistance : X;
                double? y1 = (Y == null) ? null : (SlackCompensationY.NeedsSlackCompensation(CurrentY, (double)Y)) ? Y - SlackCompensationY.RequiredDistance : Y;
                double? z1 = (Z == null) ? null : (SlackCompensationZ.NeedsSlackCompensation(CurrentZ, (double)Z)) ? Z - SlackCompensationZ.RequiredDistance : Z;
                double? a1 = (A == null) ? null : (SlackCompensationA.NeedsSlackCompensation(CurrentA, (double)A)) ? A - SlackCompensationA.RequiredDistance : A;

                if (Z != null && Z == 0) z1 = 0; //don't try to slack compensate when gonig for zero
                else SlackCompensationZ.AppliedSlackCompensation();

                Move(x1, y1, z1, a1);
                if (x1 != X || y1 != Y || z1 != Z || a1 != A) Move(X, Y, Z, A);

                SlackCompensationX.AppliedSlackCompensation();
                SlackCompensationY.AppliedSlackCompensation();
                SlackCompensationA.AppliedSlackCompensation();
            }
            CNC_BlockingWriteDone = true;
        }

        public bool CNC_XY(PartLocation loc) { if (loc==null) return false; return CNC_XYZA(loc.X, loc.Y); }
        public bool CNC_XYA(PartLocation loc) { if (loc==null) return false; return CNC_XYZA(loc.X, loc.Y, loc.A); }
        public bool CNC_XYA(double X, double Y, double A) { return CNC_XYZA(X, Y, A); }
        public bool CNC_XY(double X, double Y) { return CNC_XYZA(X, Y); }
        public bool CNC_Z(double Z) { return CNC_XYZA(null, null, null, Z); }
        public bool CNC_A(double A) { return CNC_XYZA(null, null, A); }

        public bool CNC_XYZA(double? X, double? Y, double? A = null, double? Z = null) {
            Global.Instance.DisplayText(string.Format("CNC_XYZA: {0:F2},{1:F2},{2:F2},{3:F2}",X,Y,Z,A),Color.DarkMagenta);

            if (AbortPlacement) {
                return false;
            }

            if ((X != null || Y != null) && !CNC_MoveIsSafe_m(new PartLocation(X, Y))) return false;


            if (!Connected && !Simulation) {
                ShowSimpleMessageBox("CNC_XY: Cnc not connected");
                return false;
            }

            CNC_BlockingWriteDone = false;
            Thread t = new Thread(() => CNC_BlockingMove_thread(X, Y, Z, A)) {
                Name = "CNC_BlockingMove",
                IsBackground = true
            };
            t.Start();

            int i = 0;
            while (!CNC_BlockingWriteDone) {
                Thread.Sleep(2);
                Application.DoEvents();
                i++;
                if (i > CNC_MoveTimeout) {
                    _readyEvent.Set();   // causes CNC_Blocking_thread to exit
                }
            }

            CNC_BlockingWriteDone = true;
            if ((i > CNC_MoveTimeout) && Connected) {
                Global.Instance.mainForm.ShowSimpleMessageBox("CNC: Timeout / Cnc connection cut!");
                Close();
                Global.Instance.mainForm.UpdateCncConnectionStatus();
            }
            return (Connected);
        }





        private void Tx_G0(string cmd) {            
            if (cmd.Contains("A")) {
                MainForm.DisplayText("** " + cmd, Color.Red);
            }
            if (Simulation) { return; }
            _readyEvent.Reset();
            Com.Write("{\"gc\":\"" + "G0 " + cmd + "\"}");
            _readyEvent.Wait();
        }

        private void Tx_G1(int? speed, string cmd) {
            if (cmd.Contains("A")) {
                MainForm.DisplayText("** " + cmd, Color.Red);
            }
            if (Simulation) { return; }
            _readyEvent.Reset();
            string F = (speed == null) ? "" : "F" + ((int)speed).ToString();
            Com.Write("{\"gc\":\"" + "G1 " + F + " " + cmd + "\"}");
            _readyEvent.Wait();
        }

        private double MinAMovement(double A) {
            //  Global.Instance.DisplayText(string.Format("Orig : CurrentA={0} -> A={1}", CurrentA, A), Color.Red);
            A = A % 360;
            if (A < 0) A += 360;
            //  Global.Instance.DisplayText(string.Format("Mod :  CurrentA={0} -> A={1}", CurrentA, A), Color.Red);
            if (A == CurrentA) return 0;
            if (A < CurrentA) {
                //     Global.Instance.DisplayText("Decreasing Move", Color.Blue);
                var cw = CurrentA - A;
                var ccw = A + 360 - CurrentA;
                //   Global.Instance.DisplayText("cw="+cw+" ccw="+ccw, Color.Blue);
                return (cw <= ccw) ? -cw : ccw;
            } else {
                //  Global.Instance.DisplayText("Increasing move Move", Color.Blue);
                var cw = A - CurrentA;
                var ccw = CurrentA + 360 - A;
                //    Global.Instance.DisplayText("cw=" + cw + " ccw=" + ccw, Color.Blue);
                return (cw <= ccw) ? cw : -ccw;
            }
        }

        public void UpdatePositionWithZcompensation()
        {
            Move(CurrentX, CurrentY, CurrentZ, CurrentA);
        }

        private void Move(double? X = null, double? Y = null, double? Z = null, double? A = null) {
            // Global.Instance.DisplayText(string.Format("Move({0},{1},{2},{3})", X, Y, Z, A),Color.Blue);

            // Compensate X/Y for Z movements
            if (Z != null)
            {
                double zTravel = ((double) Z) - Properties.Settings.Default.zTravelTotalZ;
                if (Properties.Settings.Default.zTravelTotalZ != 0) {
                    if (X == null) { X = CurrentX; }
                    if (Y == null) { Y = CurrentY; }
 
                    X = X + (Properties.Settings.Default.zTravelXCompensation / Properties.Settings.Default.zTravelTotalZ * Z);
                    Y = Y + (Properties.Settings.Default.zTravelYCompensation / Properties.Settings.Default.zTravelTotalZ * Z);
                    LastMoveZAdjusted = true;
                }
            }
            else
            {
                //LastMoveZAdjusted = false;
            }
            if (Y != null && X != null) X = X + SquareCorrection * Y;

            //minimum movement distances required to make it count
            double dX = (X == null) ? 0 : (Math.Abs((double)X - CurrentX) < .004) ? 0 : Math.Abs((double)X - CurrentX);
            double dY = (Y == null) ? 0 : (Math.Abs((double)Y - CurrentY) < .004) ? 0 : Math.Abs((double)Y - CurrentY);
            double dZ = (Z == null) ? 0 : (Math.Abs((double)Z - CurrentZ) < .005) ? 0 : Math.Abs((double)Z - CurrentZ);

            // angle movement is a bit tricky as we want to optimize the rotation direction to give us the least travel to get where we want to go
            double dA = (A == null) ? 0 : MinAMovement((double)A);
            if (dA > 0) Global.Instance.DisplayText(string.Format("currentAMachine {0} + da {1} = A {2} (%360={3})", CurrentAMachine, dA, (CurrentAMachine + dA), (CurrentAMachine + dA) % 360));
            dA = (Math.Abs(dA) < .01) ? 0 : dA;
            A = (CurrentAMachine + dA);

            //building list of commands
            string normalMove = "", slowMoveXY = "", slowMoveA = "", slowMoveZ = "";
            if (X != null && dX > 1 && X != _trueX) normalMove += " X" + ((double)X).ToString(CultureInfo.InvariantCulture);
            else if (X != null && X != _trueX /*&& dX != 0*/) slowMoveXY += " X" + ((double)X).ToString(CultureInfo.InvariantCulture);
            if (Y != null && dY > 1 && Y != _trueY) normalMove += " Y" + ((double)Y).ToString(CultureInfo.InvariantCulture);
            else if (Y != null && Y != _trueY /*&& dY != 0*/) slowMoveXY += " Y" + ((double)Y).ToString(CultureInfo.InvariantCulture);
            
            if (Z != null && dZ > 1 && Z != CurrZ) normalMove += " Z" + ((double)Z).ToString(CultureInfo.InvariantCulture);
            else if (Z != null && Z != CurrZ /*&& dZ != 0*/) slowMoveZ += " Z" + ((double)Z).ToString(CultureInfo.InvariantCulture);
            // again, angle is a bit different given how we compute it
            if (Math.Abs(dA) > 5 && A != CurrA) normalMove += " A" + ((double)A).ToString(CultureInfo.InvariantCulture);
            else if (dA != 0 && A != CurrA) slowMoveA += " A" + ((double)A).ToString(CultureInfo.InvariantCulture);

                //the slow move speeds are specified here

            bool alreadyMoved = false;
            // If the needle is going down, we should move the XY first, if the needle is going up, we should
            // move the Z first
            if (Z == null || CurrentZ > Z) {
                if (slowMoveZ.Length > 0) Tx_G1(null, slowMoveZ);
                if (normalMove.Length > 0) Tx_G0(normalMove);
                alreadyMoved = true;
            }
            if (slowMoveXY.Length > 0) Tx_G1((int?)Properties.Settings.Default.CNC_SmallMovementSpeed, slowMoveXY);
            if (slowMoveA.Length > 0) Tx_G1(3000, slowMoveA);
            if (!alreadyMoved)
            {
                if (normalMove.Length > 0) Tx_G0(normalMove);
                if (slowMoveZ.Length > 0) Tx_G1(null, slowMoveZ);
            }

            // Make sure variables are updated even if no status report has come in yet
            // should be safe as they should be written over by any status report
            if (X != null) { setCurrX((double)X); }
            if (Y != null) { setCurrY((double)Y); }
            if (Z != null) { setCurrZ((double)Z); }
        }


    }  // end Class CNC


}


