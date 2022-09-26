using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LitePlacer {
    public partial class QuickAddForm : Form {
        public QuickAddForm() {
            InitializeComponent();

            //setup bindings
            partOrientation_comboBox.DataSource = Enum.GetNames(typeof(Orientation));
            tapeOrientation_comboBox.DataSource = Enum.GetNames(typeof(Orientation));
            tapeType_comboBox.DataSource = new BindingSource() { DataSource = AForgeFunctionSet.GetTapeTypes()};
            partType_comboBox.DataSource = Enum.GetNames(typeof(ComponentType));


        }

        public string PartOrientation { get { return partOrientation_comboBox.Text; }}
        public string TapeOrientation { get { return tapeOrientation_comboBox.Text; }}
        public string TapeType { get { return tapeType_comboBox.Text; }}
        public string PartType { get { return partType_comboBox.Text; }}
        public string[] Parts { get {
            return partList_textbox.Text.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }}
        public PartLocation HoleOffset {
            get {
                double x, y;
                if (double.TryParse(xmm_textbox.Text, out x) && (double.TryParse(ymm_textbox.Text, out y))) {
                    return new PartLocation(x, y);
                }
                return null;
            }
        }


        private void numeric_textbox_KeyPress(object sender, KeyPressEventArgs e) {
            double x;
            e.Handled = !double.TryParse(((TextBox)sender).Text, out x);
        }

    }
}
