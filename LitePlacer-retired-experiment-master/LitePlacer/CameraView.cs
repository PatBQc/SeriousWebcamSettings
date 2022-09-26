using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Linq;
using System.Collections.Generic;

namespace LitePlacer {
    public delegate void PictureBoxClickDelegate(double x, double y, int rawx, int rawy);

    public partial class CameraView : Form {
        public VideoCapture upVideoCapture;
        public VideoCapture downVideoCapture;
        public VideoProcessing upVideoProcessing;
        public VideoProcessing downVideoProcessing;
        public VideoOverlaySettings downSettings = new VideoOverlaySettings();
        public VideoOverlaySettings upSettings = new VideoOverlaySettings();

        private AForgeFunctionSet upSet, downSet;
        private BindingList<AForgeFunction> currentUpBinding = new BindingList<AForgeFunction>();
        private BindingList<AForgeFunction> currentDownBinding = new BindingList<AForgeFunction>();
        public PictureBoxClickDelegate upClickDelegate, downClickDelegate;
        private bool ShuttingDown = false;
        System.Timers.Timer FPStimer = new System.Timers.Timer(1000);

        public List<string> GetTapeTypes() {
            return downSet.GetNames().Where(x => x.Contains("Tape")).Select(x => x.Replace("Tape", "")).ToList();
        }

        private void CameraView_FormClosing(object sender, FormClosingEventArgs e) {
            if (!ShuttingDown) e.Cancel = true;
            return;
        }

        public CameraView() {
            InitializeComponent();
            //call this function when closing
            //this.Closing +=new CancelEventHandler(Cleanup); //this is depreciated
            this.FormClosing += new FormClosingEventHandler(CameraView_FormClosing); //do not allow the cameraview to be closed by itself

            //setup key handeling
            KeyPreview = true;
            Global.Instance.mainForm.SetupCursorNavigation(Controls);
            KeyUp += Global.Instance.mainForm.My_KeyUp;

            //setup video processing
            // load the different filter blocks
            upSet = new AForgeFunctionSet("UP");
            downSet = new AForgeFunctionSet("DOWN");
            HideFilters(); //start hidden

            //Setup Checkbox Bindings
            DownCamera_drawGrid_checkBox.DataBindings.Add("Checked", downSettings, "Draw1mmGrid", false, DataSourceUpdateMode.OnPropertyChanged);
            DownCameraDrawCross_checkBox.DataBindings.Add("Checked", downSettings, "DrawCross", false, DataSourceUpdateMode.OnPropertyChanged);
            DownCameraDrawBox_checkBox.DataBindings.Add("Checked", downSettings, "DrawBox", false, DataSourceUpdateMode.OnPropertyChanged);
            DownCameraDrawTicks_checkBox.DataBindings.Add("Checked", downSettings, "DrawSidemarks", false, DataSourceUpdateMode.OnPropertyChanged);
            DownCamFindCircles_checkBox.DataBindings.Add("Checked", downSettings, "FindCircles", false, DataSourceUpdateMode.OnPropertyChanged);
            DownCamFindRectangles_checkBox.DataBindings.Add("Checked", downSettings, "FindRectangles", false, DataSourceUpdateMode.OnPropertyChanged);
            DownCam_FindComponents_checkBox.DataBindings.Add("Checked", downSettings, "FindComponent", false, DataSourceUpdateMode.OnPropertyChanged);
            DownCamera_FindFiducials_cb.DataBindings.Add("Checked", downSettings, "FindFiducial", false, DataSourceUpdateMode.OnPropertyChanged);

            UpCam_DrawCross.DataBindings.Add("Checked", upSettings, "DrawCross", false, DataSourceUpdateMode.OnPropertyChanged);
            UpCan_DrawBox.DataBindings.Add("Checked", upSettings, "DrawBox", false, DataSourceUpdateMode.OnPropertyChanged);
            UpCam_DrawDashedBox.DataBindings.Add("Checked", upSettings, "DrawDashedCross", false, DataSourceUpdateMode.OnPropertyChanged);
            UpCam_FindCircles.DataBindings.Add("Checked", upSettings, "FindCircles", false, DataSourceUpdateMode.OnPropertyChanged);
            UpCam_FindComponents.DataBindings.Add("Checked", upSettings, "FindComponent", false, DataSourceUpdateMode.OnPropertyChanged);
            UpCam_FindRectangles.DataBindings.Add("Checked", upSettings, "FindRectangles", false, DataSourceUpdateMode.OnPropertyChanged);

            //start video capture
            upVideoCapture = new VideoCapture(CameraType.UpCamera);
            upVideoProcessing = new VideoProcessing(upVideoCapture, upSettings);
            upVideoCapture.FrameCaptureDelegates.Add(UpCameraVideoProcessingCallback);
            upVideoProcessing.SetFunctionsList("");

            downVideoCapture = new VideoCapture(CameraType.DownCamera);
            downVideoProcessing = new VideoProcessing(downVideoCapture, downSettings);
            downVideoCapture.FrameCaptureDelegates.Add(DownCameraVideoProcessingCallback);
            downVideoProcessing.SetFunctionsList("");


            //fill combobox // todo - restore from defaults 
            UpCamera_FilterSet.DataSource = new BindingSource { DataSource = upSet.GetNames() };
            DownCamera_FilterSet.DataSource = new BindingSource { DataSource = downSet.GetNames() };
            var videoSources = VideoCapture.GetVideoDeviceList();
            UpCam_ComboBox.DataSource = new BindingSource { DataSource =  videoSources};
            DownCam_ComboBox.DataSource = new BindingSource { DataSource = videoSources};

            //load saved values
            var s = Properties.Settings.Default;
            if (s.UpCam_index != s.DownCam_index) {
                if (s.DownCam_index > 0 && s.DownCam_index <= videoSources.Count) {
                    DownCam_ComboBox.SelectedIndex = s.DownCam_index-1;
                    downVideoCapture.Start(DownCam_ComboBox.SelectedIndex);
                }

                if (s.UpCam_index > 0 && s.UpCam_index <= videoSources.Count) {
                    UpCam_ComboBox.SelectedIndex = s.UpCam_index-1;
                    upVideoCapture.Start(UpCam_ComboBox.SelectedIndex);
                }
            }
            
            //right click context menu
            ContextMenu cms = new System.Windows.Forms.ContextMenu();
            var i = Global.Instance;
            cms.MenuItems.Add(new MenuItem("Needle To Table (Probe) And Back", delegate {  var x = i.cnc.XYLocation; if (i.needle.Move_m(x) && i.needle.ProbeDown() && i.cnc.Zup() && i.cnc.CNC_XY(x)) { }; }));
            cms.MenuItems.Add(new MenuItem("Move To Closest Circle", delegate { i.mainForm.FindPositionAndMoveToClosest(Shapes.ShapeTypes.Circle, 5, .1); }));
            DownCamera_PictureBox.ContextMenu = cms;


            //bind editor values
            uFilter_dataGridView.DataSource = currentUpBinding;
            dFilter_dataGridView.DataSource = currentDownBinding;
            methodDataGridViewTextBoxColumn.DataSource = Enum.GetValues(typeof(AForgeMethod));
            methodDataGridViewTextBoxColumn1.DataSource = Enum.GetValues(typeof(AForgeMethod));

            //monitor frame rate
            FPStimer.Elapsed += delegate { UpdateFPS();};
            FPStimer.Start();     
        }

        private void UpdateFPS() {
            if (InvokeRequired) { Invoke(new Action(UpdateFPS)); return; }
            cameraStats_label.Text = String.Format("UP:   {0:D2} Got {2:D2} Drop Exp:{6} ({11}) Bright:{8} ({10}\n" +
                                                   "DOWN: {3:D2} Got {5:D2} Drop Exp:{7} ({13}) Bright:{9} ({12}\n",
                                                   upVideoCapture.FPSReceived, upVideoCapture.FPSProcessed, upVideoCapture.FPSDropped,
                                                   downVideoCapture.FPSReceived, downVideoCapture.FPSProcessed, downVideoCapture.FPSDropped,
                                                   upVideoCapture.GetExposure(), downVideoCapture.GetExposure(),
                                                   upVideoCapture.GetBrightness(), downVideoCapture.GetBrightness(),
                                                   upVideoCapture.BrightnessRange, upVideoCapture.ExposureRange,
                                                   downVideoCapture.BrightnessRange, downVideoCapture.ExposureRange);
        }


        private void UpCameraVideoProcessingCallback(Bitmap frame) {
            frame = upVideoProcessing.ProcessFrame(frame); //apply filters, etc
            frame = upVideoProcessing.ApplyMarkup(frame); //draw lines, etc
            SetFrame(frame, "UpCamera");
        }

        private void DownCameraVideoProcessingCallback(Bitmap frame) {
            frame = downVideoProcessing.ProcessFrame(frame); //apply filters, etc
            frame = downVideoProcessing.ApplyMarkup(frame); //draw lines, etc
            SetFrame(frame, "DownCamera");
        }

     
        public void SetDownCameraFunctionSet(string name) {
            UpCamera_FilterSet.SelectedIndex = 0;            
            DownCamera_FilterSet.SelectedIndex = DownCamera_FilterSet.FindString(name);
        }

        public void SetUpCameraFunctionSet(string name) {
            UpCamera_FilterSet.SelectedIndex = 0;
            UpCamera_FilterSet.SelectedIndex = UpCamera_FilterSet.FindString(name);
        }

        public void UpCameraReset() {
            SetUpCameraFunctionSet("");
            upVideoProcessing.Reset();
            upVideoCapture.UnlockExposure();
            upVideoCapture.UnlockBrightness();
        }

        public void DownCameraReset() {
            SetDownCameraFunctionSet("");
            downVideoProcessing.Reset();
            downVideoCapture.UnlockExposure();
            downVideoCapture.UnlockBrightness();
        }


        private void Restart_Button_Click(object sender, EventArgs e) {
            if (DownCam_ComboBox.SelectedIndex == UpCam_ComboBox.SelectedIndex) {
                Global.Instance.DisplayText("Up cam can't be the same as downcam");
                return;
            }
            new Thread(delegate() {
                if (downVideoCapture.IsRunning()) downVideoCapture.Close();
                if (upVideoCapture.IsRunning()) upVideoCapture.Close();
            }).Start();

            while (downVideoCapture.IsRunning()) Global.DoBackgroundWork();
            while (upVideoCapture.IsRunning()) Global.DoBackgroundWork();
            
            upVideoCapture.Start(UpCam_ComboBox.SelectedIndex);
            downVideoCapture.Start(DownCam_ComboBox.SelectedIndex);

            //save
            Properties.Settings.Default.DownCam_index = DownCam_ComboBox.SelectedIndex + 1;
            Properties.Settings.Default.UpCam_index = UpCam_ComboBox.SelectedIndex + 1;
            Properties.Settings.Default.Save();
        }

        //forward mouse clicks
        private void PictureBox_MouseClick(object sender, MouseEventArgs e) {
            //MouseEventArgs me = (MouseEventArgs)e;
            //Point coordinates = me.Location;

            if (e.Button == MouseButtons.Right) return; //skip right button presses

            var pictureBox = (PictureBox)sender;
            if (pictureBox.Image == null) return;

            // as we are stretching the image, we find the point on the bitmap (where measurements are based) 
            // to do the computation
            

            double strechX = (double)pictureBox.Image.Width/(double)pictureBox.Width;
            double strechY = (double)pictureBox.Image.Height / (double)pictureBox.Height;
            double x = (e.X - pictureBox.Size.Width/2) * strechX;
            double y = (pictureBox.Size.Height/2 - e.Y) * strechY;

            // make sure we get it to the right handler in case the images are swapped
            if ((pictureBox.Equals(DownCamera_PictureBox) && !_isSwap) ||
                (pictureBox.Equals(UpCamera_PictureBox) && _isSwap)) {
                var zoom = downVideoProcessing.GetZoom();
                if (downClickDelegate != null) downClickDelegate(x/zoom, y/zoom, e.X, e.Y);
            }
            else {
                var zoom = upVideoProcessing.GetZoom();
                if (upClickDelegate != null) upClickDelegate(x/zoom, y/zoom, e.X, e.Y);
            }
        }


        public void HideFilters() { Width = 672; }
        public void ShowFilters() { Width = 1041; }

        //delegates to handle updating the pictureframes
        public delegate void PassBitmapDelegate(Bitmap frame, string cam);

        public void SetFrame(Bitmap frame, string cam) {
            if (this.IsDisposed || ShuttingDown) return;
            if (InvokeRequired) {
                Invoke(new PassBitmapDelegate(SetFrame), frame, cam);
                return;
            }
            if (_isSwap && cam == "UpCamera") cam = "DownCamera";
            else if (_isSwap && cam == "DownCamera") cam = "UpCamera";
            switch (cam) {
                case "UpCamera":
                    if (UpCamera_PictureBox.Image != null) UpCamera_PictureBox.Image.Dispose();
                    UpCamera_PictureBox.Image = frame;
                    break;
                case "DownCamera":
                    if (DownCamera_PictureBox.Image != null) DownCamera_PictureBox.Image.Dispose();
                    DownCamera_PictureBox.Image = frame;
                    break;
            }
        }

        private void showFilters_button_Click(object sender, EventArgs e) {
            var b = (Button)sender;
            if (Width > 1000) {
                HideFilters();
                b.Text = "Show Filters";
            } else {
                ShowFilters();
                b.Text = "Hide Filters";
            }
        }


        private object SelectedRowObject(DataGridView grid) {
            if (grid.SelectedCells.Count == 1)  return grid.CurrentCell.OwningRow.DataBoundItem;
            return null;
        }

        private int SelectedRow(DataGridView grid) {
            if (grid.SelectedCells.Count == 1)  return grid.CurrentCell.RowIndex;
            return -1;
        }

        private void FilterEditorButtonAction(object sender, EventArgs e) {
            var b = (Button)sender;
            string buttonName = b.Name;
            var target = (buttonName[0] == 'u') ? currentUpBinding : currentDownBinding;
            var row = (buttonName[0] == 'u') ? SelectedRow(uFilter_dataGridView) : SelectedRow(dFilter_dataGridView);

            switch (buttonName.Remove(0, 1)) {
                case "AddButton":
                    target.Add(new AForgeFunction());
                    break;
                case "DeleteButton":
                    if (row != -1) target.RemoveAt(row);
                    break;
                case "MoveUpButton":
                    if (row != -1) Global.MoveItem(target, row, -1);
                    break;
                case "MoveDownButton":
                    if (row != -1) Global.MoveItem(target, row, +1);
                    break;
                case "ClearButton":
                    target.Clear();
                    break;
            }

        }

                    

        /* Filter Set Management */
        /*************************/
        private void DownCamera_FilterSet_SelectedIndexChanged(object sender, EventArgs e) {
            var cb = (ComboBox)sender;
            var list = downSet.GetSet(cb.SelectedItem.ToString());
            if (list != null) {
                currentDownBinding = list; //local copy
                dFilter_dataGridView.DataSource = currentDownBinding;
                downVideoProcessing.SetFunctionsList(currentDownBinding);
            }
        }

        private void UpCamea_FilterSet_SelectedIndexChanged(object sender, EventArgs e) {
            var cb = (ComboBox)sender;
            var list = upSet.GetSet(cb.SelectedItem.ToString());
            if (list != null) {
                currentUpBinding = list; //local copy;
                uFilter_dataGridView.DataSource = currentUpBinding;
                upVideoProcessing.SetFunctionsList(currentUpBinding);
            }
        }

        private void DownCameraFilterSave_button_Click(object sender, EventArgs e) {
            downSet.Save(DownCamera_FilterSet.SelectedValue.ToString(), currentDownBinding);
        }

        private void UpCameraFilterSave_button_Click(object sender, EventArgs e) {
            upSet.Save(UpCamera_FilterSet.SelectedValue.ToString(), currentUpBinding);
        }


        public void SetDownCameraDefaults() {
            downVideoProcessing.Reset();
        }
        public void SetUpCameraDefaults() {
            upVideoProcessing.Reset();
        }



        // if selected, then the next click is fowarded to GetDownRGB then 
        // restored to whatever it was
        PictureBoxClickDelegate oldUpDelegate, oldDownDelegate;
        private void SelectColor_Click(object sender, EventArgs e) {
            oldDownDelegate = downClickDelegate;
            oldUpDelegate = upClickDelegate;
            downClickDelegate = GetDownRGB;
            upClickDelegate = GetUpRGB;
        }

        private void GetDownRGB(double x, double y, int rawx, int rawy) {
            var frame = downVideoCapture.GetFrame();
            var o = (AForgeFunction)SelectedRowObject(dFilter_dataGridView);
            if (o != null) {
                Color color = frame.GetPixel(rawx, rawy);
                o.SetColor(color);
            }
            downClickDelegate = oldDownDelegate;
            upClickDelegate = oldUpDelegate;
        }


        private void GetUpRGB(double x, double y, int rawx, int rawy) {
            var frame = upVideoCapture.GetFrame();
            var row = SelectedRow(uFilter_dataGridView);
            var o = (AForgeFunction)SelectedRowObject(dFilter_dataGridView);
            if (o != null) {
                Color color = frame.GetPixel(rawx, rawy);
                o.SetColor(color);
            }
            downClickDelegate = oldDownDelegate;
            upClickDelegate = oldUpDelegate;
        }

        //was getting in an ugly race condition while shutting down the camera with the async layer - this fixed it.
        public void Shutdown() {
            ShuttingDown = true;
            new Thread(delegate() {
                if (downVideoCapture.IsRunning()) downVideoCapture.Close();
                if (upVideoCapture.IsRunning()) upVideoCapture.Close();
            }).Start();
        }

        //commit changes to combobox or checkbox instantly
        private void EndEditMode(Object sender, EventArgs e) {
            var dgv = (DataGridView)sender;
            if (dgv.CurrentCell.GetType() == typeof(DataGridViewComboBoxCell) ||
                dgv.CurrentCell.GetType() == typeof(DataGridViewCheckBoxCell)) {
                if (dgv.IsCurrentCellDirty) dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }



        private void dFilter_dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            e.Cancel = true;
        }



        private bool _isSwap = false;
        private void swap_button_Click(object sender, EventArgs e) {
            _isSwap = !_isSwap;
            top_label.Text = (_isSwap) ? "Up Camera" : "Down Camera";
            bottom_label.Text = (!_isSwap) ? "Up Camera" : "Down Camera";
        }

        private void dNewFilter_button_Click(object sender, EventArgs e) {
            string name = Interaction.InputBox("Specify Name","New Filter", "Untitled");
            downSet.Add(name);
            DownCamera_FilterSet.DataSource = new BindingSource { DataSource = downSet.GetNames() };
            downVideoProcessing.SetFunctionsList(name);
            Global.Instance.mainForm.UpdateTapeTypesBinding();
        }

        private void uNewFilter_button_Click(object sender, EventArgs e) {
            string name = Interaction.InputBox("Specify Name", "New Filter", "Untitled");
            upSet.Add(name);
            UpCamera_FilterSet.DataSource = new BindingSource { DataSource = downSet.GetNames() };
            upVideoProcessing.SetFunctionsList(name);
            Global.Instance.mainForm.UpdateTapeTypesBinding();
        }

        private void clearFilters_button_Click(object sender, EventArgs e) {
            UpCameraReset();
            DownCameraReset();
        }

        public DialogResult ShowMessageBox(String message, String header, MessageBoxButtons buttons)
        {
/*            if (InvokeRequired)
            {
                return (DialogResult)Invoke(new PassStringStringReturnDialogResultDelegate(ShowMessageBox), message, header, buttons);
            }*/
            return MessageBox.Show(this, message, header, buttons);
        }

     /*   private void DownCam_ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            Properties.Settings.Default.DownCam_index = DownCam_ComboBox.SelectedIndex+1;
            Properties.Settings.Default.Save();
        }

        private void UpCam_ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            Properties.Settings.Default.UpCam_index = UpCam_ComboBox.SelectedIndex+1;
            Properties.Settings.Default.Save();
        }*/



    }
}