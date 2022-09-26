#define TINYG_SHORTUNITS
// Some firmvare versions use units in millions, some don't. If not, comment out the above line.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Drawing;
using System.Reflection;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Timers;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;

namespace LitePlacer {

    public partial class FormMain: Form {
        public CNC Cnc;
        public CameraView cameraView;
        public NeedleClass Needle;
        TapesClass Tapes;
        CAD Cad;
        private LocationManager Locations;


        private bool isHomed ;
        public bool IsHomed { get { return isHomed; } 
            set {
                OpticalHome_button.BackColor = (value) ? Color.LightGreen : Color.White;
                isHomed = value;}
        }
        private bool IsShown { get; set; }


        public bool JoggingBusy {
            get { return Cnc.JoggingBusy; }
            set { Cnc.JoggingBusy = value; }
        }

        public bool AbortPlacement {
            get { return Cnc.AbortPlacement; }
            set { Cnc.AbortPlacement = value; }
        }

        // =================================================================================
        // General and "global" functions 
        // =================================================================================
        #region General

        // Note about thread guards: The prologue "if(InvokeRequired) {something long}" at a start of a function, 
        // makes the function safe to call from another thread.
        // See http://stackoverflow.com/questions/661561/how-to-update-the-gui-from-another-thread-in-c, 
        // "MajesticRa"'s answer near the bottom of first page



        #region MessageDialogs
        // =================================================================================
        // Thread safe dialog box:
        // (see http://stackoverflow.com/questions/559252/does-messagebox-show-automatically-marshall-to-the-ui-thread )

        public DialogResult ShowMessageBox(String message, String header, MessageBoxButtons buttons) {
            if (InvokeRequired) {
                return (DialogResult)Invoke(new PassStringStringReturnDialogResultDelegate(ShowMessageBox), message, header, buttons);
            }
            if (buttons.Equals(MessageBoxButtons.OK)) {
                return ShowSimpleMessageBox(message);
            }
            return MessageBox.Show(this, message, header, buttons);
        }
        public delegate DialogResult PassStringStringReturnDialogResultDelegate(String s1, String s2, MessageBoxButtons buttons);



        // This block of code will queue error messages that are sent within
        // mTimer.Interval and show them all at once - as a simpler way of handling
        // multiple error messages vs. the _m() method -- lets you be a lazier coder
        // without annoying the user.  Also provides a sort of 'error stack trace' 
        // which is useful in debugging
        List<string> errorMessages = new List<string>();
        public System.Timers.Timer mtimer;

        public void setupQueuedMessages() {
            mtimer = new System.Timers.Timer();
            mtimer.Elapsed += DisplayQueuedMessages;
            mtimer.Interval = 500;
        }

        public void DisplayQueuedMessages(object source, ElapsedEventArgs args) {
            mtimer.Enabled = false;
            ShowSimpleMessageBox(null);
        }

        public DialogResult ShowSimpleMessageBox(String message) {
            if (!IsShown) return DialogResult.Cancel;
            if (InvokeRequired) {
                return (DialogResult)Invoke(new PassStringDelegate(ShowSimpleMessageBox), message);
            }
            //null message to terminate
            if (message == null) {
                errorMessages.Reverse();
                string msg = String.Join("\n", errorMessages);
                errorMessages.Clear();
                if (!IsDisposed)
                    return MessageBox.Show(this, msg, "Error", MessageBoxButtons.OK);
            }
            errorMessages.Add(message);
            mtimer.Stop();
            mtimer.Enabled = true;
            mtimer.Start();
            return DialogResult.OK;
        }
        public delegate DialogResult PassStringDelegate(String s1);
        #endregion

        // =================================================================================


        // We need some functions both in JSON and in text mode:
        public const bool JSON = true;

        // This event is raised in the CNC class, and we'll wait for it when we want to continue only after TinyG has stabilized

        public void UpdateTapeTypesBinding() {
            Tapes.TapeTypes = cameraView.GetTapeTypes();
            TapeType.DataSource = new BindingSource() { DataSource = Tapes.TapeTypes };
        }

        private DataGridDrag locationDrag, componentDrag, jobDrag, tapeDrag;

        public FormMain() {
            InitializeComponent();
            setupQueuedMessages(); //error handling

            Do_Upgrade();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            // setup objects
            Cnc = new CNC(this);
            CNC.SquareCorrection = Properties.Settings.Default.CNC_SquareCorrection;

            Needle = new NeedleClass(this);
            Tapes = new TapesClass(Tapes_dataGridView, Needle, Cnc, this);
            Cad = new CAD(this);
            Locations = new LocationManager();

            Global.Instance.cnc = Cnc;
            Global.Instance.needle = Needle;
            Global.Instance.mainForm = this;
            Global.Instance.Locations = Locations;

            // open camera form
            // show the camera view stuff right away
            cameraView = new CameraView();
            cameraView.Show();
            cameraView.SetDownCameraDefaults();
            cameraView.SetUpCameraDefaults();
            cameraView.downClickDelegate = DownClickDelegate;
            cameraView.upClickDelegate = UpClickDelegate;

            //setup location jumps
            UpdateGoToPulldownMenu();
            Locations.GetList().ListChanged += delegate { 
                UpdateGoToPulldownMenu();
                Locations.Save();
            }; //update if list changed
            Locations.LocationChangeEvent += (s, e) => {
                if (e.PropertyName.Equals("Name")) UpdateGoToPulldownMenu();
                Locations.Save();
            }; //update if name on list changes

            //setup table entry dragging
            componentDrag = new DataGridDrag(CadData_GridView);
            locationDrag = new DataGridDrag(locations_dataGridView);
            tapeDrag = new DataGridDrag(Tapes_dataGridView);
            jobDrag = new DataGridDrag(JobData_GridView);

            //setup table bindings
            CadData_GridView.DataSource = Cad.ComponentData;
            locations_dataGridView.DataSource = Locations.GetList();            
            Tapes_dataGridView.DataSource = Tapes.tapeObjs;
            OriginalPartOrientation.DataSource = TapeObj.Orientation;
            OriginalTapeOrientation.DataSource = TapeObj.Orientation;
            UpdateTapeTypesBinding();
            PartType.DataSource = Enum.GetNames(typeof(ComponentType));

            //jobs table
            JobData_GridView.DataSource = Cad.JobData;
            methodDataGridViewComboBoxColumn.DataSource = new[] {"?", "Place", "LoosePlace", "Change Needle", "Recalibrate", "Ignore", "Pause", "Fiducial", "Place With UpCam"};
            // apply changes to multiple placement types at once
            JobData_GridView.CellValueChanged += (o, e) => {
                if (e.ColumnIndex != 3) return;
                for (int i = 0; i < JobData_GridView.SelectedCells.Count; i++) {
                    if (JobData_GridView.SelectedCells[i].ColumnIndex == 3) {
                        ((JobData)JobData_GridView.SelectedCells[i].OwningRow.DataBoundItem).Method =
                            ((JobData)JobData_GridView.Rows[e.RowIndex].DataBoundItem).Method;

                    }
                }
            };

            // right click context menu
            CadData_GridView.CellMouseDown += CadData_CellMouseDown;
            JobData_GridView.CellMouseDown += JobData_CellMouseDown;
            Tapes_dataGridView.CellMouseDown += Tapes_CellMouseDown;

            // setup key handling
            KeyPreview = true;
            SetupCursorNavigation(Controls);
            KeyUp += My_KeyUp;

            instructions_label.Text = "";
            instructions_label.Visible = false;

            // setup a bunch of stuff on the forms
            LightPlacerFormsSetup();
        }

        private void JobData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            if ( e.RowIndex != -1 && e.Button == MouseButtons.Right) {
                var x = ((DataGridView)sender)[e.ColumnIndex, e.RowIndex];
                var y = (JobData)x.OwningRow.DataBoundItem;
                ContextMenu m = new ContextMenu();

                var m1 = new MenuItem("Mark All Components Not Placed");
                m1.Click += (o, args) => {foreach (var z in y.Components) z.IsPlaced = true;};
                var m2 = new MenuItem("Mark All Components Placed");
                m2.Click += (o, args) => { foreach (var z in y.Components) z.IsPlaced = false; };
                var m3 = new MenuItem("Place All Part");
                m3.Click += (o, args) => { PlaceComponents(y.Components); };
                var m4 = new MenuItem("Move To Component Location On Tape");
                m4.Click += (o, args) => {
                    var a = Tapes.GetTapeObjByID(y.MethodParameters); if (a == null) return;
                    var b = Tapes.GetNextComponentPartLocation(a);    if (b == null) return;
                    Cnc.CNC_XY(b);
                };

                m.MenuItems.Add(m1);
                m.MenuItems.Add(m2);
                m.MenuItems.Add(m3);
                m.MenuItems.Add(m4);

                Rectangle r = x.DataGridView.GetCellDisplayRectangle(x.ColumnIndex, x.RowIndex, false);
                Point p = new Point(e.X, r.Y + e.Y);
                m.Show((DataGridView)sender, p);
            }

        }

        private void ShowImage(Image img) {
            using (Form form = new Form()) {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Size = img.Size;

                var pb = new PictureBox {
                    Dock = DockStyle.Fill,
                    Image = img
                };

                form.Controls.Add(pb);
                form.ShowDialog();
            }
        }


        private void Tapes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.RowIndex != -1 && e.Button == MouseButtons.Right) {
                var x = ((DataGridView)sender)[e.ColumnIndex, e.RowIndex];
                var y = (TapeObj)x.OwningRow.DataBoundItem;
                var m = new ContextMenuStrip();

                var m1 = new ToolStripMenuItem("Calibrate Part Via Holes");
                m1.Click += (o, args) => { Tapes.CalibrateWithHole(y); };
                var m2 = new ToolStripMenuItem("Calibrate Part Via Template (part image)");
                m2.Click += (o, args) => { Tapes.CalibrateWithTemplate(y); };                
                var m10 = new ToolStripMenuItem("Set Currently Visible Component As Next");
                m10.Click += (o, args) => { Tapes.setNextOptically(y); };

                var m3 = new ToolStripMenuItem("Add Tape");
                m3.Click += (o, args) => { Tapes.AddTapeObject(e.RowIndex); };
                var m4 = new ToolStripMenuItem("Delete This Tape");
                m4.Click += (o, args) => { Tapes.tapeObjs.RemoveAt(e.RowIndex); };

                var m5 = new ToolStripMenuItem("Go To Next Component");
                m5.Click += (o, args) => { Tapes.GoToNextComponent(y);  };
                var m6 = new ToolStripMenuItem("Pickup Next Component");
                m6.Click += (o, args) => { PickUpPart_m(y);  };
                var m13 = new ToolStripMenuItem("Pickup and Place In Dropoff Location");
                m13.Click += (o, args) => { if (!PickUpPart_m(y)) return; Global.NeedleTo("Component Dropoff"); Needle.ProbeDown(); PumpOff(); Cnc.Zup(); };
                
                var m9 = new ToolStripMenuItem("Set/Change Image");
                m9.Click += (o, args) => { setTemplateImage(y); };
                var m11 = new ToolStripMenuItem("Take Photo Of Component At Current Location");
                m11.Click += (o, args) => { Tapes.TakePhotoOfPartAtCurrentLocation(y); };

                var m12 = new ToolStripMenuItem("Measure Available Parts On Tape");
                m12.Click += (o,args) => {  Tapes.PopulateAvailableParts(y); };

                var m14 = new ToolStripMenuItem("Set first hole");
                m14.Click += (o, args) => { Tapes.SetFirstHole(y); };
                var m15 = new ToolStripMenuItem("Set last hole");
                m15.Click += (o, args) => { Tapes.SetLastHole(y); };

                var m16 = new ToolStripMenuItem("Set as feeder");
                m16.Click += (o, args) => { Tapes.SetAsFeeder(y); };

                m.Items.Add(m1);
                m.Items.Add(m2);
                m.Items.Add(m10);
                m.Items.Add( new ToolStripSeparator() );
                m.Items.Add(m3);
                m.Items.Add(m4);
                m.Items.Add( new ToolStripSeparator() );
                m.Items.Add(m5);
                m.Items.Add(m6);
                m.Items.Add(m13);
                m.Items.Add( new ToolStripSeparator() );
                 if (!string.IsNullOrEmpty(y.TemplateFilename) && File.Exists(y.TemplateFilename)) {
                    var m7 = new ToolStripMenuItem("View Part Image");
                    m7.Click += (o, args) => { using (var img = Image.FromFile(y.TemplateFilename)) ShowImage(img); };
                    m.Items.Add(m7);
                }
                m.Items.Add(m9);                
                m.Items.Add(m11);
                m.Items.Add( new ToolStripSeparator() );
                m.Items.Add(m12);

                m.Items.Add(new ToolStripSeparator());
                m.Items.Add(m14);
                m.Items.Add(m15);
                m.Items.Add(m16);
                
                Rectangle r = x.DataGridView.GetCellDisplayRectangle(x.ColumnIndex, x.RowIndex, false);
                Point p = new Point(r.X + e.X, r.Y + e.Y);
                m.Show((DataGridView)sender, p);
            }

        }



        private void CadData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.RowIndex != -1 && e.Button == MouseButtons.Right) {
                var x = ((DataGridView) sender)[e.ColumnIndex, e.RowIndex];
                var y = (PhysicalComponent) x.OwningRow.DataBoundItem;
                if (y.JobData == null) y.JobData = Cad.FindJobDataThatContainsComponent(y);

                ContextMenu m = new ContextMenu();

                var m1 = new MenuItem("Mark Not Placed"); 
                m1.Click += (o, args) => { y.IsPlaced = false; };
                var m2 = new MenuItem("Mark Placed"); 
                m2.Click += (o, args) => { y.IsPlaced = true; };
                var m3 = new MenuItem("Place Part"); 
                m3.Click += (o, args) => {if (y.JobData != null) PlaceComponent(y); };
                var m4 = new MenuItem("Move To PCB Location"); 
                m4.Click += (o, args) => {if (!IsMeasurementValid) if (!BuildMachineCoordinateData_m()) return; Cnc.CNC_XY( y.machine ); };
                var m5 = new MenuItem("Move To Component Location On Tape");
                m5.Click += (o, args) => {Cnc.CNC_XY(Tapes.GetNextComponentPartLocation(Tapes.GetTapeObjByID(y.MethodParameters))); };


                m.MenuItems.Add(y.IsPlaced ? m1 : m2);
                m.MenuItems.Add(m3);
                m.MenuItems.Add(m4);
                m.MenuItems.Add(m5);

                Rectangle r = x.DataGridView.GetCellDisplayRectangle(x.ColumnIndex, x.RowIndex, false);
                Point p = new Point(e.X, r.Y + e.Y);
                m.Show((DataGridView)sender,p);

            }
            
        }


        private void MethodSelectedIndexChanged(object sender, EventArgs e) {
            VacuumOff(true);
        }

        private void DownClickDelegate(double x, double y, int rawx, int rawy) {
            var offset = new PartLocation(x, y);
            Cnc.CNC_XY(Cnc.XYLocation + (cameraView.downVideoProcessing.mmPerPixel * offset));
        }
        private void UpClickDelegate(double x, double y, int rawx, int rawy) {
            var offset = new PartLocation(x, y);
            Cnc.CNC_XY(Cnc.XYLocation + (cameraView.upVideoProcessing.mmPerPixel * offset));
        }

        void LightPlacerFormsSetup() {
            UpdateCncConnectionStatus();

            Z0toPCB_BasicTab_label.Text = Properties.Settings.Default.ZDistanceToTable.ToString("0.00", CultureInfo.InvariantCulture);
            Z_Backoff_label.Text = Properties.Settings.Default.General_ProbingBackOff.ToString("0.00", CultureInfo.InvariantCulture);
            SquareCorrection_textBox.Text = Properties.Settings.Default.CNC_SquareCorrection.ToString();
            VacuumTime_textBox.Text = Properties.Settings.Default.General_PickupVacuumTime.ToString();
            VacuumRelease_textBox.Text = Properties.Settings.Default.General_PickupReleaseTime.ToString();
            SmallMovement_numericUpDown.Value = Properties.Settings.Default.CNC_SmallMovementSpeed;


            // Does this machine have any ports? (Maybe not, if TinyG is powered down.)
            RefreshPortList();
            if (comboBoxSerialPorts.Items.Count == 0) return;

            // At least there are some ports. Show the default port, if it is still there:
            comboBoxSerialPorts.SelectedIndex = 0;
            for (int i = 0; i < comboBoxSerialPorts.Items.Count; i++) {
                if (comboBoxSerialPorts.Items[i].Equals(setting.CNC_SerialPort)) {
                    comboBoxSerialPorts.SelectedIndex = i;
                    break;
                }
            }

            var f = Properties.Settings.Default.DownCam_XmmPerPixel * cameraView.downVideoProcessing.box.Width;
            DownCameraBoxX_textBox.Text = f.ToString("0.00", CultureInfo.InvariantCulture);
            DownCameraBoxXmmPerPixel_label.Text = "(" + Properties.Settings.Default.DownCam_XmmPerPixel.ToString("0.0000", CultureInfo.InvariantCulture) + "mm/pixel)";
            f = Properties.Settings.Default.DownCam_YmmPerPixel * cameraView.downVideoProcessing.box.Height;
            DownCameraBoxY_textBox.Text = f.ToString("0.00", CultureInfo.InvariantCulture);
            DownCameraBoxYmmPerPixel_label.Text = "(" + Properties.Settings.Default.DownCam_YmmPerPixel.ToString("0.0000", CultureInfo.InvariantCulture) + "mm/pixel)";

            f = Properties.Settings.Default.UpCam_XmmPerPixel * cameraView.upVideoProcessing.box.Width;
            UpCameraBoxX_textBox.Text = f.ToString("0.00", CultureInfo.InvariantCulture);
            UpCameraBoxXmmPerPixel_label.Text = "(" + Properties.Settings.Default.UpCam_XmmPerPixel.ToString("0.000", CultureInfo.InvariantCulture) + "mm/pixel)";
            f = Properties.Settings.Default.UpCam_YmmPerPixel * cameraView.upVideoProcessing.box.Height;
            UpCameraBoxY_textBox.Text = f.ToString("0.00", CultureInfo.InvariantCulture);
            UpCameraBoxYmmPerPixel_label.Text = "(" + Properties.Settings.Default.UpCam_YmmPerPixel.ToString("0.000", CultureInfo.InvariantCulture) + "mm/pixel)";

            HeightOffsetLabel.Text = "X corr: " + Math.Round(Properties.Settings.Default.zTravelXCompensation, 2).ToString() +
                " Y Corr: " + Math.Round(Properties.Settings.Default.zTravelYCompensation, 2).ToString();
            ZCorrectionX.Text = Math.Round(Properties.Settings.Default.zTravelXCompensation, 2).ToString();
            ZCorrectionY.Text = Math.Round(Properties.Settings.Default.zTravelYCompensation, 2).ToString();
            ZCorrectionDeltaZ.Text = Math.Round(Properties.Settings.Default.zTravelTotalZ, 2).ToString();


            NeedleOffsetX_textBox.Text = Properties.Settings.Default.DownCam_NeedleOffsetX.ToString("0.00", CultureInfo.InvariantCulture);
            NeedleOffsetY_textBox.Text = Properties.Settings.Default.DownCam_NeedleOffsetY.ToString("0.00", CultureInfo.InvariantCulture);

            SlackCompensation_checkBox.Checked = Properties.Settings.Default.CNC_SlackCompensation;
            ZTestTravel_textBox.Text = Properties.Settings.Default.General_ZTestTravel.ToString(CultureInfo.InvariantCulture);
            // Z0toPCB_CamerasTab_label.Text = Properties.Settings.Default.ZDistanceToTable.ToString("0.00", CultureInfo.InvariantCulture) + " mm";


            //my stuff -rn
            zoffset_textbox.Text = Properties.Settings.Default.z_offset.ToString(CultureInfo.InvariantCulture);
            Needle.ProbingMode(false);
            Cnc.z_offset = Properties.Settings.Default.z_offset;
            // template based fudical locating RN
            cb_useTemplate.Checked = Properties.Settings.Default.use_template;
            fiducialTemlateMatch_textBox.Text = Properties.Settings.Default.template_threshold.ToString(CultureInfo.InvariantCulture);
            fiducial_designator_regexp_textBox.Text = Properties.Settings.Default.fiducial_designator_regexp;
            needleCalibrationHeight_textbox.Text = Properties.Settings.Default.focus_height.ToString(CultureInfo.InvariantCulture);
            pressureSenstorPresent_button.Checked = Properties.Settings.Default.pressure_sensor;
            vacuumDeltaADC_textbox.Text = Properties.Settings.Default.vacuumDeltaExpected.ToString();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (Cnc.Connected) {
                if (IsHomed) Global.GoTo("Park");
                Cnc.CNC_A(0);
                PumpDefaultSetting();
                VacuumDefaultSetting();
                Cnc.CNC_Write_m("{\"md\":\"\"}");  // motor power off
                Cnc.Close();
            }

            Properties.Settings.Default.Save();
            Tapes.SaveAll();
            cameraView.Shutdown();
        }

        // =================================================================================
        // Get and save settings from old version if necessary
        // http://blog.johnsworkshop.net/automatically-upgrading-user-settings-after-an-application-version-change/

        private void Do_Upgrade() {
            try {
                if (Properties.Settings.Default.General_UpgradeRequired) {
                    DisplayText("Updating from previous version");
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.General_UpgradeRequired = false;
                    Properties.Settings.Default.Save();
                }
            } catch (SettingsPropertyNotFoundException) {
                DisplayText("Updating from previous version (through ex)");
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.General_UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }

        }
        // =================================================================================


        private void ShowBuildNumber() {
            // see http://stackoverflow.com/questions/1600962/displaying-the-build-date

            var version = Assembly.GetEntryAssembly().GetName().Version;
            var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(
            TimeSpan.TicksPerDay * version.Build + // days since 1 January 2000
            TimeSpan.TicksPerSecond * 2 * version.Revision)); // seconds since midnight, (multiply by 2 to get original)
            DisplayText("Version: " + version + ", build date: " + buildDateTime);
        }

        private void FormMain_Shown(object sender, EventArgs e) {
            //psoition window
            this.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            this.Top = 0;
            cameraView.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right - this.Width - cameraView.Width;
            cameraView.Top = 0;

            //----
            ShowBuildNumber();

          //  tabControlPages.SelectedTab = tabPageBasicSetup;
            UpdateCncConnectionStatus();

            if (Cnc.Connected) {
                Thread.Sleep(200); // Give TinyG time to wake up
                Cnc.CNC_RawWrite("\x11");  // Xon
                Thread.Sleep(50);

                VacuumOff(true);
                PumpOff(true);

                UpdateWindowValues_m();

            }

            IsShown = true;
        }




        // =================================================================================
        // Forcing a DataGridview display update
        // Ugly hack if you ask me, but MS didn't give us any other reliable way...
        private void Update_GridView(DataGridView Grid) {
            Grid.Invalidate();
            Grid.Update();
        }

        #endregion

        // =================================================================================
        // Jogging
        // =================================================================================
        #region Jogging

        // see https://github.com/synthetos/TinyG/wiki/TinyG-Feedhold-and-Resume


        List<Keys> JoggingKeys = new List<Keys>        {
	        Keys.Up,
	        Keys.Down,
	        Keys.Left,
            Keys.Right,
            Keys.F5,
            Keys.F6,
            Keys.F7,
            Keys.F8,
            Keys.F9,
            Keys.F10,
            Keys.F11,
            Keys.F12
	    };

        // To make sure we get to see all keydown events:
        public void SetupCursorNavigation(System.Windows.Forms.Control.ControlCollection controls) {
            foreach (System.Windows.Forms.Control ctrl in controls) {
                ctrl.PreviewKeyDown += My_KeyDown;
                SetupCursorNavigation(ctrl.Controls);
            }
        }


        public void My_KeyUp(object sender, KeyEventArgs e) {
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right)) {
                JoggingBusy = false;
                e.Handled = true;
                e.SuppressKeyPress = true;
                Cnc.RawWrite("!%");
            }
        }

        public void My_KeyDown(object sender, PreviewKeyDownEventArgs e) {
            if (!JoggingKeys.Contains(e.KeyCode)) {
                return;
            }

            e.IsInputKey = true;
            string Movestr = "{\"gc\":\"G1 F2000 ";

            if (JoggingBusy) return;
            if (!Cnc.Connected) return;

            if (e.KeyCode == Keys.Up) {
                JoggingBusy = true;
                Cnc.RawWrite(Movestr + "Y" + Locations.GetLocation("Max Machine").Y + "\"}");
                e.IsInputKey = true;
            } else if (e.KeyCode == Keys.Down) {
                JoggingBusy = true;
                Cnc.RawWrite(Movestr + "Y0\"}");
                e.IsInputKey = true;
            } else if (e.KeyCode == Keys.Left) {
                JoggingBusy = true;
                Cnc.RawWrite(Movestr + "X0\"}");
                e.IsInputKey = true;
            } else if (e.KeyCode == Keys.Right) {
                JoggingBusy = true;
                Cnc.RawWrite(Movestr + "X" + Locations.GetLocation("Max Machine").X + "\"}");
                e.IsInputKey = true;
            }
            Jog(sender, e);
        }


        //  [DllImport("user32.dll")]
        //  private static extern int HideCaret(IntPtr hwnd);

        private void Jog(object sender, PreviewKeyDownEventArgs e) {
            if (JoggingBusy) {
                return;
            }

            if (!Cnc.Connected) {
                return;
            }

            double Mag = 0.1;
            if ((e.Alt) && (e.Shift)) {
                Mag = 100.0;
            } else if ((e.Alt) && (e.Control)) {
                Mag = 4.0;
            } else if (e.Alt) {
                Mag = 10.0;
            } else if (e.Shift) {
                Mag = 1.0;
            } else if (e.Control) {
                Mag = 0.01;
            }

            // move right
            if (e.KeyCode == Keys.F5) {
                JoggingBusy = true;
                Cnc.CNC_XY(Cnc.CurrentX - Mag, Cnc.CurrentY);
                e.IsInputKey = true;
                JoggingBusy = false;
                return;
            }

            // move left
            if (e.KeyCode == Keys.F6) {
                JoggingBusy = true;
                Cnc.CNC_XY(Cnc.CurrentX + Mag, Cnc.CurrentY);
                e.IsInputKey = true;
                JoggingBusy = false;
                return;
            }

            // move away
            if (e.KeyCode == Keys.F7) {
                JoggingBusy = true;
                Cnc.CNC_XY(Cnc.CurrentX, Cnc.CurrentY + Mag);
                e.IsInputKey = true;
                JoggingBusy = false;
                return;
            }

            // move closer
            if (e.KeyCode == Keys.F8) {
                JoggingBusy = true;
                Cnc.CNC_XY(Cnc.CurrentX, Cnc.CurrentY - Mag);
                e.IsInputKey = true;
                JoggingBusy = false;
                return;
            };

            // rotate ccw
            if (e.KeyCode == Keys.F9) {
                JoggingBusy = true;
                if ((Mag > 99) && (Mag < 101)) {
                    Mag = 90.0;
                }
                Cnc.CNC_A(Cnc.CurrentA + Mag);

                e.IsInputKey = true;
                JoggingBusy = false;
                return;
            }

            // rotate cw
            if (e.KeyCode == Keys.F10) {
                JoggingBusy = true;
                if ((Mag > 99) && (Mag < 101)) {
                    Mag = 90.0;
                }
                Cnc.CNC_A(Cnc.CurrentA - Mag);

                e.IsInputKey = true;
                JoggingBusy = false;
                return;
            }

            // move up
            if (e.KeyCode == Keys.F11) {
                JoggingBusy = true;
                Cnc.CNC_Z(Cnc.CurrentZ - Mag);
                e.IsInputKey = true;
                JoggingBusy = false;
                return;
            }

            // move down
            if ((e.KeyCode == Keys.F12) && (Mag < 50)) {
                JoggingBusy = true;
                Cnc.CNC_Z(Cnc.CurrentZ + Mag);
                JoggingBusy = false;
                e.IsInputKey = true;
            }

        }

        #endregion

        // =================================================================================
        // CNC interface functions
        // =================================================================================
        #region CNC interface functions


        private bool VacuumIsOn;
        private bool PumpIsOn;
        private bool PressureSensorPresent { get { return pressureSenstorPresent_button.Checked; } }

        private void VacuumDefaultSetting() {
            VacuumOff(true);
        }
        private void PumpDefaultSetting() {
            PumpOff(true);
        }
        private void VacuumOn() {
            if (!PumpIsOn) PumpOn();
            if (!VacuumIsOn) {
                DisplayText("VacuumOn()");
                Cnc.CNC_RawWrite("{\"gc\":\"M08\"}");
                Vacuum_checkBox.Checked = true;
                Thread.Sleep(Properties.Settings.Default.General_PickupVacuumTime);
                VacuumIsOn = true;
            }
        }

        private void VacuumOff(bool force = false) {
            if (force || VacuumIsOn) {
                DisplayText("VacuumOff()");
                Cnc.CNC_RawWrite("{\"gc\":\"M09\"}");
                Vacuum_checkBox.Checked = false;
                Thread.Sleep(Properties.Settings.Default.General_PickupReleaseTime);
                VacuumIsOn = false;
            }
        }



        private void BugWorkaround() {
            // see https://www.synthetos.com/topics/file-not-open-error/#post-7194
            // Summary: In some cases, we need a dummy move.
            bool slackSave = Cnc.SlackCompensation;
            Cnc.SlackCompensation = false;
            Cnc.CNC_XY(Cnc.CurrentX - 0.5, Cnc.CurrentY - 0.5);
            Cnc.CNC_XY(Cnc.CurrentX + 0.5, Cnc.CurrentY + 0.5);
            Cnc.SlackCompensation = slackSave;
        }

        private void PumpOn() {
            if (!PumpIsOn) {
                //Cnc.CNC_RawWrite("M03");
                Cnc.CNC_RawWrite("{\"gc\":\"M03\"}");
                Pump_checkBox.Checked = true;
                Thread.Sleep(500);  // this much to develop vacuum
                BugWorkaround();
                PumpIsOn = true;
            }
        }

        private void PumpOff(bool force = false) {
            if (VacuumIsOn) VacuumOff();
            if (force || PumpIsOn) {
                //CNC_RawWrite("M05");
                Cnc.CNC_RawWrite("{\"gc\":\"M05\"}");
                Thread.Sleep(50);
                BugWorkaround();
                Pump_checkBox.Checked = false;
                PumpIsOn = false;
            }
        }



        // moved logic from this function into Needle.Calibrate
        public bool CalibrateNeedle_m() {
            if (Needle.Calibrate(4.0 / Properties.Settings.Default.UpCam_XmmPerPixel)) {
                for (int i = 0; i < Needle.CalibrationPoints.Count; i++)
                    DisplayText(Needle.CalibrationPoints[i].ToString(), Color.Purple);
                Needle.Calibrated = true;
                return true;
            }
            ShowSimpleMessageBox("Needle Calibration Failed");
            Needle.Calibrated = false;
            return false;
        }


        /// <summary>
        /// Will return the actual location of the part
        /// </summary>
        public PartLocation FindPositionAndMoveToClosest(Shapes.ShapeTypes type, double FindTolerance, double MoveTolerance, string template=null, double threshold=-1) {
            var x = GoToClosestThing(type, FindTolerance, MoveTolerance, null, template, threshold);
            if (x != null) return x.ToPartLocation();
            return null;
        }
        

        // will only work with downcamera
        public Shapes.Thing GoToClosestThing(Shapes.ShapeTypes type, double FindTolerance, double MoveTolerance, VideoProcessing vp = null, string template = null, double threshold = -1) {
            DisplayText("GoToThing(" + type + "), FindTolerance: " + FindTolerance + ", MoveTolerance: " + MoveTolerance, Color.Orange);
            vp = vp ?? cameraView.downVideoProcessing;
            Shapes.Thing thing = null;
            double dir = (vp.IsUpCamera()) ? -1 : 1;
            
            for (int i = 0; i < 8; i++) { //move up to 8 times
                Global.DoBackgroundWork();
                thing = VideoDetection.FindClosest(vp, type, FindTolerance, 2, template, threshold);

                if (thing == null) break; //couldn't find

                thing.ToMMResolution();
                DisplayText("--> round " + i + ", offset= " + thing.ToPartLocation() + " dist/tol = " + thing.ToPartLocation().VectorLength().ToString("F2") + " of " + FindTolerance);

                // If we are further than move tolerance, go there, else end loop
                if (thing.ToPartLocation().VectorLength() > MoveTolerance) {
                    Console.WriteLine("\tmoving by " + thing.ToPartLocation());
                    Cnc.CNC_XY(Cnc.XYLocation + (dir * thing.ToPartLocation()));
                } else break;
            }

            //if (thing == null)  ShowSimpleMessageBox("Optical positioning: Process is unstable, result is unreliable!");
            if (thing != null) thing.AddOffset(Cnc.XYLocation);

            return thing;
        }
        

        private bool OpticalHoming_m() {
            if (Cnc.Simulation) { return true;  }

            DisplayText("Optical homing");
            cameraView.SetDownCameraFunctionSet("homing");
            var loc = FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 20, .05);
            if (loc == null) {
                cameraView.DownCameraReset();
                return false;
            }
            loc = -1*(loc - Cnc.XYLocation);
            Cnc.CNC_RawWrite("{\"gc\":\"G28.3 X" + loc.X.ToString("0.000") + " Y" + loc.Y.ToString("0.000") + "\"}");
            Thread.Sleep(50);

            Cnc.CurrentX = loc.X;
            Cnc.CurrentY = loc.Y;
            UpdatePositionDisplay();

            DisplayText("Optical homing OK.");
            cameraView.DownCameraReset();
            return true;
        }


        private bool MechanicalHoming_m() {
            Needle.ProbingMode(false);
            DisplayText("Homing Z");
            if (!Cnc.CNC_Home_m("Z")) return false;
            DisplayText("Homing X");
            if (!Cnc.CNC_Home_m("X")) return false;
            DisplayText("Homing Y");
            if (!Cnc.CNC_Home_m("Y")) return false;
            DisplayText("Homing A");
            if (!Cnc.CNC_A(0)) return false;
            return true;
        }


        private void OpticalHome_button_Click(object sender, EventArgs e) {
            if (!MechanicalHoming_m()) return;
            IsHomed = OpticalHoming_m();
        }

        #endregion

   

        // =================================================================================
        private static int SetNeedleOffset_stage;
        private static double NeedleOffsetMarkX;
        private static double NeedleOffsetMarkY;
        private static double NeedleOffsetMarkZ;

        private void Offset2Method_button_Click(object sender, EventArgs e) {
            Cnc.ZGuardOff();
            cameraView.SetDownCameraDefaults();
            switch (SetNeedleOffset_stage) {

                case 0:
                    SetNeedleOffset_stage = 1;
                    Offset2Method_button.Text = "Next";
                    Offset2Method_button.BackColor = Color.Yellow;
                    Cnc.CNC_A(0.0);
                    instructions_label.Visible = true;
                    instructions_label.Text = "Jog needle to a point on a PCB, then click \"Next\"";
                    break;

                case 1:
                    SetNeedleOffset_stage = 2;
                    NeedleOffsetMarkY = Cnc.CurrentY;
                    NeedleOffsetMarkX = Cnc.TrueX;
                    NeedleOffsetMarkZ = Cnc.CurrentZ;
                    Cnc.Zup();
                    Cnc.CNC_XY(Cnc.CurrentX - 75.0, Cnc.CurrentY - 25.0);
                    cameraView.downSettings.DrawCross = true;
                    instructions_label.Text = "Jog camera above the same point, \n\rthen click \"Next\"";
                    break;

                case 2:
                    SetNeedleOffset_stage = 3;
                    double CorrectedX, CorrectedY;
                    if (!Needle.CorrectedPosition_m(Cnc.CurrentA, out CorrectedX, out CorrectedY)) {
                        ShowSimpleMessageBox("Needle not calibrated");
                        SetNeedleOffset_stage = 0;
                        instructions_label.Visible = false;
                        instructions_label.Text = "   ";
                        Offset2Method_button.Text = "Camera Offset";
                        Offset2Method_button.BackColor = ChangeNeedle_button.BackColor;
                        break;
                    }
                    if (Properties.Settings.Default.zTravelTotalZ != 0 && NeedleOffsetMarkZ != 0)
                    {
                        Properties.Settings.Default.DownCam_NeedleOffsetX = NeedleOffsetMarkX - Cnc.TrueX
                            + (Properties.Settings.Default.zTravelXCompensation /
                            Properties.Settings.Default.zTravelTotalZ * NeedleOffsetMarkZ) + CorrectedX;
                        Properties.Settings.Default.DownCam_NeedleOffsetY = NeedleOffsetMarkY - Cnc.CurrentY
                            + (Properties.Settings.Default.zTravelYCompensation /
                            Properties.Settings.Default.zTravelTotalZ * NeedleOffsetMarkZ) + CorrectedY;
                    }
                    else
                    {
                        Properties.Settings.Default.DownCam_NeedleOffsetX = NeedleOffsetMarkX - Cnc.TrueX
                             + CorrectedX;
                        Properties.Settings.Default.DownCam_NeedleOffsetY = NeedleOffsetMarkY - Cnc.CurrentY
                             + CorrectedY;
                    }
                    Properties.Settings.Default.Save();
                    NeedleOffsetX_textBox.Text = Properties.Settings.Default.DownCam_NeedleOffsetX.ToString("0.00", CultureInfo.InvariantCulture);
                    NeedleOffsetY_textBox.Text = Properties.Settings.Default.DownCam_NeedleOffsetY.ToString("0.00", CultureInfo.InvariantCulture);
                    instructions_label.Text = "Ensure that the 'Up Camera' location, focus height, and\nNeedle video filters are set such that the needle is\n in the camera's field of view and you can detect a circle. Select Next when these conditions are met";
                    Cnc.Zup();
                    break;

                case 3:
                    SetNeedleOffset_stage = 0;
                    /*upCameraZero_button_Click(null, null);*/
                    instructions_label.Visible = false;
                    instructions_label.Text = "   ";
                    Offset2Method_button.Text = "Camera Offset";
                    Offset2Method_button.BackColor = ChangeNeedle_button.BackColor;
                    break;
            }
        }

        public void upCameraZero_button_Click(object sender, EventArgs e) {
            needleToFocus_button_Click(null, null);
            Cnc.ZGuardOff();
            cameraView.SetUpCameraFunctionSet("Needle");
            cameraView.upSettings.DrawCross = true;
            cameraView.upSettings.FindCircles = true;
            Global.DoBackgroundWork(200);
            var loc = GoToClosestThing(Shapes.ShapeTypes.Circle, 20, .05, cameraView.upVideoProcessing);
            if (loc != null) Locations.GetLocation("Up Camera").SetTo(loc.ToPartLocation());
            Global.GoTo("Up Camera"); //test
            cameraView.UpCameraReset();
            Cnc.Zup();
        }




        // =================================================================================

        public void CalibrateNeedle_button_Click(object sender, EventArgs e) {
            var p = Cnc.XYALocation;
            CalibrateNeedle_m();
            Cnc.Zup();
            Cnc.CNC_XYA(p);
            cameraView.UpCameraReset();
        }


        // =================================================================================
        // Basic setup page functions
        // =================================================================================
        #region Basic setup page functions


        private void buttonRefreshPortList_Click(object sender, EventArgs e) {
            RefreshPortList();
        }

        private void RefreshPortList() {
            comboBoxSerialPorts.Items.Clear();
            foreach (string s in SerialPort.GetPortNames()) {
                comboBoxSerialPorts.Items.Add(s);
            }
            if (comboBoxSerialPorts.Items.Count == 0) {
                labelSerialPortStatus.Text = "No serial ports found. Is TinyG powered on?";
            } else {
                // show the first available port
                comboBoxSerialPorts.SelectedIndex = 0;
            }
        }


        public void UpdateCncConnectionStatus() {
            if (InvokeRequired) { Invoke(new Action(UpdateCncConnectionStatus)); return; }

            if (Cnc.Connected) {
                buttonConnectSerial.Text = "Reset Conn.";
                labelSerialPortStatus.Text = "Connected";
                labelSerialPortStatus.ForeColor = Color.Black;
            } else {
                buttonConnectSerial.Text = "Connect";
                labelSerialPortStatus.Text = "Not connected";
                labelSerialPortStatus.ForeColor = Color.Red;
            }
        }

        private void buttonConnectSerial_Click(object sender, EventArgs e) {
            if (comboBoxSerialPorts.SelectedItem == null) return;

            if (Cnc.Connected) {
                Cnc.Close();
                Thread.Sleep(250);
            } else {
                if (Cnc.Connect(comboBoxSerialPorts.SelectedItem.ToString())) {
                    Properties.Settings.Default.CNC_SerialPort = comboBoxSerialPorts.SelectedItem.ToString();
                    UpdateWindowValues_m();
                }
            }
            UpdateCncConnectionStatus();
        }



        // TinyG communication monitor textbox  
        public void DisplayText(string txt, Color color) {
            if (!IsShown) return;
            // XXX need to add robust mechanism to only show desired debugging messages
            if (!showTinyGComms_checkbox.Checked && (color.Equals(Color.Gray) || color.Equals(Color.Green))) return;
            try {
                if (InvokeRequired) { Invoke(new Action<string, Color>(DisplayText), txt, color); return; }
                txt = txt.Replace("\n", "");
                // TinyG sends \n, textbox needs \r\n. (TinyG could be set to send \n\r, which does not work with textbox.)
                // Adding end of line here saves typing elsewhere
                txt = txt + "\r\n";
                if (SerialMonitor_richTextBox.Text.Length > 1000000) {
                    SerialMonitor_richTextBox.Text = SerialMonitor_richTextBox.Text.Substring(SerialMonitor_richTextBox.Text.Length - 10000);
                }
                SerialMonitor_richTextBox.AppendText(txt, color);
                SerialMonitor_richTextBox.ScrollToCaret();
            } catch {
            }
        }

        public void DisplayText(string txt) {
            DisplayText(txt, SerialMonitor_richTextBox.ForeColor);
        }


        private void textBoxSendtoTinyG_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                Cnc.RawWrite(textBoxSendtoTinyG.Text);
                textBoxSendtoTinyG.Clear();
            }
        }

        // Sends the calls that will result to messages that update the values shown
        string[] tinyg_commands = new string[] {"sr","xjm","xvm","xsv","xsn","xjh","xsx","1mi","1sa","1tr","yjm",
                "yvm","ysn","ysx","yjh","ysv","2mi","2sa","2tr","zjm","zvm","zsn","zsx","zjh","zsv","3mi","3sa",
                "3tr","ajm","avm","4mi","4sa","4tr", "4po"};

        private bool UpdateWindowValues_m() {
            foreach (var c in tinyg_commands) {
                if (!Cnc.CNC_Write_m("{\"" + c + "\":\"\"}")) return false;
            }

            //RN - needed to change angle of rotation of needle to match stuff
            // gooz
            //if (!Cnc.CNC_Write_m("{\"4po\":1}")) return false;

            // Do settings that need to be done always
            // Cnc.IgnoreError = true;
            Needle.ProbingMode(false);

            // RN
            //Cnc.CNC_Write_m("{\"me\":\"\"}");  // motor power off -  wait till we actually send a command it should power itself on
            return true;
        }

        // Called from CNC class when UI need updating
        public void ValueUpdater(string item, string value) {
            if (InvokeRequired) { Invoke(new Action<string, string>(ValueUpdater), item, value); return; }

            switch (item) {
                case "4po": reverseRotation_checkbox.Checked = (value == "1");
                    break;
                case "xsn": Update_xsn(value);
                    break;
                case "ysn": Update_ysn(value);
                    break;
                case "zsn": Update_zsn(value);
                    break;
                case "xsx": Xmax_checkBox.Checked = (value == "2");
                    break;
                case "ysx": Ymax_checkBox.Checked = (value == "2");
                    break;
                case "zsx": Zmax_checkBox.Checked = (value == "2");
                    break;
                case "xvm": Update_xvm(value);
                    break;
                case "yvm": Update_yvm(value);
                    break;
                case "zvm": Update_zvm(value);
                    break;
                case "avm": Update_avm(value);
                    break;
                case "1sa": Update_1sa(value);
                    break;
                case "2sa": Update_2sa(value);
                    break;
                case "3sa": Update_3sa(value);
                    break;
                case "4sa": Update_4sa(value);
                    break;
                default: update_field(item, value);
                    break;
            }
        }

        // =========================================================================
        // Thread-safe update functions and value setting fuctions


        // =========================================================================
        // *jm setting
        private void xjm_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            xjm_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {

#if (TINYG_SHORTUNITS)
                Cnc.CNC_Write_m("{\"xjm\":" + xjm_maskedTextBox.Text + "}");
#else
                Cnc.CNC_Write_m("{\"xjm\":" + xjm_maskedTextBox.Text + "000000}");
#endif
                Thread.Sleep(50);
                xjm_maskedTextBox.ForeColor = Color.Black;
            }
        }

        private void yjm_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            yjm_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {

#if (TINYG_SHORTUNITS)
                Cnc.CNC_Write_m("{\"yjm\":" + yjm_maskedTextBox.Text + "}");
#else
                Cnc.CNC_Write_m("{\"yjm\":" + yjm_maskedTextBox.Text + "000000}");
#endif
                Thread.Sleep(50);
                yjm_maskedTextBox.ForeColor = Color.Black;
            }
        }

        private void zjm_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            zjm_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {

#if (TINYG_SHORTUNITS)
                Cnc.CNC_Write_m("{\"zjm\":" + zjm_maskedTextBox.Text + "}");
#else
                Cnc.CNC_Write_m("{\"zjm\":" + zjm_maskedTextBox.Text + "000000}");
#endif
                Thread.Sleep(50);
                zjm_maskedTextBox.ForeColor = Color.Black;
            }
        }

        private void ajm_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            ajm_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {

#if (TINYG_SHORTUNITS)
                Cnc.CNC_Write_m("{\"ajm\":" + ajm_maskedTextBox.Text + "}");
#else
                Cnc.CNC_Write_m("{\"ajm\":" + ajm_maskedTextBox.Text + "000000}");
#endif
                Thread.Sleep(50);
                ajm_maskedTextBox.ForeColor = Color.Black;
            }
        }

        #endregion


        // =========================================================================
        // *jh setting

        private void xjh_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            xjh_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                Cnc.CNC_Write_m("{\"xjh\":" + xjh_maskedTextBox.Text + "}");
                Thread.Sleep(50);
                xjh_maskedTextBox.ForeColor = Color.Black;
            }
        }

        private void yjh_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            yjh_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                Cnc.CNC_Write_m("{\"yjh\":" + yjh_maskedTextBox.Text + "}");
                Thread.Sleep(50);
                yjh_maskedTextBox.ForeColor = Color.Black;
            }
        }

        private void zjh_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            zjh_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                Cnc.CNC_Write_m("{\"zjh\":" + zjh_maskedTextBox.Text + "}");
                Thread.Sleep(50);
                zjh_maskedTextBox.ForeColor = Color.Black;
            }
        }

        // =========================================================================
        // *sv setting

        private void xsv_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            xsv_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                Cnc.CNC_Write_m("{\"xsv\":" + xsv_maskedTextBox.Text + "}");
                Thread.Sleep(50);
                xsv_maskedTextBox.ForeColor = Color.Black;
            }
        }

        private void ysv_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            ysv_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                Cnc.CNC_Write_m("{\"ysv\":" + ysv_maskedTextBox.Text + "}");
                Thread.Sleep(50);
                ysv_maskedTextBox.ForeColor = Color.Black;
            }
        }

        private void zsv_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            zsv_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                Cnc.CNC_Write_m("{\"zsv\":" + zsv_maskedTextBox.Text + "}");
                Thread.Sleep(50);
                zsv_maskedTextBox.ForeColor = Color.Black;
            }
        }

        // =========================================================================
        #region sn
        // *sn: Negative limit switch
        // *sn update

        private void Update_xsn(string value) {
            var x = int.Parse(value);
            Xhome_checkBox.Checked = ((x & 0x01) > 0);
            Xlim_checkBox.Checked = ((x & 0x02) > 0);
        }
        private void Update_ysn(string value) {
            var x = int.Parse(value);
            Yhome_checkBox.Checked = ((x & 0x01) > 0);
            Ylim_checkBox.Checked = ((x & 0x02) > 0);
        }

        private void Update_zsn(string value) {
            var x = int.Parse(value);
            Yhome_checkBox.Checked = ((x & 0x01) > 0);
            Ylim_checkBox.Checked = ((x & 0x02) > 0);
        }


        // =========================================================================
        // *sn setting

        private void Xhome_checkBox_Click(object sender, EventArgs e) {
            int i = 0;
            if (Xlim_checkBox.Checked) i = 2;
            if (Xhome_checkBox.Checked) i++;
            Cnc.CNC_Write_m("{\"xsn\":" + i + "}");
            Thread.Sleep(50);
        }

        private void Xlim_checkBox_Click(object sender, EventArgs e) {
            int i = 0;
            if (Xlim_checkBox.Checked) i = 2;
            if (Xhome_checkBox.Checked) i++;
            Cnc.CNC_Write_m("{\"xsn\":" + i + "}");
            Thread.Sleep(50);
        }

        private void Yhome_checkBox_Click(object sender, EventArgs e) {
            int i = 0;
            if (Ylim_checkBox.Checked) i = 2;
            if (Yhome_checkBox.Checked) i++;
            Cnc.CNC_Write_m("{\"ysn\":" + i + "}");
            Thread.Sleep(50);
        }

        private void Ylim_checkBox_Click(object sender, EventArgs e) {
            int i = 0;
            if (Ylim_checkBox.Checked) i = 2;
            if (Yhome_checkBox.Checked) i++;
            Cnc.CNC_Write_m("{\"ysn\":" + i + "}");
            Thread.Sleep(50);
        }

        private void Zhome_checkBox_Click(object sender, EventArgs e) {
            int i = 0;
            if (Zlim_checkBox.Checked) i = 2;
            if (Zhome_checkBox.Checked) i++;
            Cnc.CNC_Write_m("{\"zsn\":" + i + "}");
            Thread.Sleep(50);
        }

        private void Zlim_checkBox_Click(object sender, EventArgs e) {
            int i = 0;
            if (Zlim_checkBox.Checked) i = 2;
            if (Zhome_checkBox.Checked) i++;
            Cnc.CNC_Write_m("{\"zsn\":" + i + "}");
            Thread.Sleep(50);
        }

        #endregion

        private void reverseRotation_checkbox_Click(object sender, EventArgs e) {
            var c = (CheckBox) sender;
            Cnc.CNC_SetValue("4po", (c.Checked) ? 1 : 0);
            Global.DoBackgroundWork(50);
        }


        // =========================================================================
        #region sx

        // =========================================================================
        // *sx setting

        private void Xmax_checkBox_Click(object sender, EventArgs e) {
            if (Xmax_checkBox.Checked) {
                Cnc.CNC_Write_m("{\"xsx\":2}");
                Thread.Sleep(50);
            } else {
                Cnc.CNC_Write_m("{\"xsx\":0}");
                Thread.Sleep(50);
            }
        }

        private void Ymax_checkBox_Click(object sender, EventArgs e) {
            if (Ymax_checkBox.Checked) {
                Cnc.CNC_Write_m("{\"ysx\":2}");
                Thread.Sleep(50);
            } else {
                Cnc.CNC_Write_m("{\"ysx\":0}");
                Thread.Sleep(50);
            }
        }

        private void Zmax_checkBox_Click(object sender, EventArgs e) {
            if (Zmax_checkBox.Checked) {
                Cnc.CNC_Write_m("{\"zsx\":2}");
                Thread.Sleep(50);
            } else {
                Cnc.CNC_Write_m("{\"zsx\":0}");
                Thread.Sleep(50);
            }
        }

        #endregion

        // =========================================================================
        #region vm
        // *vm: Velocity maximum
        // *vm update

        private void Update_xvm(string value) {
            if (InvokeRequired) { Invoke(new Action<string>(Update_xvm), value); return; }

            int ind = value.IndexOf('.');   // Cut off the decimal portion, otherwise convert fails in some non-US cultures 
            if (ind > 0) {
                value = value.Substring(0, ind);
            };
            double val = Convert.ToDouble(value);
            val = val / 1000;
            xvm_maskedTextBox.Text = val.ToString();
        }

        private void Update_yvm(string value) {
            if (InvokeRequired) { Invoke(new Action<string>(Update_yvm), value); return; }

            int ind = value.IndexOf('.');   // Cut off the decimal portion, otherwise convert fails in some non-US cultures 
            if (ind > 0) {
                value = value.Substring(0, ind);
            };
            double val = Convert.ToDouble(value);
            val = val / 1000;
            yvm_maskedTextBox.Text = val.ToString();
        }

        private void Update_zvm(string value) {
            if (InvokeRequired) { Invoke(new Action<string>(Update_zvm), value); return; }

            int ind = value.IndexOf('.');   // Cut off the decimal portion, otherwise convert fails in some non-US cultures 
            if (ind > 0) {
                value = value.Substring(0, ind);
            };
            double val = Convert.ToDouble(value);
            zvm_maskedTextBox.Text = val.ToString();
        }


        private void Update_avm(string value) {
            if (InvokeRequired) { Invoke(new Action<string>(Update_avm), value); return; }

            int ind = value.IndexOf('.');   // Cut off the decimal portion, otherwise convert fails in some non-US cultures 
            if (ind > 0) {
                value = value.Substring(0, ind);
            };
            double val = Convert.ToDouble(value);
            val = val / 1000;
            avm_maskedTextBox.Text = val.ToString();
        }

        // =========================================================================
        // *vm setting

        private void xvm_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            xvm_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                Cnc.CNC_Write_m("{\"xvm\":" + xvm_maskedTextBox.Text + "000}");
                Thread.Sleep(50);
                Cnc.CNC_Write_m("{\"xfr\":" + xvm_maskedTextBox.Text + "000}");
                Thread.Sleep(50);
                xvm_maskedTextBox.ForeColor = Color.Black;
            }
        }

        private void yvm_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            yvm_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                Cnc.CNC_Write_m("{\"yvm\":" + yvm_maskedTextBox.Text + "000}");
                Thread.Sleep(50);
                Cnc.CNC_Write_m("{\"yfr\":" + yvm_maskedTextBox.Text + "000}");
                Thread.Sleep(50);
                yvm_maskedTextBox.ForeColor = Color.Black;
            }
        }

        private void zvm_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            zvm_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                Cnc.CNC_Write_m("{\"zvm\":" + zvm_maskedTextBox.Text + "}");
                Thread.Sleep(50);
                Cnc.CNC_Write_m("{\"zfr\":" + zvm_maskedTextBox.Text + "}");
                Thread.Sleep(50);
                zvm_maskedTextBox.ForeColor = Color.Black;
                int peek = Convert.ToInt32(zvm_maskedTextBox.Text);
                Properties.Settings.Default.CNC_ZspeedMax = peek;
            }
        }

        private void avm_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            avm_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                Cnc.CNC_Write_m("{\"avm\":" + avm_maskedTextBox.Text + "000}");
                Thread.Sleep(50);
                Cnc.CNC_Write_m("{\"afr\":" + avm_maskedTextBox.Text + "000}");
                Thread.Sleep(50);
                avm_maskedTextBox.ForeColor = Color.Black;
                int peek = Convert.ToInt32(avm_maskedTextBox.Text);
                Properties.Settings.Default.CNC_AspeedMax = peek;
            }
        }

        #endregion

        // =========================================================================
        #region mi
        // *mi: microstepping
        // *mi update


        // =========================================================================
        // *mi setting

        private void mi1_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            mi1_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                if ((mi1_maskedTextBox.Text == "1") || (mi1_maskedTextBox.Text == "2")
                    || (mi1_maskedTextBox.Text == "4") || (mi1_maskedTextBox.Text == "8")) {
                    Cnc.CNC_Write_m("{\"1mi\":" + mi1_maskedTextBox.Text + "}");
                    Thread.Sleep(50);
                    mi1_maskedTextBox.ForeColor = Color.Black;
                }
            }
        }

        private void mi2_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            mi2_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                if ((mi2_maskedTextBox.Text == "1") || (mi2_maskedTextBox.Text == "2")
                    || (mi2_maskedTextBox.Text == "4") || (mi2_maskedTextBox.Text == "8")) {
                    Cnc.CNC_Write_m("{\"2mi\":" + mi2_maskedTextBox.Text + "}");
                    Thread.Sleep(50);
                    mi2_maskedTextBox.ForeColor = Color.Black;
                }
            }
        }


        private void mi3_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            mi3_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                if ((mi3_maskedTextBox.Text == "1") || (mi3_maskedTextBox.Text == "2")
                    || (mi3_maskedTextBox.Text == "4") || (mi3_maskedTextBox.Text == "8")) {
                    Cnc.CNC_Write_m("{\"3mi\":" + mi3_maskedTextBox.Text + "}");
                    Thread.Sleep(50);
                    mi3_maskedTextBox.ForeColor = Color.Black;
                }
            }
        }

        private void mi4_maskedTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            mi4_maskedTextBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                if ((mi4_maskedTextBox.Text == "1") || (mi4_maskedTextBox.Text == "2")
                    || (mi4_maskedTextBox.Text == "4") || (mi4_maskedTextBox.Text == "8")) {
                    Cnc.CNC_Write_m("{\"4mi\":" + mi4_maskedTextBox.Text + "}");
                    Thread.Sleep(50);
                    mi4_maskedTextBox.ForeColor = Color.Black;
                }
            }
        }

        #endregion

        // =========================================================================
        #region tr



        // =========================================================================
        // *tr setting
        private void tr1_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            double val;
            tr1_textBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                if (double.TryParse(tr1_textBox.Text, out val)) {
                    Cnc.CNC_Write_m("{\"1tr\":" + tr1_textBox.Text + "}");
                    Thread.Sleep(50);
                    tr1_textBox.ForeColor = Color.Black;
                }
            }
        }

        private void tr2_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            double val;
            tr2_textBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                if (double.TryParse(tr2_textBox.Text, out val)) {
                    Cnc.CNC_Write_m("{\"2tr\":" + tr2_textBox.Text + "}");
                    Thread.Sleep(50);
                    tr2_textBox.ForeColor = Color.Black;
                }
            }
        }

        private void tr3_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            double val;
            tr3_textBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                if (double.TryParse(tr3_textBox.Text, out val)) {
                    Cnc.CNC_Write_m("{\"3tr\":" + tr3_textBox.Text + "}");
                    Thread.Sleep(50);
                    tr3_textBox.ForeColor = Color.Black;
                }
            }
        }

        private void tr4_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            double val;
            tr4_textBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                if (double.TryParse(tr1_textBox.Text, out val)) {
                    Cnc.CNC_Write_m("{\"4tr\":" + tr4_textBox.Text + "}");
                    Thread.Sleep(50);
                    tr4_textBox.ForeColor = Color.Black;
                }
            }
        }

        #endregion

        // =========================================================================
        #region sa
        // *sa: Step angle, 0.9 or 1.8
        // *sa update

        private void Update_1sa(string value) {
            if (InvokeRequired) { Invoke(new Action<string>(Update_1sa), value); return; }

            if ((value == "0.90") || (value == "0.900")) {
                m1deg09_radioButton.Checked = true;
            } else if ((value == "1.80") || (value == "1.800")) {
                m1deg18_radioButton.Checked = true;
            }
        }

        private void Update_2sa(string value) {
            if (InvokeRequired) { Invoke(new Action<string>(Update_2sa), value); return; }

            if ((value == "0.90") || (value == "0.900")) {
                m2deg09_radioButton.Checked = true;
            } else if ((value == "1.80") || (value == "1.800")) {
                m2deg18_radioButton.Checked = true;
            }
        }

        private void Update_3sa(string value) {
            if (InvokeRequired) { Invoke(new Action<string>(Update_3sa), value); return; }

            if ((value == "0.90") || (value == "0.900")) {
                m3deg09_radioButton.Checked = true;
            } else if ((value == "1.80") || (value == "1.800")) {
                m3deg18_radioButton.Checked = true;
            }
        }

        private void Update_4sa(string value) {
            if (InvokeRequired) { Invoke(new Action<string>(Update_4sa), value); return; }

            if ((value == "0.90") || (value == "0.900")) {
                m4deg09_radioButton.Checked = true;
            } else if ((value == "1.80") || (value == "1.800")) {
                m4deg18_radioButton.Checked = true;
            }
        }

        // =========================================================================
        // *sa setting

        private void m1deg09_radioButton_Click(object sender, EventArgs e) {
            if (m1deg09_radioButton.Checked) {
                Cnc.CNC_Write_m("{\"1sa\":0.9}");
                Thread.Sleep(50);
            } else {
                Cnc.CNC_Write_m("{\"1sa\":1.8}");
                Thread.Sleep(50);
            }
        }

        private void m1deg18_radioButton_Click(object sender, EventArgs e) {
            if (m1deg09_radioButton.Checked) {
                Cnc.CNC_Write_m("{\"1sa\":0.9}");
                Thread.Sleep(50);
            } else {
                Cnc.CNC_Write_m("{\"1sa\":1.8}");
                Thread.Sleep(50);
            }
        }


        private void m2deg09_radioButton_Click(object sender, EventArgs e) {
            if (m2deg09_radioButton.Checked) {
                Cnc.CNC_Write_m("{\"2sa\":0.9}");
                Thread.Sleep(50);
            } else {
                Cnc.CNC_Write_m("{\"2sa\":1.8}");
                Thread.Sleep(50);
            }
        }

        private void m2deg18_radioButton_Click(object sender, EventArgs e) {
            if (m2deg09_radioButton.Checked) {
                Cnc.CNC_Write_m("{\"2sa\":0.9}");
                Thread.Sleep(50);
            } else {
                Cnc.CNC_Write_m("{\"2sa\":1.8}");
                Thread.Sleep(50);
            }
        }

        private void m3deg09_radioButton_Click(object sender, EventArgs e) {
            if (m3deg09_radioButton.Checked) {
                Cnc.CNC_Write_m("{\"3sa\":0.9}");
                Thread.Sleep(50);
            } else {
                Cnc.CNC_Write_m("{\"3sa\":1.8}");
                Thread.Sleep(50);
            }
        }

        private void m3deg18_radioButton_Click(object sender, EventArgs e) {
            if (m3deg09_radioButton.Checked) {
                Cnc.CNC_Write_m("{\"3sa\":0.9}");
                Thread.Sleep(50);
            } else {
                Cnc.CNC_Write_m("{\"3sa\":1.8}");
                Thread.Sleep(50);
            }
        }

        private void m4deg09_radioButton_Click(object sender, EventArgs e) {
            if (m4deg09_radioButton.Checked) {
                Cnc.CNC_Write_m("{\"4sa\":0.9}");
                Thread.Sleep(50);
            } else {
                Cnc.CNC_Write_m("{\"4sa\":1.8}");
                Thread.Sleep(50);
            }
        }

        private void m4deg18_radioButton_Click(object sender, EventArgs e) {
            if (m4deg09_radioButton.Checked) {
                Cnc.CNC_Write_m("{\"4sa\":0.9}");
                Thread.Sleep(50);
            } else {
                Cnc.CNC_Write_m("{\"4sa\":1.8}");
                Thread.Sleep(50);
            }
        }

        #endregion

        // =========================================================================
        #region mpo
        // mpo*: Position
        // * update

        private readonly Regex numberFirstRegex = new Regex(@"^(\d)([^\d]+)$");
        private void update_field(string field, string value) {
            if (InvokeRequired) { Invoke(new Action<string, string>(update_field), value, field); return; }

            var m = numberFirstRegex.Match(field);
            if (m.Success) {// flip it if number is first
                field = m.Groups[2].ToString() + m.Groups[1].ToString();
            }

            var x = GetType().GetField(field + "_textBox", BindingFlags.NonPublic | BindingFlags.Instance);
            var y = GetType().GetField(field + "_maskedTextBox", BindingFlags.NonPublic | BindingFlags.Instance);
            if (x == null && y == null) {
                DisplayText("No _textBox or _maskedText For Field " + field,Color.Red);
                return;
            }

            if (x != null) {
                var textbox = (TextBox)x.GetValue(this);
                textbox.Text = value;
            } else {
                var textbox = (MaskedTextBox)y.GetValue(this);
                textbox.Text = value;
            }
        }


        public void UpdatePositionDisplay() {
            if (InvokeRequired) { Invoke(new Action(UpdatePositionDisplay)); return; }
            xpos_textBox.Text = Cnc.CurrentX.ToString("F3", CultureInfo.InvariantCulture);
            ypos_textBox.Text = Cnc.CurrentY.ToString("F3", CultureInfo.InvariantCulture);
            zpos_textBox.Text = Cnc.CurrentZ.ToString("F3", CultureInfo.InvariantCulture);
            apos_textBox.Text = Cnc.CurrentA.ToString("F3", CultureInfo.InvariantCulture);
        }

        #endregion

        // =========================================================================


        // =========================================================================



        private void TestX_button_Click(object sender, EventArgs e) {
            if (!Cnc.CNC_XY(0.0, Cnc.CurrentY))
                return;
            if (!Cnc.CNC_XY(Locations.GetLocation("max machine").X, Cnc.CurrentY))
                return;
            if (!Cnc.CNC_XY(0.0, Cnc.CurrentY))
                return;
        }

        private void TestY_thread() {
            if (!Cnc.CNC_XY(Cnc.CurrentX, 0))
                return;
            if (!Cnc.CNC_XY(Cnc.CurrentX, Locations.GetLocation("max machine").Y))
                return;
            if (!Cnc.CNC_XY(Cnc.CurrentX, 0))
                return;
        }

        private void TestY_button_Click(object sender, EventArgs e) {
            Thread t = new Thread(() => TestY_thread());
            t.IsBackground = true;
            t.Start();
        }

        private void TestXYA_thread() {
            if (!Cnc.CNC_XYA(0, 0, 0))
                return;
            if (!Cnc.CNC_XYA(Locations.GetLocation("max machine").X, Locations.GetLocation("max machine").Y, 360.0))
                return;
            if (!Cnc.CNC_XYA(0, 0, 0))
                return;
        }

        private void TestXYA_button_Click(object sender, EventArgs e) {
            Thread t = new Thread(() => TestXYA_thread());
            t.IsBackground = true;
            t.Start();
        }

        private void TestXY_thread() {
            if (!Cnc.CNC_XY(0, 0)) return;
            if (!Cnc.CNC_XY(Locations.GetLocation("max machine"))) return;
            if (!Cnc.CNC_XY(0, 0)) return;
        }

        private void TestXY_button_Click(object sender, EventArgs e) {
            Thread t = new Thread(() => TestXY_thread());
            t.IsBackground = true;
            t.Start();
        }

        private void TestYX_thread() {
            if (!Cnc.CNC_XY(Locations.GetLocation("max machine").X, 0)) return;
            if (!Cnc.CNC_XY(0, Locations.GetLocation("max machine").Y)) return;
            if (!Cnc.CNC_XY(Locations.GetLocation("max machine").X, 0)) return;
        }

        private void TestYX_button_Click(object sender, EventArgs e) {
            Thread t = new Thread(() => TestYX_thread());
            t.IsBackground = true;
            t.Start();
        }

        private void HomeX_button_Click(object sender, EventArgs e) {
            Cnc.CNC_Home_m("X");
        }

        private void HomeXY_button_Click(object sender, EventArgs e) {
            if (!Cnc.CNC_Home_m("X")) return;
            Cnc.CNC_Home_m("Y");
        }

        private void HomeY_button_Click(object sender, EventArgs e) {
            Cnc.CNC_Home_m("Y");
        }

        private void HomeZ_button_Click(object sender, EventArgs e) {
            Needle.ProbingMode(false);
            Cnc.CNC_Home_m("Z");
        }

        private void TestZ_thread() {
            if (!Cnc.Zup()) return;
            if (!Cnc.CNC_Z(Properties.Settings.Default.General_ZTestTravel)) return;
            if (!Cnc.Zup()) return;
        }

        private void TestZ_button_Click(object sender, EventArgs e) {
            Thread t = new Thread(() => TestZ_thread());
            t.IsBackground = true;
            t.Start();
        }


        private void TestA_thread() {
            if (!Cnc.CNC_A(0)) return;
            if (!Cnc.CNC_A(360)) return;
            if (!Cnc.CNC_A(0)) return;
        }

        private void TestA_button_Click(object sender, EventArgs e) {
            Thread t = new Thread(() => TestA_thread());
            t.IsBackground = true;
            t.Start();
        }

        private void Homebutton_Click(object sender, EventArgs e) {
            if (!Cnc.CNC_Home_m("Z")) return;
            if (!Cnc.CNC_Home_m("X")) return;
            if (!Cnc.CNC_Home_m("Y")) return;
            Cnc.CNC_A(0);
        }



        private void MotorPowerOff() { Cnc.CNC_Write_m("{\"md\":\"\"}"); }
        private void MotorPowerOn() { Cnc.CNC_Write_m("{\"me\":\"\"}"); }


        private void MotorPower_checkBox_Click(object sender, EventArgs e) {
            if (MotorPower_checkBox.Checked) {
                MotorPowerOn();
            } else {
                MotorPowerOff();
            }
        }

        private void Pump_checkBox_Click(object sender, EventArgs e) {
            if (!PumpIsOn) {
                PumpOn();
            } else {
                PumpOff();
            }
        }


        private void Vacuum_checkBox_Click(object sender, EventArgs e) {
            if (!VacuumIsOn) {
                VacuumOn();
            } else {
                VacuumOff();
            }
        }

        private void SlackCompensation_checkBox_Click(object sender, EventArgs e) {
            if (SlackCompensation_checkBox.Checked) {
                Cnc.SlackCompensation = true;
                Properties.Settings.Default.CNC_SlackCompensation = true;
            } else {
                Cnc.SlackCompensation = false;
                Properties.Settings.Default.CNC_SlackCompensation = false;
            }
        }




        private readonly string[] defaultSettings = {
            "{\"st\":0}", "{\"mt\":300}", "{\"jv\":3}", "{\"tv\":1}", "{\"qv\":2}",
            "{\"sv\":1}", "{\"si\":200}", "{\"ec\":0}", "{\"ee\":0}", "{\"gun\":1}", "{\"1ma\":0}", "{\"1sa\":0.9}",
            "{\"1tr\":40.0}", "{\"1mi\":8}", "{\"1po\":0}", "{\"1pm\":2}", "{\"2ma\":1}", "{\"2sa\":0.9}",
            "{\"2tr\":40.0}", "{\"2mi\":8}", "{\"2po\":0}", "{\"2pm\":2}", "{\"3ma\":2}", "{\"3sa\":1.8}",
            "{\"3tr\":8.0}", "{\"3mi\":8}", "{\"3po\":0}", "{\"3pm\":2}", "{\"4ma\":3}", "{\"4sa\":0.9}",
            "{\"4tr\":160.0}", "{\"4mi\":8}", "{\"4po\":0}", "{\"4pm\":2}", "{\"xam\":1}", "{\"xvm\":10000}",
            "{\"xfr\":10000}", "{\"xtm\":600}", "{\"xjm\":1000}", "{\"xjh\":2000}", "{\"xjd\":0.01}", "{\"xsn\":0}",
            "{\"xsx\":0}", "{\"xsv\":2000}", "{\"xlv\":100}", "{\"xlb\":8}", "{\"xzb\":2}", "{\"yam\":1}",
            "{\"yvm\":10000}", "{\"yfr\":10000}", "{\"ytm\":400}", "{\"yjm\":1000}", "{\"yjh\":2000}", "{\"yjd\":0.01}",
            "{\"ysn\":0}", "{\"ysx\":0}", "{\"ysv\":2000}", "{\"ylv\":100}", "{\"ylb\":8}", "{\"yzb\":2}", "{\"zam\":1}",
            "{\"zvm\":5000}", "{\"zfr\":2000}", "{\"ztm\":80}", "{\"zjm\":500}", "{\"zjh\":500}", "{\"zjd\":0.01}",
            "{\"zsn\":0}", "{\"zsx\":0}", "{\"zsv\":1000}", "{\"zlv\":100}", "{\"zlb\":4}", "{\"zzb\":2}", "{\"aam\":1}",
            "{\"avm\":50000}", "{\"afr\":200000}", "{\"atm\":400}", "{\"ajm\":5000}", "{\"ajh\":5000}", "{\"ajd\":0.01}",
            "{\"asn\":0}", "{\"asx\":0}"
        };



        private void BuiltInSettings() {
            foreach (var cmd in defaultSettings) {
                Cnc.CNC_Write_m(cmd); // disable switches, no homing an A
                Thread.Sleep(50);
            }
            Cnc.CNC_RawWrite("{\"gc\":\"f2000\"}");
            UpdateWindowValues_m();
        }


        private static int SetProbing_stage = 0;
        private double _probeZ;
        private void SetProbing_button_Click(object sender, EventArgs e) {
            switch (SetProbing_stage) {
                case 0:
                    instructions_label.Text = "Ensure 'Needle Zeroing' Position is set. Nothing should be here.  Select Next when ready";
                    instructions_label.Visible = true;
                    Cnc.CNC_Home_m("Z");
                    SetProbing_button.Text = "Next";
                    SetProbing_button.BackColor = Color.Yellow;
                    SetProbing_stage++;
                    break;
                case 1:
                    Global.GoTo("Needle Zeroing");
                    Properties.Settings.Default.ZDistanceToTable = Needle.ProbeDistance(); //distance to bottom
                    instructions_label.Text = "Place a piece of paper underneath the needle and select Next";
                    SetProbing_stage++;
                    break;
                case 2:
                    Needle.ProbeDown();
                    Needle.ProbingMode(true);
                    _probeZ = Cnc.CurrentZ; //paper location
                    instructions_label.Text = "Jog Z axis until the paper can move a little then click Next";
                    SetProbing_stage++;
                    break;
                case 3:
                    Properties.Settings.Default.General_ProbingBackOff = _probeZ - Cnc.CurrentZ; //backoff
                    Properties.Settings.Default.ZDistanceToTable -= Properties.Settings.Default.General_ProbingBackOff; //the base of the build platform
                    Needle.ProbingMode(false);
                    Cnc.Zup();
                    SetProbing_button.Text = "Backoff";
                    SetProbing_button.BackColor = ChangeNeedle_button.BackColor; //dunno what the color is supposed to be so copying it
                    instructions_label.Text = "";
                    instructions_label.Visible = false;
                    ShowMessageBox(
                       "Probing Backoff set successfully.\n" +
                            "Table surface: " + Properties.Settings.Default.ZDistanceToTable.ToString("0.00", CultureInfo.InvariantCulture) +
                            "\nBackoff:  " + Properties.Settings.Default.General_ProbingBackOff.ToString("0.00", CultureInfo.InvariantCulture),
                       "Done",
                       MessageBoxButtons.OK);
                    SetProbing_stage = 0;
                    Z0toPCB_BasicTab_label.Text = Properties.Settings.Default.ZDistanceToTable.ToString("0.00", CultureInfo.InvariantCulture);
                    Z_Backoff_label.Text = Properties.Settings.Default.General_ProbingBackOff.ToString("0.00", CultureInfo.InvariantCulture);
                    Properties.Settings.Default.Save();
                    break;
            }
        }

        private void needleHeight_button_Click(object sender, EventArgs e) {
            switch (SetProbing_stage) {
                case 0:
                    instructions_label.Text = "Ensure 'Needle Zeroing' Position is set. Nothing should be here.  Select Next when ready";
                    instructions_label.Visible = true;
                    Cnc.CNC_Home_m("Z");
                    needleHeight_button.Text = "Next";
                    needleHeight_button.BackColor = Color.Yellow;
                    SetProbing_stage++;
                    break;
                case 1:
                    Global.GoTo("Needle Zeroing");
                    Properties.Settings.Default.ZDistanceToTable = Needle.ProbeDistance() - Properties.Settings.Default.General_ProbingBackOff;
                    Properties.Settings.Default.Save();
                    needleHeight_button.Text = "Needle Height";
                    needleHeight_button.BackColor = ChangeNeedle_button.BackColor; //dunno what the color is supposed to be so copying it
                    instructions_label.Text = "";
                    instructions_label.Visible = false;
                    SetProbing_stage = 0;
                    Z0toPCB_BasicTab_label.Text = Properties.Settings.Default.ZDistanceToTable.ToString("0.00", CultureInfo.InvariantCulture);
                    break;
            }
        }


        private void pcbThickness_button_Click(object sender, EventArgs e) {
            switch (SetProbing_stage) {
                case 0:
                    instructions_label.Text = "Place PCB Under needle.  Select Next when ready";
                    instructions_label.Visible = true;
                    pcbThickness_button.Text = "Next";
                    pcbThickness_button.BackColor = Color.Yellow;
                    SetProbing_stage++;
                    break;
                case 1:
                    Global.GoTo("Needle Zeroing");
                    Properties.Settings.Default.pcb_thickness = Properties.Settings.Default.ZDistanceToTable -
                                                                Needle.ProbeDistance() +
                                                                Properties.Settings.Default.General_ProbingBackOff;
                    Properties.Settings.Default.Save();
                    pcbThickness_button.Text = "PCB Thickness";
                    pcbThickness_button.BackColor = ChangeNeedle_button.BackColor; //dunno what the color is supposed to be so copying it
                    instructions_label.Text = "";
                    instructions_label.Visible = false;
                    SetProbing_stage = 0;
                    pcbThickness_label.Text = Properties.Settings.Default.pcb_thickness.ToString("0.00", CultureInfo.InvariantCulture);
                    break;
            }
        }

        // =================================================================================
        // Run job page functions
        // =================================================================================
        #region Job page functions
        private PartLocation JobOffset = new PartLocation(); //values on the form
        private PartLocation PCBZeroLocation { get { return Locations.GetLocation("PCB Zero"); } }






        // =================================================================================
        // CAD data and Job data load and save functions
        // =================================================================================
        private string CadDataFileName = "";
        private string JobFileName = "";




        private bool LoadCadData_m() {
            String[] AllLines;

            // read in CAD data (.csv file)
            if (CAD_openFileDialog.ShowDialog() == DialogResult.OK) {
                try {
                    bool result;
                    CadDataFileName = CAD_openFileDialog.FileName;
                    AllLines = File.ReadAllLines(CadDataFileName);
                    if (Path.GetExtension(CAD_openFileDialog.FileName) == ".pos") {
                        result = Cad.ParseKiCadData_m(AllLines);
                    } else {
                        result = Cad.ParseCadData_m(AllLines, false);
                    }
                    return result;
                } catch (Exception ex) {
                    Cursor.Current = Cursors.Default;
                    ShowMessageBox(
                        "Error in file, Msg: " + ex.Message,
                        "Can't read CAD file",
                        MessageBoxButtons.OK);
                    CadData_GridView.Rows.Clear();
                    CadDataFileName = "--";
                    return false;
                }
            }
            return false;
        }

        // =================================================================================
        private bool LoadJobData_m(string filename) {
            try {
                Cad.JobData.Clear();
                Cad.JobData.AddRange(Global.DeSerialization<SortableBindingList<JobData>>(filename));
                JobData_GridView.DataSource = Cad.JobData;
            } catch (Exception ex) {
                ShowSimpleMessageBox("Can't Load Job File " + Path.GetFileName(filename) + " : " + ex);
                JobData_GridView.Rows.Clear();
                CadDataFileName = "--";
                return false;
            };
            return true;
        }



        public int GetGridRow(DataGridView grid) {
            return (grid.CurrentCell != null) ? grid.CurrentCell.RowIndex : -1;
        }

        private void Up_button_Click(object sender, EventArgs e) {
            var row = GetGridRow(JobData_GridView);
            if (row <= 0) return;
            Global.MoveItem(Cad.JobData, row, -1);
            JobData_GridView.CurrentCell = JobData_GridView[0, row - 1];
        }

        private void Down_button_Click(object sender, EventArgs e) {
            var row = GetGridRow(JobData_GridView);
            if (row >= JobData_GridView.Rows.Count - 1) return;
            if (row != -1) {
                Global.MoveItem(Cad.JobData, row, +1);
                JobData_GridView.CurrentCell = JobData_GridView[0, row + 1];
            }
        }

        private void DeleteComponentGroup_button_Click(object sender, EventArgs e) {
            if (JobData_GridView.Rows.Count > 0) {
                var row = GetGridRow(JobData_GridView);
                if (row != -1)  Cad.JobData.RemoveAt(row);
            }
        }

        private void NewRow_button_Click(object sender, EventArgs e) {
            int row = GetGridRow(JobData_GridView);
            if (row != -1) Cad.JobData.Insert(row, new JobData());
            else Cad.JobData.Add(new JobData());
        }

        // =================================================================================
        // JobData editing
        // =================================================================================
        private void JobData_GridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (JobData_GridView.CurrentCell.ColumnIndex == 4) {
                // For method parameter, show the tape selection form if method is "place" 
                int row = JobData_GridView.CurrentCell.RowIndex;
                var x = JobData_GridView.Rows[row].Cells[3].Value;
                if (x != null && x.Equals("Place")) {
                    JobData_GridView.Rows[row].Cells[4].Value = SelectTape("Select tape for ");
                    //+ JobData_GridView.Rows[row].Cells["ComponentType"].Value.ToString());
                    Update_GridView(JobData_GridView);
                }
            }
        }


        /*  // If components are edited, update count automatically
          private void JobData_GridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
          {
              if (JobData_GridView.CurrentCell.ColumnIndex == 4)
              {
                  // components
                  List<String> Line = CAD.SplitCSV(JobData_GridView.CurrentCell.Value.ToString(), ',');
                  int row = JobData_GridView.CurrentCell.RowIndex;
                  JobData_GridView.Rows[row].Cells["ComponentCount"].Value = Line.Count.ToString();
                  Update_GridView(JobData_GridView);
              }
          }
          */

        // =================================================================================
        // Do someting to a group of components:
        // =================================================================================
        // Several rows are selected at Job data:

        private void PlaceThese_button_Click(object sender, EventArgs e) {
            int selectedCount = JobData_GridView.SelectedCells.Count;
            if (selectedCount == 0) {
                CleanupPlacement(false);
                ShowSimpleMessageBox("Nothing selected");
                return;
            }

            if (!PrepareToPlace_m()) {
                ShowSimpleMessageBox("Placement operation failed, nothing done.");
                return;
            }


            List<PhysicalComponent> toPlace = new List<PhysicalComponent>();
            for (int i = 0; i < selectedCount; i++) {
                var job = (JobData)JobData_GridView.SelectedCells[i].OwningRow.DataBoundItem;
                foreach (var component in job.GetComponents()) {
                    component.JobData = job;
                    if (!toPlace.Contains(component)) toPlace.Add(component);
                }
            }

            if (!PlaceComponents(toPlace)) {
                ShowSimpleMessageBox("Placement operation failed. Review job status.");
                CleanupPlacement(false);
                return;
            }

            CleanupPlacement(false);
        }


        private List<JobData> GetSelectedJobs() {
            int selectedCount = JobData_GridView.SelectedCells.Count;
            List<JobData> list = new List<JobData>();
            for (int i = 0; i < selectedCount; i++) {
                var job = (JobData)JobData_GridView.SelectedCells[i].OwningRow.DataBoundItem;
                if (!list.Contains(job)) list.Add(job);
            }
            return list;
        }


     /*   // =================================================================================
        // This routine places selected component(s) from CAD data grid view:
        private void PlaceOne_button_Click(object sender, EventArgs e) {
            var selectedCount = CadData_GridView.SelectedCells.Count;
            if (selectedCount == 0) {
                CleanupPlacement(false);
                ShowSimpleMessageBox("Nothing selected");
                return;
            }

            if (!PrepareToPlace_m()) {
                ShowSimpleMessageBox("Placement operation failed, nothing done.");
                return;
            }
            List<PhysicalComponent> toPlace = new List<PhysicalComponent>();
            for (int i = 0; i < selectedCount; i++) {
                var component = (PhysicalComponent)CadData_GridView.SelectedCells[i].OwningRow.DataBoundItem;
                if (component.JobData == null) {
                    component.JobData = Cad.FindJobDataThatContainsComponent(component);
                    if (component.JobData == null) {
                        ShowSimpleMessageBox("No Job Action Found That Matches This Component - Job File Corrupt");
                        CleanupPlacement(false);
                        return;
                    }
                }
                if (!toPlace.Contains(component)) toPlace.Add(component);
            }

            if (!PlaceComponents(toPlace)) {
                ShowSimpleMessageBox("Placement operation failed. Review job status.");
                CleanupPlacement(false);
                return;
            }

            CleanupPlacement(true);

        }*/

        // =================================================================================
        // This routine places the [index] row from Job data grid view:
        private bool PlaceComponents(List<PhysicalComponent> components) {
            DisplayText("Placing " + string.Join(",", components.Select(x => x.Designator)));

            foreach (var component in components) {
                if (component.JobData.Method == null || component.JobData.Method.Equals("?")) {
                    MethodSelectionForm MethodDialog = new MethodSelectionForm();
                    MethodDialog.ShowCheckBox = true;
                    // MethodDialog.HeaderString = CurrentGroup_label.Text;
                    MethodDialog.ShowDialog(this);

                    switch (MethodDialog.SelectedMethod) {
                        case "Place":
                        case "Place Fast":
                            var NewID = SelectTape("Select tape for " + component.TypePlusFootprint);
                            component.Method = MethodDialog.SelectedMethod;
                            component.MethodParameters = NewID;
                            break;
                        case "": //user pressed x
                            return false;
                        case "none":
                            break;
                        case "Ignore":
                            component.Method = "Ignore";
                            component.MethodParameters = "";
                            break;
                        case "Abort":
                            AbortPlacement = true;
                            component.Method = "Ignore";
                            component.MethodParameters = "";
                            break;
                        default:
                            component.JobData.Method = MethodDialog.SelectedMethod;
                            break;
                    }
                    MethodDialog.Dispose();
                }
            }

            foreach (var component in components) {
                if (AbortPlacement) { return false; }
                if (!PlaceComponent(component)) 
                    if (!ignoreErrors_checkbox.Checked) return false;
            }
            return true;
        }


        // =================================================================================
        // All rows:
        private void PlaceAll_button_Click(object sender, EventArgs e) {
            JobData_GridView.SelectAll();
            PlaceThese_button_Click(null, null);
        }


        // =================================================================================
        // PlaceComponent_m()
        // This routine does the actual placement of a single component.
        // Component is the component name (Such as R15); based to this, we'll find the coordinates from CAD data
        // GroupRow is the row index to Job data grid view.
        // =================================================================================

        private bool PlaceComponent(PhysicalComponent comp) {
            DisplayText("PlaceComponent_m: Component: " + comp.Designator);
            if (comp.IsFiducial) return true; //skip fiducials
            

            if ((comp.Method == "LoosePart") || (comp.Method == "Place") || (comp.Method == "Place Fast")) {
                PlacedComponent_label.Text = comp.Designator;
                PlacedComponent_label.Update();
                PlacedValue_label.Text = comp.TypePlusFootprint;
                PlacedValue_label.Update();
                var tapeObj = Tapes.GetTapeObjByID(comp.MethodParameters);
                if (tapeObj != null && !string.IsNullOrEmpty(tapeObj.TemplateFilename)) {
                    var image = Image.FromFile(tapeObj.TemplateFilename);
                    placement_Picturebox.Image = image;
                    placement_Picturebox.Visible = true;
                } else {
                    placement_Picturebox.Image = null;
                    placement_Picturebox.Visible = false;
                }
                
            };

            if (AbortPlacement) return false;

            // Component is at CadData_GridView.Rows[CADdataRow]. 
            // What to do to it is at  JobData_GridView.Rows[GroupRow].

            switch (comp.Method) {
                case "?": 
                    ShowMessageBox(
                        "Method is ? at run time",
                        "Sloppy programmer error",
                        MessageBoxButtons.OK);
                    return false;

                case "Pause":
                    ShowMessageBox(
                        "Job pause, click OK to continue.",
                        "Pause",
                        MessageBoxButtons.OK);
                    return true;

                case "LoosePart":
                    if (comp.IsPlaced && skippedPlacedComponents_checkBox.Checked) return true;
                    if (!PlacePart_m(true, comp.IsFirstInRow, comp)) {
                        comp.IsError = true;
                        return false;
                    }
                    comp.IsPlaced = true;
                    break;
                case "Place Fast":
                case "Place":
                    if (comp.IsPlaced && skippedPlacedComponents_checkBox.Checked) return true;
                    if (!PlacePart_m(false, comp.IsFirstInRow, comp)) {
                        comp.IsError = true;
                        return false;
                    }
                    comp.IsPlaced = true;
                    break;

                case "Change needle":
                    if (!ChangeNeedle_m()) return false;
                    break;

                case "Recalibrate":
                    if (!PrepareToPlace_m()) return false;
                    break;

                case "Ignore":
                    return true;

                case "Fiducials":
                    return true;

                default:
                    ShowSimpleMessageBox("No way to handle method " + comp.Method);
                    return false;
            }

            return true;
        }

        private bool _isMeasurementValid = false;
        private bool IsMeasurementValid {
            get { return _isMeasurementValid; }
            set {
                _isMeasurementValid = value;
                ReMeasure_button.BackColor = (value) ? Color.Green : Color.Red;
            }
        }


        // =================================================================================
        private bool ChangeNeedle_m() {
            Cnc.CNC_Write_m("{\"zsn\":0}");
            Cnc.CNC_Write_m("{\"zsx\":0}");
            Thread.Sleep(50);
            PumpOff();
            MotorPowerOff();
            ShowMessageBox(
                "Change Needle now, press OK when done",
                "Needle change pause",
                MessageBoxButtons.OK);
            MotorPowerOn();
            Zlim_checkBox.Checked = true;
            Zhome_checkBox.Checked = true;
            Needle.Calibrated = false;
            IsMeasurementValid = false;
            Cnc.CNC_Write_m("{\"zsn\":3}");
            Cnc.CNC_Write_m("{\"zsx\":2}");
            if (!MechanicalHoming_m()) {
                return false;
            }
            if (!OpticalHoming_m()) {
                return false;
            }
            if (!CalibrateNeedle_m()) {
                return false;
            }
            if (!BuildMachineCoordinateData_m()) {
                return false;
            }
            PumpOn();
            return true;
        }

        // =================================================================================
        // This is called before any placement is done:
        // =================================================================================
        private bool PrepareToPlace_m() {
            if (Cad.JobData.Count == 0) {
                ShowMessageBox(
                    "No Job loaded.",
                    "No Job",
                    MessageBoxButtons.OK
                );
                return false;
            }

            if (!Needle.Calibrated && !CalibrateNeedle_m()) return false;
            if (!BuildMachineCoordinateData_m()) return false;


            AbortPlacement = false;
            PlaceAll_button.Capture = false;
            JobData_GridView.ReadOnly = true;
            PumpOn();
            return true;
        }  // end PrepareToPlace_m

        // =================================================================================
        // This cleans up the UI after placement operations
        // =================================================================================
        private void CleanupPlacement(bool success) {
            PlacedComponent_label.Text = "--";
            PlacedValue_label.Text = "--";

            JobData_GridView.ReadOnly = false;
            PumpDefaultSetting();
            VacuumDefaultSetting();
            if (success) Global.GoTo("Park");
        }


        // =================================================================================
        // PickUpPart_m(): Picks next part from the tape

        private bool PickUpPart_m(string id) {
            var tape = Tapes.GetTapeObjByID(id);
            if (tape == null) return false;
            return PickUpPart_m(tape);
        }


        private bool PickUpPart_m(TapeObj tape) {
            var TapeID = tape.ID;
            DisplayText("PickUpPart_m(), tape id: " + TapeID);
         
            // Go to part location:
            PumpOn();
            VacuumOff();
            if (!Tapes.NeedleToNextPart_m(tape)) return false;

            //test vaccume pressure
            int p1 = 0;
            if (PressureSensorPresent) {
                VacuumOn();
                Thread.Sleep(1000);
                p1 = Cnc.GetADC();
                p1_label.Text = p1.ToString();
                VacuumOff();
                DisplayText("Vacuum Pressure W/O Part = " + p1);
            }

            // Pick it up:
            if (!tape.IsPickupZSet) {
                if (!Needle.ProbeDown()) return false;
                tape.PickupZ = Cnc.CurrentZ - Properties.Settings.Default.General_ProbingBackOff;
                DisplayText("PickUpPart_m(): Probing Z= " + Cnc.CurrentZ);
            } else {
                double Z = tape.PickupZ; //not sure why the .5 is there - increased pressure?
                DisplayText("PickUpPart_m(): Part pickup, Z" + Z);
                if (!Cnc.CNC_Z(Z)) return false;
            }

            VacuumOn();
            DisplayText("PickUpPart_m(): needle up");
            if (!Cnc.Zup()) return false;

            if (PressureSensorPresent) {
                int p2 = Cnc.GetADC();
                p2_label.Text = p2.ToString() + " (Delta = " + (p1 - p2) + ")";
                DisplayText("Vacuum Pressure W/  Part = " + p2 + "  (" + Math.Abs(p1 - p2) + ") change");
                if (Math.Abs(p1 - p2) < setting.vacuumDeltaExpected) {
                    //pickup failed - try again
                    Tapes.PickupFailed(TapeID);
                    DisplayText("Pressure sensor detected failed pickup", Color.Red);
                    VacuumOff();
                    PumpOff();
                    tape.PickupZ = -1; //reset pickupZ 
                    return false;
                }
            }

            return true;
        }

        // =================================================================================
        // PutPartDown_m(): Puts part down at this position. 
        // If placement Z isn't known already, updates the tape info.
        private bool PutPartDown_m(string TapeID) {
            var tape = Tapes.GetTapeObjByID(TapeID);
            if (tape == null) return false;

            if (!tape.IsPlaceZSet) {
                DisplayText("PutPartDown_m(): Probing placement Z");
                if (!Needle.ProbeDown()) return false;
                tape.PlaceZ = Cnc.CurrentZ - Properties.Settings.Default.General_ProbingBackOff;
                DisplayText("PutPartDown_m(): placement Z= " + Cnc.CurrentZ);
            } else {
                double Z = tape.PlaceZ;
                DisplayText("PlacePart_m(): Part down, Z" + Z);
                if (!Cnc.CNC_Z(Z)) return false;
            }

            DisplayText("PlacePart_m(): Needle up.");
            VacuumOff();
            if (!Cnc.Zup()) return false;
            return true;
        }

        // =================================================================================
        // 
        private bool PutLoosePartDown_m(bool Probe) {
            if (Probe) {
                DisplayText("PutLoosePartDown_m(): Probing placement Z");
                if (!Needle.ProbeDown()) {
                    return false;
                }
                LoosePartPlaceZ = Cnc.CurrentZ;
                DisplayText("PutLoosePartDown_m(): placement Z= " + Cnc.CurrentZ);
            } else {
                if (!Cnc.CNC_Z(LoosePartPlaceZ)) {
                    return false;
                }
            }
            DisplayText("PutLoosePartDown_m(): Needle up.");
            VacuumOff();
            if (!Cnc.Zup())  // back up
            {
                return false;
            }
            return true;
        }
        // =================================================================================
        // Actual placement 
        // =================================================================================
        private double LoosePartPickupZ;
        private double LoosePartPlaceZ;

        private bool PickUpLoosePart_m(bool Probe, PhysicalComponent comp) {
            if (!Cnc.CNC_XY(Locations.GetLocation("Pickup"))) {
                return false;
            }

            // ask for it
            string ComponentType = comp.TypePlusFootprint;
            DialogResult dialogResult = ShowMessageBox(
                "Put one " + ComponentType + " to the pickup location.",
                "Placing " + comp.Designator,
                MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.Cancel) {
                return false;
            }

            // Find component
            double X = 0;
            double Y = 0;
            double A = 0.0;
            cameraView.SetDownCameraFunctionSet("component");
            // If we don't get a look from straight up (more than 2mm off) we need to re-measure
            for (int i = 0; i < 2; i++) {
                // measure 5 averages, component must be 8.0mm from its place
                int count = VideoDetection.MeasureClosestComponentInPx(out X, out Y, out A, cameraView.downVideoProcessing, (8.0 / Properties.Settings.Default.DownCam_XmmPerPixel), 5);
                if (count == 0) {
                    ShowMessageBox(
                        "Could not see component",
                        "No component",
                        MessageBoxButtons.OK);
                    return false;
                }
                X = X * Properties.Settings.Default.DownCam_XmmPerPixel;
                Y = -Y * Properties.Settings.Default.DownCam_YmmPerPixel;
                DisplayText("PickUpLoosePart_m(): measurement " + i + ", X: " + X + ", Y: " + Y + ", A: " + A);
                if ((Math.Abs(X) < 2.0) && (Math.Abs(Y) < 2.0)) {
                    break;
                }
                if (!Cnc.CNC_XY(Cnc.CurrentX + X, Cnc.CurrentY + Y)) {
                    return false;
                }
            }
            Needle.Move_m(Cnc.CurrentX + X, Cnc.CurrentY + Y, A);
            // pick it up
            if (Probe) {
                DisplayText("PickUpLoosePart_m(): Probing pickup Z");
                if (!Needle.ProbeDown()) {
                    return false;
                }
                LoosePartPickupZ = Cnc.CurrentZ;
                DisplayText("PickUpLoosePart_m(): Probing Z= " + Cnc.CurrentZ);
            } else {
                DisplayText("PickUpLoosePart_m(): Part pickup, Z" + LoosePartPickupZ);
                if (!Cnc.CNC_Z(LoosePartPickupZ)) {
                    return false;
                }
            }
            VacuumOn();
            DisplayText("PickUpLoosePart_m(): needle up");
            if (!Cnc.Zup()) {
                return false;
            }
            if (AbortPlacement) {
                return false;
            }
            return true;
        }
        
        private const int SamePartRetryCount = 3;
        private const int NextPartRetryCount = 3;

        private bool PlacePart_m(bool LoosePart, bool resetTapeZ, PhysicalComponent comp) {
            if (AbortPlacement) return false;
            if (resetTapeZ && !LoosePart && !Tapes.ClearHeights_m(comp.MethodParameters)) return false;

            // Pickup:
            if (AbortPlacement) return false;
            if (LoosePart && !PickUpLoosePart_m(resetTapeZ, comp)) return false;

            bool win = false;
            var to = Tapes.GetTapeObjByID(comp.MethodParameters);
            if (to == null) return false;

            int startingPart = to.CurrentPart;
            for (int i = 0; i < NextPartRetryCount; i++) {
                for (int j = 0; j < SamePartRetryCount; j++) {
                    if (AbortPlacement) return false;
                    win = PickUpPart_m(comp.MethodParameters);
                    if (win) break;
                }
                if (win) break;
                Tapes.GetTapeObjByID(comp.MethodParameters).IncrementPartNumber(); //try skipping a part
            }
            if (!win) {
                to.CurrentPart = startingPart; //didn't pick up anything so don't modify next part
                return false;
            }

            // Take the part to position:
            if (AbortPlacement) return false;
            DisplayText("PlacePart_m: " + comp.Designator + " At" + comp.machine, Color.Blue);
            if (!Needle.Move_m(comp.machine)) return false;

            // Place it:
            if (AbortPlacement) return false;
            if (LoosePart && !PutLoosePartDown_m(resetTapeZ)) return false;
            if (!PutPartDown_m(comp.MethodParameters)) return false;

            return true;
        }



      
        // =================================================================================
        // MeasureFiducial_m():
        // Takes the parameter nominal location and measures its physical location.
        // Assumes measurement parameters already set.

        private bool MeasureFiducial_m(ref PhysicalComponent fid) {
            Cnc.CNC_XY(fid.nominal + JobOffset + PCBZeroLocation);

            var type = Properties.Settings.Default.use_template ? Shapes.ShapeTypes.Fiducial : Shapes.ShapeTypes.Circle;
            var loc = FindPositionAndMoveToClosest(type, 3, .1);
            if (loc == null) {
                ShowSimpleMessageBox("Can't Find Fiducial " + fid.Designator);
                return false;
            }
            fid.machine = loc;
            return true;
        }

        // =================================================================================
        // BuildMachineCoordinateData_m():
        private bool BuildMachineCoordinateData_m() {
            if (IsMeasurementValid) return true;

            // Get ready for position measurements
            DisplayText("SetFiducialsMeasurement");
            cameraView.SetDownCameraFunctionSet("fiducial");

            // populate fiducal data
            PhysicalComponent[] Fiducials = Cad.ComponentData.Where(x => x.IsFiducial).ToArray();
            if (Fiducials.Length < 2)
            {
                ShowSimpleMessageBox("Only " + Fiducials.Length + " fiducials set - not able to calibrate machine coordinates");
                return false;
            }

            if (!Cnc.Simulation)
            {
                // measure the actual data
                for (int i = 0; i < Fiducials.Length; i++)
                {
                    if (!MeasureFiducial_m(ref Fiducials[i])) return false;
                }
            }
            else {
                Fiducials[0].X_machine = 296.83;
                Fiducials[0].Y_machine = 165.97;
                Fiducials[1].X_machine = 57.23;
                Fiducials[1].Y_machine = 165.17;
                Fiducials[2].X_machine = 57.44;
                Fiducials[2].Y_machine = 75.05;
                Fiducials[3].X_machine = 297.21;
                Fiducials[3].Y_machine = 75.65;
            }

            // Find the homographic tranformation from CAD data (fiducials.nominal) to measured machine coordinates
            // (fiducials.machine):

            // RN - setup lists of points and do a least squares fit of a afine transform
            List<PartLocation> nominalLocations = new List<PartLocation>(Fiducials.Length);
            List<PartLocation> measuredLocations = new List<PartLocation>(Fiducials.Length);
            foreach (PhysicalComponent t in Fiducials) {
                nominalLocations.Add(t.nominal);
                measuredLocations.Add(t.machine);
            }
            LeastSquaresMapping lsm = new LeastSquaresMapping(nominalLocations, measuredLocations);
            PCBOffset_label.Text = lsm.ToString();
            var error = lsm.RMSError();
            //    if (error > 1) { //some arbitrary thershold
            //       ShowSimpleMessageBox("Fiducial fit high RMS diff value - aborting (" + error + ")");
            //       return false;
            //    }


            // verify that no fiducial moved more then 0.4mm
            if (lsm.MaxFiducialMovement() > 0.4) {
                DisplayText(" ** A fiducial moved more than 0.4mm from its measured location");
                DisplayText(" ** when applied the same calculations than regular componets.");
                DisplayText(" ** (Maybe the camera picked a via instead of a fiducial?)");
                DisplayText(" ** Placement data is likely not good.");
                DialogResult dialogResult = ShowMessageBox(
                    "Nominal to machine trasnformation seems to be off. (See log window)",
                    "Cancel operation?",
                    MessageBoxButtons.OKCancel
                );
                if (dialogResult == DialogResult.Cancel) return false;
            }


            //apply mapping
            foreach (var component in Cad.ComponentData.Where(x => !x.IsFiducial).ToArray()) {
                component.machine = lsm.Map2(component.nominal);
            }

            // Refresh UI:
            Update_GridView(CadData_GridView);

            // Done! 
            IsMeasurementValid = true;
            cameraView.DownCameraReset();
            return true;
        }// end BuildMachineCoordinateData_m

        // =================================================================================
        // BuildMachineCoordinateData_m functions end
        // =================================================================================


        // =================================================================================
        private void PausePlacement_button_Click(object sender, EventArgs e) {
            DialogResult dialogResult = ShowMessageBox(
                "Placement Paused. Continue? (Cancel aborts)",
                "Placement Paused",
                MessageBoxButtons.OKCancel
            );
            if (dialogResult == DialogResult.Cancel) {
                AbortPlacement = true;
            }

        }

        // =================================================================================
        private void AbortPlacement_button_Click(object sender, EventArgs e) {
            var x = (Button)sender;
            AbortPlacement = !AbortPlacement;
            x.BackColor = (AbortPlacement) ? Color.Orange : Color.Red;
            x.Text = (AbortPlacement) ? "Restart" : "Stop";
        }
        

        // =================================================================================
        // Checks what is needed to check before doing something for a single component selected at "CAD data" table. 
        // If succesful, sets X, Y to component machine coordinates.
        private PartLocation GetSelectComponentFromCadData() {
            DataGridViewCell cell = CadData_GridView.CurrentCell;
            if (cell == null) return null;

            PhysicalComponent component = (PhysicalComponent)cell.OwningRow.DataBoundItem;
            if (component.X_machine == -1) {
                DialogResult dialogResult = ShowMessageBox(
                    "Component locations not yet measured. Measure now?",
                    "Measure now?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No) return null;
                if (!BuildMachineCoordinateData_m()) return null;
            }
            return component.machine;
        }

        // =================================================================================
        private void ShowMachine_button_Click(object sender, EventArgs e) {
            var part = GetSelectComponentFromCadData();
            if (part != null) Cnc.CNC_XY(part);
        }


        // =================================================================================
        private void ReMeasure_button_Click(object sender, EventArgs e) {
            IsMeasurementValid = false;
            ReMeasure_button.BackColor = Color.Yellow;
            IsMeasurementValid = BuildMachineCoordinateData_m();
        }



        #endregion


        // =================================================================================
        // Test functions
        // =================================================================================
        #region test functions

        // =================================================================================

        // test 1

        private void pickupThis_buttonClick(object sender, EventArgs e) {
            double Xmark = Cnc.CurrentX;
            double Ymark = Cnc.CurrentY;
            DisplayText("test 1: Pick up this (probing)");
            PumpOn();
            VacuumOff();
            if (!Needle.Move_m(Cnc.XYALocation)) {
                PumpOff();
                return;
            }
            Needle.ProbeDown();

            double tempZ = Cnc.CurrentZ;
            Cnc.CNC_Z(tempZ - 10);
            Cnc.CNC_Z(tempZ);
            
            VacuumOn();
            Cnc.Zup();  // pick up
            Cnc.CNC_XY(Xmark, Ymark);
        }

        // =================================================================================
        // test 2

        // static int test2_state = 0;
        private void placeHere_buttonClick(object sender, EventArgs e) {
            var loc = Cnc.XYALocation;
            if (!Needle.Move_m(loc)) return;
            Needle.ProbeDown();

            double dX;
            double dY;
            if (Needle.CorrectedPosition_m(Cnc.CurrentA, out dX, out dY))
            {
                double tempZ = Cnc.CurrentZ;
                //Cnc.CNC_Z(tempZ - 10);
                Cnc.CNC_XY(loc.X + Needle.NeedleOffset.X - dX, loc.Y + Needle.NeedleOffset.Y - dY);
                Cnc.CNC_Z(tempZ);
            };
            VacuumOff();
            Cnc.Zup();  // back up
            Cnc.CNC_XY(loc);  // show results
        }






        #endregion


        public void ZDown_button_Click(object sender, EventArgs e) {
            Cnc.Zdown();
        }

        public void ZUp_button_Click(object sender, EventArgs e) {
            Cnc.Zup();
        }

        private void needleToFocus_button_Click(object sender, EventArgs e) {
            var p = Locations.GetLocation("Up Camera");
            p.A = 0;
            Cnc.CNC_XYA(p);
            Cnc.Zdown(setting.focus_height, false);
        }

        private void needleToPCBHeight_Click(object sender, EventArgs e) {
            Cnc.Zdown(setting.pcb_thickness, false);
        }


   

        // =================================================================================

        private void AddTape_button_Click(object sender, EventArgs e) {
            Tapes.AddTapeObject(0);
        }

        private void SmallMovement_numericUpDown_ValueChanged(object sender, EventArgs e) {
            Properties.Settings.Default.CNC_SmallMovementSpeed = SmallMovement_numericUpDown.Value;
        }







        // ==========================================================================================================

        // ==========================================================================================================
        // SelectTape(): 
        // Displays a dialog with buttons for all defined tapes, returns the ID of the tape.
        // used in placement when the user selects the tape to be used in runtime.
        // In this case, we remove the regular Tapes_dataGridView_CellClick() handler; the TapeDialog
        // changes the button text to "select" (and back to "reset" on return) and uses its own handler.
        private string SelectTape(string header) {
            Point loc = Tapes_dataGridView.Location;
            Size size = Tapes_dataGridView.Size;
            DataGridView Grid = Tapes_dataGridView;
            TapeSelectionForm TapeDialog = new TapeSelectionForm(Tapes);
            TapeDialog.HeaderString = header;
            // Tapes_dataGridView.CellClick -= Tapes_dataGridView_CellClick;
            Controls.Remove(Tapes_dataGridView);

            TapeDialog.ShowDialog(this);

            string ID = TapeDialog.ID;  // get the result
            DisplayText("Selected tape: " + ID);

            Tapes_tabPage.Controls.Add(Tapes_dataGridView);
            Tapes_dataGridView = Grid;
            Tapes_dataGridView.Location = loc;
            Tapes_dataGridView.Size = size;

            TapeDialog.Dispose();
            // Tapes_dataGridView.CellClick += Tapes_dataGridView_CellClick;
            return ID;
        }



        private void ChangeNeedle_button_Click(object sender, EventArgs e) {
            ChangeNeedle_m();
        }

        private void ZTestTravel_textBox_TextChanged(object sender, EventArgs e) {
            double val;
            if (double.TryParse(ZTestTravel_textBox.Text, out val)) {
                Properties.Settings.Default.General_ZTestTravel = val;
            }

        }

        private void button1_Click(object sender, EventArgs e) {
            setNeedleAtCameraPosition();
        }

        private void button2_Click(object sender, EventArgs e) {
            /*var loc = Cnc.XYALocation;
            Cnc.Zup(); //needle up
            for (int i = 0; i <= 360; i += 90) {
                Needle.Move_m(loc); // move to target
                Cnc.Zdown(Properties.Settings.Default.pcb_thickness);//move down
                Thread.Sleep(1000); //wait 1 second
                Cnc.Zup();

                loc.A = i;
            }*/
            calibrateAll((Button) sender);
        }




        private void needle_calibration_test_button_Click(object sender, EventArgs e) {
            AutoCalibration.DoNeedleErrorMeasurement(cameraView.upVideoProcessing);
        }

        private void mechHome_button_Click(object sender, EventArgs e) {
            MechanicalHoming_m();
        }

        private void OptHome_button_Click(object sender, EventArgs e) {
            IsHomed = OpticalHoming_m();
        }



        private void EndEditModeForTapeSelection(Object sender, EventArgs e) {
            var dgv = (DataGridView)sender;
            if (dgv.CurrentCell.GetType() == typeof(DataGridViewComboBoxCell) && (dgv.IsCurrentCellDirty))
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void loadCADFileToolStripMenuItem_Click(object sender, EventArgs e) {
            IsMeasurementValid = false;
            if (LoadCadData_m()) {
                // Read in job data (.lpj file), if exists
                string ext = Path.GetExtension(CadDataFileName);
                JobFileName = CadDataFileName.Replace(ext, ".lpj");
                if (File.Exists(JobFileName)) {
                    if (!LoadJobData_m(JobFileName)) {
                        ShowMessageBox(
                            "Attempt to read in existing Job Data file failed. Job data automatically created, review situation!",
                            "Job Data load error",
                            MessageBoxButtons.OK);
                        Cad.AutoFillJobEntry();
                    }
                } else {
                    // If not, build job data ourselves.
                    Cad.AutoFillJobEntry();
                }
            } else {
                // CAD data load failed, clear to false data
                CadData_GridView.Rows.Clear();
                CadDataFileName = "--";
            }
        }

        private void loadJobFileToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Job_openFileDialog.ShowDialog() == DialogResult.OK) {
                JobFileName = Job_openFileDialog.FileName;
                LoadJobData_m(JobFileName);
                Cad.CopyComponentsFromJob(); // repopulate component data based on saved file
                IsMeasurementValid = false;
            }
        }

        private void saveJobFileToolStripMenuItem_Click(object sender, EventArgs e) {
            Job_saveFileDialog.Filter = "LitePlacer Job files (*.lpj)|*.lpj|All files (*.*)|*.*";
            if (Job_saveFileDialog.ShowDialog() == DialogResult.OK) {
                Global.Serialization(Cad.JobData, Job_saveFileDialog.FileName);
                JobFileName = Job_saveFileDialog.FileName;
            }
        }



        private void resetToDefaultsToolStripMenuItem_Click(object sender, EventArgs e) {
            DialogResult dialogResult = ShowMessageBox(
                "All your current settings on TinyG will be lost. Are you sure?",
                "Confirm Loading Built-In settings", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) return;

            Thread t = new Thread(BuiltInSettings) { Name = "BuiltInSettings" };
            t.Start();
        }

        private void loadUserDefaultsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!Properties.Settings.Default.TinyG_settings_saved) {
                ShowMessageBox(
                "You don't have saved User Default settings.",
                "No Saved settings", MessageBoxButtons.OK);
                return;
            }

            DialogResult dialogResult = ShowMessageBox(
                "All your current settings on TinyG will be lost. Are you sure?",
                "Confirm Loading Saved settings", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) {
                return;
            }

            DisplayText("Start of DefaultSettings()");
            Cnc.CNC_Write_m(Properties.Settings.Default.TinyG_sys);
            Thread.Sleep(150);
            Cnc.CNC_Write_m(Properties.Settings.Default.TinyG_x);
            Thread.Sleep(150);
            Cnc.CNC_Write_m(Properties.Settings.Default.TinyG_y);
            Thread.Sleep(150);
            Cnc.CNC_Write_m(Properties.Settings.Default.TinyG_z);
            Thread.Sleep(150);
            Cnc.CNC_Write_m(Properties.Settings.Default.TinyG_a);
            Thread.Sleep(150);
            Cnc.CNC_Write_m(Properties.Settings.Default.TinyG_m1);
            Thread.Sleep(150);
            Cnc.CNC_Write_m(Properties.Settings.Default.TinyG_m2);
            Thread.Sleep(150);
            Cnc.CNC_Write_m(Properties.Settings.Default.TinyG_m3);
            Thread.Sleep(150);
            Cnc.CNC_Write_m(Properties.Settings.Default.TinyG_m4);
            Thread.Sleep(150);
            UpdateWindowValues_m();
            ShowMessageBox(
                "Settings restored.",
                "Saved settings restored",
                MessageBoxButtons.OK);
        }

        private void saveUserDefaultsToolStripMenuItem_Click(object sender, EventArgs e) {
            Cnc.CNC_Write_m("{\"sys\":\"\"}");
            Cnc.CNC_Write_m("{\"x\":\"\"}");
            Cnc.CNC_Write_m("{\"y\":\"\"}");
            Cnc.CNC_Write_m("{\"z\":\"\"}");
            Cnc.CNC_Write_m("{\"a\":\"\"}");
            Cnc.CNC_Write_m("{\"1\":\"\"}");
            Cnc.CNC_Write_m("{\"2\":\"\"}");
            Cnc.CNC_Write_m("{\"3\":\"\"}");
            Cnc.CNC_Write_m("{\"4\":\"\"}");

            // And save
            Properties.Settings.Default.Save();
            DisplayText("Settings saved.");
            Properties.Settings.Default.TinyG_settings_saved = true;
            DisplayText("sys:");
            DisplayText(Properties.Settings.Default.TinyG_sys);
            DisplayText("x:");
            DisplayText(Properties.Settings.Default.TinyG_x);
            DisplayText("y:");
            DisplayText(Properties.Settings.Default.TinyG_y);
            DisplayText("m1:");
            DisplayText(Properties.Settings.Default.TinyG_m1);
            // save tape stuff
            Tapes.SaveAll();
        }

        /********* LOCATIONS ***********/
        #region locations
        private void UpdateGoToPulldownMenu() {
            goToLocationToolStripMenuItem.DropDownItems.Clear();
            foreach (var name in Locations.GetNames()) {
                var x = new ToolStripMenuItem(name);
                var y = new ToolStripMenuItem(name);
                x.Click += (s, e) => { Cnc.CNC_XY(Locations.GetLocation(s.ToString())); };
                //y.Click += (s, e) => { Locations.GetLocation(s.ToString()).SetTo(Cnc.XYLocation); };
                //y.BackColor = Color.Orange;
                goToLocationToolStripMenuItem.DropDownItems.Add(x);
            }
        }

        private void locationAdd_button_Click(object sender, EventArgs e) {
            Locations.AddLocation(Cnc.CurrentX, Cnc.CurrentY, "Untitled");
        }

        private void locationDelete_button_Click(object sender, EventArgs e) {
            var row = GetGridRow(locations_dataGridView);
            if (row < 0) return;
            var name = (string)locations_dataGridView.Rows[row].Cells[0].Value;
            if (Locations.IsLocationMandatory(name)) {
                ShowSimpleMessageBox("Unable to delete required location " + name);
                return;
            }
            Locations.DeleteIndex(row);
        }

        private void locationSet_button_Click(object sender, EventArgs e) {
            var row = GetGridRow(locations_dataGridView);
            if (row < 0) return;
            var name = (string)locations_dataGridView.Rows[row].Cells[0].Value;
            var p = Locations.GetLocation(name);
            p.X = Cnc.CurrentX;
            p.Y = Cnc.CurrentY;
        }

        // do not allow key location names to be edited
        private void locations_dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e) {
            if (e.ColumnIndex != 0) return;
            var name = (string)locations_dataGridView.Rows[e.RowIndex].Cells[0].Value;
            e.Cancel = Locations.IsLocationMandatory(name);
        }


        private void locationGoTo_button_Click_1(object sender, EventArgs e) {
            var row = GetGridRow(locations_dataGridView);
            if (row < 0) return;
            var name = (string)locations_dataGridView.Rows[row].Cells[0].Value;
            var p = Locations.GetLocation(name);
            Cnc.CNC_XYA(p);
        }

        #endregion

        //downcamera
        private void button_camera_calibrate_Click(object sender, EventArgs e) {
            double x;
            if (!double.TryParse(downCalibMoveDistance_textbox.Text, out x)) {
                ShowSimpleMessageBox("Cannot parse move distance: " + downCalibMoveDistance_textbox.Text);
                return;
            }
            var ret = AutoCalibration.DownCamera_Calibration(cameraView, x);
            if (ret == null) return;
            // update values
            Properties.Settings.Default.DownCam_XmmPerPixel = ret.X;
            Properties.Settings.Default.DownCam_YmmPerPixel = ret.Y;
            DownCameraBoxXmmPerPixel_label.Text = "(" + ret.X.ToString("0.0000", CultureInfo.InvariantCulture) + "mm/pixel)";
            DownCameraBoxYmmPerPixel_label.Text = "(" + ret.Y.ToString("0.0000", CultureInfo.InvariantCulture) + "mm/pixel)";
            DownCameraBoxX_textBox.Text = (ret.X * cameraView.downVideoProcessing.box.Width).ToString("0.000", CultureInfo.InvariantCulture);
            DownCameraBoxY_textBox.Text = (ret.Y * cameraView.downVideoProcessing.box.Height).ToString("0.000", CultureInfo.InvariantCulture);
        }

        private void UpCamera_Calibration_button_Click(object sender, EventArgs e) {
            double x;
            if (!double.TryParse(UpCalibMoveDistance_textbox.Text, out x)) {
                ShowSimpleMessageBox("Cannot parse move distance: " + UpCalibMoveDistance_textbox.Text);
                return;
            }

            var ret = AutoCalibration.UpCamera_Calibration(cameraView, x);
            if (ret == null) return;
            // update values
            Properties.Settings.Default.UpCam_XmmPerPixel = ret.X;
            Properties.Settings.Default.UpCam_YmmPerPixel = ret.Y;
            UpCameraBoxXmmPerPixel_label.Text = "(" + ret.X.ToString("0.0000", CultureInfo.InvariantCulture) + "mm/pixel)";
            UpCameraBoxYmmPerPixel_label.Text = "(" + ret.Y.ToString("0.0000", CultureInfo.InvariantCulture) + "mm/pixel)";
            UpCameraBoxX_textBox.Text = (ret.X * cameraView.downVideoProcessing.box.Width).ToString("0.000", CultureInfo.InvariantCulture);
            UpCameraBoxY_textBox.Text = (ret.Y * cameraView.downVideoProcessing.box.Height).ToString("0.000", CultureInfo.InvariantCulture);
        }



        #region textBoxMapping


        readonly LitePlacer.Properties.Settings setting = LitePlacer.Properties.Settings.Default;

        private void VacuumTime_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            int val;
            VacuumTime_textBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                if (int.TryParse(VacuumTime_textBox.Text, out val)) {
                    setting.General_PickupVacuumTime = val;
                    VacuumTime_textBox.ForeColor = Color.Black;
                }
            }
        }

        private void VacuumTime_textBox_Leave(object sender, EventArgs e) {
            int val;
            if (int.TryParse(VacuumTime_textBox.Text, out val)) {
                setting.General_PickupVacuumTime = val;
                VacuumTime_textBox.ForeColor = Color.Black;
            }
        }

        private void VacuumRelease_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            int val;
            VacuumRelease_textBox.ForeColor = Color.Red;
            if (e.KeyChar == '\r') {
                if (int.TryParse(VacuumRelease_textBox.Text, out val)) {
                    setting.General_PickupReleaseTime = val;
                    VacuumRelease_textBox.ForeColor = Color.Black;
                }
            }
        }

        private void VacuumRelease_textBox_Leave(object sender, EventArgs e) {
            int val;
            if (int.TryParse(VacuumRelease_textBox.Text, out val)) {
                setting.General_PickupReleaseTime = val;
                VacuumRelease_textBox.ForeColor = Color.Black;
            }
        }

        private void NeedleOffsetX_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            double val;
            if (e.KeyChar == '\r') {
                if (double.TryParse(NeedleOffsetX_textBox.Text, out val)) {
                    setting.DownCam_NeedleOffsetX = val;
                }
            }
        }

        private void NeedleOffsetX_textBox_Leave(object sender, EventArgs e) {
            double val;
            if (double.TryParse(NeedleOffsetX_textBox.Text, out val)) {
                setting.DownCam_NeedleOffsetX = val;
            }
        }

        private void NeedleOffsetY_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            double val;
            if (e.KeyChar == '\r') {
                if (double.TryParse(NeedleOffsetY_textBox.Text, out val)) {
                    setting.DownCam_NeedleOffsetY = val;
                }
            }
        }

        private void NeedleOffsetY_textBox_Leave(object sender, EventArgs e) {
            double val;
            if (double.TryParse(NeedleOffsetY_textBox.Text, out val)) {
                setting.DownCam_NeedleOffsetY = val;
            }
        }

        private void SquareCorrection_textBox_Leave(object sender, EventArgs e) {
            double val;
            if (double.TryParse(SquareCorrection_textBox.Text, out val)) {
                setting.CNC_SquareCorrection = val;
                CNC.SquareCorrection = val;
            }
        }

        private void SquareCorrection_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            double val;
            if (e.KeyChar == '\r') {
                if (double.TryParse(SquareCorrection_textBox.Text, out val)) {
                    setting.CNC_SquareCorrection = val;
                    CNC.SquareCorrection = val;
                }
            }
        }

        // =================================================================================
        private void DownCameraBoxX_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                DownCameraUpdateXmmPerPixel();
            }
        }

        private void DownCameraBoxX_textBox_Leave(object sender, EventArgs e) {
            DownCameraUpdateXmmPerPixel();
        }


        private void DownCameraUpdateXmmPerPixel() {
            double val;
            if (double.TryParse(DownCameraBoxX_textBox.Text, out val)) {
                setting.DownCam_XmmPerPixel = val / cameraView.downVideoProcessing.box.Width;
                DownCameraBoxXmmPerPixel_label.Text = "(" + setting.DownCam_XmmPerPixel.ToString("0.0000", CultureInfo.InvariantCulture) + "mm/pixel)";
            }
        }

        // ====
        private void UpCameraBoxX_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                UpCameraUpdateXmmPerPixel();
            }
        }

        private void UpCameraBoxX_textBox_Leave(object sender, EventArgs e) {
            UpCameraUpdateXmmPerPixel();
        }

        private void UpCameraUpdateXmmPerPixel() {
            double val;
            if (double.TryParse(UpCameraBoxX_textBox.Text, out val)) {
                setting.UpCam_XmmPerPixel = val / cameraView.upVideoProcessing.box.Width;
                UpCameraBoxXmmPerPixel_label.Text = "(" + setting.UpCam_XmmPerPixel.ToString("0.0000", CultureInfo.InvariantCulture) + "mm/pixel)";
            }
        }

        // =================================================================================
        private void DownCameraBoxY_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') DownCameraUpdateYmmPerPixel();
        }

        private void DownCameraBoxY_textBox_Leave(object sender, EventArgs e) {
            DownCameraUpdateYmmPerPixel();
        }

        private void DownCameraUpdateYmmPerPixel() {
            double val;
            if (double.TryParse(DownCameraBoxY_textBox.Text, out val)) {
                setting.DownCam_YmmPerPixel = val / cameraView.downVideoProcessing.box.Height;
                DownCameraBoxYmmPerPixel_label.Text = "(" + setting.DownCam_YmmPerPixel.ToString("0.0000", CultureInfo.InvariantCulture) + "mm/pixel)";
            }
        }

        // ====
        private void UpCameraBoxY_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') UpCameraUpdateYmmPerPixel();
        }

        private void UpCameraBoxY_textBox_Leave(object sender, EventArgs e) {
            UpCameraUpdateYmmPerPixel();
        }

        private void UpCameraUpdateYmmPerPixel() {
            double val;
            if (double.TryParse(UpCameraBoxY_textBox.Text, out val)) {
                setting.UpCam_YmmPerPixel = val / cameraView.upVideoProcessing.box.Height;
                UpCameraBoxYmmPerPixel_label.Text = "(" + setting.UpCam_YmmPerPixel.ToString("0.0000", CultureInfo.InvariantCulture) + "mm/pixel)";
            }
        }


        // =================================================================================
        private void JobOffsetX_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            double val;
            if (e.KeyChar == '\r') {
                if (double.TryParse(JobOffsetX_textBox.Text, out val)) {
                    JobOffset.X = val;
                }
            }
        }

        private void JobOffsetX_textBox_Leave(object sender, EventArgs e) {
            double val;
            if (double.TryParse(JobOffsetX_textBox.Text, out val)) {
                JobOffset.X = val;
            }
        }

        // =================================================================================
        private void JobOffsetY_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            double val;
            if (e.KeyChar == '\r') {
                if (double.TryParse(JobOffsetY_textBox.Text, out val)) {
                    JobOffset.Y = val;
                }
            }
        }

        private void JobOffsetY_textBox_Leave(object sender, EventArgs e) {
            double val;
            if (double.TryParse(JobOffsetY_textBox.Text, out val)) {
                JobOffset.Y = val;
            }
        }

        // Template Based Fiducal Finding Settings - RN 5/23

        private void fiducial_designator_regexp_textBox_TextChanged(object sender, EventArgs e) {
            setting.fiducial_designator_regexp = ((TextBox)sender).Text;
            setting.Save();
        }


        /// <summary>
        /// If the use template checkbox is toggled, save the state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_useTemplate_CheckedChanged(object sender, EventArgs e) {
            setting.use_template = ((CheckBox)sender).Checked;
            setting.Save();
        }

        private void button_setTemplate_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "jpg (*.jpg)|*.jpg|png (*.png)|*.png|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK) {
                setting.template_file = ofd.FileName;
                setting.Save();
            }
        }


        private void fiducialTemlateMatch_textBox_Leave(object sender, EventArgs e) {
            fiducialTemlateMatch_textBox_KeyPress(sender, new KeyPressEventArgs('\r'));
        }

        private void HandleNumericKeypress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            }
        }

        private void fiducialTemlateMatch_textBox_KeyPress(object sender, KeyPressEventArgs e) {
            HandleNumericKeypress(sender, e);
            if (e.KeyChar == '\r') {
                setting.template_threshold = Double.Parse(((TextBox)sender).Text);
                setting.Save();
            }
        }

        private void z_offset_textbox_keypress(object sender, KeyPressEventArgs e) {
            HandleNumericKeypress(sender, e);
            if (e.KeyChar == '\r') {
                setting.z_offset = Double.Parse(((TextBox)sender).Text);
                Cnc.z_offset = setting.z_offset;
                setting.Save();
            }
        }


        private void needleCalibrationHeight_textbox_Leave(object sender, EventArgs e) {
            needleCalibrationHeight_textbox_KeyPress(sender, new KeyPressEventArgs('\r'));
        }
        private void needleCalibrationHeight_textbox_KeyPress(object sender, KeyPressEventArgs e) {
            HandleNumericKeypress(sender, e);
            if (e.KeyChar == '\r') {
                setting.focus_height = double.Parse(((TextBox)sender).Text);
                setting.Save();
            }
        }


        private void pressureSenstorPresent_button_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.pressure_sensor = ((CheckBox)sender).Checked;
            Properties.Settings.Default.Save();
        }

        #endregion

        private void zguardoff_button_Click(object sender, EventArgs e) {
            Cnc.ZGuardOff();
        }



        private void clearTextBox_button_Click(object sender, EventArgs e) {
            SerialMonitor_richTextBox.Text = "";
        }

        // just a first pass at this, can be made better
        private void autoMapJob_button_Click(object sender, EventArgs e) {
            foreach (var j in Cad.JobData) {
                //   if (j.Method != "Place") continue;
                if (j.Method != "?" && j.Method != "Place") continue; //don't overwrite something that has been set
                var type = j.Components[0].Type;
                var foot = j.Components[0].Footprint;
                var to = Tapes.FindClosestMatch(type);
                if (to == null) {
                    var end = type.ToLower()[type.Length - 1];
                    if (end == 'p') to = Tapes.FindClosestMatch(type + "F");
                }
                if (to != null) j.MethodParameters = to.ID;
            }
        }

        private void tapesQuickAdd_button_Click(object sender, EventArgs e) {
            var quickAddForm = new QuickAddForm();
            if (quickAddForm.ShowDialog() == DialogResult.OK) {
                foreach (var name in quickAddForm.Parts) {
                    var t = Tapes.AddTapeObject(0);
                    t.ID = name;
                    t.PartType = quickAddForm.PartType;
                    t.TapeType = quickAddForm.TapeType;
                    t.OriginalPartOrientation = quickAddForm.PartOrientation;
                    t.OriginalTapeOrientation = quickAddForm.TapeOrientation;
                    Tapes.CalibrateTape(t);
                    cameraView.DownCameraReset();
                    Cnc.CNC_XYA(Cnc.XYLocation + quickAddForm.HoleOffset);
                }
            }
        }

        private void vacuumDeltaADC_textbox_TextChanged(object sender, EventArgs e) {
            int x;
            if (int.TryParse(vacuumDeltaADC_textbox.Text, out x)) {
                setting.vacuumDeltaExpected = x;
                setting.Save();
            }
        }

        private void SerialMonitor_richTextBox_TextChanged(object sender, EventArgs e) {
            SerialMonitor_richTextBox.Text = "";
            return;
            smallDebugWindow.Text = ((RichTextBox)sender).Text;
            smallDebugWindow.SelectionStart = smallDebugWindow.Text.Length;
            smallDebugWindow.ScrollToCaret();
        }



        private void saveToolStripMenuItem_Click(object sender, EventArgs e) { Tapes.SaveAll(); }
        private void reLoadToolStripMenuItem_Click(object sender, EventArgs e) { Tapes.ReLoad(); }


        private void setTemplateImage(TapeObj t) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "jpg (*.jpg)|*.jpg|png (*.png)|*.png|All Files (*.*)|*.*";
            ofd.Title = "Select Template To Match The Part";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            t.TemplateFilename = ofd.FileName;
        }



        private void resetAllPlacedComponentsToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (var x in Cad.ComponentData) { x.IsPlaced = false; }
        }



        internal void ReColorComponentsTable() {
            for (int i = 0; i < CadData_GridView.RowCount; i++) {
                var x = (PhysicalComponent)CadData_GridView.Rows[i].DataBoundItem;
                CadData_GridView.Rows[i].Cells[0].Style.BackColor = (x.IsError) ? Color.HotPink : Color.White;
                CadData_GridView.Rows[i].Cells[0].Style.BackColor = (x.IsPlaced) ? Color.LightGreen : Color.White;
            }
        }



        private void resetAllPickupZsToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (var x in Tapes.tapeObjs) { x.PickupZ = -1; }
        }

        private void resetAllPlaceZsToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (var x in Tapes.tapeObjs) { x.PlaceZ = -1; }
        }

        private void takePhotosOfComponentsToolStripMenuItem_Click(object sender, EventArgs e) {
            Tapes.TakePhotosOfAllComponents();
        }

        private void manualLocationEntry_Click(object sender, EventArgs e) {
            var l = (TextBox) sender;
            var b = l.Name;
            b = b.Replace("_textBox", "");
            string val = Interaction.InputBox("Upadate "+b, "Manual Position Entry", l.Text);
            double x;
            if (double.TryParse(val, out x)) {
                switch (b) {
                    case "xpos": Cnc.CNC_XYZA(x, null, null, null); return;
                    case "ypos": Cnc.CNC_XYZA(null, x, null, null); return;
                    case "apos": Cnc.CNC_XYZA(null, null, x, null); return;
                    case "zpos": Cnc.CNC_XYZA(null, null, null, x); return;
                }
            }
            else ShowSimpleMessageBox("Unable to parse entry " + val);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            var ofd = new SaveFileDialog();
            ofd.Filter = "xml (*.xml)|*.xml|All Files (*.*)|*.*";
            ofd.Title = "Select Tapes File";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            Tapes.SaveAll(ofd.FileName);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e) {
            var ofd = new OpenFileDialog();
            ofd.Filter = "xml (*.xml)|*.xml|All Files (*.*)|*.*";
            ofd.Title = "Select Tapes File";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            Tapes.ReLoad(ofd.FileName);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void rescalAllTapesForAvailablePartsToolStripMenuItem_Click(object sender, EventArgs e) {
            AbortPlacement = false;
            foreach (var t in Tapes.tapeObjs) {
                if (AbortPlacement) {
                    AbortPlacement = false;
                    return;
                }
                Tapes.PopulateAvailableParts(t);
            }
        }

        private void pickupMultipleComponentsToolStripMenuItem_Click(object sender, EventArgs e) {
            var list =(string) Microsoft.VisualBasic.Interaction.InputBox("List Components", "List Components Comma Separated");
            int index = -1;            
            double offset = 0;
            foreach (var id in list.Split(new[] {','})) {
                index++;
                var o = Tapes.GetTapeObjByID(id);
                if (o==null) continue;
                if (!PickUpPart_m(o)) continue; 
                Global.NeedleTo("Component Dropoff");
                offset += o.GetComponentSize().Width * 4; 
                Cnc.CNC_XY(Cnc.XYLocation.OffsetBy(offset,0));
                Needle.ProbeDown(); 
                PumpOff(); 
                Cnc.Zup(); 
            }
            
        }

        private bool setNeedleAtCameraPosition()
        {
            var loc = Cnc.XYALocation;
            SetNeedleOffset_stage = 3;

            double CorrectedX, CorrectedY;
            if (!Needle.CorrectedPosition_m(Cnc.CurrentA, out CorrectedX, out CorrectedY))
            {
                ShowSimpleMessageBox("Needle not calibrated");
                return false;
            }
            loc.X -= CorrectedX;
            loc.Y -= CorrectedY;
            //loc.X -= loc.Y * CNC.SquareCorrection;
            return Needle.Move_m(loc);
        }

        private static int HeightOffset_stage;
        private PartLocation HeightOffset_StartLocation = new PartLocation();
        private void HeightOffsetButton_Click(object sender, EventArgs e)
        {
            switch (HeightOffset_stage)
            {
                case 0:
                    instructions_label.Text = "Focus the camera onto a point";
                    instructions_label.Visible = true;
                    Cnc.CNC_Z(0);
                    HeightOffsetButton.Text = "Next";
                    HeightOffsetButton.BackColor = Color.Yellow;
                    HeightOffset_stage++;
                    break;
                case 1:
                    Properties.Settings.Default.zTravelXCompensation = 0;
                    Properties.Settings.Default.zTravelYCompensation = 0;
                    Properties.Settings.Default.zTravelTotalZ = 1;

                    if (!setNeedleAtCameraPosition())
                    {
                        ShowSimpleMessageBox("FAILURE!");
                        return;
                    }

                    HeightOffset_StartLocation.X = Cnc.TrueX;
                    HeightOffset_StartLocation.Y = Cnc.CurrentY;

                    Cnc.CNC_Z(Properties.Settings.Default.ZDistanceToTable);
                    Needle.ProbingMode(true);
                    
                    Cnc.ZGuardOff();
                    
                    instructions_label.Text = "Jog camera back to the original point";
                    HeightOffset_stage++;
                    break;
                case 2:
                    Cnc.ZGuardOn();

                    double CorrectedX = 0, CorrectedY = 0;
                    if (!Needle.CorrectedPosition_m(Cnc.CurrentA, out CorrectedX, out CorrectedY))
                    {
                        ShowSimpleMessageBox("Needle not calibrated");
                        return;
                    }

                    double xDiff = Cnc.TrueX - HeightOffset_StartLocation.X;
                    double yDiff = Cnc.CurrentY - HeightOffset_StartLocation.Y;

                    Properties.Settings.Default.zTravelXCompensation = xDiff;
                    Properties.Settings.Default.zTravelYCompensation = yDiff;
                    /*
                                        Properties.Settings.Default.zTravelXCompensation = Cnc.CurrentX - 
                                            HeightOffset_StartLocation.X - Properties.Settings.Default.DownCam_NeedleOffsetX + CorrectedX;
                                        Properties.Settings.Default.zTravelYCompensation = Cnc.CurrentY - 
                                            HeightOffset_StartLocation.Y - Properties.Settings.Default.DownCam_NeedleOffsetY + CorrectedY;
                    */
                    Properties.Settings.Default.zTravelTotalZ = Cnc.CurrentZ;

                    HeightOffsetLabel.Text = "X corr: " + Math.Round(Properties.Settings.Default.zTravelXCompensation, 2).ToString() +
                        " Y Corr: " + Math.Round(Properties.Settings.Default.zTravelYCompensation, 2).ToString();

                    ZCorrectionX.Text = Math.Round(Properties.Settings.Default.zTravelXCompensation, 2).ToString();
                    ZCorrectionY.Text = Math.Round(Properties.Settings.Default.zTravelYCompensation, 2).ToString();
                    ZCorrectionDeltaZ.Text = Math.Round(Properties.Settings.Default.zTravelTotalZ, 2).ToString();

                    Needle.ProbingMode(false);
                    Cnc.CNC_Z(0);
                    HeightOffsetButton.Text = "Height Offset";
                    HeightOffsetButton.BackColor = ChangeNeedle_button.BackColor;
                    instructions_label.Text = "";
                    instructions_label.Visible = false;
                    HeightOffset_stage = 0;
                    Z0toPCB_BasicTab_label.Text = Properties.Settings.Default.ZDistanceToTable.ToString("0.00", CultureInfo.InvariantCulture);
                    Z_Backoff_label.Text = Properties.Settings.Default.General_ProbingBackOff.ToString("0.00", CultureInfo.InvariantCulture);
                    Properties.Settings.Default.Save();
                    break;
            }
        }

        private void label60_Click(object sender, EventArgs e)
        {

        }

        private void UpCameraBoxX_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ZCorrectionX_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                UpdateZCorrection();
            }
        }

        private void ZCorrectionX_Leave(object sender, EventArgs e) {
            UpdateZCorrection();
        }

        private void ZCorrectionY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                UpdateZCorrection();
            }
        }

        private void ZCorrectionY_Leave(object sender, EventArgs e)
        {
            UpdateZCorrection();
        }

        private void ZCorrectionDeltaZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                UpdateZCorrection();
            }
        }

        private void ZCorrectionDeltaZ_Leave(object sender, EventArgs e)
        {
            UpdateZCorrection();
        }

        private void UpdateZCorrection() {
            double val;
            if (double.TryParse(ZCorrectionX.Text, out val)) Properties.Settings.Default.zTravelXCompensation = val;
            if (double.TryParse(ZCorrectionY.Text, out val)) Properties.Settings.Default.zTravelYCompensation = val;
            if (double.TryParse(ZCorrectionDeltaZ.Text, out val)) Properties.Settings.Default.zTravelTotalZ = val;            
        }


        private static int calibrateAllStage = 0, calibrateXYmmRevStage = 0;
        private static PartLocation calibrateAllStartingPoint = null, calibrateAllEdgeX = null, calibrateAllEdgeY = null;

        private void calibrateXYmmRev_m(Button sender)
        {
            PartLocation loc = null;
            switch (calibrateXYmmRevStage) {
                case 0:
                    DialogResult dialogresult = ShowMessageBox("All down camera settings, travel settings and offsets will be reset", "Resetting Data", MessageBoxButtons.OKCancel);
                    if (dialogresult == DialogResult.Cancel) { return; }

                    // Reset all necessary values

                    tr1_textBox.Text = "40";
                    Cnc.CNC_Write_m("{\"1tr\":" + tr1_textBox.Text + "}");
                    Thread.Sleep(50);

                    tr2_textBox.Text = "40";
                    Cnc.CNC_Write_m("{\"2tr\":" + tr2_textBox.Text + "}");
                    Thread.Sleep(50);

                    SquareCorrection_textBox.Text = "0";
                    setting.CNC_SquareCorrection = 0;
                    CNC.SquareCorrection = 0;

                    instructions_label.Text = "Jog the machine to the center and place the measurement papers center dot under the camera";
                    instructions_label.Visible = true;
                    Cnc.CNC_Home_m("Z");
                    sender.Text = "Next";
                    sender.BackColor = Color.Yellow;
                    calibrateXYmmRevStage++;
                    break;
                case 1:
                    if (Cnc.Simulation)
                    {
                        loc = new PartLocation();
                        loc = Cnc.XYLocation;
                    }
                    else
                    {
                        cameraView.SetDownCameraFunctionSet("homing");
                        cameraView.downSettings.FindCircles = true;
                    retryMidpoint:
                        loc = FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 2, .1);
                        if (loc == null)
                        {
                            ShowSimpleMessageBox("Error, middle point not found - try again");
                            return;
                        }
                        else
                        {
                            if (cameraView.ShowMessageBox("Please verify point placement. Is point found correctly?", "verification",
                                MessageBoxButtons.YesNo) == DialogResult.No) { goto retryMidpoint; }
                        }
                    }
                    calibrateAllStartingPoint = loc;
                    Cnc.CNC_XY(loc.X + 175, loc.Y);

                    instructions_label.Text = "Jog camera to the right edge of the circle only using the X axis";
                    calibrateXYmmRevStage++;
                    break;
                case 2:
                    if (Cnc.XYLocation.Y != calibrateAllStartingPoint.Y ||
                        Cnc.XYLocation.X < calibrateAllStartingPoint.X)
                    {
                        ShowSimpleMessageBox("Y axis moved or X axis more negative than starting point - try again");
                        Cnc.CNC_XY(loc.X + 175, loc.Y);
                        return;
                    }

                    calibrateAllEdgeX = Cnc.XYLocation;
                    Cnc.CNC_XY(calibrateAllStartingPoint.X, calibrateAllStartingPoint.Y + 175);

                    instructions_label.Text = "Jog camera to the top edge of the circle only using the Y axis";
                    calibrateXYmmRevStage++;
                    break;
                case 3:
                    if (Cnc.XYLocation.X != calibrateAllStartingPoint.X ||
                        Cnc.XYLocation.Y < calibrateAllStartingPoint.Y)
                    {
                        ShowSimpleMessageBox("X axis moved or Y axis more negative than starting point - try again");
                        Cnc.CNC_XY(calibrateAllStartingPoint.X, calibrateAllStartingPoint.Y + 175);
                        return;
                    }
                    calibrateAllEdgeY = Cnc.XYLocation;

                    double xStep, yStep;
                    xStep = calibrateAllEdgeX.X / (calibrateAllStartingPoint.X + 175) * 40;
                    yStep = calibrateAllEdgeY.Y / (calibrateAllStartingPoint.Y + 175) * 40;

                    instructions_label.Text = "The calculated xStep is " + Math.Round(xStep, 2).ToString() +
                        " mm/rev, the calculated Y step is " + Math.Round(yStep, 2).ToString() +
                        " mm/rev";

                    tr1_textBox.Text = xStep.ToString();
                    Cnc.CNC_Write_m("{\"1tr\":" + tr1_textBox.Text + "}");
                    Thread.Sleep(50);

                    tr2_textBox.Text = yStep.ToString();
                    Cnc.CNC_Write_m("{\"2tr\":" + tr2_textBox.Text + "}");
                    Thread.Sleep(50);
                    calibrateXYmmRevStage = 0;
                    break;

            }
        }

        private void calibrateSkew_m()
        {
            PartLocation loc = null;
        retrySkew:
            SquareCorrection_textBox.Text = "0";
            setting.CNC_SquareCorrection = 0;
            CNC.SquareCorrection = 0;

            instructions_label.Text = "Finding center dot";

            Cnc.CNC_XY(calibrateAllStartingPoint);
            if (Cnc.Simulation)
            {
                loc = Cnc.XYLocation;
            }
            else
            {
                cameraView.SetDownCameraFunctionSet("homing");
                cameraView.downSettings.FindCircles = true;
            retryMidpoint:
                loc = FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 2, .1);
                if (loc == null)
                {
                    ShowSimpleMessageBox("Error, middle point not found - try again");
                    return;
                }
                if (cameraView.ShowMessageBox("Please verify point placement. Is point found correctly?", "verification",
                    MessageBoxButtons.YesNo) == DialogResult.No) { goto retryMidpoint; }
            }
            calibrateAllStartingPoint = loc;

            instructions_label.Text = "Finding point at X axis";
            Cnc.CNC_XY(calibrateAllStartingPoint.X + 185.5, calibrateAllStartingPoint.Y);

            if (Cnc.Simulation)
            {
                loc = new PartLocation();
                loc.X = calibrateAllStartingPoint.X + 185.5;
                loc.Y = calibrateAllStartingPoint.Y - 1;
            }
            else
            {
            retryXPoint:
                loc = FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 2, .1);
                if (loc == null)
                {
                    ShowSimpleMessageBox("Error, x axis point not found - try again");
                    return;
                }
                if (cameraView.ShowMessageBox("Please verify point placement. Is point found correctly?", "verification",
                    MessageBoxButtons.YesNo) == DialogResult.No) { goto retryXPoint; }
            }
            calibrateAllEdgeX = loc;

            double paperRotation;
            paperRotation = Math.Asin((calibrateAllEdgeX.X - calibrateAllStartingPoint.X) /
                Math.Sqrt(Math.Pow(calibrateAllEdgeX.X - calibrateAllStartingPoint.X, 2) +
                Math.Pow(calibrateAllEdgeX.Y - calibrateAllStartingPoint.Y, 2)));

            instructions_label.Text = "Finding point at Y axis using a calculated rotation of " +
                (paperRotation / Math.PI * 180).ToString() + " degrees";

            PartLocation projectedEdgeY = new PartLocation();

            projectedEdgeY.X = calibrateAllStartingPoint.X + 185.5 * Math.Sin(paperRotation - (Math.PI / 2));
            projectedEdgeY.Y = calibrateAllStartingPoint.Y + 185.5 * Math.Cos(paperRotation - (Math.PI / 2));

            Cnc.CNC_XY(projectedEdgeY);
            if (Cnc.Simulation)
            {
                loc = new PartLocation();
                loc.X = calibrateAllStartingPoint.X - 2;
                loc.Y = calibrateAllStartingPoint.Y + 185.5;
            }
            else
            {
                cameraView.SetDownCameraFunctionSet("homing");
                cameraView.downSettings.FindCircles = true;
            retryYPoint:
                loc = FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 4, .1);
                if (loc == null)
                {
                    ShowSimpleMessageBox("Error, y axis point not found - try again");
                    return;
                }
                if (cameraView.ShowMessageBox("Please verify point placement. Is point found correctly?", "verification",
                    MessageBoxButtons.YesNo) == DialogResult.No) { goto retryYPoint; }
            }
            calibrateAllEdgeY = loc;

            double yOffset = calibrateAllEdgeY.Y - projectedEdgeY.Y;
            if (yOffset > 0.3 || yOffset < -0.3)
            {
                switch (ShowMessageBox("Y offset off by " + Math.Round(yOffset, 3).ToString() +
                    "mm, try again?", "Operation failed", MessageBoxButtons.AbortRetryIgnore))
                {
                    case DialogResult.Retry:
                        goto retrySkew;
                        break;
                    case DialogResult.Ignore:
                        goto ignoreSkewError;
                }
                //sender.BackColor = ChangeNeedle_button.BackColor;
                instructions_label.Text = "";
                instructions_label.Visible = false;
                calibrateAllStage = 0;
                return;
            };
        ignoreSkewError:
            double skew = (calibrateAllEdgeY.X - projectedEdgeY.X) / 185.5;
            instructions_label.Text = "Calculated skew is " + Math.Round(skew, 5).ToString()
                + ", Y offset is " + Math.Round(yOffset, 3) +
                " press next to calibrate down camera mm/pixel values";
            SquareCorrection_textBox.Text = skew.ToString();
            setting.CNC_SquareCorrection = skew;
            CNC.SquareCorrection = skew;
            Thread.Sleep(2000);

            instructions_label.Text = "Verifying data";
            calibrateAllStartingPoint.X -= calibrateAllStartingPoint.Y * setting.CNC_SquareCorrection;
            Cnc.CNC_XY(calibrateAllStartingPoint);
            loc = FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 4, .1);
            
            if (cameraView.ShowMessageBox("Please verify point placement. Is point found correctly?", "verification",
                MessageBoxButtons.YesNo) == DialogResult.No) { goto retrySkew; }

            calibrateAllStartingPoint = loc;

            Cnc.CNC_XY(calibrateAllStartingPoint.X + 185.5, calibrateAllStartingPoint.Y);
            if (cameraView.ShowMessageBox("Please verify point placement. Is point found correctly?", "verification",
                MessageBoxButtons.YesNo) == DialogResult.No) { goto retrySkew; }

            Cnc.CNC_XY(calibrateAllStartingPoint.X, calibrateAllStartingPoint.Y + 185.5);
            if (cameraView.ShowMessageBox("Please verify point placement. Is point found correctly?", "verification",
                MessageBoxButtons.YesNo) == DialogResult.No) { goto retrySkew; }
        }

        private void calibrateXYZcompensation()
        {
            PartLocation loc = null;
retryAll:
            cameraView.SetDownCameraFunctionSet("");

            ZCorrectionX.Text = "0";
            ZCorrectionY.Text = "0";
            ZCorrectionDeltaZ.Text = "0";
            UpdateZCorrection();

            instructions_label.Text = "Calibrating Z dependent X/Y offset";

            Cnc.CNC_Z(0);
            Cnc.CNC_XY(calibrateAllStartingPoint);

            if (!Cnc.Simulation)
            {
                cameraView.SetDownCameraFunctionSet("homing");
                cameraView.downSettings.FindCircles = true;
            retryMidPoint:
                loc = FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 2, .1);
                if (loc == null)
                {
                    ShowSimpleMessageBox("Error, middle point not found - try again");
                    return;
                }
                if (cameraView.ShowMessageBox("Please verify point placement. Is point found correctly?", "verification",
                    MessageBoxButtons.YesNo) == DialogResult.No) { goto retryMidPoint; }

                calibrateAllStartingPoint = new PartLocation();
                calibrateAllStartingPoint.X = Cnc.TrueX;
                calibrateAllStartingPoint.Y = Cnc.CurrentY;
            }


            Cnc.CNC_Z(Properties.Settings.Default.ZDistanceToTable -
                Properties.Settings.Default.General_ProbingBackOff);
            Cnc.ZGuardOff();

            if (Cnc.Simulation)
            {
                loc = new PartLocation();
                loc.X = calibrateAllStartingPoint.X + 1;
                loc.Y = calibrateAllStartingPoint.Y - 1;
            }
            else
            {
                cameraView.SetDownCameraFunctionSet("homing");
                cameraView.downSettings.FindCircles = true;
            retryMidPointAgain:
                for (int i = 0; i < 8; i++)
                {
                    loc = FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 1.5, .05);
                    if (loc != null) break;
                }
                if (loc == null)
                {
                    ShowSimpleMessageBox("Error, middle point not found - try again");
                    return;
                }
                if (cameraView.ShowMessageBox("Please verify point placement. Is point found correctly?", "verification",
                    MessageBoxButtons.YesNo) == DialogResult.No) { goto retryMidPointAgain; }
                Cnc.ZGuardOn();
            }

            ZCorrectionX.Text = (Cnc.TrueX - calibrateAllStartingPoint.X).ToString();

            ZCorrectionY.Text = (Cnc.CurrentY - calibrateAllStartingPoint.Y).ToString();

            ZCorrectionDeltaZ.Text = (Properties.Settings.Default.ZDistanceToTable -
                Properties.Settings.Default.General_ProbingBackOff).ToString();
            UpdateZCorrection();

            Cnc.CNC_Z(0);
            instructions_label.Text = "";
            instructions_label.Visible = false;

            if (!Cnc.Simulation)
            {
                cameraView.SetDownCameraFunctionSet("homing");
                cameraView.downSettings.FindCircles = true;
            retryMidPoint3:
                loc = FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 2, .1);
                if (loc == null)
                {
                    ShowSimpleMessageBox("Error, middle point not found - try again");
                    return;
                }
                if (cameraView.ShowMessageBox("Please verify point placement. Is point found correctly?", "verification",
                    MessageBoxButtons.YesNo) == DialogResult.No) { goto retryMidPoint3; }
                calibrateAllStartingPoint = loc;
            }
            setNeedleAtCameraPosition();
            Cnc.CNC_Z(Properties.Settings.Default.ZDistanceToTable -
                Properties.Settings.Default.General_ProbingBackOff);
            if (cameraView.ShowMessageBox("Please verify touchdown point. Is point found correctly?", "verification",
                MessageBoxButtons.YesNo) == DialogResult.No) { goto retryAll; }
            Cnc.CNC_Z(0);
        }
                    
        private void calibrateAll(Button sender)
        {
            PartLocation loc = null;
            switch (calibrateAllStage)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    calibrateXYmmRev_m(sender);
                    if (calibrateAllStage == 3)
                    {
                        instructions_label.Text = instructions_label.Text + " Press next to continue";
                    }
                    calibrateAllStage++;
                    break;

                case 4:
                    calibrateSkew_m();

                    instructions_label.Text = "Calculated skew is " + 
                        Math.Round(setting.CNC_SquareCorrection, 5).ToString()
                        +  ". Press next to calibrate down camera mm/pixel values";
                    calibrateAllStage++;
                    break;
                
                case 5:
                    if (!Cnc.Simulation) { 
                        instructions_label.Text = "Calibrating down camera mm/pixel";
                        Application.DoEvents();
                        button_camera_calibrate_Click(null, null);

                        instructions_label.Text = "Calibrating up camera zero positin";
                        Application.DoEvents();
                        upCameraZero_button_Click(null, null);

                        instructions_label.Text = "Calibrating up camera mm/pixel";
                        Application.DoEvents();
                        UpCamera_Calibration_button_Click(null, null);
                    
                        instructions_label.Text = "Calibrating needle wobble";
                        Application.DoEvents();
                        CalibrateNeedle_button_Click(null, null);
                    }


                    instructions_label.Text="Please calibrate needle to down camera offset, press next when ready";
                    calibrateAllStage++;
                    break;
                    
                case 6:
                    calibrateXYZcompensation();

                    sender.BackColor = ChangeNeedle_button.BackColor;
                    instructions_label.Text = "";
                    instructions_label.Visible = false;
                    calibrateAllStage = 0;
                    break;
                
            }
        }

        private void verifyPlacements_m() {
            if (JobData_GridView.RowCount == 0)
            {
                ShowSimpleMessageBox("No data");
                return;
            }
reinitialize:
            IsMeasurementValid = false;
            ReMeasure_button.BackColor = Color.Yellow;
            IsMeasurementValid = BuildMachineCoordinateData_m();            

            JobData_GridView.SelectAll();

            List<PhysicalComponent> placementList = new List<PhysicalComponent>();

            for (int i = 0; i < JobData_GridView.SelectedCells.Count; i++)
            {
                var job = (JobData)JobData_GridView.SelectedCells[i].OwningRow.DataBoundItem;
                foreach (var component in job.GetComponents())
                {
                    if (component.IsFiducial && !placementList.Contains(component)) placementList.Add(component);
                }
            }

            Random random = new Random();
            int k, l;
            for (int j = 0; j < 10; j++) {
            tryAgain:
                l = random.Next(0, JobData_GridView.SelectedCells.Count);
                var job = (JobData)JobData_GridView.SelectedCells[l].OwningRow.DataBoundItem;
                k = random.Next(0, job.Components.Count);
                var component = job.Components.ElementAt(k);
                if (component.IsFiducial) { goto tryAgain; }
                if (!placementList.Contains(component)) placementList.Add(component);
            }

            foreach (var component in placementList)
            {
                Cnc.CNC_XY(component.machine);
                if (cameraView.ShowMessageBox("Please verify component position for component " + component.Designator +
                    ", is it correct?", "verification",
                    MessageBoxButtons.YesNo) == DialogResult.No) { goto reinitialize; }
            }
        }


        private void calibrateXYmmRev_Click(object sender, EventArgs e)
        {
            calibrateXYmmRev_m((Button)sender);
        }

        private void calibrateSkew_Click(object sender, EventArgs e)
        {
            calibrateSkew_m();
        }

        private void calibrateZXYCompensation_Click(object sender, EventArgs e)
        {
            calibrateXYZcompensation();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            verifyPlacements_m();
        }

    }	// end of: 	public partial class FormMain : Form

    // allows additionl of color info to displayText
    public static class RichTextBoxExtensions {
        public static void AppendText(this RichTextBox box, string text, Color color) {
            if (color != box.ForeColor) {
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;
                box.SelectionColor = color;
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;
            } else {
                box.AppendText(text);
            }
        }



    }

}	// end of: namespace LitePlacer