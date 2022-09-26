using System.ComponentModel;
using System.Windows.Forms;

namespace LitePlacer
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TestNeedleRecognition_button = new System.Windows.Forms.Button();
            this.textBoxSendtoTinyG = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.xpos_textBox = new System.Windows.Forms.TextBox();
            this.ypos_textBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.zpos_textBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.apos_textBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.SerialMonitor_richTextBox = new System.Windows.Forms.RichTextBox();
            this.Test1_button = new System.Windows.Forms.Button();
            this.Test2_button = new System.Windows.Forms.Button();
            this.Job_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Job_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpticalHome_button = new System.Windows.Forms.Button();
            this.label97 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.label145 = new System.Windows.Forms.Label();
            this.Tapes_tabPage = new System.Windows.Forms.TabPage();
            this.AddTape_button = new System.Windows.Forms.Button();
            this.label109 = new System.Windows.Forms.Label();
            this.Tapes_dataGridView = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberPartsAvailable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TemplateBased = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CurrentPart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginalTapeOrientation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.OriginalPartOrientation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TapeType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PartType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.HolePitch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartPitch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoleToPartX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoleToPartY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pickupZDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.placeZDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TapeAngle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tapeObjBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.GotoPickupCenter_button = new System.Windows.Forms.Button();
            this.GotoPCB0_button = new System.Windows.Forms.Button();
            this.Snapshot_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.zoffset_textbox = new System.Windows.Forms.TextBox();
            this.label130 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label127 = new System.Windows.Forms.Label();
            this.fiducial_designator_regexp_textBox = new System.Windows.Forms.TextBox();
            this.button_setTemplate = new System.Windows.Forms.Button();
            this.label126 = new System.Windows.Forms.Label();
            this.fiducialTemlateMatch_textBox = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.cb_useTemplate = new System.Windows.Forms.CheckBox();
            this.label129 = new System.Windows.Forms.Label();
            this.downCalibMoveDistance_textbox = new System.Windows.Forms.TextBox();
            this.SlackMeasurement_label = new System.Windows.Forms.Label();
            this.button_camera_calibrate = new System.Windows.Forms.Button();
            this.DownCameraBoxYmmPerPixel_label = new System.Windows.Forms.Label();
            this.DownCameraBoxXmmPerPixel_label = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.DownCameraBoxX_textBox = new System.Windows.Forms.TextBox();
            this.label70 = new System.Windows.Forms.Label();
            this.DownCameraBoxY_textBox = new System.Windows.Forms.TextBox();
            this.DownCamera_Calibration_button = new System.Windows.Forms.Button();
            this.UpCameraBoxYmmPerPixel_label = new System.Windows.Forms.Label();
            this.UpCameraBoxXmmPerPixel_label = new System.Windows.Forms.Label();
            this.UpCameraBoxY_textBox = new System.Windows.Forms.TextBox();
            this.UpCameraBoxX_textBox = new System.Windows.Forms.TextBox();
            this.label106 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.NeedleOffsetY_textBox = new System.Windows.Forms.TextBox();
            this.NeedleOffsetX_textBox = new System.Windows.Forms.TextBox();
            this.label149 = new System.Windows.Forms.Label();
            this.label148 = new System.Windows.Forms.Label();
            this.label146 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.Offset2Method_button = new System.Windows.Forms.Button();
            this.instructions_label = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.ZUp_button = new System.Windows.Forms.Button();
            this.ZDown_button = new System.Windows.Forms.Button();
            this.tabPageBasicSetup = new System.Windows.Forms.TabPage();
            this.button7 = new System.Windows.Forms.Button();
            this.clearTextBox_button = new System.Windows.Forms.Button();
            this.showTinyGComms_checkbox = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabpage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label73 = new System.Windows.Forms.Label();
            this.xsv_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.xjh_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.Xmax_checkBox = new System.Windows.Forms.CheckBox();
            this.Xlim_checkBox = new System.Windows.Forms.CheckBox();
            this.Xhome_checkBox = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tr1_textBox = new System.Windows.Forms.TextBox();
            this.m1deg18_radioButton = new System.Windows.Forms.RadioButton();
            this.m1deg09_radioButton = new System.Windows.Forms.RadioButton();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.mi1_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.xvm_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.xjm_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.TestX_button = new System.Windows.Forms.Button();
            this.TestXY_button = new System.Windows.Forms.Button();
            this.TestYX_button = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label77 = new System.Windows.Forms.Label();
            this.ysv_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.yjh_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.Ymax_checkBox = new System.Windows.Forms.CheckBox();
            this.Ylim_checkBox = new System.Windows.Forms.CheckBox();
            this.Yhome_checkBox = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tr2_textBox = new System.Windows.Forms.TextBox();
            this.m2deg18_radioButton = new System.Windows.Forms.RadioButton();
            this.m2deg09_radioButton = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.mi2_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.yvm_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.yjm_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TestY_button = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label123 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label81 = new System.Windows.Forms.Label();
            this.zsv_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.zjh_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label84 = new System.Windows.Forms.Label();
            this.Zmax_checkBox = new System.Windows.Forms.CheckBox();
            this.Zlim_checkBox = new System.Windows.Forms.CheckBox();
            this.Zhome_checkBox = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tr3_textBox = new System.Windows.Forms.TextBox();
            this.m3deg18_radioButton = new System.Windows.Forms.RadioButton();
            this.m3deg09_radioButton = new System.Windows.Forms.RadioButton();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.mi3_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.zvm_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.zjm_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.ZTestTravel_textBox = new System.Windows.Forms.TextBox();
            this.TestZ_button = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.reverseRotation_checkbox = new System.Windows.Forms.CheckBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tr4_textBox = new System.Windows.Forms.TextBox();
            this.m4deg18_radioButton = new System.Windows.Forms.RadioButton();
            this.m4deg09_radioButton = new System.Windows.Forms.RadioButton();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.mi4_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.avm_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.ajm_maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.TestA_button = new System.Windows.Forms.Button();
            this.label90 = new System.Windows.Forms.Label();
            this.SquareCorrection_textBox = new System.Windows.Forms.TextBox();
            this.SmallMovement_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label87 = new System.Windows.Forms.Label();
            this.SlackCompensation_checkBox = new System.Windows.Forms.CheckBox();
            this.Homebutton = new System.Windows.Forms.Button();
            this.HomeZ_button = new System.Windows.Forms.Button();
            this.HomeY_button = new System.Windows.Forms.Button();
            this.HomeXY_button = new System.Windows.Forms.Button();
            this.HomeX_button = new System.Windows.Forms.Button();
            this.buttonRefreshPortList = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxSerialPorts = new System.Windows.Forms.ComboBox();
            this.VacuumRelease_textBox = new System.Windows.Forms.TextBox();
            this.label119 = new System.Windows.Forms.Label();
            this.VacuumTime_textBox = new System.Windows.Forms.TextBox();
            this.label118 = new System.Windows.Forms.Label();
            this.labelSerialPortStatus = new System.Windows.Forms.Label();
            this.Z_Backoff_label = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.Z0toPCB_BasicTab_label = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.SetProbing_button = new System.Windows.Forms.Button();
            this.buttonConnectSerial = new System.Windows.Forms.Button();
            this.MotorPower_checkBox = new System.Windows.Forms.CheckBox();
            this.Vacuum_checkBox = new System.Windows.Forms.CheckBox();
            this.Pump_checkBox = new System.Windows.Forms.CheckBox();
            this.RunJob_tabPage = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.p2_label = new System.Windows.Forms.Label();
            this.p1_label = new System.Windows.Forms.Label();
            this.ignoreErrors_checkbox = new System.Windows.Forms.CheckBox();
            this.skippedPlacedComponents_checkBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.NewRow_button = new System.Windows.Forms.Button();
            this.DeleteComponentGroup_button = new System.Windows.Forms.Button();
            this.autoMapJob_button = new System.Windows.Forms.Button();
            this.PlaceAll_button = new System.Windows.Forms.Button();
            this.JobOffsetY_textBox = new System.Windows.Forms.TextBox();
            this.JobOffsetX_textBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PCBOffset_label = new System.Windows.Forms.Label();
            this.placement_Picturebox = new System.Windows.Forms.PictureBox();
            this.PlacedValue_label = new System.Windows.Forms.Label();
            this.PlacedComponent_label = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.JobData_GridView = new System.Windows.Forms.DataGridView();
            this.countDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.componentListDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.componentTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.methodParametersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jobDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Bottom_checkBox = new System.Windows.Forms.CheckBox();
            this.CadData_GridView = new System.Windows.Forms.DataGridView();
            this.designatorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.footprintDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xnominalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ynominalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rotationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isFiducialDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.physicalComponentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AbortPlacement_button = new System.Windows.Forms.Button();
            this.PausePlacement_button = new System.Windows.Forms.Button();
            this.ReMeasure_button = new System.Windows.Forms.Button();
            this.ChangeNeedle_button = new System.Windows.Forms.Button();
            this.needle_calibration_test_button = new System.Windows.Forms.Button();
            this.StopDemo_button = new System.Windows.Forms.Button();
            this.Demo_button = new System.Windows.Forms.Button();
            this.tabControlPages = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.button8 = new System.Windows.Forms.Button();
            this.calibrateZXYCompensation = new System.Windows.Forms.Button();
            this.calibrateSkew = new System.Windows.Forms.Button();
            this.calibrateXYmmRev = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.vacuumDeltaADC_textbox = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.pressureSenstorPresent_button = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.locations_dataGridView = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namedLocationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.locationSet_button = new System.Windows.Forms.Button();
            this.locationGoTo_button = new System.Windows.Forms.Button();
            this.locationDelete_button = new System.Windows.Forms.Button();
            this.locationAdd_button = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ZCorrectionDeltaZ = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.ZCorrectionY = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.ZCorrectionX = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.HeightOffsetLabel = new System.Windows.Forms.Label();
            this.HeightOffsetButton = new System.Windows.Forms.Button();
            this.label54 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.UpCalibMoveDistance_textbox = new System.Windows.Forms.TextBox();
            this.needleCalibrationHeight_textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.upCameraZero_button = new System.Windows.Forms.Button();
            this.pcbThickness_label = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.pcbThickness_button = new System.Windows.Forms.Button();
            this.needleHeight_button = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.CAD_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button5 = new System.Windows.Forms.Button();
            this.TrueX_label = new System.Windows.Forms.Label();
            this.mechHome_button = new System.Windows.Forms.Button();
            this.OptHome_button = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadJobFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveJobFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.loadCADFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tinyGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToDefaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadUserDefaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveUserDefaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.resetAllPickupZsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetAllPlaceZsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.takePhotosOfComponentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickAddMultipleTapesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rescalAllTapesForAvailablePartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.pickupMultipleComponentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jobOperationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetAllPlacedComponentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.needleToFocus_button = new System.Windows.Forms.Button();
            this.zguardoff_button = new System.Windows.Forms.Button();
            this.smallDebugWindow = new System.Windows.Forms.RichTextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.Tapes_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tapes_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tapeObjBindingSource)).BeginInit();
            this.groupBox12.SuspendLayout();
            this.tabPageBasicSetup.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabpage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SmallMovement_numericUpDown)).BeginInit();
            this.RunJob_tabPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.placement_Picturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobData_GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CadData_GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.physicalComponentBindingSource)).BeginInit();
            this.tabControlPages.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.locations_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namedLocationBindingSource)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // TestNeedleRecognition_button
            // 
            this.TestNeedleRecognition_button.Location = new System.Drawing.Point(541, 838);
            this.TestNeedleRecognition_button.Name = "TestNeedleRecognition_button";
            this.TestNeedleRecognition_button.Size = new System.Drawing.Size(110, 23);
            this.TestNeedleRecognition_button.TabIndex = 63;
            this.TestNeedleRecognition_button.Text = "Calibrate Needle";
            this.toolTip1.SetToolTip(this.TestNeedleRecognition_button, "Re-runs needle calibration routine.");
            this.TestNeedleRecognition_button.UseVisualStyleBackColor = true;
            this.TestNeedleRecognition_button.Click += new System.EventHandler(this.CalibrateNeedle_button_Click);
            // 
            // textBoxSendtoTinyG
            // 
            this.textBoxSendtoTinyG.Location = new System.Drawing.Point(353, 657);
            this.textBoxSendtoTinyG.Name = "textBoxSendtoTinyG";
            this.textBoxSendtoTinyG.Size = new System.Drawing.Size(271, 20);
            this.textBoxSendtoTinyG.TabIndex = 8;
            this.toolTip1.SetToolTip(this.textBoxSendtoTinyG, "On enter, the text is sent directly to TinyG.");
            this.textBoxSendtoTinyG.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSendtoTinyG_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(278, 660);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Text to send:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 762);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 18);
            this.label14.TabIndex = 7;
            this.label14.Text = "X:";
            // 
            // xpos_textBox
            // 
            this.xpos_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.xpos_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpos_textBox.Location = new System.Drawing.Point(38, 761);
            this.xpos_textBox.Name = "xpos_textBox";
            this.xpos_textBox.ReadOnly = true;
            this.xpos_textBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.xpos_textBox.Size = new System.Drawing.Size(113, 19);
            this.xpos_textBox.TabIndex = 9;
            this.xpos_textBox.Text = "- - - -";
            this.xpos_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.xpos_textBox, "Current X position");
            this.xpos_textBox.Click += new System.EventHandler(this.manualLocationEntry_Click);
            // 
            // ypos_textBox
            // 
            this.ypos_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ypos_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ypos_textBox.Location = new System.Drawing.Point(38, 788);
            this.ypos_textBox.Name = "ypos_textBox";
            this.ypos_textBox.ReadOnly = true;
            this.ypos_textBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ypos_textBox.Size = new System.Drawing.Size(113, 19);
            this.ypos_textBox.TabIndex = 11;
            this.ypos_textBox.Text = "- - - -";
            this.ypos_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.ypos_textBox, "Current Y position");
            this.ypos_textBox.Click += new System.EventHandler(this.manualLocationEntry_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 789);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 18);
            this.label17.TabIndex = 10;
            this.label17.Text = "Y:";
            // 
            // zpos_textBox
            // 
            this.zpos_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.zpos_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zpos_textBox.Location = new System.Drawing.Point(38, 815);
            this.zpos_textBox.Name = "zpos_textBox";
            this.zpos_textBox.ReadOnly = true;
            this.zpos_textBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.zpos_textBox.Size = new System.Drawing.Size(113, 19);
            this.zpos_textBox.TabIndex = 13;
            this.zpos_textBox.Text = "- - - -";
            this.zpos_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.zpos_textBox, "Current Z position");
            this.zpos_textBox.Click += new System.EventHandler(this.manualLocationEntry_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(3, 816);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(23, 18);
            this.label18.TabIndex = 12;
            this.label18.Text = "Z:";
            // 
            // apos_textBox
            // 
            this.apos_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.apos_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apos_textBox.Location = new System.Drawing.Point(38, 842);
            this.apos_textBox.Name = "apos_textBox";
            this.apos_textBox.ReadOnly = true;
            this.apos_textBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.apos_textBox.Size = new System.Drawing.Size(113, 19);
            this.apos_textBox.TabIndex = 15;
            this.apos_textBox.Text = "- - - -";
            this.apos_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.apos_textBox, "Current A (rotation) position");
            this.apos_textBox.Click += new System.EventHandler(this.manualLocationEntry_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(3, 843);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(23, 18);
            this.label19.TabIndex = 14;
            this.label19.Text = "A:";
            // 
            // SerialMonitor_richTextBox
            // 
            this.SerialMonitor_richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SerialMonitor_richTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SerialMonitor_richTextBox.Location = new System.Drawing.Point(280, 12);
            this.SerialMonitor_richTextBox.Name = "SerialMonitor_richTextBox";
            this.SerialMonitor_richTextBox.ReadOnly = true;
            this.SerialMonitor_richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.SerialMonitor_richTextBox.Size = new System.Drawing.Size(535, 634);
            this.SerialMonitor_richTextBox.TabIndex = 16;
            this.SerialMonitor_richTextBox.TabStop = false;
            this.SerialMonitor_richTextBox.Text = "";
            this.toolTip1.SetToolTip(this.SerialMonitor_richTextBox, "Shows the TinyG communication and diagnostic messages\r\n");
            this.SerialMonitor_richTextBox.TextChanged += new System.EventHandler(this.SerialMonitor_richTextBox_TextChanged);
            // 
            // Test1_button
            // 
            this.Test1_button.Location = new System.Drawing.Point(543, 879);
            this.Test1_button.Name = "Test1_button";
            this.Test1_button.Size = new System.Drawing.Size(108, 23);
            this.Test1_button.TabIndex = 18;
            this.Test1_button.Text = "Pickup This";
            this.Test1_button.UseVisualStyleBackColor = true;
            this.Test1_button.Click += new System.EventHandler(this.pickupThis_buttonClick);
            // 
            // Test2_button
            // 
            this.Test2_button.Location = new System.Drawing.Point(543, 907);
            this.Test2_button.Name = "Test2_button";
            this.Test2_button.Size = new System.Drawing.Size(108, 23);
            this.Test2_button.TabIndex = 19;
            this.Test2_button.Text = "Place Here";
            this.Test2_button.UseVisualStyleBackColor = true;
            this.Test2_button.Click += new System.EventHandler(this.placeHere_buttonClick);
            // 
            // Job_openFileDialog
            // 
            this.Job_openFileDialog.Filter = "LitePlacer Job files (*.lpj)|*.lpj|All files (*.*)|*.*";
            this.Job_openFileDialog.ReadOnlyChecked = true;
            this.Job_openFileDialog.SupportMultiDottedExtensions = true;
            this.Job_openFileDialog.Title = "Job File to Load";
            // 
            // Job_saveFileDialog
            // 
            this.Job_saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // OpticalHome_button
            // 
            this.OpticalHome_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpticalHome_button.Location = new System.Drawing.Point(100, 867);
            this.OpticalHome_button.Name = "OpticalHome_button";
            this.OpticalHome_button.Size = new System.Drawing.Size(75, 45);
            this.OpticalHome_button.TabIndex = 37;
            this.OpticalHome_button.Text = "Home";
            this.toolTip1.SetToolTip(this.OpticalHome_button, "Homes the machine\r\nFirst basic homing using limit swithces,\r\nthen optical homing " +
        "based on home mark.");
            this.OpticalHome_button.UseVisualStyleBackColor = true;
            this.OpticalHome_button.Click += new System.EventHandler(this.OpticalHome_button_Click);
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(317, 752);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(53, 117);
            this.label97.TabIndex = 61;
            this.label97.Text = "Jogging: \r\nF5: <  \r\nF6: >\r\nF7: ^  \r\nF8: v\r\nF9: CCW \r\nF10: CW\r\nF11: Z^  \r\nF12: Z v" +
    "";
            this.toolTip1.SetToolTip(this.label97, resources.GetString("label97.ToolTip"));
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(368, 773);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(52, 91);
            this.label124.TabIndex = 67;
            this.label124.Text = "\r\nAlt+Shift: \r\nAlt: \r\nAlt+Ctrl: \r\nShift: \r\nnone:\r\nCtrl:";
            this.label124.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.Location = new System.Drawing.Point(417, 773);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(91, 91);
            this.label145.TabIndex = 76;
            this.label145.Text = "\r\n100 mm / 90 deg.\r\n10 mm\r\n4 mm\r\n1 mm\r\n0.1 mm\r\n0.01 mm";
            // 
            // Tapes_tabPage
            // 
            this.Tapes_tabPage.Controls.Add(this.AddTape_button);
            this.Tapes_tabPage.Controls.Add(this.label109);
            this.Tapes_tabPage.Controls.Add(this.Tapes_dataGridView);
            this.Tapes_tabPage.Location = new System.Drawing.Point(4, 22);
            this.Tapes_tabPage.Name = "Tapes_tabPage";
            this.Tapes_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.Tapes_tabPage.Size = new System.Drawing.Size(821, 690);
            this.Tapes_tabPage.TabIndex = 6;
            this.Tapes_tabPage.Text = "Tape Positions";
            this.Tapes_tabPage.UseVisualStyleBackColor = true;
            // 
            // AddTape_button
            // 
            this.AddTape_button.Location = new System.Drawing.Point(740, 6);
            this.AddTape_button.Name = "AddTape_button";
            this.AddTape_button.Size = new System.Drawing.Size(75, 23);
            this.AddTape_button.TabIndex = 17;
            this.AddTape_button.Text = "Add";
            this.toolTip1.SetToolTip(this.AddTape_button, "Adds a tape position to the table. \r\nCamera should be on hole 1");
            this.AddTape_button.UseVisualStyleBackColor = true;
            this.AddTape_button.Click += new System.EventHandler(this.AddTape_button_Click);
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label109.Location = new System.Drawing.Point(3, 8);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(113, 16);
            this.label109.TabIndex = 16;
            this.label109.Text = "Tape Positions";
            // 
            // Tapes_dataGridView
            // 
            this.Tapes_dataGridView.AllowUserToAddRows = false;
            this.Tapes_dataGridView.AllowUserToDeleteRows = false;
            this.Tapes_dataGridView.AllowUserToResizeRows = false;
            this.Tapes_dataGridView.AutoGenerateColumns = false;
            this.Tapes_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Tapes_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.NumberPartsAvailable,
            this.TemplateBased,
            this.CurrentPart,
            this.OriginalTapeOrientation,
            this.OriginalPartOrientation,
            this.TapeType,
            this.PartType,
            this.HolePitch,
            this.PartPitch,
            this.HoleToPartX,
            this.HoleToPartY,
            this.pickupZDataGridViewTextBoxColumn,
            this.placeZDataGridViewTextBoxColumn,
            this.TapeAngle});
            this.Tapes_dataGridView.DataSource = this.tapeObjBindingSource;
            this.Tapes_dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Tapes_dataGridView.Location = new System.Drawing.Point(3, 35);
            this.Tapes_dataGridView.MultiSelect = false;
            this.Tapes_dataGridView.Name = "Tapes_dataGridView";
            this.Tapes_dataGridView.RowHeadersVisible = false;
            this.Tapes_dataGridView.RowHeadersWidth = 50;
            this.Tapes_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Tapes_dataGridView.Size = new System.Drawing.Size(814, 652);
            this.Tapes_dataGridView.TabIndex = 15;
            this.Tapes_dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.EndEditModeForTapeSelection);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Width = 60;
            // 
            // NumberPartsAvailable
            // 
            this.NumberPartsAvailable.DataPropertyName = "NumberPartsAvailable";
            this.NumberPartsAvailable.HeaderText = "Count";
            this.NumberPartsAvailable.Name = "NumberPartsAvailable";
            this.NumberPartsAvailable.ReadOnly = true;
            this.NumberPartsAvailable.Width = 50;
            // 
            // TemplateBased
            // 
            this.TemplateBased.DataPropertyName = "TemplateBased";
            this.TemplateBased.HeaderText = "P.B.";
            this.TemplateBased.Name = "TemplateBased";
            this.TemplateBased.ReadOnly = true;
            this.TemplateBased.Width = 30;
            // 
            // CurrentPart
            // 
            this.CurrentPart.DataPropertyName = "CurrentPart";
            this.CurrentPart.HeaderText = "NextPart";
            this.CurrentPart.Name = "CurrentPart";
            this.CurrentPart.Width = 50;
            // 
            // OriginalTapeOrientation
            // 
            this.OriginalTapeOrientation.DataPropertyName = "OriginalTapeOrientation";
            this.OriginalTapeOrientation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OriginalTapeOrientation.HeaderText = "TapeOrien";
            this.OriginalTapeOrientation.Name = "OriginalTapeOrientation";
            this.OriginalTapeOrientation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OriginalTapeOrientation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OriginalTapeOrientation.Width = 60;
            // 
            // OriginalPartOrientation
            // 
            this.OriginalPartOrientation.DataPropertyName = "OriginalPartOrientation";
            this.OriginalPartOrientation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OriginalPartOrientation.HeaderText = "PartOrien";
            this.OriginalPartOrientation.Name = "OriginalPartOrientation";
            this.OriginalPartOrientation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OriginalPartOrientation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OriginalPartOrientation.Width = 60;
            // 
            // TapeType
            // 
            this.TapeType.DataPropertyName = "TapeType";
            this.TapeType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TapeType.HeaderText = "TapeType";
            this.TapeType.Name = "TapeType";
            this.TapeType.Width = 60;
            // 
            // PartType
            // 
            this.PartType.DataPropertyName = "PartType";
            this.PartType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PartType.HeaderText = "PartType";
            this.PartType.Name = "PartType";
            this.PartType.Width = 60;
            // 
            // HolePitch
            // 
            this.HolePitch.DataPropertyName = "HolePitch";
            this.HolePitch.HeaderText = "HolePitch";
            this.HolePitch.Name = "HolePitch";
            this.HolePitch.Width = 55;
            // 
            // PartPitch
            // 
            this.PartPitch.DataPropertyName = "PartPitch";
            this.PartPitch.HeaderText = "PartPitch";
            this.PartPitch.Name = "PartPitch";
            this.PartPitch.Width = 50;
            // 
            // HoleToPartX
            // 
            this.HoleToPartX.DataPropertyName = "HoleToPartSpacingX";
            this.HoleToPartX.HeaderText = "HoleToPartX";
            this.HoleToPartX.Name = "HoleToPartX";
            this.HoleToPartX.Width = 70;
            // 
            // HoleToPartY
            // 
            this.HoleToPartY.DataPropertyName = "HoleToPartSpacingY";
            this.HoleToPartY.HeaderText = "HoleToPartY";
            this.HoleToPartY.Name = "HoleToPartY";
            this.HoleToPartY.ReadOnly = true;
            this.HoleToPartY.Width = 70;
            // 
            // pickupZDataGridViewTextBoxColumn
            // 
            this.pickupZDataGridViewTextBoxColumn.DataPropertyName = "PickupZ";
            this.pickupZDataGridViewTextBoxColumn.HeaderText = "PickupZ";
            this.pickupZDataGridViewTextBoxColumn.Name = "pickupZDataGridViewTextBoxColumn";
            this.pickupZDataGridViewTextBoxColumn.Width = 50;
            // 
            // placeZDataGridViewTextBoxColumn
            // 
            this.placeZDataGridViewTextBoxColumn.DataPropertyName = "PlaceZ";
            this.placeZDataGridViewTextBoxColumn.HeaderText = "PlaceZ";
            this.placeZDataGridViewTextBoxColumn.Name = "placeZDataGridViewTextBoxColumn";
            this.placeZDataGridViewTextBoxColumn.Width = 50;
            // 
            // TapeAngle
            // 
            this.TapeAngle.DataPropertyName = "TapeAngle";
            this.TapeAngle.HeaderText = "Angle";
            this.TapeAngle.Name = "TapeAngle";
            this.TapeAngle.ReadOnly = true;
            // 
            // tapeObjBindingSource
            // 
            this.tapeObjBindingSource.DataSource = typeof(LitePlacer.TapeObj);
            // 
            // GotoPickupCenter_button
            // 
            this.GotoPickupCenter_button.Location = new System.Drawing.Point(0, 0);
            this.GotoPickupCenter_button.Name = "GotoPickupCenter_button";
            this.GotoPickupCenter_button.Size = new System.Drawing.Size(75, 23);
            this.GotoPickupCenter_button.TabIndex = 117;
            // 
            // GotoPCB0_button
            // 
            this.GotoPCB0_button.Location = new System.Drawing.Point(0, 0);
            this.GotoPCB0_button.Name = "GotoPCB0_button";
            this.GotoPCB0_button.Size = new System.Drawing.Size(75, 23);
            this.GotoPCB0_button.TabIndex = 118;
            // 
            // Snapshot_button
            // 
            this.Snapshot_button.Location = new System.Drawing.Point(677, 749);
            this.Snapshot_button.Name = "Snapshot_button";
            this.Snapshot_button.Size = new System.Drawing.Size(75, 23);
            this.Snapshot_button.TabIndex = 30;
            this.Snapshot_button.Text = "Snapshot";
            this.Snapshot_button.UseVisualStyleBackColor = true;
            this.Snapshot_button.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(90, 66);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 42);
            this.button2.TabIndex = 134;
            this.button2.Text = "Calibrate All";
            this.toolTip1.SetToolTip(this.button2, "Do all calibrations, requires the calibration paper");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(721, 524);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 133;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // zoffset_textbox
            // 
            this.zoffset_textbox.Location = new System.Drawing.Point(63, 20);
            this.zoffset_textbox.Name = "zoffset_textbox";
            this.zoffset_textbox.Size = new System.Drawing.Size(36, 20);
            this.zoffset_textbox.TabIndex = 130;
            this.zoffset_textbox.Text = "0";
            this.toolTip1.SetToolTip(this.zoffset_textbox, "Nominal postion difference between\r\nthe needle tip and down camera image center.");
            this.zoffset_textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.z_offset_textbox_keypress);
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Location = new System.Drawing.Point(98, 23);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(23, 13);
            this.label130.TabIndex = 132;
            this.label130.Text = "mm";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(6, 23);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(51, 13);
            this.label131.TabIndex = 131;
            this.label131.Text = "Zo Offset";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label127);
            this.groupBox12.Controls.Add(this.fiducial_designator_regexp_textBox);
            this.groupBox12.Controls.Add(this.button_setTemplate);
            this.groupBox12.Controls.Add(this.label126);
            this.groupBox12.Controls.Add(this.fiducialTemlateMatch_textBox);
            this.groupBox12.Controls.Add(this.button3);
            this.groupBox12.Controls.Add(this.cb_useTemplate);
            this.groupBox12.Location = new System.Drawing.Point(8, 382);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(250, 114);
            this.groupBox12.TabIndex = 129;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Fiducial Settings";
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(109, 43);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(65, 13);
            this.label127.TabIndex = 133;
            this.label127.Text = "Fid RegExp:";
            // 
            // fiducial_designator_regexp_textBox
            // 
            this.fiducial_designator_regexp_textBox.Location = new System.Drawing.Point(176, 39);
            this.fiducial_designator_regexp_textBox.Name = "fiducial_designator_regexp_textBox";
            this.fiducial_designator_regexp_textBox.Size = new System.Drawing.Size(63, 20);
            this.fiducial_designator_regexp_textBox.TabIndex = 132;
            this.fiducial_designator_regexp_textBox.Text = "^FID?";
            this.fiducial_designator_regexp_textBox.TextChanged += new System.EventHandler(this.fiducial_designator_regexp_textBox_TextChanged);
            // 
            // button_setTemplate
            // 
            this.button_setTemplate.Location = new System.Drawing.Point(98, 15);
            this.button_setTemplate.Name = "button_setTemplate";
            this.button_setTemplate.Size = new System.Drawing.Size(35, 23);
            this.button_setTemplate.TabIndex = 131;
            this.button_setTemplate.Text = "...";
            this.button_setTemplate.UseVisualStyleBackColor = true;
            this.button_setTemplate.Click += new System.EventHandler(this.button_setTemplate_Click);
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(7, 43);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(60, 13);
            this.label126.TabIndex = 130;
            this.label126.Text = "Threshold :";
            // 
            // fiducialTemlateMatch_textBox
            // 
            this.fiducialTemlateMatch_textBox.Location = new System.Drawing.Point(70, 40);
            this.fiducialTemlateMatch_textBox.Name = "fiducialTemlateMatch_textBox";
            this.fiducialTemlateMatch_textBox.Size = new System.Drawing.Size(33, 20);
            this.fiducialTemlateMatch_textBox.TabIndex = 129;
            this.fiducialTemlateMatch_textBox.Text = "0.7";
            this.fiducialTemlateMatch_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fiducialTemlateMatch_textBox_KeyPress);
            this.fiducialTemlateMatch_textBox.Leave += new System.EventHandler(this.fiducialTemlateMatch_textBox_Leave);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(10, 66);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 23);
            this.button3.TabIndex = 77;
            this.button3.Text = "Measure PCB Fiducials";
            this.toolTip1.SetToolTip(this.button3, "Re-measures PCB, convertign CAD data coordinates to \r\nmachine coordinates, based " +
        "on PCB fiducials.");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ReMeasure_button_Click);
            // 
            // cb_useTemplate
            // 
            this.cb_useTemplate.AutoSize = true;
            this.cb_useTemplate.Location = new System.Drawing.Point(6, 19);
            this.cb_useTemplate.Name = "cb_useTemplate";
            this.cb_useTemplate.Size = new System.Drawing.Size(92, 17);
            this.cb_useTemplate.TabIndex = 127;
            this.cb_useTemplate.Text = "Use Template";
            this.cb_useTemplate.UseVisualStyleBackColor = true;
            this.cb_useTemplate.CheckedChanged += new System.EventHandler(this.cb_useTemplate_CheckedChanged);
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(2, 128);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(57, 26);
            this.label129.TabIndex = 146;
            this.label129.Text = "CalibMove\r\nDistance";
            // 
            // downCalibMoveDistance_textbox
            // 
            this.downCalibMoveDistance_textbox.Location = new System.Drawing.Point(130, 128);
            this.downCalibMoveDistance_textbox.Name = "downCalibMoveDistance_textbox";
            this.downCalibMoveDistance_textbox.Size = new System.Drawing.Size(46, 20);
            this.downCalibMoveDistance_textbox.TabIndex = 145;
            this.downCalibMoveDistance_textbox.Text = ".25";
            // 
            // SlackMeasurement_label
            // 
            this.SlackMeasurement_label.AutoSize = true;
            this.SlackMeasurement_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SlackMeasurement_label.Location = new System.Drawing.Point(127, 246);
            this.SlackMeasurement_label.Name = "SlackMeasurement_label";
            this.SlackMeasurement_label.Size = new System.Drawing.Size(16, 13);
            this.SlackMeasurement_label.TabIndex = 142;
            this.SlackMeasurement_label.Text = "---";
            this.toolTip1.SetToolTip(this.SlackMeasurement_label, "Set the true size of the box on the image.");
            // 
            // button_camera_calibrate
            // 
            this.button_camera_calibrate.Location = new System.Drawing.Point(130, 156);
            this.button_camera_calibrate.Name = "button_camera_calibrate";
            this.button_camera_calibrate.Size = new System.Drawing.Size(46, 23);
            this.button_camera_calibrate.TabIndex = 140;
            this.button_camera_calibrate.Text = "Calib.";
            this.button_camera_calibrate.UseVisualStyleBackColor = true;
            this.button_camera_calibrate.Click += new System.EventHandler(this.button_camera_calibrate_Click);
            // 
            // DownCameraBoxYmmPerPixel_label
            // 
            this.DownCameraBoxYmmPerPixel_label.AutoSize = true;
            this.DownCameraBoxYmmPerPixel_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownCameraBoxYmmPerPixel_label.Location = new System.Drawing.Point(127, 223);
            this.DownCameraBoxYmmPerPixel_label.Name = "DownCameraBoxYmmPerPixel_label";
            this.DownCameraBoxYmmPerPixel_label.Size = new System.Drawing.Size(16, 13);
            this.DownCameraBoxYmmPerPixel_label.TabIndex = 27;
            this.DownCameraBoxYmmPerPixel_label.Text = "---";
            this.toolTip1.SetToolTip(this.DownCameraBoxYmmPerPixel_label, "Set the true size of the box on the image.");
            // 
            // DownCameraBoxXmmPerPixel_label
            // 
            this.DownCameraBoxXmmPerPixel_label.AutoSize = true;
            this.DownCameraBoxXmmPerPixel_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownCameraBoxXmmPerPixel_label.Location = new System.Drawing.Point(127, 203);
            this.DownCameraBoxXmmPerPixel_label.Name = "DownCameraBoxXmmPerPixel_label";
            this.DownCameraBoxXmmPerPixel_label.Size = new System.Drawing.Size(16, 13);
            this.DownCameraBoxXmmPerPixel_label.TabIndex = 26;
            this.DownCameraBoxXmmPerPixel_label.Text = "---";
            this.toolTip1.SetToolTip(this.DownCameraBoxXmmPerPixel_label, "Set the true size of the box on the image.");
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(182, 74);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(23, 13);
            this.label71.TabIndex = 23;
            this.label71.Text = "mm";
            this.toolTip1.SetToolTip(this.label71, "Set the true size of the box on the image.");
            // 
            // DownCameraBoxX_textBox
            // 
            this.DownCameraBoxX_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownCameraBoxX_textBox.Location = new System.Drawing.Point(130, 45);
            this.DownCameraBoxX_textBox.Name = "DownCameraBoxX_textBox";
            this.DownCameraBoxX_textBox.Size = new System.Drawing.Size(46, 20);
            this.DownCameraBoxX_textBox.TabIndex = 20;
            this.toolTip1.SetToolTip(this.DownCameraBoxX_textBox, "Set the true size of the box on the image.");
            this.DownCameraBoxX_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DownCameraBoxX_textBox_KeyPress);
            this.DownCameraBoxX_textBox.Leave += new System.EventHandler(this.DownCameraBoxX_textBox_Leave);
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(182, 48);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(23, 13);
            this.label70.TabIndex = 22;
            this.label70.Text = "mm";
            this.toolTip1.SetToolTip(this.label70, "Set the true size of the box on the image.");
            // 
            // DownCameraBoxY_textBox
            // 
            this.DownCameraBoxY_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownCameraBoxY_textBox.Location = new System.Drawing.Point(130, 71);
            this.DownCameraBoxY_textBox.Name = "DownCameraBoxY_textBox";
            this.DownCameraBoxY_textBox.Size = new System.Drawing.Size(46, 20);
            this.DownCameraBoxY_textBox.TabIndex = 21;
            this.toolTip1.SetToolTip(this.DownCameraBoxY_textBox, "Set the true size of the box on the image.");
            this.DownCameraBoxY_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DownCameraBoxY_textBox_KeyPress);
            this.DownCameraBoxY_textBox.Leave += new System.EventHandler(this.DownCameraBoxY_textBox_Leave);
            // 
            // DownCamera_Calibration_button
            // 
            this.DownCamera_Calibration_button.Location = new System.Drawing.Point(65, 157);
            this.DownCamera_Calibration_button.Name = "DownCamera_Calibration_button";
            this.DownCamera_Calibration_button.Size = new System.Drawing.Size(46, 23);
            this.DownCamera_Calibration_button.TabIndex = 141;
            this.DownCamera_Calibration_button.Text = "Calib.";
            this.DownCamera_Calibration_button.UseVisualStyleBackColor = true;
            this.DownCamera_Calibration_button.Click += new System.EventHandler(this.UpCamera_Calibration_button_Click);
            // 
            // UpCameraBoxYmmPerPixel_label
            // 
            this.UpCameraBoxYmmPerPixel_label.AutoSize = true;
            this.UpCameraBoxYmmPerPixel_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpCameraBoxYmmPerPixel_label.Location = new System.Drawing.Point(23, 230);
            this.UpCameraBoxYmmPerPixel_label.Name = "UpCameraBoxYmmPerPixel_label";
            this.UpCameraBoxYmmPerPixel_label.Size = new System.Drawing.Size(16, 13);
            this.UpCameraBoxYmmPerPixel_label.TabIndex = 66;
            this.UpCameraBoxYmmPerPixel_label.Text = "---";
            this.toolTip1.SetToolTip(this.UpCameraBoxYmmPerPixel_label, "Set the true size of the box on the image.");
            // 
            // UpCameraBoxXmmPerPixel_label
            // 
            this.UpCameraBoxXmmPerPixel_label.AutoSize = true;
            this.UpCameraBoxXmmPerPixel_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpCameraBoxXmmPerPixel_label.Location = new System.Drawing.Point(23, 205);
            this.UpCameraBoxXmmPerPixel_label.Name = "UpCameraBoxXmmPerPixel_label";
            this.UpCameraBoxXmmPerPixel_label.Size = new System.Drawing.Size(16, 13);
            this.UpCameraBoxXmmPerPixel_label.TabIndex = 65;
            this.UpCameraBoxXmmPerPixel_label.Text = "---";
            this.toolTip1.SetToolTip(this.UpCameraBoxXmmPerPixel_label, "Set the true size of the box on the image.");
            // 
            // UpCameraBoxY_textBox
            // 
            this.UpCameraBoxY_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpCameraBoxY_textBox.Location = new System.Drawing.Point(65, 71);
            this.UpCameraBoxY_textBox.Name = "UpCameraBoxY_textBox";
            this.UpCameraBoxY_textBox.Size = new System.Drawing.Size(46, 20);
            this.UpCameraBoxY_textBox.TabIndex = 60;
            this.toolTip1.SetToolTip(this.UpCameraBoxY_textBox, "Set the true size of the box on the image.");
            this.UpCameraBoxY_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UpCameraBoxY_textBox_KeyPress);
            this.UpCameraBoxY_textBox.Leave += new System.EventHandler(this.UpCameraBoxY_textBox_Leave);
            // 
            // UpCameraBoxX_textBox
            // 
            this.UpCameraBoxX_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpCameraBoxX_textBox.Location = new System.Drawing.Point(65, 45);
            this.UpCameraBoxX_textBox.Name = "UpCameraBoxX_textBox";
            this.UpCameraBoxX_textBox.Size = new System.Drawing.Size(46, 20);
            this.UpCameraBoxX_textBox.TabIndex = 59;
            this.toolTip1.SetToolTip(this.UpCameraBoxX_textBox, "Set the true size of the box on the image.");
            this.UpCameraBoxX_textBox.TextChanged += new System.EventHandler(this.UpCameraBoxX_textBox_TextChanged);
            this.UpCameraBoxX_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UpCameraBoxX_textBox_KeyPress);
            this.UpCameraBoxX_textBox.Leave += new System.EventHandler(this.UpCameraBoxX_textBox_Leave);
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label106.Location = new System.Drawing.Point(21, 48);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(38, 13);
            this.label106.TabIndex = 57;
            this.label106.Text = "Box X:";
            this.toolTip1.SetToolTip(this.label106, "Set the true size of the box on the image.");
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.Location = new System.Drawing.Point(21, 74);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(38, 13);
            this.label105.TabIndex = 58;
            this.label105.Text = "Box Y:";
            this.toolTip1.SetToolTip(this.label105, "Set the true size of the box on the image.");
            // 
            // NeedleOffsetY_textBox
            // 
            this.NeedleOffsetY_textBox.Location = new System.Drawing.Point(49, 218);
            this.NeedleOffsetY_textBox.Name = "NeedleOffsetY_textBox";
            this.NeedleOffsetY_textBox.Size = new System.Drawing.Size(36, 20);
            this.NeedleOffsetY_textBox.TabIndex = 86;
            this.NeedleOffsetY_textBox.Text = "6.99";
            this.toolTip1.SetToolTip(this.NeedleOffsetY_textBox, "Nominal postion difference between\r\nthe needle tip and down camera image center.");
            this.NeedleOffsetY_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NeedleOffsetY_textBox_KeyPress);
            this.NeedleOffsetY_textBox.Leave += new System.EventHandler(this.NeedleOffsetY_textBox_Leave);
            // 
            // NeedleOffsetX_textBox
            // 
            this.NeedleOffsetX_textBox.Location = new System.Drawing.Point(48, 195);
            this.NeedleOffsetX_textBox.Name = "NeedleOffsetX_textBox";
            this.NeedleOffsetX_textBox.Size = new System.Drawing.Size(37, 20);
            this.NeedleOffsetX_textBox.TabIndex = 85;
            this.NeedleOffsetX_textBox.Text = "42.88";
            this.toolTip1.SetToolTip(this.NeedleOffsetX_textBox, "Nominal postion difference between\r\nthe needle tip and down camera image center.");
            this.NeedleOffsetX_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NeedleOffsetX_textBox_KeyPress);
            this.NeedleOffsetX_textBox.Leave += new System.EventHandler(this.NeedleOffsetX_textBox_Leave);
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Location = new System.Drawing.Point(89, 221);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(23, 13);
            this.label149.TabIndex = 90;
            this.label149.Text = "mm";
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Location = new System.Drawing.Point(89, 198);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(23, 13);
            this.label148.TabIndex = 89;
            this.label148.Text = "mm";
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Location = new System.Drawing.Point(26, 221);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(17, 13);
            this.label146.TabIndex = 88;
            this.label146.Text = "Y:";
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Location = new System.Drawing.Point(25, 197);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(17, 13);
            this.label143.TabIndex = 87;
            this.label143.Text = "X:";
            // 
            // Offset2Method_button
            // 
            this.Offset2Method_button.Location = new System.Drawing.Point(23, 167);
            this.Offset2Method_button.Name = "Offset2Method_button";
            this.Offset2Method_button.Size = new System.Drawing.Size(108, 24);
            this.Offset2Method_button.TabIndex = 53;
            this.Offset2Method_button.Tag = "Runs the needle calibration routine";
            this.Offset2Method_button.Text = "Camera Offset";
            this.Offset2Method_button.UseVisualStyleBackColor = true;
            this.Offset2Method_button.Click += new System.EventHandler(this.Offset2Method_button_Click);
            // 
            // instructions_label
            // 
            this.instructions_label.AutoSize = true;
            this.instructions_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.instructions_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructions_label.Location = new System.Drawing.Point(8, 628);
            this.instructions_label.Name = "instructions_label";
            this.instructions_label.Size = new System.Drawing.Size(130, 22);
            this.instructions_label.TabIndex = 50;
            this.instructions_label.Text = "Instructions here";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label115.Location = new System.Drawing.Point(718, 502);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(56, 18);
            this.label115.TabIndex = 49;
            this.label115.Text = "Testing";
            // 
            // ZUp_button
            // 
            this.ZUp_button.Location = new System.Drawing.Point(227, 778);
            this.ZUp_button.Name = "ZUp_button";
            this.ZUp_button.Size = new System.Drawing.Size(84, 23);
            this.ZUp_button.TabIndex = 86;
            this.ZUp_button.Text = "Needle Up";
            this.toolTip1.SetToolTip(this.ZUp_button, "Takes needle up to Z0");
            this.ZUp_button.UseVisualStyleBackColor = true;
            this.ZUp_button.Click += new System.EventHandler(this.ZUp_button_Click);
            // 
            // ZDown_button
            // 
            this.ZDown_button.Location = new System.Drawing.Point(227, 874);
            this.ZDown_button.Name = "ZDown_button";
            this.ZDown_button.Size = new System.Drawing.Size(84, 23);
            this.ZDown_button.TabIndex = 85;
            this.ZDown_button.Text = "To Table";
            this.toolTip1.SetToolTip(this.ZDown_button, "Takes needle down to PCB level");
            this.ZDown_button.UseVisualStyleBackColor = true;
            this.ZDown_button.Click += new System.EventHandler(this.ZDown_button_Click);
            // 
            // tabPageBasicSetup
            // 
            this.tabPageBasicSetup.Controls.Add(this.button7);
            this.tabPageBasicSetup.Controls.Add(this.clearTextBox_button);
            this.tabPageBasicSetup.Controls.Add(this.showTinyGComms_checkbox);
            this.tabPageBasicSetup.Controls.Add(this.tabControl1);
            this.tabPageBasicSetup.Controls.Add(this.label90);
            this.tabPageBasicSetup.Controls.Add(this.SquareCorrection_textBox);
            this.tabPageBasicSetup.Controls.Add(this.SmallMovement_numericUpDown);
            this.tabPageBasicSetup.Controls.Add(this.label87);
            this.tabPageBasicSetup.Controls.Add(this.SlackCompensation_checkBox);
            this.tabPageBasicSetup.Controls.Add(this.Homebutton);
            this.tabPageBasicSetup.Controls.Add(this.HomeZ_button);
            this.tabPageBasicSetup.Controls.Add(this.HomeY_button);
            this.tabPageBasicSetup.Controls.Add(this.SerialMonitor_richTextBox);
            this.tabPageBasicSetup.Controls.Add(this.HomeXY_button);
            this.tabPageBasicSetup.Controls.Add(this.HomeX_button);
            this.tabPageBasicSetup.Controls.Add(this.label4);
            this.tabPageBasicSetup.Controls.Add(this.buttonRefreshPortList);
            this.tabPageBasicSetup.Controls.Add(this.label2);
            this.tabPageBasicSetup.Controls.Add(this.comboBoxSerialPorts);
            this.tabPageBasicSetup.Controls.Add(this.textBoxSendtoTinyG);
            this.tabPageBasicSetup.Location = new System.Drawing.Point(4, 22);
            this.tabPageBasicSetup.Name = "tabPageBasicSetup";
            this.tabPageBasicSetup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBasicSetup.Size = new System.Drawing.Size(821, 690);
            this.tabPageBasicSetup.TabIndex = 1;
            this.tabPageBasicSetup.Text = "Hardware Setup";
            this.tabPageBasicSetup.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(0, 0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 0;
            // 
            // clearTextBox_button
            // 
            this.clearTextBox_button.Location = new System.Drawing.Point(647, 652);
            this.clearTextBox_button.Name = "clearTextBox_button";
            this.clearTextBox_button.Size = new System.Drawing.Size(58, 23);
            this.clearTextBox_button.TabIndex = 91;
            this.clearTextBox_button.Text = "Clear";
            this.clearTextBox_button.UseVisualStyleBackColor = true;
            this.clearTextBox_button.Click += new System.EventHandler(this.clearTextBox_button_Click);
            // 
            // showTinyGComms_checkbox
            // 
            this.showTinyGComms_checkbox.Appearance = System.Windows.Forms.Appearance.Button;
            this.showTinyGComms_checkbox.AutoSize = true;
            this.showTinyGComms_checkbox.Location = new System.Drawing.Point(709, 652);
            this.showTinyGComms_checkbox.Name = "showTinyGComms_checkbox";
            this.showTinyGComms_checkbox.Size = new System.Drawing.Size(106, 23);
            this.showTinyGComms_checkbox.TabIndex = 90;
            this.showTinyGComms_checkbox.Text = "Show Raw Comms";
            this.showTinyGComms_checkbox.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabpage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(6, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(268, 424);
            this.tabControl1.TabIndex = 89;
            // 
            // tabpage1
            // 
            this.tabpage1.Controls.Add(this.panel3);
            this.tabpage1.Controls.Add(this.TestX_button);
            this.tabpage1.Controls.Add(this.TestXY_button);
            this.tabpage1.Controls.Add(this.TestYX_button);
            this.tabpage1.Location = new System.Drawing.Point(4, 22);
            this.tabpage1.Name = "tabpage1";
            this.tabpage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabpage1.Size = new System.Drawing.Size(260, 398);
            this.tabpage1.TabIndex = 0;
            this.tabpage1.Text = "X";
            this.tabpage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label73);
            this.panel3.Controls.Add(this.xsv_maskedTextBox);
            this.panel3.Controls.Add(this.label74);
            this.panel3.Controls.Add(this.label75);
            this.panel3.Controls.Add(this.xjh_maskedTextBox);
            this.panel3.Controls.Add(this.label76);
            this.panel3.Controls.Add(this.Xmax_checkBox);
            this.panel3.Controls.Add(this.Xlim_checkBox);
            this.panel3.Controls.Add(this.Xhome_checkBox);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.xvm_maskedTextBox);
            this.panel3.Controls.Add(this.label27);
            this.panel3.Controls.Add(this.label28);
            this.panel3.Controls.Add(this.label29);
            this.panel3.Controls.Add(this.xjm_maskedTextBox);
            this.panel3.Controls.Add(this.label30);
            this.panel3.Location = new System.Drawing.Point(6, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 350);
            this.panel3.TabIndex = 19;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(3, 189);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(78, 13);
            this.label73.TabIndex = 26;
            this.label73.Text = "Homing speed:";
            // 
            // xsv_maskedTextBox
            // 
            this.xsv_maskedTextBox.Location = new System.Drawing.Point(112, 186);
            this.xsv_maskedTextBox.Mask = "99999";
            this.xsv_maskedTextBox.Name = "xsv_maskedTextBox";
            this.xsv_maskedTextBox.PromptChar = ' ';
            this.xsv_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.xsv_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.xsv_maskedTextBox.TabIndex = 27;
            this.xsv_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.xsv_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.xsv_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.xsv_maskedTextBox_KeyPress);
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(161, 189);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(44, 13);
            this.label74.TabIndex = 25;
            this.label74.Text = "mm/min";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(4, 163);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(107, 13);
            this.label75.TabIndex = 23;
            this.label75.Text = "Homing acceleration:";
            // 
            // xjh_maskedTextBox
            // 
            this.xjh_maskedTextBox.Location = new System.Drawing.Point(112, 160);
            this.xjh_maskedTextBox.Mask = "99999";
            this.xjh_maskedTextBox.Name = "xjh_maskedTextBox";
            this.xjh_maskedTextBox.PromptChar = ' ';
            this.xjh_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.xjh_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.xjh_maskedTextBox.TabIndex = 24;
            this.xjh_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.xjh_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.xjh_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.xjh_maskedTextBox_KeyPress);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(159, 163);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(80, 13);
            this.label76.TabIndex = 22;
            this.label76.Text = "10^6mm/min^3";
            // 
            // Xmax_checkBox
            // 
            this.Xmax_checkBox.AutoSize = true;
            this.Xmax_checkBox.Location = new System.Drawing.Point(7, 133);
            this.Xmax_checkBox.Name = "Xmax_checkBox";
            this.Xmax_checkBox.Size = new System.Drawing.Size(125, 17);
            this.Xmax_checkBox.TabIndex = 21;
            this.Xmax_checkBox.Text = "Max limit switch used";
            this.Xmax_checkBox.UseVisualStyleBackColor = true;
            this.Xmax_checkBox.Click += new System.EventHandler(this.Xmax_checkBox_Click);
            // 
            // Xlim_checkBox
            // 
            this.Xlim_checkBox.AutoSize = true;
            this.Xlim_checkBox.Location = new System.Drawing.Point(7, 110);
            this.Xlim_checkBox.Name = "Xlim_checkBox";
            this.Xlim_checkBox.Size = new System.Drawing.Size(122, 17);
            this.Xlim_checkBox.TabIndex = 20;
            this.Xlim_checkBox.Text = "Min limit switch used";
            this.Xlim_checkBox.UseVisualStyleBackColor = true;
            this.Xlim_checkBox.Click += new System.EventHandler(this.Xlim_checkBox_Click);
            // 
            // Xhome_checkBox
            // 
            this.Xhome_checkBox.AutoSize = true;
            this.Xhome_checkBox.Location = new System.Drawing.Point(7, 87);
            this.Xhome_checkBox.Name = "Xhome_checkBox";
            this.Xhome_checkBox.Size = new System.Drawing.Size(121, 17);
            this.Xhome_checkBox.TabIndex = 19;
            this.Xhome_checkBox.Text = "Homing switch used";
            this.Xhome_checkBox.UseVisualStyleBackColor = true;
            this.Xhome_checkBox.Click += new System.EventHandler(this.Xhome_checkBox_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.tr1_textBox);
            this.panel4.Controls.Add(this.m1deg18_radioButton);
            this.panel4.Controls.Add(this.m1deg09_radioButton);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.label21);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.label23);
            this.panel4.Controls.Add(this.mi1_maskedTextBox);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.label25);
            this.panel4.Location = new System.Drawing.Point(3, 246);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(236, 99);
            this.panel4.TabIndex = 18;
            // 
            // tr1_textBox
            // 
            this.tr1_textBox.Location = new System.Drawing.Point(101, 63);
            this.tr1_textBox.Name = "tr1_textBox";
            this.tr1_textBox.Size = new System.Drawing.Size(54, 20);
            this.tr1_textBox.TabIndex = 19;
            this.tr1_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tr1_textBox_KeyPress);
            // 
            // m1deg18_radioButton
            // 
            this.m1deg18_radioButton.AutoSize = true;
            this.m1deg18_radioButton.Location = new System.Drawing.Point(153, 40);
            this.m1deg18_radioButton.Name = "m1deg18_radioButton";
            this.m1deg18_radioButton.Size = new System.Drawing.Size(64, 17);
            this.m1deg18_radioButton.TabIndex = 28;
            this.m1deg18_radioButton.TabStop = true;
            this.m1deg18_radioButton.Text = "1.8 deg.";
            this.m1deg18_radioButton.UseVisualStyleBackColor = true;
            this.m1deg18_radioButton.Click += new System.EventHandler(this.m1deg18_radioButton_Click);
            // 
            // m1deg09_radioButton
            // 
            this.m1deg09_radioButton.AutoSize = true;
            this.m1deg09_radioButton.Location = new System.Drawing.Point(91, 40);
            this.m1deg09_radioButton.Name = "m1deg09_radioButton";
            this.m1deg09_radioButton.Size = new System.Drawing.Size(64, 17);
            this.m1deg09_radioButton.TabIndex = 27;
            this.m1deg09_radioButton.TabStop = true;
            this.m1deg09_radioButton.Text = "0.9 deg.";
            this.m1deg09_radioButton.UseVisualStyleBackColor = true;
            this.m1deg09_radioButton.Click += new System.EventHandler(this.m1deg09_radioButton_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 68);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 13);
            this.label20.TabIndex = 25;
            this.label20.Text = "Travel per rev. [1tr]:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(161, 68);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(23, 13);
            this.label21.TabIndex = 24;
            this.label21.Text = "mm";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 42);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(87, 13);
            this.label22.TabIndex = 22;
            this.label22.Text = "Step angle [1sa]:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(3, 16);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(86, 13);
            this.label23.TabIndex = 19;
            this.label23.Text = "Microsteps [1mi]:";
            // 
            // mi1_maskedTextBox
            // 
            this.mi1_maskedTextBox.Location = new System.Drawing.Point(101, 14);
            this.mi1_maskedTextBox.Mask = "99999";
            this.mi1_maskedTextBox.Name = "mi1_maskedTextBox";
            this.mi1_maskedTextBox.PromptChar = ' ';
            this.mi1_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mi1_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.mi1_maskedTextBox.TabIndex = 20;
            this.mi1_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mi1_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mi1_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mi1_maskedTextBox_KeyPress);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(150, 16);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(55, 13);
            this.label24.TabIndex = 18;
            this.label24.Text = "[1, 2, 4, 8]";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(3, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(59, 16);
            this.label25.TabIndex = 15;
            this.label25.Text = "Motor1:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(4, 54);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(86, 13);
            this.label26.TabIndex = 16;
            this.label26.Text = "Speed [xvm, xfr]:";
            // 
            // xvm_maskedTextBox
            // 
            this.xvm_maskedTextBox.Location = new System.Drawing.Point(110, 51);
            this.xvm_maskedTextBox.Mask = "99999";
            this.xvm_maskedTextBox.Name = "xvm_maskedTextBox";
            this.xvm_maskedTextBox.PromptChar = ' ';
            this.xvm_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.xvm_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.xvm_maskedTextBox.TabIndex = 17;
            this.xvm_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.xvm_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.xvm_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.xvm_maskedTextBox_KeyPress);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(159, 54);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 13);
            this.label27.TabIndex = 15;
            this.label27.Text = "000 mm/min";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(3, 4);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(26, 20);
            this.label28.TabIndex = 14;
            this.label28.Text = "X:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(3, 28);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(93, 13);
            this.label29.TabIndex = 12;
            this.label29.Text = "Acceleration [xjm]:";
            // 
            // xjm_maskedTextBox
            // 
            this.xjm_maskedTextBox.Location = new System.Drawing.Point(110, 25);
            this.xjm_maskedTextBox.Mask = "99999";
            this.xjm_maskedTextBox.Name = "xjm_maskedTextBox";
            this.xjm_maskedTextBox.PromptChar = ' ';
            this.xjm_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.xjm_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.xjm_maskedTextBox.TabIndex = 13;
            this.xjm_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.xjm_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.xjm_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.xjm_maskedTextBox_KeyPress);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(159, 28);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(80, 13);
            this.label30.TabIndex = 11;
            this.label30.Text = "10^6mm/min^3";
            // 
            // TestX_button
            // 
            this.TestX_button.Location = new System.Drawing.Point(6, 369);
            this.TestX_button.Name = "TestX_button";
            this.TestX_button.Size = new System.Drawing.Size(75, 23);
            this.TestX_button.TabIndex = 22;
            this.TestX_button.Text = "Test X";
            this.toolTip1.SetToolTip(this.TestX_button, "Makes some moves to test axis settings");
            this.TestX_button.UseVisualStyleBackColor = true;
            this.TestX_button.Click += new System.EventHandler(this.TestX_button_Click);
            // 
            // TestXY_button
            // 
            this.TestXY_button.Location = new System.Drawing.Point(87, 369);
            this.TestXY_button.Name = "TestXY_button";
            this.TestXY_button.Size = new System.Drawing.Size(75, 23);
            this.TestXY_button.TabIndex = 24;
            this.TestXY_button.Text = "Test XY";
            this.toolTip1.SetToolTip(this.TestXY_button, "Makes some moves to test axis settings");
            this.TestXY_button.UseVisualStyleBackColor = true;
            this.TestXY_button.Click += new System.EventHandler(this.TestXY_button_Click);
            // 
            // TestYX_button
            // 
            this.TestYX_button.Location = new System.Drawing.Point(169, 369);
            this.TestYX_button.Name = "TestYX_button";
            this.TestYX_button.Size = new System.Drawing.Size(75, 23);
            this.TestYX_button.TabIndex = 55;
            this.TestYX_button.Text = "Test YX";
            this.toolTip1.SetToolTip(this.TestYX_button, "Makes some moves to test axis settings");
            this.TestYX_button.UseVisualStyleBackColor = true;
            this.TestYX_button.Click += new System.EventHandler(this.TestYX_button_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.TestY_button);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(260, 398);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Y";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label77);
            this.panel1.Controls.Add(this.ysv_maskedTextBox);
            this.panel1.Controls.Add(this.label78);
            this.panel1.Controls.Add(this.label79);
            this.panel1.Controls.Add(this.yjh_maskedTextBox);
            this.panel1.Controls.Add(this.label80);
            this.panel1.Controls.Add(this.Ymax_checkBox);
            this.panel1.Controls.Add(this.Ylim_checkBox);
            this.panel1.Controls.Add(this.Yhome_checkBox);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.yvm_maskedTextBox);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.yjm_maskedTextBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 350);
            this.panel1.TabIndex = 14;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(4, 189);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(78, 13);
            this.label77.TabIndex = 32;
            this.label77.Text = "Homing speed:";
            // 
            // ysv_maskedTextBox
            // 
            this.ysv_maskedTextBox.Location = new System.Drawing.Point(110, 186);
            this.ysv_maskedTextBox.Mask = "99999";
            this.ysv_maskedTextBox.Name = "ysv_maskedTextBox";
            this.ysv_maskedTextBox.PromptChar = ' ';
            this.ysv_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ysv_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.ysv_maskedTextBox.TabIndex = 33;
            this.ysv_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ysv_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.ysv_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ysv_maskedTextBox_KeyPress);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(159, 189);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(44, 13);
            this.label78.TabIndex = 31;
            this.label78.Text = "mm/min";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(3, 163);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(107, 13);
            this.label79.TabIndex = 29;
            this.label79.Text = "Homing acceleration:";
            // 
            // yjh_maskedTextBox
            // 
            this.yjh_maskedTextBox.Location = new System.Drawing.Point(110, 160);
            this.yjh_maskedTextBox.Mask = "99999";
            this.yjh_maskedTextBox.Name = "yjh_maskedTextBox";
            this.yjh_maskedTextBox.PromptChar = ' ';
            this.yjh_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.yjh_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.yjh_maskedTextBox.TabIndex = 30;
            this.yjh_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.yjh_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.yjh_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.yjh_maskedTextBox_KeyPress);
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(159, 163);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(80, 13);
            this.label80.TabIndex = 28;
            this.label80.Text = "10^6mm/min^3";
            // 
            // Ymax_checkBox
            // 
            this.Ymax_checkBox.AutoSize = true;
            this.Ymax_checkBox.Location = new System.Drawing.Point(7, 133);
            this.Ymax_checkBox.Name = "Ymax_checkBox";
            this.Ymax_checkBox.Size = new System.Drawing.Size(125, 17);
            this.Ymax_checkBox.TabIndex = 24;
            this.Ymax_checkBox.Text = "Max limit switch used";
            this.Ymax_checkBox.UseVisualStyleBackColor = true;
            this.Ymax_checkBox.Click += new System.EventHandler(this.Ymax_checkBox_Click);
            // 
            // Ylim_checkBox
            // 
            this.Ylim_checkBox.AutoSize = true;
            this.Ylim_checkBox.Location = new System.Drawing.Point(7, 110);
            this.Ylim_checkBox.Name = "Ylim_checkBox";
            this.Ylim_checkBox.Size = new System.Drawing.Size(122, 17);
            this.Ylim_checkBox.TabIndex = 23;
            this.Ylim_checkBox.Text = "Min limit switch used";
            this.Ylim_checkBox.UseVisualStyleBackColor = true;
            this.Ylim_checkBox.Click += new System.EventHandler(this.Ylim_checkBox_Click);
            // 
            // Yhome_checkBox
            // 
            this.Yhome_checkBox.AutoSize = true;
            this.Yhome_checkBox.Location = new System.Drawing.Point(7, 87);
            this.Yhome_checkBox.Name = "Yhome_checkBox";
            this.Yhome_checkBox.Size = new System.Drawing.Size(121, 17);
            this.Yhome_checkBox.TabIndex = 22;
            this.Yhome_checkBox.Text = "Homing switch used";
            this.Yhome_checkBox.UseVisualStyleBackColor = true;
            this.Yhome_checkBox.Click += new System.EventHandler(this.Yhome_checkBox_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tr2_textBox);
            this.panel2.Controls.Add(this.m2deg18_radioButton);
            this.panel2.Controls.Add(this.m2deg09_radioButton);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.mi2_maskedTextBox);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Location = new System.Drawing.Point(3, 246);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(236, 99);
            this.panel2.TabIndex = 18;
            // 
            // tr2_textBox
            // 
            this.tr2_textBox.Location = new System.Drawing.Point(101, 63);
            this.tr2_textBox.Name = "tr2_textBox";
            this.tr2_textBox.Size = new System.Drawing.Size(54, 20);
            this.tr2_textBox.TabIndex = 29;
            this.tr2_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tr2_textBox_KeyPress);
            // 
            // m2deg18_radioButton
            // 
            this.m2deg18_radioButton.AutoSize = true;
            this.m2deg18_radioButton.Location = new System.Drawing.Point(153, 40);
            this.m2deg18_radioButton.Name = "m2deg18_radioButton";
            this.m2deg18_radioButton.Size = new System.Drawing.Size(64, 17);
            this.m2deg18_radioButton.TabIndex = 28;
            this.m2deg18_radioButton.TabStop = true;
            this.m2deg18_radioButton.Text = "1.8 deg.";
            this.m2deg18_radioButton.UseVisualStyleBackColor = true;
            this.m2deg18_radioButton.Click += new System.EventHandler(this.m2deg18_radioButton_Click);
            // 
            // m2deg09_radioButton
            // 
            this.m2deg09_radioButton.AutoSize = true;
            this.m2deg09_radioButton.Location = new System.Drawing.Point(91, 40);
            this.m2deg09_radioButton.Name = "m2deg09_radioButton";
            this.m2deg09_radioButton.Size = new System.Drawing.Size(64, 17);
            this.m2deg09_radioButton.TabIndex = 27;
            this.m2deg09_radioButton.TabStop = true;
            this.m2deg09_radioButton.Text = "0.9 deg.";
            this.m2deg09_radioButton.UseVisualStyleBackColor = true;
            this.m2deg09_radioButton.Click += new System.EventHandler(this.m2deg09_radioButton_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 68);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 13);
            this.label15.TabIndex = 25;
            this.label15.Text = "Travel per rev. [2tr]:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(161, 68);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 13);
            this.label16.TabIndex = 24;
            this.label16.Text = "mm";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "Step angle [2sa]:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Microsteps [2mi]:";
            // 
            // mi2_maskedTextBox
            // 
            this.mi2_maskedTextBox.Location = new System.Drawing.Point(101, 13);
            this.mi2_maskedTextBox.Mask = "99999";
            this.mi2_maskedTextBox.Name = "mi2_maskedTextBox";
            this.mi2_maskedTextBox.PromptChar = ' ';
            this.mi2_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mi2_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.mi2_maskedTextBox.TabIndex = 20;
            this.mi2_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mi2_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mi2_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mi2_maskedTextBox_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(150, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "[1, 2, 4, 8]";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "Motor2:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Speed [yvm, yfr]:";
            // 
            // yvm_maskedTextBox
            // 
            this.yvm_maskedTextBox.Location = new System.Drawing.Point(110, 50);
            this.yvm_maskedTextBox.Mask = "99999";
            this.yvm_maskedTextBox.Name = "yvm_maskedTextBox";
            this.yvm_maskedTextBox.PromptChar = ' ';
            this.yvm_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.yvm_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.yvm_maskedTextBox.TabIndex = 17;
            this.yvm_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.yvm_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.yvm_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.yvm_maskedTextBox_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(159, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "000 mm/min";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Acceleration [yjm]:";
            // 
            // yjm_maskedTextBox
            // 
            this.yjm_maskedTextBox.Location = new System.Drawing.Point(110, 25);
            this.yjm_maskedTextBox.Mask = "99999";
            this.yjm_maskedTextBox.Name = "yjm_maskedTextBox";
            this.yjm_maskedTextBox.PromptChar = ' ';
            this.yjm_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.yjm_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.yjm_maskedTextBox.TabIndex = 13;
            this.yjm_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.yjm_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.yjm_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.yjm_maskedTextBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(159, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "10^6mm/min^3";
            // 
            // TestY_button
            // 
            this.TestY_button.Location = new System.Drawing.Point(6, 368);
            this.TestY_button.Name = "TestY_button";
            this.TestY_button.Size = new System.Drawing.Size(75, 23);
            this.TestY_button.TabIndex = 23;
            this.TestY_button.Text = "Test Y";
            this.toolTip1.SetToolTip(this.TestY_button, "Makes some moves to test axis settings");
            this.TestY_button.UseVisualStyleBackColor = true;
            this.TestY_button.Click += new System.EventHandler(this.TestY_button_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label123);
            this.tabPage3.Controls.Add(this.panel5);
            this.tabPage3.Controls.Add(this.ZTestTravel_textBox);
            this.tabPage3.Controls.Add(this.TestZ_button);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(260, 398);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Z";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(92, 365);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(70, 13);
            this.label123.TabIndex = 83;
            this.label123.Text = "Z Test travel:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label81);
            this.panel5.Controls.Add(this.zsv_maskedTextBox);
            this.panel5.Controls.Add(this.label82);
            this.panel5.Controls.Add(this.label83);
            this.panel5.Controls.Add(this.zjh_maskedTextBox);
            this.panel5.Controls.Add(this.label84);
            this.panel5.Controls.Add(this.Zmax_checkBox);
            this.panel5.Controls.Add(this.Zlim_checkBox);
            this.panel5.Controls.Add(this.Zhome_checkBox);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.label37);
            this.panel5.Controls.Add(this.zvm_maskedTextBox);
            this.panel5.Controls.Add(this.label38);
            this.panel5.Controls.Add(this.label39);
            this.panel5.Controls.Add(this.label40);
            this.panel5.Controls.Add(this.zjm_maskedTextBox);
            this.panel5.Controls.Add(this.label41);
            this.panel5.Location = new System.Drawing.Point(6, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(244, 350);
            this.panel5.TabIndex = 20;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(3, 189);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(78, 13);
            this.label81.TabIndex = 32;
            this.label81.Text = "Homing speed:";
            // 
            // zsv_maskedTextBox
            // 
            this.zsv_maskedTextBox.Location = new System.Drawing.Point(110, 186);
            this.zsv_maskedTextBox.Mask = "99999";
            this.zsv_maskedTextBox.Name = "zsv_maskedTextBox";
            this.zsv_maskedTextBox.PromptChar = ' ';
            this.zsv_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.zsv_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.zsv_maskedTextBox.TabIndex = 33;
            this.zsv_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.zsv_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.zsv_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.zsv_maskedTextBox_KeyPress);
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(159, 189);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(44, 13);
            this.label82.TabIndex = 31;
            this.label82.Text = "mm/min";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(3, 163);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(107, 13);
            this.label83.TabIndex = 29;
            this.label83.Text = "Homing acceleration:";
            // 
            // zjh_maskedTextBox
            // 
            this.zjh_maskedTextBox.Location = new System.Drawing.Point(110, 160);
            this.zjh_maskedTextBox.Mask = "99999";
            this.zjh_maskedTextBox.Name = "zjh_maskedTextBox";
            this.zjh_maskedTextBox.PromptChar = ' ';
            this.zjh_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.zjh_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.zjh_maskedTextBox.TabIndex = 30;
            this.zjh_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.zjh_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.zjh_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.zjh_maskedTextBox_KeyPress);
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(159, 163);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(80, 13);
            this.label84.TabIndex = 28;
            this.label84.Text = "10^6mm/min^3";
            // 
            // Zmax_checkBox
            // 
            this.Zmax_checkBox.AutoSize = true;
            this.Zmax_checkBox.Location = new System.Drawing.Point(7, 133);
            this.Zmax_checkBox.Name = "Zmax_checkBox";
            this.Zmax_checkBox.Size = new System.Drawing.Size(125, 17);
            this.Zmax_checkBox.TabIndex = 24;
            this.Zmax_checkBox.Text = "Max limit switch used";
            this.Zmax_checkBox.UseVisualStyleBackColor = true;
            this.Zmax_checkBox.Click += new System.EventHandler(this.Zmax_checkBox_Click);
            // 
            // Zlim_checkBox
            // 
            this.Zlim_checkBox.AutoSize = true;
            this.Zlim_checkBox.Location = new System.Drawing.Point(7, 110);
            this.Zlim_checkBox.Name = "Zlim_checkBox";
            this.Zlim_checkBox.Size = new System.Drawing.Size(122, 17);
            this.Zlim_checkBox.TabIndex = 23;
            this.Zlim_checkBox.Text = "Min limit switch used";
            this.Zlim_checkBox.UseVisualStyleBackColor = true;
            this.Zlim_checkBox.Click += new System.EventHandler(this.Zlim_checkBox_Click);
            // 
            // Zhome_checkBox
            // 
            this.Zhome_checkBox.AutoSize = true;
            this.Zhome_checkBox.Location = new System.Drawing.Point(7, 87);
            this.Zhome_checkBox.Name = "Zhome_checkBox";
            this.Zhome_checkBox.Size = new System.Drawing.Size(121, 17);
            this.Zhome_checkBox.TabIndex = 22;
            this.Zhome_checkBox.Text = "Homing switch used";
            this.Zhome_checkBox.UseVisualStyleBackColor = true;
            this.Zhome_checkBox.Click += new System.EventHandler(this.Zhome_checkBox_Click);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.tr3_textBox);
            this.panel6.Controls.Add(this.m3deg18_radioButton);
            this.panel6.Controls.Add(this.m3deg09_radioButton);
            this.panel6.Controls.Add(this.label31);
            this.panel6.Controls.Add(this.label32);
            this.panel6.Controls.Add(this.label33);
            this.panel6.Controls.Add(this.mi3_maskedTextBox);
            this.panel6.Controls.Add(this.label34);
            this.panel6.Controls.Add(this.label35);
            this.panel6.Controls.Add(this.label36);
            this.panel6.Location = new System.Drawing.Point(3, 246);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(236, 99);
            this.panel6.TabIndex = 18;
            // 
            // tr3_textBox
            // 
            this.tr3_textBox.Location = new System.Drawing.Point(101, 65);
            this.tr3_textBox.Name = "tr3_textBox";
            this.tr3_textBox.Size = new System.Drawing.Size(54, 20);
            this.tr3_textBox.TabIndex = 21;
            this.tr3_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tr3_textBox_KeyPress);
            // 
            // m3deg18_radioButton
            // 
            this.m3deg18_radioButton.AutoSize = true;
            this.m3deg18_radioButton.Location = new System.Drawing.Point(153, 40);
            this.m3deg18_radioButton.Name = "m3deg18_radioButton";
            this.m3deg18_radioButton.Size = new System.Drawing.Size(64, 17);
            this.m3deg18_radioButton.TabIndex = 28;
            this.m3deg18_radioButton.TabStop = true;
            this.m3deg18_radioButton.Text = "1.8 deg.";
            this.m3deg18_radioButton.UseVisualStyleBackColor = true;
            this.m3deg18_radioButton.Click += new System.EventHandler(this.m3deg18_radioButton_Click);
            // 
            // m3deg09_radioButton
            // 
            this.m3deg09_radioButton.AutoSize = true;
            this.m3deg09_radioButton.Location = new System.Drawing.Point(91, 40);
            this.m3deg09_radioButton.Name = "m3deg09_radioButton";
            this.m3deg09_radioButton.Size = new System.Drawing.Size(64, 17);
            this.m3deg09_radioButton.TabIndex = 27;
            this.m3deg09_radioButton.TabStop = true;
            this.m3deg09_radioButton.Text = "0.9 deg.";
            this.m3deg09_radioButton.UseVisualStyleBackColor = true;
            this.m3deg09_radioButton.Click += new System.EventHandler(this.m3deg09_radioButton_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(3, 68);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(100, 13);
            this.label31.TabIndex = 25;
            this.label31.Text = "Travel per rev. [3tr]:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(161, 68);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(23, 13);
            this.label32.TabIndex = 24;
            this.label32.Text = "mm";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(3, 42);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(87, 13);
            this.label33.TabIndex = 22;
            this.label33.Text = "Step angle [3sa]:";
            // 
            // mi3_maskedTextBox
            // 
            this.mi3_maskedTextBox.Location = new System.Drawing.Point(101, 13);
            this.mi3_maskedTextBox.Mask = "99999";
            this.mi3_maskedTextBox.Name = "mi3_maskedTextBox";
            this.mi3_maskedTextBox.PromptChar = ' ';
            this.mi3_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mi3_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.mi3_maskedTextBox.TabIndex = 20;
            this.mi3_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mi3_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mi3_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mi3_maskedTextBox_KeyPress);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(3, 16);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(86, 13);
            this.label34.TabIndex = 19;
            this.label34.Text = "Microsteps [3mi]:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(150, 16);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(55, 13);
            this.label35.TabIndex = 18;
            this.label35.Text = "[1, 2, 4, 8]";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(3, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(59, 16);
            this.label36.TabIndex = 15;
            this.label36.Text = "Motor3:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(3, 54);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(89, 13);
            this.label37.TabIndex = 16;
            this.label37.Text = "Speed [zvm, zvr]:";
            // 
            // zvm_maskedTextBox
            // 
            this.zvm_maskedTextBox.Location = new System.Drawing.Point(110, 50);
            this.zvm_maskedTextBox.Mask = "99999";
            this.zvm_maskedTextBox.Name = "zvm_maskedTextBox";
            this.zvm_maskedTextBox.PromptChar = ' ';
            this.zvm_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.zvm_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.zvm_maskedTextBox.TabIndex = 17;
            this.zvm_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.zvm_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.zvm_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.zvm_maskedTextBox_KeyPress);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(159, 54);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(44, 13);
            this.label38.TabIndex = 15;
            this.label38.Text = "mm/min";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(3, 4);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(25, 20);
            this.label39.TabIndex = 14;
            this.label39.Text = "Z:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(3, 28);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(73, 13);
            this.label40.TabIndex = 12;
            this.label40.Text = "Acceler. [zjm]:";
            // 
            // zjm_maskedTextBox
            // 
            this.zjm_maskedTextBox.Location = new System.Drawing.Point(110, 25);
            this.zjm_maskedTextBox.Mask = "99999";
            this.zjm_maskedTextBox.Name = "zjm_maskedTextBox";
            this.zjm_maskedTextBox.PromptChar = ' ';
            this.zjm_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.zjm_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.zjm_maskedTextBox.TabIndex = 13;
            this.zjm_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.zjm_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.zjm_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.zjm_maskedTextBox_KeyPress);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(159, 27);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(80, 13);
            this.label41.TabIndex = 11;
            this.label41.Text = "10^6mm/min^3";
            // 
            // ZTestTravel_textBox
            // 
            this.ZTestTravel_textBox.Location = new System.Drawing.Point(169, 362);
            this.ZTestTravel_textBox.Name = "ZTestTravel_textBox";
            this.ZTestTravel_textBox.Size = new System.Drawing.Size(75, 20);
            this.ZTestTravel_textBox.TabIndex = 82;
            this.ZTestTravel_textBox.TextChanged += new System.EventHandler(this.ZTestTravel_textBox_TextChanged);
            // 
            // TestZ_button
            // 
            this.TestZ_button.Location = new System.Drawing.Point(10, 362);
            this.TestZ_button.Name = "TestZ_button";
            this.TestZ_button.Size = new System.Drawing.Size(75, 23);
            this.TestZ_button.TabIndex = 34;
            this.TestZ_button.Text = "Test Z";
            this.toolTip1.SetToolTip(this.TestZ_button, "Makes some moves to test axis settings");
            this.TestZ_button.UseVisualStyleBackColor = true;
            this.TestZ_button.Click += new System.EventHandler(this.TestZ_button_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel7);
            this.tabPage4.Controls.Add(this.TestA_button);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(260, 398);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "A";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.reverseRotation_checkbox);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.label48);
            this.panel7.Controls.Add(this.avm_maskedTextBox);
            this.panel7.Controls.Add(this.label49);
            this.panel7.Controls.Add(this.label50);
            this.panel7.Controls.Add(this.label51);
            this.panel7.Controls.Add(this.ajm_maskedTextBox);
            this.panel7.Controls.Add(this.label52);
            this.panel7.Location = new System.Drawing.Point(6, 6);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(244, 350);
            this.panel7.TabIndex = 21;
            // 
            // reverseRotation_checkbox
            // 
            this.reverseRotation_checkbox.AutoSize = true;
            this.reverseRotation_checkbox.Location = new System.Drawing.Point(10, 76);
            this.reverseRotation_checkbox.Name = "reverseRotation_checkbox";
            this.reverseRotation_checkbox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.reverseRotation_checkbox.Size = new System.Drawing.Size(109, 17);
            this.reverseRotation_checkbox.TabIndex = 19;
            this.reverseRotation_checkbox.Text = "Reverse Rotation";
            this.reverseRotation_checkbox.UseVisualStyleBackColor = true;
            this.reverseRotation_checkbox.Click += new System.EventHandler(this.reverseRotation_checkbox_Click);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.tr4_textBox);
            this.panel8.Controls.Add(this.m4deg18_radioButton);
            this.panel8.Controls.Add(this.m4deg09_radioButton);
            this.panel8.Controls.Add(this.label42);
            this.panel8.Controls.Add(this.label43);
            this.panel8.Controls.Add(this.label44);
            this.panel8.Controls.Add(this.mi4_maskedTextBox);
            this.panel8.Controls.Add(this.label45);
            this.panel8.Controls.Add(this.label46);
            this.panel8.Controls.Add(this.label47);
            this.panel8.Location = new System.Drawing.Point(3, 246);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(236, 99);
            this.panel8.TabIndex = 18;
            // 
            // tr4_textBox
            // 
            this.tr4_textBox.Location = new System.Drawing.Point(101, 65);
            this.tr4_textBox.Name = "tr4_textBox";
            this.tr4_textBox.Size = new System.Drawing.Size(55, 20);
            this.tr4_textBox.TabIndex = 21;
            this.tr4_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tr4_textBox_KeyPress);
            // 
            // m4deg18_radioButton
            // 
            this.m4deg18_radioButton.AutoSize = true;
            this.m4deg18_radioButton.Location = new System.Drawing.Point(153, 40);
            this.m4deg18_radioButton.Name = "m4deg18_radioButton";
            this.m4deg18_radioButton.Size = new System.Drawing.Size(64, 17);
            this.m4deg18_radioButton.TabIndex = 28;
            this.m4deg18_radioButton.TabStop = true;
            this.m4deg18_radioButton.Text = "1.8 deg.";
            this.m4deg18_radioButton.UseVisualStyleBackColor = true;
            this.m4deg18_radioButton.Click += new System.EventHandler(this.m4deg18_radioButton_Click);
            // 
            // m4deg09_radioButton
            // 
            this.m4deg09_radioButton.AutoSize = true;
            this.m4deg09_radioButton.Location = new System.Drawing.Point(91, 40);
            this.m4deg09_radioButton.Name = "m4deg09_radioButton";
            this.m4deg09_radioButton.Size = new System.Drawing.Size(64, 17);
            this.m4deg09_radioButton.TabIndex = 27;
            this.m4deg09_radioButton.TabStop = true;
            this.m4deg09_radioButton.Text = "0.9 deg.";
            this.m4deg09_radioButton.UseVisualStyleBackColor = true;
            this.m4deg09_radioButton.Click += new System.EventHandler(this.m4deg09_radioButton_Click);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(3, 68);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(100, 13);
            this.label42.TabIndex = 25;
            this.label42.Text = "Travel per rev. [4tr]:";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(162, 68);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(23, 13);
            this.label43.TabIndex = 24;
            this.label43.Text = "mm";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(3, 42);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(87, 13);
            this.label44.TabIndex = 22;
            this.label44.Text = "Step angle [4sa]:";
            // 
            // mi4_maskedTextBox
            // 
            this.mi4_maskedTextBox.Location = new System.Drawing.Point(101, 13);
            this.mi4_maskedTextBox.Mask = "99999";
            this.mi4_maskedTextBox.Name = "mi4_maskedTextBox";
            this.mi4_maskedTextBox.PromptChar = ' ';
            this.mi4_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mi4_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.mi4_maskedTextBox.TabIndex = 20;
            this.mi4_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mi4_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mi4_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mi4_maskedTextBox_KeyPress);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(3, 16);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(86, 13);
            this.label45.TabIndex = 19;
            this.label45.Text = "Microsteps [4mi]:";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(150, 16);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(55, 13);
            this.label46.TabIndex = 18;
            this.label46.Text = "[1, 2, 4, 8]";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(3, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(59, 16);
            this.label47.TabIndex = 15;
            this.label47.Text = "Motor4:";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(3, 53);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(91, 13);
            this.label48.TabIndex = 16;
            this.label48.Text = "Speed [avm, avr]:";
            // 
            // avm_maskedTextBox
            // 
            this.avm_maskedTextBox.Location = new System.Drawing.Point(105, 50);
            this.avm_maskedTextBox.Mask = "99999";
            this.avm_maskedTextBox.Name = "avm_maskedTextBox";
            this.avm_maskedTextBox.PromptChar = ' ';
            this.avm_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.avm_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.avm_maskedTextBox.TabIndex = 17;
            this.avm_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.avm_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.avm_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.avm_maskedTextBox_KeyPress);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(154, 54);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(67, 13);
            this.label49.TabIndex = 15;
            this.label49.Text = "000 deg/min";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(3, 4);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(26, 20);
            this.label50.TabIndex = 14;
            this.label50.Text = "A:";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(3, 28);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(74, 13);
            this.label51.TabIndex = 12;
            this.label51.Text = "Acceler. [ajm]:";
            // 
            // ajm_maskedTextBox
            // 
            this.ajm_maskedTextBox.Location = new System.Drawing.Point(105, 25);
            this.ajm_maskedTextBox.Mask = "99999";
            this.ajm_maskedTextBox.Name = "ajm_maskedTextBox";
            this.ajm_maskedTextBox.PromptChar = ' ';
            this.ajm_maskedTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ajm_maskedTextBox.Size = new System.Drawing.Size(43, 20);
            this.ajm_maskedTextBox.TabIndex = 13;
            this.ajm_maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ajm_maskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.ajm_maskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ajm_maskedTextBox_KeyPress);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(151, 27);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(80, 13);
            this.label52.TabIndex = 11;
            this.label52.Text = "10^6mm/min^3";
            // 
            // TestA_button
            // 
            this.TestA_button.Location = new System.Drawing.Point(6, 362);
            this.TestA_button.Name = "TestA_button";
            this.TestA_button.Size = new System.Drawing.Size(75, 23);
            this.TestA_button.TabIndex = 35;
            this.TestA_button.Text = "Test A";
            this.toolTip1.SetToolTip(this.TestA_button, "Makes some moves to test axis settings");
            this.TestA_button.UseVisualStyleBackColor = true;
            this.TestA_button.Click += new System.EventHandler(this.TestA_button_Click);
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(30, 572);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(116, 13);
            this.label90.TabIndex = 74;
            this.label90.Text = "Squareness correction:";
            // 
            // SquareCorrection_textBox
            // 
            this.SquareCorrection_textBox.Location = new System.Drawing.Point(152, 569);
            this.SquareCorrection_textBox.Name = "SquareCorrection_textBox";
            this.SquareCorrection_textBox.Size = new System.Drawing.Size(76, 20);
            this.SquareCorrection_textBox.TabIndex = 73;
            this.toolTip1.SetToolTip(this.SquareCorrection_textBox, "If set to zero: For each mm of +Y movement, the \r\nmachine moves this much in X. S" +
        "et the value\r\nfor square movement.");
            this.SquareCorrection_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SquareCorrection_textBox_KeyPress);
            this.SquareCorrection_textBox.Leave += new System.EventHandler(this.SquareCorrection_textBox_Leave);
            // 
            // SmallMovement_numericUpDown
            // 
            this.SmallMovement_numericUpDown.Location = new System.Drawing.Point(180, 543);
            this.SmallMovement_numericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.SmallMovement_numericUpDown.Name = "SmallMovement_numericUpDown";
            this.SmallMovement_numericUpDown.Size = new System.Drawing.Size(48, 20);
            this.SmallMovement_numericUpDown.TabIndex = 72;
            this.toolTip1.SetToolTip(this.SmallMovement_numericUpDown, "To avoid jerkiness, small movements are done\r\nwith smaller speed. That speed is s" +
        "et here.");
            this.SmallMovement_numericUpDown.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.SmallMovement_numericUpDown.ValueChanged += new System.EventHandler(this.SmallMovement_numericUpDown_ValueChanged);
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(73, 548);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(101, 13);
            this.label87.TabIndex = 71;
            this.label87.Text = "Small moves speed:";
            // 
            // SlackCompensation_checkBox
            // 
            this.SlackCompensation_checkBox.AutoSize = true;
            this.SlackCompensation_checkBox.Location = new System.Drawing.Point(105, 520);
            this.SlackCompensation_checkBox.Name = "SlackCompensation_checkBox";
            this.SlackCompensation_checkBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SlackCompensation_checkBox.Size = new System.Drawing.Size(123, 17);
            this.SlackCompensation_checkBox.TabIndex = 57;
            this.SlackCompensation_checkBox.Text = "Slack Compensation";
            this.toolTip1.SetToolTip(this.SlackCompensation_checkBox, "All movements will go to position from same direction.\r\nIf there is slack in your" +
        " machine, the same side is loaded,\r\npresumably minimizing slack effects.");
            this.SlackCompensation_checkBox.UseVisualStyleBackColor = true;
            this.SlackCompensation_checkBox.Click += new System.EventHandler(this.SlackCompensation_checkBox_Click);
            // 
            // Homebutton
            // 
            this.Homebutton.Location = new System.Drawing.Point(89, 489);
            this.Homebutton.Name = "Homebutton";
            this.Homebutton.Size = new System.Drawing.Size(75, 23);
            this.Homebutton.TabIndex = 17;
            this.Homebutton.Text = "Home XYZ";
            this.toolTip1.SetToolTip(this.Homebutton, "Homes X, Y and Z axis, using limit switch only.");
            this.Homebutton.UseVisualStyleBackColor = true;
            this.Homebutton.Click += new System.EventHandler(this.Homebutton_Click);
            // 
            // HomeZ_button
            // 
            this.HomeZ_button.Location = new System.Drawing.Point(8, 503);
            this.HomeZ_button.Name = "HomeZ_button";
            this.HomeZ_button.Size = new System.Drawing.Size(75, 23);
            this.HomeZ_button.TabIndex = 33;
            this.HomeZ_button.Text = "Home Z";
            this.toolTip1.SetToolTip(this.HomeZ_button, "Homes Z axis, using limit switch only.");
            this.HomeZ_button.UseVisualStyleBackColor = true;
            this.HomeZ_button.Click += new System.EventHandler(this.HomeZ_button_Click);
            // 
            // HomeY_button
            // 
            this.HomeY_button.Location = new System.Drawing.Point(8, 474);
            this.HomeY_button.Name = "HomeY_button";
            this.HomeY_button.Size = new System.Drawing.Size(75, 23);
            this.HomeY_button.TabIndex = 32;
            this.HomeY_button.Text = "Home Y";
            this.toolTip1.SetToolTip(this.HomeY_button, "Homes Y axis, using limit switch only.");
            this.HomeY_button.UseVisualStyleBackColor = true;
            this.HomeY_button.Click += new System.EventHandler(this.HomeY_button_Click);
            // 
            // HomeXY_button
            // 
            this.HomeXY_button.Location = new System.Drawing.Point(89, 460);
            this.HomeXY_button.Name = "HomeXY_button";
            this.HomeXY_button.Size = new System.Drawing.Size(75, 23);
            this.HomeXY_button.TabIndex = 31;
            this.HomeXY_button.Text = "Home XY";
            this.toolTip1.SetToolTip(this.HomeXY_button, "Homes X and Y axis, using limit switch only.");
            this.HomeXY_button.UseVisualStyleBackColor = true;
            this.HomeXY_button.Click += new System.EventHandler(this.HomeXY_button_Click);
            // 
            // HomeX_button
            // 
            this.HomeX_button.Location = new System.Drawing.Point(8, 447);
            this.HomeX_button.Name = "HomeX_button";
            this.HomeX_button.Size = new System.Drawing.Size(75, 23);
            this.HomeX_button.TabIndex = 30;
            this.HomeX_button.Text = "Home X";
            this.toolTip1.SetToolTip(this.HomeX_button, "Homes X axis, using limit switch only.");
            this.HomeX_button.UseVisualStyleBackColor = true;
            this.HomeX_button.Click += new System.EventHandler(this.HomeX_button_Click);
            // 
            // buttonRefreshPortList
            // 
            this.buttonRefreshPortList.Location = new System.Drawing.Point(174, 604);
            this.buttonRefreshPortList.Name = "buttonRefreshPortList";
            this.buttonRefreshPortList.Size = new System.Drawing.Size(58, 23);
            this.buttonRefreshPortList.TabIndex = 4;
            this.buttonRefreshPortList.Text = "Refresh";
            this.toolTip1.SetToolTip(this.buttonRefreshPortList, "Re-scans the serial ports on this computer");
            this.buttonRefreshPortList.UseVisualStyleBackColor = true;
            this.buttonRefreshPortList.Click += new System.EventHandler(this.buttonRefreshPortList_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 607);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Serial Port:";
            // 
            // comboBoxSerialPorts
            // 
            this.comboBoxSerialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialPorts.FormattingEnabled = true;
            this.comboBoxSerialPorts.Location = new System.Drawing.Point(93, 604);
            this.comboBoxSerialPorts.Name = "comboBoxSerialPorts";
            this.comboBoxSerialPorts.Size = new System.Drawing.Size(75, 21);
            this.comboBoxSerialPorts.TabIndex = 0;
            this.toolTip1.SetToolTip(this.comboBoxSerialPorts, "Serial port used by TinyG");
            // 
            // VacuumRelease_textBox
            // 
            this.VacuumRelease_textBox.Location = new System.Drawing.Point(151, 45);
            this.VacuumRelease_textBox.Name = "VacuumRelease_textBox";
            this.VacuumRelease_textBox.Size = new System.Drawing.Size(58, 20);
            this.VacuumRelease_textBox.TabIndex = 81;
            this.toolTip1.SetToolTip(this.VacuumRelease_textBox, "If set to zero: For each mm of +Y movement, the \r\nmachine moves this much in X. S" +
        "et the value\r\nfor square movement.");
            this.VacuumRelease_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VacuumRelease_textBox_KeyPress);
            this.VacuumRelease_textBox.Leave += new System.EventHandler(this.VacuumRelease_textBox_Leave);
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Location = new System.Drawing.Point(16, 48);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(130, 13);
            this.label119.TabIndex = 80;
            this.label119.Text = "Vacuum release time (ms):";
            // 
            // VacuumTime_textBox
            // 
            this.VacuumTime_textBox.Location = new System.Drawing.Point(151, 19);
            this.VacuumTime_textBox.Name = "VacuumTime_textBox";
            this.VacuumTime_textBox.Size = new System.Drawing.Size(58, 20);
            this.VacuumTime_textBox.TabIndex = 79;
            this.toolTip1.SetToolTip(this.VacuumTime_textBox, "If set to zero: For each mm of +Y movement, the \r\nmachine moves this much in X. S" +
        "et the value\r\nfor square movement.");
            this.VacuumTime_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VacuumTime_textBox_KeyPress);
            this.VacuumTime_textBox.Leave += new System.EventHandler(this.VacuumTime_textBox_Leave);
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(17, 22);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(128, 13);
            this.label118.TabIndex = 78;
            this.label118.Text = "Pickup vacuum time (ms):";
            // 
            // labelSerialPortStatus
            // 
            this.labelSerialPortStatus.AutoSize = true;
            this.labelSerialPortStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSerialPortStatus.Location = new System.Drawing.Point(456, 752);
            this.labelSerialPortStatus.Name = "labelSerialPortStatus";
            this.labelSerialPortStatus.Size = new System.Drawing.Size(105, 18);
            this.labelSerialPortStatus.TabIndex = 3;
            this.labelSerialPortStatus.Text = "Not connected";
            this.toolTip1.SetToolTip(this.labelSerialPortStatus, "Connection status");
            // 
            // Z_Backoff_label
            // 
            this.Z_Backoff_label.AutoSize = true;
            this.Z_Backoff_label.Location = new System.Drawing.Point(90, 45);
            this.Z_Backoff_label.Name = "Z_Backoff_label";
            this.Z_Backoff_label.Size = new System.Drawing.Size(47, 13);
            this.Z_Backoff_label.TabIndex = 77;
            this.Z_Backoff_label.Text = "3.00 mm";
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(39, 44);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(47, 13);
            this.label117.TabIndex = 76;
            this.label117.Text = "Backoff:";
            // 
            // Z0toPCB_BasicTab_label
            // 
            this.Z0toPCB_BasicTab_label.AutoSize = true;
            this.Z0toPCB_BasicTab_label.Location = new System.Drawing.Point(85, 93);
            this.Z0toPCB_BasicTab_label.Name = "Z0toPCB_BasicTab_label";
            this.Z0toPCB_BasicTab_label.Size = new System.Drawing.Size(53, 13);
            this.Z0toPCB_BasicTab_label.TabIndex = 53;
            this.Z0toPCB_BasicTab_label.Text = "37.00 mm";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(21, 92);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(65, 13);
            this.label111.TabIndex = 52;
            this.label111.Text = "Z0 to Table:";
            // 
            // SetProbing_button
            // 
            this.SetProbing_button.Location = new System.Drawing.Point(23, 13);
            this.SetProbing_button.Name = "SetProbing_button";
            this.SetProbing_button.Size = new System.Drawing.Size(108, 23);
            this.SetProbing_button.TabIndex = 50;
            this.SetProbing_button.Text = "Backoff";
            this.toolTip1.SetToolTip(this.SetProbing_button, "Runs needle height calibration routine");
            this.SetProbing_button.UseVisualStyleBackColor = true;
            this.SetProbing_button.Click += new System.EventHandler(this.SetProbing_button_Click);
            // 
            // buttonConnectSerial
            // 
            this.buttonConnectSerial.Location = new System.Drawing.Point(567, 752);
            this.buttonConnectSerial.Name = "buttonConnectSerial";
            this.buttonConnectSerial.Size = new System.Drawing.Size(84, 23);
            this.buttonConnectSerial.TabIndex = 2;
            this.buttonConnectSerial.Text = "Connect";
            this.toolTip1.SetToolTip(this.buttonConnectSerial, "Try to connect to TinyG at port shown above");
            this.buttonConnectSerial.UseVisualStyleBackColor = true;
            this.buttonConnectSerial.Click += new System.EventHandler(this.buttonConnectSerial_Click);
            // 
            // MotorPower_checkBox
            // 
            this.MotorPower_checkBox.AutoSize = true;
            this.MotorPower_checkBox.Location = new System.Drawing.Point(320, 878);
            this.MotorPower_checkBox.Name = "MotorPower_checkBox";
            this.MotorPower_checkBox.Size = new System.Drawing.Size(86, 17);
            this.MotorPower_checkBox.TabIndex = 54;
            this.MotorPower_checkBox.Text = "Motor Power";
            this.toolTip1.SetToolTip(this.MotorPower_checkBox, "Motor power on/off \r\n(Motor power on holds machine position)");
            this.MotorPower_checkBox.UseVisualStyleBackColor = true;
            this.MotorPower_checkBox.Click += new System.EventHandler(this.MotorPower_checkBox_Click);
            // 
            // Vacuum_checkBox
            // 
            this.Vacuum_checkBox.AutoSize = true;
            this.Vacuum_checkBox.Location = new System.Drawing.Point(320, 913);
            this.Vacuum_checkBox.Name = "Vacuum_checkBox";
            this.Vacuum_checkBox.Size = new System.Drawing.Size(82, 17);
            this.Vacuum_checkBox.TabIndex = 26;
            this.Vacuum_checkBox.Text = "Vacuum On";
            this.toolTip1.SetToolTip(this.Vacuum_checkBox, "Valve control, vacuum on needle on/off");
            this.Vacuum_checkBox.UseVisualStyleBackColor = true;
            this.Vacuum_checkBox.Click += new System.EventHandler(this.Vacuum_checkBox_Click);
            // 
            // Pump_checkBox
            // 
            this.Pump_checkBox.AutoSize = true;
            this.Pump_checkBox.Location = new System.Drawing.Point(320, 896);
            this.Pump_checkBox.Name = "Pump_checkBox";
            this.Pump_checkBox.Size = new System.Drawing.Size(70, 17);
            this.Pump_checkBox.TabIndex = 25;
            this.Pump_checkBox.Text = "Pump On";
            this.toolTip1.SetToolTip(this.Pump_checkBox, "Vacuum pump on/off");
            this.Pump_checkBox.UseVisualStyleBackColor = true;
            this.Pump_checkBox.Click += new System.EventHandler(this.Pump_checkBox_Click);
            // 
            // RunJob_tabPage
            // 
            this.RunJob_tabPage.Controls.Add(this.groupBox3);
            this.RunJob_tabPage.Controls.Add(this.ignoreErrors_checkbox);
            this.RunJob_tabPage.Controls.Add(this.skippedPlacedComponents_checkBox);
            this.RunJob_tabPage.Controls.Add(this.groupBox2);
            this.RunJob_tabPage.Controls.Add(this.JobOffsetY_textBox);
            this.RunJob_tabPage.Controls.Add(this.JobOffsetX_textBox);
            this.RunJob_tabPage.Controls.Add(this.groupBox1);
            this.RunJob_tabPage.Controls.Add(this.label89);
            this.RunJob_tabPage.Controls.Add(this.label88);
            this.RunJob_tabPage.Controls.Add(this.label86);
            this.RunJob_tabPage.Controls.Add(this.label85);
            this.RunJob_tabPage.Controls.Add(this.JobData_GridView);
            this.RunJob_tabPage.Controls.Add(this.Bottom_checkBox);
            this.RunJob_tabPage.Controls.Add(this.CadData_GridView);
            this.RunJob_tabPage.Location = new System.Drawing.Point(4, 22);
            this.RunJob_tabPage.Name = "RunJob_tabPage";
            this.RunJob_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.RunJob_tabPage.Size = new System.Drawing.Size(821, 690);
            this.RunJob_tabPage.TabIndex = 2;
            this.RunJob_tabPage.Text = "Run Job";
            this.RunJob_tabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label57);
            this.groupBox3.Controls.Add(this.label59);
            this.groupBox3.Controls.Add(this.p2_label);
            this.groupBox3.Controls.Add(this.p1_label);
            this.groupBox3.Location = new System.Drawing.Point(10, 644);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 41);
            this.groupBox3.TabIndex = 75;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Vacuum Info";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(6, 16);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(23, 13);
            this.label57.TabIndex = 35;
            this.label57.Text = "P1:";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(95, 16);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(26, 13);
            this.label59.TabIndex = 36;
            this.label59.Text = "P2 :";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // p2_label
            // 
            this.p2_label.AutoSize = true;
            this.p2_label.Location = new System.Drawing.Point(123, 17);
            this.p2_label.Name = "p2_label";
            this.p2_label.Size = new System.Drawing.Size(0, 13);
            this.p2_label.TabIndex = 38;
            // 
            // p1_label
            // 
            this.p1_label.AutoSize = true;
            this.p1_label.Location = new System.Drawing.Point(29, 18);
            this.p1_label.Name = "p1_label";
            this.p1_label.Size = new System.Drawing.Size(0, 13);
            this.p1_label.TabIndex = 37;
            // 
            // ignoreErrors_checkbox
            // 
            this.ignoreErrors_checkbox.AutoSize = true;
            this.ignoreErrors_checkbox.Checked = true;
            this.ignoreErrors_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreErrors_checkbox.Location = new System.Drawing.Point(443, 287);
            this.ignoreErrors_checkbox.Name = "ignoreErrors_checkbox";
            this.ignoreErrors_checkbox.Size = new System.Drawing.Size(156, 17);
            this.ignoreErrors_checkbox.TabIndex = 74;
            this.ignoreErrors_checkbox.Text = "Skip Over Placement Errors";
            this.ignoreErrors_checkbox.UseVisualStyleBackColor = true;
            // 
            // skippedPlacedComponents_checkBox
            // 
            this.skippedPlacedComponents_checkBox.AutoSize = true;
            this.skippedPlacedComponents_checkBox.Checked = true;
            this.skippedPlacedComponents_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skippedPlacedComponents_checkBox.Location = new System.Drawing.Point(443, 268);
            this.skippedPlacedComponents_checkBox.Name = "skippedPlacedComponents_checkBox";
            this.skippedPlacedComponents_checkBox.Size = new System.Drawing.Size(145, 17);
            this.skippedPlacedComponents_checkBox.TabIndex = 73;
            this.skippedPlacedComponents_checkBox.Text = "Skip Placed Components";
            this.skippedPlacedComponents_checkBox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.NewRow_button);
            this.groupBox2.Controls.Add(this.DeleteComponentGroup_button);
            this.groupBox2.Controls.Add(this.autoMapJob_button);
            this.groupBox2.Controls.Add(this.PlaceAll_button);
            this.groupBox2.Location = new System.Drawing.Point(10, 268);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 136);
            this.groupBox2.TabIndex = 51;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Row Operations";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(135, 77);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(109, 23);
            this.button6.TabIndex = 51;
            this.button6.Text = "Place Selected";
            this.toolTip1.SetToolTip(this.button6, "Places all components on the Job Data table.");
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.PlaceThese_button_Click);
            // 
            // NewRow_button
            // 
            this.NewRow_button.Location = new System.Drawing.Point(11, 19);
            this.NewRow_button.Name = "NewRow_button";
            this.NewRow_button.Size = new System.Drawing.Size(110, 23);
            this.NewRow_button.TabIndex = 22;
            this.NewRow_button.Text = "Add Row";
            this.toolTip1.SetToolTip(this.NewRow_button, "Adds a new row");
            this.NewRow_button.UseVisualStyleBackColor = true;
            this.NewRow_button.Click += new System.EventHandler(this.NewRow_button_Click);
            // 
            // DeleteComponentGroup_button
            // 
            this.DeleteComponentGroup_button.Location = new System.Drawing.Point(13, 48);
            this.DeleteComponentGroup_button.Name = "DeleteComponentGroup_button";
            this.DeleteComponentGroup_button.Size = new System.Drawing.Size(110, 23);
            this.DeleteComponentGroup_button.TabIndex = 16;
            this.DeleteComponentGroup_button.Text = "Delete Row(s)";
            this.toolTip1.SetToolTip(this.DeleteComponentGroup_button, "Deletes selected rows");
            this.DeleteComponentGroup_button.UseVisualStyleBackColor = true;
            this.DeleteComponentGroup_button.Click += new System.EventHandler(this.DeleteComponentGroup_button_Click);
            // 
            // autoMapJob_button
            // 
            this.autoMapJob_button.Location = new System.Drawing.Point(134, 19);
            this.autoMapJob_button.Name = "autoMapJob_button";
            this.autoMapJob_button.Size = new System.Drawing.Size(110, 23);
            this.autoMapJob_button.TabIndex = 50;
            this.autoMapJob_button.Text = "AutoMap Parts";
            this.autoMapJob_button.UseVisualStyleBackColor = true;
            this.autoMapJob_button.Click += new System.EventHandler(this.autoMapJob_button_Click);
            // 
            // PlaceAll_button
            // 
            this.PlaceAll_button.Location = new System.Drawing.Point(135, 48);
            this.PlaceAll_button.Name = "PlaceAll_button";
            this.PlaceAll_button.Size = new System.Drawing.Size(109, 23);
            this.PlaceAll_button.TabIndex = 20;
            this.PlaceAll_button.Text = "Place All";
            this.toolTip1.SetToolTip(this.PlaceAll_button, "Places all components on the Job Data table.");
            this.PlaceAll_button.UseVisualStyleBackColor = true;
            this.PlaceAll_button.Click += new System.EventHandler(this.PlaceAll_button_Click);
            // 
            // JobOffsetY_textBox
            // 
            this.JobOffsetY_textBox.Location = new System.Drawing.Point(769, 6);
            this.JobOffsetY_textBox.Name = "JobOffsetY_textBox";
            this.JobOffsetY_textBox.Size = new System.Drawing.Size(43, 20);
            this.JobOffsetY_textBox.TabIndex = 39;
            this.JobOffsetY_textBox.Text = "0.0";
            this.JobOffsetY_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.JobOffsetY_textBox_KeyPress);
            this.JobOffsetY_textBox.Leave += new System.EventHandler(this.JobOffsetY_textBox_Leave);
            // 
            // JobOffsetX_textBox
            // 
            this.JobOffsetX_textBox.Location = new System.Drawing.Point(633, 4);
            this.JobOffsetX_textBox.Name = "JobOffsetX_textBox";
            this.JobOffsetX_textBox.Size = new System.Drawing.Size(43, 20);
            this.JobOffsetX_textBox.TabIndex = 37;
            this.JobOffsetX_textBox.Text = "0.0";
            this.JobOffsetX_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.JobOffsetX_textBox_KeyPress);
            this.JobOffsetX_textBox.Leave += new System.EventHandler(this.JobOffsetX_textBox_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PCBOffset_label);
            this.groupBox1.Controls.Add(this.placement_Picturebox);
            this.groupBox1.Controls.Add(this.PlacedValue_label);
            this.groupBox1.Controls.Add(this.PlacedComponent_label);
            this.groupBox1.Controls.Add(this.label66);
            this.groupBox1.Controls.Add(this.label58);
            this.groupBox1.Location = new System.Drawing.Point(10, 410);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 230);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Placement Details";
            // 
            // PCBOffset_label
            // 
            this.PCBOffset_label.AutoSize = true;
            this.PCBOffset_label.Location = new System.Drawing.Point(7, 58);
            this.PCBOffset_label.Name = "PCBOffset_label";
            this.PCBOffset_label.Size = new System.Drawing.Size(59, 13);
            this.PCBOffset_label.TabIndex = 39;
            this.PCBOffset_label.Text = "PCB Offset";
            // 
            // placement_Picturebox
            // 
            this.placement_Picturebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.placement_Picturebox.Location = new System.Drawing.Point(45, 89);
            this.placement_Picturebox.Name = "placement_Picturebox";
            this.placement_Picturebox.Size = new System.Drawing.Size(149, 119);
            this.placement_Picturebox.TabIndex = 51;
            this.placement_Picturebox.TabStop = false;
            // 
            // PlacedValue_label
            // 
            this.PlacedValue_label.AutoSize = true;
            this.PlacedValue_label.Location = new System.Drawing.Point(95, 37);
            this.PlacedValue_label.Name = "PlacedValue_label";
            this.PlacedValue_label.Size = new System.Drawing.Size(13, 13);
            this.PlacedValue_label.TabIndex = 33;
            this.PlacedValue_label.Text = "--";
            // 
            // PlacedComponent_label
            // 
            this.PlacedComponent_label.AutoSize = true;
            this.PlacedComponent_label.Location = new System.Drawing.Point(95, 16);
            this.PlacedComponent_label.Name = "PlacedComponent_label";
            this.PlacedComponent_label.Size = new System.Drawing.Size(13, 13);
            this.PlacedComponent_label.TabIndex = 29;
            this.PlacedComponent_label.Text = "--";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(6, 37);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(86, 13);
            this.label66.TabIndex = 28;
            this.label66.Text = "Value | Footprint:";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(6, 16);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(69, 13);
            this.label58.TabIndex = 23;
            this.label58.Text = "Now placing:";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(262, 287);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(78, 20);
            this.label89.TabIndex = 44;
            this.label89.Text = "Job Data:";
            this.toolTip1.SetToolTip(this.label89, "The placement operations are done according\r\nto Job Data specifications.");
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(11, 11);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(86, 20);
            this.label88.TabIndex = 43;
            this.label88.Text = "CAD Data:";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(687, 12);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(68, 13);
            this.label86.TabIndex = 40;
            this.label86.Text = "Job Offset Y:";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(551, 10);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(68, 13);
            this.label85.TabIndex = 38;
            this.label85.Text = "Job Offset X:";
            // 
            // JobData_GridView
            // 
            this.JobData_GridView.AllowUserToAddRows = false;
            this.JobData_GridView.AutoGenerateColumns = false;
            this.JobData_GridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.JobData_GridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.JobData_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.JobData_GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countDataGridViewTextBoxColumn,
            this.componentListDataGridViewTextBoxColumn,
            this.componentTypeDataGridViewTextBoxColumn,
            this.methodDataGridViewComboBoxColumn,
            this.methodParametersDataGridViewTextBoxColumn});
            this.JobData_GridView.DataSource = this.jobDataBindingSource;
            this.JobData_GridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.JobData_GridView.Location = new System.Drawing.Point(266, 310);
            this.JobData_GridView.Name = "JobData_GridView";
            this.JobData_GridView.RowHeadersVisible = false;
            this.JobData_GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.JobData_GridView.Size = new System.Drawing.Size(549, 372);
            this.JobData_GridView.TabIndex = 11;
            this.JobData_GridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.JobData_GridView_CellClick);
            this.JobData_GridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.EndEditModeForTapeSelection);
            // 
            // countDataGridViewTextBoxColumn
            // 
            this.countDataGridViewTextBoxColumn.DataPropertyName = "Count";
            this.countDataGridViewTextBoxColumn.HeaderText = "Count";
            this.countDataGridViewTextBoxColumn.Name = "countDataGridViewTextBoxColumn";
            this.countDataGridViewTextBoxColumn.ReadOnly = true;
            this.countDataGridViewTextBoxColumn.Width = 45;
            // 
            // componentListDataGridViewTextBoxColumn
            // 
            this.componentListDataGridViewTextBoxColumn.DataPropertyName = "ComponentList";
            this.componentListDataGridViewTextBoxColumn.HeaderText = "ComponentList";
            this.componentListDataGridViewTextBoxColumn.Name = "componentListDataGridViewTextBoxColumn";
            this.componentListDataGridViewTextBoxColumn.ReadOnly = true;
            this.componentListDataGridViewTextBoxColumn.Width = 200;
            // 
            // componentTypeDataGridViewTextBoxColumn
            // 
            this.componentTypeDataGridViewTextBoxColumn.DataPropertyName = "ComponentType";
            this.componentTypeDataGridViewTextBoxColumn.HeaderText = "ComponentType";
            this.componentTypeDataGridViewTextBoxColumn.Name = "componentTypeDataGridViewTextBoxColumn";
            this.componentTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // methodDataGridViewComboBoxColumn
            // 
            this.methodDataGridViewComboBoxColumn.DataPropertyName = "Method";
            this.methodDataGridViewComboBoxColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.methodDataGridViewComboBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.methodDataGridViewComboBoxColumn.HeaderText = "Method";
            this.methodDataGridViewComboBoxColumn.Name = "methodDataGridViewComboBoxColumn";
            this.methodDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.methodDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // methodParametersDataGridViewTextBoxColumn
            // 
            this.methodParametersDataGridViewTextBoxColumn.DataPropertyName = "MethodParameters";
            this.methodParametersDataGridViewTextBoxColumn.HeaderText = "MethodParameters";
            this.methodParametersDataGridViewTextBoxColumn.Name = "methodParametersDataGridViewTextBoxColumn";
            // 
            // jobDataBindingSource
            // 
            this.jobDataBindingSource.DataSource = typeof(LitePlacer.JobData);
            // 
            // Bottom_checkBox
            // 
            this.Bottom_checkBox.AutoSize = true;
            this.Bottom_checkBox.Location = new System.Drawing.Point(614, 268);
            this.Bottom_checkBox.Name = "Bottom_checkBox";
            this.Bottom_checkBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Bottom_checkBox.Size = new System.Drawing.Size(141, 17);
            this.Bottom_checkBox.TabIndex = 8;
            this.Bottom_checkBox.Text = "Populating Bottom Layer";
            this.Bottom_checkBox.UseVisualStyleBackColor = true;
            // 
            // CadData_GridView
            // 
            this.CadData_GridView.AllowUserToAddRows = false;
            this.CadData_GridView.AllowUserToDeleteRows = false;
            this.CadData_GridView.AllowUserToResizeRows = false;
            this.CadData_GridView.AutoGenerateColumns = false;
            this.CadData_GridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.CadData_GridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CadData_GridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.CadData_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CadData_GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.designatorDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn1,
            this.footprintDataGridViewTextBoxColumn,
            this.xnominalDataGridViewTextBoxColumn,
            this.ynominalDataGridViewTextBoxColumn,
            this.rotationDataGridViewTextBoxColumn,
            this.methodDataGridViewTextBoxColumn,
            this.isFiducialDataGridViewCheckBoxColumn});
            this.CadData_GridView.DataSource = this.physicalComponentBindingSource;
            this.CadData_GridView.Location = new System.Drawing.Point(10, 34);
            this.CadData_GridView.Name = "CadData_GridView";
            this.CadData_GridView.RowHeadersVisible = false;
            this.CadData_GridView.RowHeadersWidth = 16;
            this.CadData_GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.CadData_GridView.Size = new System.Drawing.Size(805, 228);
            this.CadData_GridView.TabIndex = 5;
            // 
            // designatorDataGridViewTextBoxColumn
            // 
            this.designatorDataGridViewTextBoxColumn.DataPropertyName = "Designator";
            this.designatorDataGridViewTextBoxColumn.HeaderText = "Designator";
            this.designatorDataGridViewTextBoxColumn.Name = "designatorDataGridViewTextBoxColumn";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Type";
            this.dataGridViewTextBoxColumn1.HeaderText = "Type";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // footprintDataGridViewTextBoxColumn
            // 
            this.footprintDataGridViewTextBoxColumn.DataPropertyName = "Footprint";
            this.footprintDataGridViewTextBoxColumn.HeaderText = "Footprint";
            this.footprintDataGridViewTextBoxColumn.Name = "footprintDataGridViewTextBoxColumn";
            // 
            // xnominalDataGridViewTextBoxColumn
            // 
            this.xnominalDataGridViewTextBoxColumn.DataPropertyName = "X_nominal";
            this.xnominalDataGridViewTextBoxColumn.HeaderText = "X_nominal";
            this.xnominalDataGridViewTextBoxColumn.Name = "xnominalDataGridViewTextBoxColumn";
            // 
            // ynominalDataGridViewTextBoxColumn
            // 
            this.ynominalDataGridViewTextBoxColumn.DataPropertyName = "Y_nominal";
            this.ynominalDataGridViewTextBoxColumn.HeaderText = "Y_nominal";
            this.ynominalDataGridViewTextBoxColumn.Name = "ynominalDataGridViewTextBoxColumn";
            // 
            // rotationDataGridViewTextBoxColumn
            // 
            this.rotationDataGridViewTextBoxColumn.DataPropertyName = "Rotation";
            this.rotationDataGridViewTextBoxColumn.HeaderText = "Rotation";
            this.rotationDataGridViewTextBoxColumn.Name = "rotationDataGridViewTextBoxColumn";
            // 
            // methodDataGridViewTextBoxColumn
            // 
            this.methodDataGridViewTextBoxColumn.DataPropertyName = "Method";
            this.methodDataGridViewTextBoxColumn.HeaderText = "Method";
            this.methodDataGridViewTextBoxColumn.Name = "methodDataGridViewTextBoxColumn";
            // 
            // isFiducialDataGridViewCheckBoxColumn
            // 
            this.isFiducialDataGridViewCheckBoxColumn.DataPropertyName = "IsFiducial";
            this.isFiducialDataGridViewCheckBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.isFiducialDataGridViewCheckBoxColumn.HeaderText = "IsFiducial";
            this.isFiducialDataGridViewCheckBoxColumn.Name = "isFiducialDataGridViewCheckBoxColumn";
            this.isFiducialDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // physicalComponentBindingSource
            // 
            this.physicalComponentBindingSource.DataSource = typeof(LitePlacer.PhysicalComponent);
            // 
            // AbortPlacement_button
            // 
            this.AbortPlacement_button.BackColor = System.Drawing.Color.Red;
            this.AbortPlacement_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AbortPlacement_button.Location = new System.Drawing.Point(428, 871);
            this.AbortPlacement_button.Name = "AbortPlacement_button";
            this.AbortPlacement_button.Size = new System.Drawing.Size(109, 35);
            this.AbortPlacement_button.TabIndex = 36;
            this.AbortPlacement_button.Text = "Stop";
            this.toolTip1.SetToolTip(this.AbortPlacement_button, "Aborts the whole operation.");
            this.AbortPlacement_button.UseVisualStyleBackColor = false;
            this.AbortPlacement_button.Click += new System.EventHandler(this.AbortPlacement_button_Click);
            // 
            // PausePlacement_button
            // 
            this.PausePlacement_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.PausePlacement_button.Location = new System.Drawing.Point(428, 907);
            this.PausePlacement_button.Name = "PausePlacement_button";
            this.PausePlacement_button.Size = new System.Drawing.Size(109, 23);
            this.PausePlacement_button.TabIndex = 35;
            this.PausePlacement_button.Text = "Pause";
            this.toolTip1.SetToolTip(this.PausePlacement_button, "Temporary pause");
            this.PausePlacement_button.UseVisualStyleBackColor = false;
            this.PausePlacement_button.Click += new System.EventHandler(this.PausePlacement_button_Click);
            // 
            // ReMeasure_button
            // 
            this.ReMeasure_button.BackColor = System.Drawing.Color.Red;
            this.ReMeasure_button.Location = new System.Drawing.Point(5, 918);
            this.ReMeasure_button.Name = "ReMeasure_button";
            this.ReMeasure_button.Size = new System.Drawing.Size(170, 23);
            this.ReMeasure_button.TabIndex = 48;
            this.ReMeasure_button.Text = "Re-measure PCB Fiducials";
            this.toolTip1.SetToolTip(this.ReMeasure_button, "Re-measures PCB, convertign CAD data coordinates to \r\nmachine coordinates, based " +
        "on PCB fiducials.");
            this.ReMeasure_button.UseVisualStyleBackColor = false;
            this.ReMeasure_button.Click += new System.EventHandler(this.ReMeasure_button_Click);
            // 
            // ChangeNeedle_button
            // 
            this.ChangeNeedle_button.Location = new System.Drawing.Point(541, 809);
            this.ChangeNeedle_button.Name = "ChangeNeedle_button";
            this.ChangeNeedle_button.Size = new System.Drawing.Size(110, 23);
            this.ChangeNeedle_button.TabIndex = 72;
            this.ChangeNeedle_button.Text = "Change Needle";
            this.ChangeNeedle_button.UseVisualStyleBackColor = true;
            this.ChangeNeedle_button.Click += new System.EventHandler(this.ChangeNeedle_button_Click);
            // 
            // needle_calibration_test_button
            // 
            this.needle_calibration_test_button.Location = new System.Drawing.Point(13, 330);
            this.needle_calibration_test_button.Name = "needle_calibration_test_button";
            this.needle_calibration_test_button.Size = new System.Drawing.Size(125, 23);
            this.needle_calibration_test_button.TabIndex = 76;
            this.needle_calibration_test_button.Text = "Needle Cal. Test";
            this.toolTip1.SetToolTip(this.needle_calibration_test_button, "Will see if the corrects to the needle are correct");
            this.needle_calibration_test_button.UseVisualStyleBackColor = true;
            this.needle_calibration_test_button.Click += new System.EventHandler(this.needle_calibration_test_button_Click);
            // 
            // StopDemo_button
            // 
            this.StopDemo_button.Location = new System.Drawing.Point(667, 749);
            this.StopDemo_button.Name = "StopDemo_button";
            this.StopDemo_button.Size = new System.Drawing.Size(75, 23);
            this.StopDemo_button.TabIndex = 74;
            this.StopDemo_button.Text = "Stop Demo";
            this.StopDemo_button.UseVisualStyleBackColor = true;
            this.StopDemo_button.Visible = false;
            // 
            // Demo_button
            // 
            this.Demo_button.Location = new System.Drawing.Point(657, 749);
            this.Demo_button.Name = "Demo_button";
            this.Demo_button.Size = new System.Drawing.Size(75, 23);
            this.Demo_button.TabIndex = 73;
            this.Demo_button.Text = "Start Demo";
            this.Demo_button.UseVisualStyleBackColor = true;
            this.Demo_button.Visible = false;
            // 
            // tabControlPages
            // 
            this.tabControlPages.Controls.Add(this.RunJob_tabPage);
            this.tabControlPages.Controls.Add(this.tabPageBasicSetup);
            this.tabControlPages.Controls.Add(this.Tapes_tabPage);
            this.tabControlPages.Controls.Add(this.tabPage5);
            this.tabControlPages.Location = new System.Drawing.Point(6, 27);
            this.tabControlPages.Name = "tabControlPages";
            this.tabControlPages.SelectedIndex = 0;
            this.tabControlPages.Size = new System.Drawing.Size(829, 716);
            this.tabControlPages.TabIndex = 3;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox9);
            this.tabPage5.Controls.Add(this.groupBox8);
            this.tabPage5.Controls.Add(this.groupBox6);
            this.tabPage5.Controls.Add(this.groupBox5);
            this.tabPage5.Controls.Add(this.groupBox7);
            this.tabPage5.Controls.Add(this.groupBox12);
            this.tabPage5.Controls.Add(this.groupBox4);
            this.tabPage5.Controls.Add(this.button1);
            this.tabPage5.Controls.Add(this.label115);
            this.tabPage5.Controls.Add(this.instructions_label);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(821, 690);
            this.tabPage5.TabIndex = 7;
            this.tabPage5.Text = "Calibrations & Locations";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(173, 19);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 42);
            this.button8.TabIndex = 154;
            this.button8.Text = "Verify placements";
            this.toolTip1.SetToolTip(this.button8, "Verify fiducial and a few random component placements");
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // calibrateZXYCompensation
            // 
            this.calibrateZXYCompensation.Location = new System.Drawing.Point(90, 19);
            this.calibrateZXYCompensation.Name = "calibrateZXYCompensation";
            this.calibrateZXYCompensation.Size = new System.Drawing.Size(75, 42);
            this.calibrateZXYCompensation.TabIndex = 153;
            this.calibrateZXYCompensation.Text = "Calibrate Z XY comp.";
            this.toolTip1.SetToolTip(this.calibrateZXYCompensation, "Calibrate the XY correction over the Z axis using the calibration paper");
            this.calibrateZXYCompensation.UseVisualStyleBackColor = true;
            this.calibrateZXYCompensation.Click += new System.EventHandler(this.calibrateZXYCompensation_Click);
            // 
            // calibrateSkew
            // 
            this.calibrateSkew.Location = new System.Drawing.Point(9, 66);
            this.calibrateSkew.Name = "calibrateSkew";
            this.calibrateSkew.Size = new System.Drawing.Size(75, 42);
            this.calibrateSkew.TabIndex = 152;
            this.calibrateSkew.Text = "calibrate skew";
            this.toolTip1.SetToolTip(this.calibrateSkew, "Calibrate the squareness correction using the calibration paper");
            this.calibrateSkew.UseVisualStyleBackColor = true;
            this.calibrateSkew.Click += new System.EventHandler(this.calibrateSkew_Click);
            // 
            // calibrateXYmmRev
            // 
            this.calibrateXYmmRev.Location = new System.Drawing.Point(9, 19);
            this.calibrateXYmmRev.Name = "calibrateXYmmRev";
            this.calibrateXYmmRev.Size = new System.Drawing.Size(75, 42);
            this.calibrateXYmmRev.TabIndex = 151;
            this.calibrateXYmmRev.Text = "calibrate XY mm/rev";
            this.toolTip1.SetToolTip(this.calibrateXYmmRev, "Calibrate the mm/revolution movements of the X & Y axis using the calibration pap" +
        "er");
            this.calibrateXYmmRev.UseVisualStyleBackColor = true;
            this.calibrateXYmmRev.Click += new System.EventHandler(this.calibrateXYmmRev_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.zoffset_textbox);
            this.groupBox8.Controls.Add(this.label130);
            this.groupBox8.Controls.Add(this.label131);
            this.groupBox8.Location = new System.Drawing.Point(264, 382);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(141, 114);
            this.groupBox8.TabIndex = 150;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Movement Speedup";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.vacuumDeltaADC_textbox);
            this.groupBox6.Controls.Add(this.label56);
            this.groupBox6.Controls.Add(this.pressureSenstorPresent_button);
            this.groupBox6.Controls.Add(this.VacuumTime_textBox);
            this.groupBox6.Controls.Add(this.VacuumRelease_textBox);
            this.groupBox6.Controls.Add(this.label118);
            this.groupBox6.Controls.Add(this.label119);
            this.groupBox6.Location = new System.Drawing.Point(8, 502);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(239, 123);
            this.groupBox6.TabIndex = 149;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Vacuum Settings";
            // 
            // vacuumDeltaADC_textbox
            // 
            this.vacuumDeltaADC_textbox.Location = new System.Drawing.Point(151, 94);
            this.vacuumDeltaADC_textbox.Name = "vacuumDeltaADC_textbox";
            this.vacuumDeltaADC_textbox.Size = new System.Drawing.Size(58, 20);
            this.vacuumDeltaADC_textbox.TabIndex = 136;
            this.toolTip1.SetToolTip(this.vacuumDeltaADC_textbox, "If set to zero: For each mm of +Y movement, the \r\nmachine moves this much in X. S" +
        "et the value\r\nfor square movement.");
            this.vacuumDeltaADC_textbox.TextChanged += new System.EventHandler(this.vacuumDeltaADC_textbox_TextChanged);
            this.vacuumDeltaADC_textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HandleNumericKeypress);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(16, 97);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(105, 13);
            this.label56.TabIndex = 135;
            this.label56.Text = "ADC Delta Expected";
            // 
            // pressureSenstorPresent_button
            // 
            this.pressureSenstorPresent_button.AutoSize = true;
            this.pressureSenstorPresent_button.Location = new System.Drawing.Point(22, 71);
            this.pressureSenstorPresent_button.Name = "pressureSenstorPresent_button";
            this.pressureSenstorPresent_button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pressureSenstorPresent_button.Size = new System.Drawing.Size(142, 17);
            this.pressureSenstorPresent_button.TabIndex = 134;
            this.pressureSenstorPresent_button.Text = "Pressure Sensor Present";
            this.pressureSenstorPresent_button.UseVisualStyleBackColor = true;
            this.pressureSenstorPresent_button.CheckedChanged += new System.EventHandler(this.pressureSenstorPresent_button_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.locations_dataGridView);
            this.groupBox5.Controls.Add(this.locationSet_button);
            this.groupBox5.Controls.Add(this.locationGoTo_button);
            this.groupBox5.Controls.Add(this.locationDelete_button);
            this.groupBox5.Controls.Add(this.locationAdd_button);
            this.groupBox5.Location = new System.Drawing.Point(416, 14);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(388, 481);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Locations";
            // 
            // locations_dataGridView
            // 
            this.locations_dataGridView.AllowUserToAddRows = false;
            this.locations_dataGridView.AllowUserToDeleteRows = false;
            this.locations_dataGridView.AutoGenerateColumns = false;
            this.locations_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.locations_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.xDataGridViewTextBoxColumn,
            this.yDataGridViewTextBoxColumn});
            this.locations_dataGridView.DataSource = this.namedLocationBindingSource;
            this.locations_dataGridView.Location = new System.Drawing.Point(15, 21);
            this.locations_dataGridView.MultiSelect = false;
            this.locations_dataGridView.Name = "locations_dataGridView";
            this.locations_dataGridView.RowHeadersVisible = false;
            this.locations_dataGridView.Size = new System.Drawing.Size(274, 436);
            this.locations_dataGridView.TabIndex = 1;
            this.locations_dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.locations_dataGridView_CellBeginEdit);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 150;
            // 
            // xDataGridViewTextBoxColumn
            // 
            this.xDataGridViewTextBoxColumn.DataPropertyName = "X";
            this.xDataGridViewTextBoxColumn.HeaderText = "X";
            this.xDataGridViewTextBoxColumn.Name = "xDataGridViewTextBoxColumn";
            this.xDataGridViewTextBoxColumn.Width = 60;
            // 
            // yDataGridViewTextBoxColumn
            // 
            this.yDataGridViewTextBoxColumn.DataPropertyName = "Y";
            this.yDataGridViewTextBoxColumn.HeaderText = "Y";
            this.yDataGridViewTextBoxColumn.Name = "yDataGridViewTextBoxColumn";
            this.yDataGridViewTextBoxColumn.Width = 60;
            // 
            // namedLocationBindingSource
            // 
            this.namedLocationBindingSource.DataSource = typeof(LitePlacer.NamedLocation);
            // 
            // locationSet_button
            // 
            this.locationSet_button.Location = new System.Drawing.Point(295, 108);
            this.locationSet_button.Name = "locationSet_button";
            this.locationSet_button.Size = new System.Drawing.Size(85, 23);
            this.locationSet_button.TabIndex = 3;
            this.locationSet_button.Text = "Set";
            this.locationSet_button.UseVisualStyleBackColor = true;
            this.locationSet_button.Click += new System.EventHandler(this.locationSet_button_Click);
            // 
            // locationGoTo_button
            // 
            this.locationGoTo_button.Location = new System.Drawing.Point(295, 79);
            this.locationGoTo_button.Name = "locationGoTo_button";
            this.locationGoTo_button.Size = new System.Drawing.Size(85, 23);
            this.locationGoTo_button.TabIndex = 2;
            this.locationGoTo_button.Text = "Go To";
            this.locationGoTo_button.UseVisualStyleBackColor = true;
            this.locationGoTo_button.Click += new System.EventHandler(this.locationGoTo_button_Click_1);
            // 
            // locationDelete_button
            // 
            this.locationDelete_button.Location = new System.Drawing.Point(295, 50);
            this.locationDelete_button.Name = "locationDelete_button";
            this.locationDelete_button.Size = new System.Drawing.Size(85, 23);
            this.locationDelete_button.TabIndex = 1;
            this.locationDelete_button.Text = "Delete";
            this.locationDelete_button.UseVisualStyleBackColor = true;
            this.locationDelete_button.Click += new System.EventHandler(this.locationDelete_button_Click);
            // 
            // locationAdd_button
            // 
            this.locationAdd_button.Location = new System.Drawing.Point(295, 21);
            this.locationAdd_button.Name = "locationAdd_button";
            this.locationAdd_button.Size = new System.Drawing.Size(85, 23);
            this.locationAdd_button.TabIndex = 0;
            this.locationAdd_button.Text = "Add";
            this.locationAdd_button.UseVisualStyleBackColor = true;
            this.locationAdd_button.Click += new System.EventHandler(this.locationAdd_button_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ZCorrectionDeltaZ);
            this.groupBox7.Controls.Add(this.label62);
            this.groupBox7.Controls.Add(this.ZCorrectionY);
            this.groupBox7.Controls.Add(this.label61);
            this.groupBox7.Controls.Add(this.ZCorrectionX);
            this.groupBox7.Controls.Add(this.label60);
            this.groupBox7.Controls.Add(this.HeightOffsetLabel);
            this.groupBox7.Controls.Add(this.HeightOffsetButton);
            this.groupBox7.Controls.Add(this.label54);
            this.groupBox7.Controls.Add(this.label53);
            this.groupBox7.Controls.Add(this.UpCalibMoveDistance_textbox);
            this.groupBox7.Controls.Add(this.needleCalibrationHeight_textbox);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.UpCameraBoxX_textBox);
            this.groupBox7.Controls.Add(this.label129);
            this.groupBox7.Controls.Add(this.label105);
            this.groupBox7.Controls.Add(this.DownCamera_Calibration_button);
            this.groupBox7.Controls.Add(this.SlackMeasurement_label);
            this.groupBox7.Controls.Add(this.downCalibMoveDistance_textbox);
            this.groupBox7.Controls.Add(this.label106);
            this.groupBox7.Controls.Add(this.UpCameraBoxY_textBox);
            this.groupBox7.Controls.Add(this.UpCameraBoxXmmPerPixel_label);
            this.groupBox7.Controls.Add(this.UpCameraBoxYmmPerPixel_label);
            this.groupBox7.Controls.Add(this.button_camera_calibrate);
            this.groupBox7.Controls.Add(this.DownCameraBoxX_textBox);
            this.groupBox7.Controls.Add(this.label70);
            this.groupBox7.Controls.Add(this.label71);
            this.groupBox7.Controls.Add(this.DownCameraBoxY_textBox);
            this.groupBox7.Controls.Add(this.DownCameraBoxXmmPerPixel_label);
            this.groupBox7.Controls.Add(this.DownCameraBoxYmmPerPixel_label);
            this.groupBox7.Location = new System.Drawing.Point(165, 14);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(240, 362);
            this.groupBox7.TabIndex = 148;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Camera mmPerPixel";
            // 
            // ZCorrectionDeltaZ
            // 
            this.ZCorrectionDeltaZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZCorrectionDeltaZ.Location = new System.Drawing.Point(65, 294);
            this.ZCorrectionDeltaZ.Name = "ZCorrectionDeltaZ";
            this.ZCorrectionDeltaZ.Size = new System.Drawing.Size(46, 20);
            this.ZCorrectionDeltaZ.TabIndex = 159;
            this.toolTip1.SetToolTip(this.ZCorrectionDeltaZ, "The Z range from 0 that the corrections span");
            this.ZCorrectionDeltaZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ZCorrectionDeltaZ_KeyPress);
            this.ZCorrectionDeltaZ.Leave += new System.EventHandler(this.ZCorrectionDeltaZ_Leave);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(21, 297);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(43, 13);
            this.label62.TabIndex = 158;
            this.label62.Text = "Z delta:";
            this.toolTip1.SetToolTip(this.label62, "Set the true size of the box on the image.");
            // 
            // ZCorrectionY
            // 
            this.ZCorrectionY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZCorrectionY.Location = new System.Drawing.Point(162, 265);
            this.ZCorrectionY.Name = "ZCorrectionY";
            this.ZCorrectionY.Size = new System.Drawing.Size(46, 20);
            this.ZCorrectionY.TabIndex = 157;
            this.toolTip1.SetToolTip(this.ZCorrectionY, "Correction on the Y axis over the entire Z delta");
            this.ZCorrectionY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ZCorrectionY_KeyPress);
            this.ZCorrectionY.Leave += new System.EventHandler(this.ZCorrectionY_Leave);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(118, 268);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(38, 13);
            this.label61.TabIndex = 156;
            this.label61.Text = "Y corr:";
            this.toolTip1.SetToolTip(this.label61, "Set the true size of the box on the image.");
            // 
            // ZCorrectionX
            // 
            this.ZCorrectionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZCorrectionX.Location = new System.Drawing.Point(64, 265);
            this.ZCorrectionX.Name = "ZCorrectionX";
            this.ZCorrectionX.Size = new System.Drawing.Size(46, 20);
            this.ZCorrectionX.TabIndex = 155;
            this.toolTip1.SetToolTip(this.ZCorrectionX, "Correction on the X axis over the entire Z delta");
            this.ZCorrectionX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ZCorrectionX_KeyPress);
            this.ZCorrectionX.Leave += new System.EventHandler(this.ZCorrectionX_Leave);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(20, 268);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(38, 13);
            this.label60.TabIndex = 154;
            this.label60.Text = "X corr:";
            this.toolTip1.SetToolTip(this.label60, "Set the true size of the box on the image.");
            this.label60.Click += new System.EventHandler(this.label60_Click);
            // 
            // HeightOffsetLabel
            // 
            this.HeightOffsetLabel.AutoSize = true;
            this.HeightOffsetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeightOffsetLabel.Location = new System.Drawing.Point(99, 325);
            this.HeightOffsetLabel.Name = "HeightOffsetLabel";
            this.HeightOffsetLabel.Size = new System.Drawing.Size(16, 13);
            this.HeightOffsetLabel.TabIndex = 153;
            this.HeightOffsetLabel.Text = "---";
            this.toolTip1.SetToolTip(this.HeightOffsetLabel, "Set the true size of the box on the image.");
            // 
            // HeightOffsetButton
            // 
            this.HeightOffsetButton.Location = new System.Drawing.Point(18, 320);
            this.HeightOffsetButton.Name = "HeightOffsetButton";
            this.HeightOffsetButton.Size = new System.Drawing.Size(75, 23);
            this.HeightOffsetButton.TabIndex = 152;
            this.HeightOffsetButton.Text = "Height offset";
            this.toolTip1.SetToolTip(this.HeightOffsetButton, "Manual Z height to XY calibration");
            this.HeightOffsetButton.UseVisualStyleBackColor = true;
            this.HeightOffsetButton.Click += new System.EventHandler(this.HeightOffsetButton_Click);
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(182, 106);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(56, 13);
            this.label54.TabIndex = 151;
            this.label54.Text = "mm above";
            this.toolTip1.SetToolTip(this.label54, "Set the true size of the box on the image.");
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(20, 96);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(39, 26);
            this.label53.TabIndex = 150;
            this.label53.Text = "Focus \r\nHeight";
            // 
            // UpCalibMoveDistance_textbox
            // 
            this.UpCalibMoveDistance_textbox.Location = new System.Drawing.Point(65, 130);
            this.UpCalibMoveDistance_textbox.Name = "UpCalibMoveDistance_textbox";
            this.UpCalibMoveDistance_textbox.Size = new System.Drawing.Size(46, 20);
            this.UpCalibMoveDistance_textbox.TabIndex = 147;
            this.UpCalibMoveDistance_textbox.Text = ".1";
            // 
            // needleCalibrationHeight_textbox
            // 
            this.needleCalibrationHeight_textbox.Location = new System.Drawing.Point(65, 102);
            this.needleCalibrationHeight_textbox.Name = "needleCalibrationHeight_textbox";
            this.needleCalibrationHeight_textbox.Size = new System.Drawing.Size(46, 20);
            this.needleCalibrationHeight_textbox.TabIndex = 149;
            this.needleCalibrationHeight_textbox.Text = "0";
            this.needleCalibrationHeight_textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.needleCalibrationHeight_textbox_KeyPress);
            this.needleCalibrationHeight_textbox.Leave += new System.EventHandler(this.needleCalibrationHeight_textbox_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 143;
            this.label3.Text = "DownCamera";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 142;
            this.label1.Text = "UpCamera";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.upCameraZero_button);
            this.groupBox4.Controls.Add(this.pcbThickness_label);
            this.groupBox4.Controls.Add(this.needle_calibration_test_button);
            this.groupBox4.Controls.Add(this.label55);
            this.groupBox4.Controls.Add(this.pcbThickness_button);
            this.groupBox4.Controls.Add(this.needleHeight_button);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.SetProbing_button);
            this.groupBox4.Controls.Add(this.label117);
            this.groupBox4.Controls.Add(this.Z0toPCB_BasicTab_label);
            this.groupBox4.Controls.Add(this.label111);
            this.groupBox4.Controls.Add(this.Z_Backoff_label);
            this.groupBox4.Controls.Add(this.Offset2Method_button);
            this.groupBox4.Controls.Add(this.label143);
            this.groupBox4.Controls.Add(this.NeedleOffsetY_textBox);
            this.groupBox4.Controls.Add(this.NeedleOffsetX_textBox);
            this.groupBox4.Controls.Add(this.label149);
            this.groupBox4.Controls.Add(this.label148);
            this.groupBox4.Controls.Add(this.label146);
            this.groupBox4.Location = new System.Drawing.Point(5, 14);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(154, 362);
            this.groupBox4.TabIndex = 147;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Needle";
            // 
            // upCameraZero_button
            // 
            this.upCameraZero_button.Location = new System.Drawing.Point(23, 257);
            this.upCameraZero_button.Name = "upCameraZero_button";
            this.upCameraZero_button.Size = new System.Drawing.Size(108, 24);
            this.upCameraZero_button.TabIndex = 137;
            this.upCameraZero_button.Tag = "Runs the needle calibration routine";
            this.upCameraZero_button.Text = "UpCamera Zero";
            this.upCameraZero_button.UseVisualStyleBackColor = true;
            this.upCameraZero_button.Click += new System.EventHandler(this.upCameraZero_button_Click);
            // 
            // pcbThickness_label
            // 
            this.pcbThickness_label.AutoSize = true;
            this.pcbThickness_label.Location = new System.Drawing.Point(85, 141);
            this.pcbThickness_label.Name = "pcbThickness_label";
            this.pcbThickness_label.Size = new System.Drawing.Size(32, 13);
            this.pcbThickness_label.TabIndex = 136;
            this.pcbThickness_label.Text = "5 mm";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(3, 141);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(83, 13);
            this.label55.TabIndex = 135;
            this.label55.Text = "PCB Thickness:";
            // 
            // pcbThickness_button
            // 
            this.pcbThickness_button.Location = new System.Drawing.Point(23, 115);
            this.pcbThickness_button.Name = "pcbThickness_button";
            this.pcbThickness_button.Size = new System.Drawing.Size(108, 23);
            this.pcbThickness_button.TabIndex = 134;
            this.pcbThickness_button.Text = "PCB Thickness";
            this.toolTip1.SetToolTip(this.pcbThickness_button, "Runs needle height calibration routine");
            this.pcbThickness_button.UseVisualStyleBackColor = true;
            this.pcbThickness_button.Click += new System.EventHandler(this.pcbThickness_button_Click);
            // 
            // needleHeight_button
            // 
            this.needleHeight_button.Location = new System.Drawing.Point(23, 66);
            this.needleHeight_button.Name = "needleHeight_button";
            this.needleHeight_button.Size = new System.Drawing.Size(108, 23);
            this.needleHeight_button.TabIndex = 133;
            this.needleHeight_button.Text = "Needle Height";
            this.toolTip1.SetToolTip(this.needleHeight_button, "Runs needle height calibration routine");
            this.needleHeight_button.UseVisualStyleBackColor = true;
            this.needleHeight_button.Click += new System.EventHandler(this.needleHeight_button_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(13, 303);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 23);
            this.button4.TabIndex = 78;
            this.button4.Text = "Cal. Needle Wobble";
            this.toolTip1.SetToolTip(this.button4, "Re-runs needle calibration routine.");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.CalibrateNeedle_button_Click);
            // 
            // CAD_openFileDialog
            // 
            this.CAD_openFileDialog.Filter = "CSV files (*.csv)|*.csv|KiCad files (*.pos)|*.pos|All files (*.*)|*.*";
            this.CAD_openFileDialog.ReadOnlyChecked = true;
            this.CAD_openFileDialog.SupportMultiDottedExtensions = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 800;
            this.toolTip1.AutoPopDelay = 16000;
            this.toolTip1.InitialDelay = 800;
            this.toolTip1.ReshowDelay = 160;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(227, 841);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(84, 23);
            this.button5.TabIndex = 152;
            this.button5.Text = "To PCB";
            this.toolTip1.SetToolTip(this.button5, "Takes needle down to PCB level");
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.needleToPCBHeight_Click);
            // 
            // TrueX_label
            // 
            this.TrueX_label.AutoSize = true;
            this.TrueX_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrueX_label.Location = new System.Drawing.Point(155, 769);
            this.TrueX_label.Name = "TrueX_label";
            this.TrueX_label.Size = new System.Drawing.Size(31, 9);
            this.TrueX_label.TabIndex = 77;
            this.TrueX_label.Text = "000.000";
            this.TrueX_label.Visible = false;
            // 
            // mechHome_button
            // 
            this.mechHome_button.Location = new System.Drawing.Point(5, 867);
            this.mechHome_button.Name = "mechHome_button";
            this.mechHome_button.Size = new System.Drawing.Size(93, 23);
            this.mechHome_button.TabIndex = 87;
            this.mechHome_button.Text = "Mech. Home";
            this.mechHome_button.UseVisualStyleBackColor = true;
            this.mechHome_button.Click += new System.EventHandler(this.mechHome_button_Click);
            // 
            // OptHome_button
            // 
            this.OptHome_button.Location = new System.Drawing.Point(4, 889);
            this.OptHome_button.Name = "OptHome_button";
            this.OptHome_button.Size = new System.Drawing.Size(94, 23);
            this.OptHome_button.TabIndex = 88;
            this.OptHome_button.Text = "Optical Home";
            this.OptHome_button.UseVisualStyleBackColor = true;
            this.OptHome_button.Click += new System.EventHandler(this.OptHome_button_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 941);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(855, 22);
            this.statusStrip1.TabIndex = 115;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tinyGToolStripMenuItem,
            this.goToLocationToolStripMenuItem,
            this.tapeToolStripMenuItem,
            this.jobOperationsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(855, 24);
            this.menuStrip1.TabIndex = 116;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadJobFileToolStripMenuItem,
            this.saveJobFileToolStripMenuItem,
            this.toolStripSeparator2,
            this.loadCADFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadJobFileToolStripMenuItem
            // 
            this.loadJobFileToolStripMenuItem.Name = "loadJobFileToolStripMenuItem";
            this.loadJobFileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.loadJobFileToolStripMenuItem.Text = "Load Job File";
            this.loadJobFileToolStripMenuItem.Click += new System.EventHandler(this.loadJobFileToolStripMenuItem_Click);
            // 
            // saveJobFileToolStripMenuItem
            // 
            this.saveJobFileToolStripMenuItem.Name = "saveJobFileToolStripMenuItem";
            this.saveJobFileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveJobFileToolStripMenuItem.Text = "Save Job File";
            this.saveJobFileToolStripMenuItem.Click += new System.EventHandler(this.saveJobFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
            // 
            // loadCADFileToolStripMenuItem
            // 
            this.loadCADFileToolStripMenuItem.Name = "loadCADFileToolStripMenuItem";
            this.loadCADFileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.loadCADFileToolStripMenuItem.Text = "Load Pick-n-Place File";
            this.loadCADFileToolStripMenuItem.Click += new System.EventHandler(this.loadCADFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // tinyGToolStripMenuItem
            // 
            this.tinyGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToDefaultsToolStripMenuItem,
            this.loadUserDefaultsToolStripMenuItem,
            this.saveUserDefaultsToolStripMenuItem});
            this.tinyGToolStripMenuItem.Name = "tinyGToolStripMenuItem";
            this.tinyGToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.tinyGToolStripMenuItem.Text = "TinyG";
            // 
            // resetToDefaultsToolStripMenuItem
            // 
            this.resetToDefaultsToolStripMenuItem.Name = "resetToDefaultsToolStripMenuItem";
            this.resetToDefaultsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.resetToDefaultsToolStripMenuItem.Text = "Reset To Built-In Defaults";
            this.resetToDefaultsToolStripMenuItem.Click += new System.EventHandler(this.resetToDefaultsToolStripMenuItem_Click);
            // 
            // loadUserDefaultsToolStripMenuItem
            // 
            this.loadUserDefaultsToolStripMenuItem.Name = "loadUserDefaultsToolStripMenuItem";
            this.loadUserDefaultsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.loadUserDefaultsToolStripMenuItem.Text = "Load User Defaults";
            this.loadUserDefaultsToolStripMenuItem.Click += new System.EventHandler(this.loadUserDefaultsToolStripMenuItem_Click);
            // 
            // saveUserDefaultsToolStripMenuItem
            // 
            this.saveUserDefaultsToolStripMenuItem.Name = "saveUserDefaultsToolStripMenuItem";
            this.saveUserDefaultsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.saveUserDefaultsToolStripMenuItem.Text = "Save User Defaults";
            this.saveUserDefaultsToolStripMenuItem.Click += new System.EventHandler(this.saveUserDefaultsToolStripMenuItem_Click);
            // 
            // goToLocationToolStripMenuItem
            // 
            this.goToLocationToolStripMenuItem.Name = "goToLocationToolStripMenuItem";
            this.goToLocationToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.goToLocationToolStripMenuItem.Text = "GoTo Location";
            // 
            // tapeToolStripMenuItem
            // 
            this.tapeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.reLoadToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.toolStripSeparator3,
            this.resetAllPickupZsToolStripMenuItem,
            this.resetAllPlaceZsToolStripMenuItem,
            this.toolStripSeparator4,
            this.takePhotosOfComponentsToolStripMenuItem,
            this.quickAddMultipleTapesToolStripMenuItem,
            this.rescalAllTapesForAvailablePartsToolStripMenuItem,
            this.toolStripSeparator5,
            this.pickupMultipleComponentsToolStripMenuItem});
            this.tapeToolStripMenuItem.Name = "tapeToolStripMenuItem";
            this.tapeToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.tapeToolStripMenuItem.Text = "Tape";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.saveToolStripMenuItem.Text = "Save Default";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // reLoadToolStripMenuItem
            // 
            this.reLoadToolStripMenuItem.Name = "reLoadToolStripMenuItem";
            this.reLoadToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.reLoadToolStripMenuItem.Text = "ReLoad Default";
            this.reLoadToolStripMenuItem.Click += new System.EventHandler(this.reLoadToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.saveAsToolStripMenuItem.Text = "Save As ...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.loadToolStripMenuItem.Text = "Load ...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(241, 6);
            // 
            // resetAllPickupZsToolStripMenuItem
            // 
            this.resetAllPickupZsToolStripMenuItem.Name = "resetAllPickupZsToolStripMenuItem";
            this.resetAllPickupZsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.resetAllPickupZsToolStripMenuItem.Text = "Reset All Pickup Zs";
            this.resetAllPickupZsToolStripMenuItem.Click += new System.EventHandler(this.resetAllPickupZsToolStripMenuItem_Click);
            // 
            // resetAllPlaceZsToolStripMenuItem
            // 
            this.resetAllPlaceZsToolStripMenuItem.Name = "resetAllPlaceZsToolStripMenuItem";
            this.resetAllPlaceZsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.resetAllPlaceZsToolStripMenuItem.Text = "Reset All Place Zs";
            this.resetAllPlaceZsToolStripMenuItem.Click += new System.EventHandler(this.resetAllPlaceZsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(241, 6);
            // 
            // takePhotosOfComponentsToolStripMenuItem
            // 
            this.takePhotosOfComponentsToolStripMenuItem.Name = "takePhotosOfComponentsToolStripMenuItem";
            this.takePhotosOfComponentsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.takePhotosOfComponentsToolStripMenuItem.Text = "Take Photos Of Components";
            this.takePhotosOfComponentsToolStripMenuItem.Click += new System.EventHandler(this.takePhotosOfComponentsToolStripMenuItem_Click);
            // 
            // quickAddMultipleTapesToolStripMenuItem
            // 
            this.quickAddMultipleTapesToolStripMenuItem.Name = "quickAddMultipleTapesToolStripMenuItem";
            this.quickAddMultipleTapesToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.quickAddMultipleTapesToolStripMenuItem.Text = "Quick Add Multiple Tapes";
            this.quickAddMultipleTapesToolStripMenuItem.Click += new System.EventHandler(this.tapesQuickAdd_button_Click);
            // 
            // rescalAllTapesForAvailablePartsToolStripMenuItem
            // 
            this.rescalAllTapesForAvailablePartsToolStripMenuItem.Name = "rescalAllTapesForAvailablePartsToolStripMenuItem";
            this.rescalAllTapesForAvailablePartsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.rescalAllTapesForAvailablePartsToolStripMenuItem.Text = "Rescal All Tapes For Available Parts";
            this.rescalAllTapesForAvailablePartsToolStripMenuItem.Click += new System.EventHandler(this.rescalAllTapesForAvailablePartsToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(241, 6);
            // 
            // pickupMultipleComponentsToolStripMenuItem
            // 
            this.pickupMultipleComponentsToolStripMenuItem.Name = "pickupMultipleComponentsToolStripMenuItem";
            this.pickupMultipleComponentsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.pickupMultipleComponentsToolStripMenuItem.Text = "Pickup Multiple Components";
            this.pickupMultipleComponentsToolStripMenuItem.Click += new System.EventHandler(this.pickupMultipleComponentsToolStripMenuItem_Click);
            // 
            // jobOperationsToolStripMenuItem
            // 
            this.jobOperationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetAllPlacedComponentsToolStripMenuItem});
            this.jobOperationsToolStripMenuItem.Name = "jobOperationsToolStripMenuItem";
            this.jobOperationsToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.jobOperationsToolStripMenuItem.Text = "Job Operations";
            // 
            // resetAllPlacedComponentsToolStripMenuItem
            // 
            this.resetAllPlacedComponentsToolStripMenuItem.Name = "resetAllPlacedComponentsToolStripMenuItem";
            this.resetAllPlacedComponentsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.resetAllPlacedComponentsToolStripMenuItem.Text = "Reset All Placed Components";
            this.resetAllPlacedComponentsToolStripMenuItem.Click += new System.EventHandler(this.resetAllPlacedComponentsToolStripMenuItem_Click);
            // 
            // needleToFocus_button
            // 
            this.needleToFocus_button.Location = new System.Drawing.Point(227, 809);
            this.needleToFocus_button.Name = "needleToFocus_button";
            this.needleToFocus_button.Size = new System.Drawing.Size(84, 23);
            this.needleToFocus_button.TabIndex = 151;
            this.needleToFocus_button.Text = "To Focus";
            this.needleToFocus_button.UseVisualStyleBackColor = true;
            this.needleToFocus_button.Click += new System.EventHandler(this.needleToFocus_button_Click);
            // 
            // zguardoff_button
            // 
            this.zguardoff_button.BackColor = System.Drawing.Color.Red;
            this.zguardoff_button.Location = new System.Drawing.Point(227, 907);
            this.zguardoff_button.Name = "zguardoff_button";
            this.zguardoff_button.Size = new System.Drawing.Size(79, 23);
            this.zguardoff_button.TabIndex = 153;
            this.zguardoff_button.Text = "ZGuard OFF";
            this.zguardoff_button.UseVisualStyleBackColor = false;
            this.zguardoff_button.Click += new System.EventHandler(this.zguardoff_button_Click);
            // 
            // smallDebugWindow
            // 
            this.smallDebugWindow.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smallDebugWindow.Location = new System.Drawing.Point(657, 761);
            this.smallDebugWindow.Name = "smallDebugWindow";
            this.smallDebugWindow.Size = new System.Drawing.Size(174, 169);
            this.smallDebugWindow.TabIndex = 154;
            this.smallDebugWindow.Text = "";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button2);
            this.groupBox9.Controls.Add(this.calibrateZXYCompensation);
            this.groupBox9.Controls.Add(this.calibrateXYmmRev);
            this.groupBox9.Controls.Add(this.button8);
            this.groupBox9.Controls.Add(this.calibrateSkew);
            this.groupBox9.Location = new System.Drawing.Point(264, 506);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(254, 119);
            this.groupBox9.TabIndex = 155;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Automatic calibrations";
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(871, 784);
            this.Controls.Add(this.smallDebugWindow);
            this.Controls.Add(this.PausePlacement_button);
            this.Controls.Add(this.AbortPlacement_button);
            this.Controls.Add(this.zguardoff_button);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.ReMeasure_button);
            this.Controls.Add(this.ChangeNeedle_button);
            this.Controls.Add(this.needleToFocus_button);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.Snapshot_button);
            this.Controls.Add(this.TestNeedleRecognition_button);
            this.Controls.Add(this.OptHome_button);
            this.Controls.Add(this.StopDemo_button);
            this.Controls.Add(this.mechHome_button);
            this.Controls.Add(this.Demo_button);
            this.Controls.Add(this.TrueX_label);
            this.Controls.Add(this.label145);
            this.Controls.Add(this.label124);
            this.Controls.Add(this.label97);
            this.Controls.Add(this.ZDown_button);
            this.Controls.Add(this.labelSerialPortStatus);
            this.Controls.Add(this.ZUp_button);
            this.Controls.Add(this.GotoPickupCenter_button);
            this.Controls.Add(this.GotoPCB0_button);
            this.Controls.Add(this.OpticalHome_button);
            this.Controls.Add(this.apos_textBox);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.zpos_textBox);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.ypos_textBox);
            this.Controls.Add(this.Test2_button);
            this.Controls.Add(this.MotorPower_checkBox);
            this.Controls.Add(this.Test1_button);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.xpos_textBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tabControlPages);
            this.Controls.Add(this.Pump_checkBox);
            this.Controls.Add(this.Vacuum_checkBox);
            this.Controls.Add(this.buttonConnectSerial);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "LitePlacer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.Tapes_tabPage.ResumeLayout(false);
            this.Tapes_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tapes_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tapeObjBindingSource)).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.tabPageBasicSetup.ResumeLayout(false);
            this.tabPageBasicSetup.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabpage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SmallMovement_numericUpDown)).EndInit();
            this.RunJob_tabPage.ResumeLayout(false);
            this.RunJob_tabPage.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.placement_Picturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobData_GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CadData_GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.physicalComponentBindingSource)).EndInit();
            this.tabControlPages.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.locations_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namedLocationBindingSource)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private Label label4;
        private TextBox textBoxSendtoTinyG;
        private Label label14;
        private TextBox xpos_textBox;
        private TextBox ypos_textBox;
        private Label label17;
        private TextBox zpos_textBox;
        private Label label18;
        private TextBox apos_textBox;
		private Label label19;
		private RichTextBox SerialMonitor_richTextBox;
        private Button Test1_button;
		private Button Test2_button;
		private OpenFileDialog Job_openFileDialog;
		private SaveFileDialog Job_saveFileDialog;
        private Button OpticalHome_button;
		private Button TestNeedleRecognition_button;
        private Label label97;
        private Label label124;
		private Label label145;
		private TabPage Tapes_tabPage;
		private Button AddTape_button;
		private Label label109;
        private DataGridView Tapes_dataGridView;
		private Button ZUp_button;
		private Button ZDown_button;
        private Label UpCameraBoxYmmPerPixel_label;
        private Label UpCameraBoxXmmPerPixel_label;
        private Label label105;
		private Label label106;
		private TextBox UpCameraBoxX_textBox;
        private TextBox UpCameraBoxY_textBox;
		private TextBox DownCameraBoxX_textBox;
		private TextBox DownCameraBoxY_textBox;
		private Label label70;
		private Label label71;
		private Label DownCameraBoxXmmPerPixel_label;
        private Label DownCameraBoxYmmPerPixel_label;
		private TextBox NeedleOffsetY_textBox;
        private TextBox NeedleOffsetX_textBox;
		private Label label149;
		private Label label148;
		private Label label146;
        private Label label143;
		private Button Offset2Method_button;
		private Label instructions_label;
        private Label label115;
		private Button GotoPickupCenter_button;
        private Button GotoPCB0_button;
        private Button Snapshot_button;
        private TabPage tabPageBasicSetup;
        private CheckBox SlackCompensation_checkBox;
        private Button TestYX_button;
		private CheckBox MotorPower_checkBox;
        private Label Z0toPCB_BasicTab_label;
        private Label label111;
        private Button SetProbing_button;
		private Button TestA_button;
		private Button Homebutton;
		private Button TestZ_button;
		private Button HomeZ_button;
		private Button HomeY_button;
		private Button HomeXY_button;
        private Button HomeX_button;
		private CheckBox Vacuum_checkBox;
		private CheckBox Pump_checkBox;
		private Button TestXY_button;
		private Button TestY_button;
		private Button TestX_button;
		private Panel panel7;
		private Panel panel8;
		private TextBox tr4_textBox;
		private RadioButton m4deg18_radioButton;
		private RadioButton m4deg09_radioButton;
		private Label label42;
		private Label label43;
		private Label label44;
		private MaskedTextBox mi4_maskedTextBox;
		private Label label45;
		private Label label46;
		private Label label47;
		private Label label48;
		private MaskedTextBox avm_maskedTextBox;
		private Label label49;
		private Label label50;
		private Label label51;
		private MaskedTextBox ajm_maskedTextBox;
		private Label label52;
		private Panel panel3;
		private Label label73;
		private MaskedTextBox xsv_maskedTextBox;
		private Label label74;
		private Label label75;
		private MaskedTextBox xjh_maskedTextBox;
		private Label label76;
		private CheckBox Xmax_checkBox;
		private CheckBox Xlim_checkBox;
		private CheckBox Xhome_checkBox;
		private Panel panel4;
		private TextBox tr1_textBox;
		private RadioButton m1deg18_radioButton;
		private RadioButton m1deg09_radioButton;
		private Label label20;
		private Label label21;
		private Label label22;
		private Label label23;
		private MaskedTextBox mi1_maskedTextBox;
		private Label label24;
		private Label label25;
		private Label label26;
		private MaskedTextBox xvm_maskedTextBox;
		private Label label27;
		private Label label28;
		private Label label29;
		private MaskedTextBox xjm_maskedTextBox;
		private Label label30;
		private Panel panel5;
		private Label label81;
		private MaskedTextBox zsv_maskedTextBox;
		private Label label82;
		private Label label83;
		private MaskedTextBox zjh_maskedTextBox;
		private Label label84;
		private CheckBox Zmax_checkBox;
		private CheckBox Zlim_checkBox;
		private CheckBox Zhome_checkBox;
		private Panel panel6;
		private TextBox tr3_textBox;
		private RadioButton m3deg18_radioButton;
		private RadioButton m3deg09_radioButton;
		private Label label31;
		private Label label32;
		private Label label33;
		private MaskedTextBox mi3_maskedTextBox;
		private Label label34;
		private Label label35;
		private Label label36;
		private Label label37;
		private MaskedTextBox zvm_maskedTextBox;
		private Label label38;
		private Label label39;
		private Label label40;
		private MaskedTextBox zjm_maskedTextBox;
		private Label label41;
		private Panel panel1;
		private Label label77;
		private MaskedTextBox ysv_maskedTextBox;
		private Label label78;
		private Label label79;
		private MaskedTextBox yjh_maskedTextBox;
		private Label label80;
		private CheckBox Ymax_checkBox;
		private CheckBox Ylim_checkBox;
		private CheckBox Yhome_checkBox;
		private Panel panel2;
		private TextBox tr2_textBox;
		private RadioButton m2deg18_radioButton;
		private RadioButton m2deg09_radioButton;
		private Label label15;
		private Label label16;
		private Label label13;
		private Label label11;
		private MaskedTextBox mi2_maskedTextBox;
		private Label label12;
		private Label label10;
		private Label label8;
		private MaskedTextBox yvm_maskedTextBox;
		private Label label9;
		private Label label7;
		private Label label6;
		private MaskedTextBox yjm_maskedTextBox;
        private Label label5;
		private Button buttonRefreshPortList;
		private Label labelSerialPortStatus;
		private Button buttonConnectSerial;
		private Label label2;
		private ComboBox comboBoxSerialPorts;
        private TabPage RunJob_tabPage;
        private Button ReMeasure_button;
		private TextBox JobOffsetY_textBox;
        private TextBox JobOffsetX_textBox;
		private GroupBox groupBox2;
        private Button NewRow_button;
        private Button DeleteComponentGroup_button;
		private GroupBox groupBox1;
		private Button AbortPlacement_button;
        private Button PausePlacement_button;
        private Label PlacedValue_label;
		private Label PlacedComponent_label;
        private Label label66;
		private Label label58;
		private Button PlaceAll_button;
		private Label label89;
		private Label label88;
		private Label label86;
        private Label label85;
		private DataGridView JobData_GridView;
		public CheckBox Bottom_checkBox;
        private DataGridView CadData_GridView;
		private TabControl tabControlPages;
		private Label label87;
        private NumericUpDown SmallMovement_numericUpDown;
        private OpenFileDialog CAD_openFileDialog;
        private ToolTip toolTip1;
		private Label TrueX_label;
		private Label label90;
		private TextBox SquareCorrection_textBox;
        private Label Z_Backoff_label;
        private Label label117;
        private Label label118;
        private TextBox VacuumTime_textBox;
        private TextBox VacuumRelease_textBox;
        private Label label119;
        private Button ChangeNeedle_button;
        private Label label123;
        private TextBox ZTestTravel_textBox;
        private Button Demo_button;
        private Button StopDemo_button;
        private Button button_camera_calibrate;
        private Label SlackMeasurement_label;
        private CheckBox cb_useTemplate;
        private GroupBox groupBox12;
        private Button button_setTemplate;
        private Label label126;
        private TextBox fiducialTemlateMatch_textBox;
        private Label label127;
        private TextBox fiducial_designator_regexp_textBox;
        private Button DownCamera_Calibration_button;
        private Label label129;
        private TextBox downCalibMoveDistance_textbox;
        private TextBox zoffset_textbox;
        private Label label130;
        private Label label131;
        private Button button1;
        private Button button2;
        private Button needle_calibration_test_button;
        private Button mechHome_button;
        private Button OptHome_button;
        private BindingSource physicalComponentBindingSource;
        //private System.Windows.Forms.DataGridViewTextBoxColumn methodParameterDataGridViewTextBoxColumn;
        private BindingSource jobDataBindingSource;
        private TabControl tabControl1;
        private TabPage tabpage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private Button button3;
        private Button button4;
        private GroupBox groupBox7;
        private Label label3;
        private Label label1;
        private GroupBox groupBox4;

        private BindingSource tapeObjBindingSource;
        private StatusStrip statusStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadCADFileToolStripMenuItem;
        private ToolStripMenuItem loadJobFileToolStripMenuItem;
        private ToolStripMenuItem saveJobFileToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem quitToolStripMenuItem;
        private DataGridView locations_dataGridView;
        private GroupBox groupBox5;
        private Button locationSet_button;
        private Button locationGoTo_button;
        private Button locationDelete_button;
        private Button locationAdd_button;
        private ToolStripMenuItem goToLocationToolStripMenuItem;
        private BindingSource namedLocationBindingSource;
        private ToolStripMenuItem tinyGToolStripMenuItem;
        private ToolStripMenuItem resetToDefaultsToolStripMenuItem;
        private ToolStripMenuItem loadUserDefaultsToolStripMenuItem;
        private ToolStripMenuItem saveUserDefaultsToolStripMenuItem;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn xDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn yDataGridViewTextBoxColumn;
        private TextBox UpCalibMoveDistance_textbox;
        private TextBox needleCalibrationHeight_textbox;
        private Label label53;
        private Button needleToFocus_button;
        private Label label54;
        private GroupBox groupBox6;
        private CheckBox pressureSenstorPresent_button;
        private Button needleHeight_button;
        private Label label55;
        private Button pcbThickness_button;
        private Label pcbThickness_label;
        private Button button5;
        private Button upCameraZero_button;
        private Button zguardoff_button;
        private CheckBox showTinyGComms_checkbox;
        private GroupBox groupBox8;
        private Button clearTextBox_button;
        private Button autoMapJob_button;
        private TextBox vacuumDeltaADC_textbox;
        private Label label56;
        private Label p2_label;
        private Label p1_label;
        private Label label59;
        private Label label57;
        private RichTextBox smallDebugWindow;
        private ToolStripMenuItem tapeToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem reLoadToolStripMenuItem;
        private Label PCBOffset_label;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem jobOperationsToolStripMenuItem;
        private ToolStripMenuItem resetAllPlacedComponentsToolStripMenuItem;
        private CheckBox skippedPlacedComponents_checkBox;
        private ToolStripMenuItem resetAllPickupZsToolStripMenuItem;
        private ToolStripMenuItem resetAllPlaceZsToolStripMenuItem;
        private CheckBox ignoreErrors_checkbox;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn componentListDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn componentTypeDataGridViewTextBoxColumn;
        private DataGridViewComboBoxColumn methodDataGridViewComboBoxColumn;
        private DataGridViewTextBoxColumn methodParametersDataGridViewTextBoxColumn;
        private PictureBox placement_Picturebox;
        private GroupBox groupBox3;
        private ToolStripMenuItem takePhotosOfComponentsToolStripMenuItem;
        private ToolStripMenuItem quickAddMultipleTapesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem rescalAllTapesForAvailablePartsToolStripMenuItem;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn NumberPartsAvailable;
        private DataGridViewCheckBoxColumn TemplateBased;
        private DataGridViewTextBoxColumn CurrentPart;
        private DataGridViewComboBoxColumn OriginalTapeOrientation;
        private DataGridViewComboBoxColumn OriginalPartOrientation;
        private DataGridViewComboBoxColumn TapeType;
        private DataGridViewComboBoxColumn PartType;
        private DataGridViewTextBoxColumn HolePitch;
        private DataGridViewTextBoxColumn PartPitch;
        private DataGridViewTextBoxColumn HoleToPartX;
        private DataGridViewTextBoxColumn HoleToPartY;
        private DataGridViewTextBoxColumn pickupZDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn placeZDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn TapeAngle;
        private Button button6;
        private DataGridViewTextBoxColumn designatorDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn footprintDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn xnominalDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ynominalDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn rotationDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn methodDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn isFiducialDataGridViewCheckBoxColumn;
        private CheckBox reverseRotation_checkbox;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem pickupMultipleComponentsToolStripMenuItem;
        private Label HeightOffsetLabel;
        private Button HeightOffsetButton;
        private TextBox ZCorrectionX;
        private Label label60;
        private TextBox ZCorrectionDeltaZ;
        private Label label62;
        private TextBox ZCorrectionY;
        private Label label61;
        private Button button7;
        private Button calibrateZXYCompensation;
        private Button calibrateSkew;
        private Button calibrateXYmmRev;
        private Button button8;
        private GroupBox groupBox9;
    }
}

