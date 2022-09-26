using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeriousWebcamSettings
{
    /// <summary>
    /// Interaction logic for CameraVideoSettingControl.xaml
    /// </summary>
    public partial class CameraVideoSettingControl : UserControl
    {
        private CameraVideoSettingEntity _setting = null;
        private bool _finishedInit = false;
        private bool _isSaving = false;

        public CameraVideoSettingControl()
        {
            InitializeComponent();
        }

        public CameraVideoSettingControl(CameraVideoSettingEntity setting) : this() => _setting = setting;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _setting;

            // All of them MUST be initialised in the C# file and not via Data Binding as it causes many Race Conditions
            _slider.IsEnabled = _setting.ControlRange.HasFlag(CameraControlFlags.Manual);
            _chkAuto.IsEnabled = _setting.ControlRange.HasFlag(CameraControlFlags.Auto);
            _chkAuto.IsChecked = _setting.ControlValue.HasFlag(CameraControlFlags.Auto);

            _slider.Minimum = _setting.Min;
            _slider.Maximum = _setting.Max;
            _slider.Value = _setting.Value;
            _slider.ValueChanged += _slider_ValueChanged;

            _finishedInit = true;
        }

        private void _slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_finishedInit)
            {
                return;
            }

            if (!_isSaving && _setting.ControlRange.HasFlag(CameraControlFlags.Manual))
            {
                _isSaving = true;
                _setting.ControlValue = CameraControlFlags.Manual;
                _chkAuto.IsChecked = false;
                _setting.Value = (int)_slider.Value;
                _lblValue.Content = _setting.Value;
                _setting.SaveCallback();
                _isSaving = false;
            }
        }

        private void _chkAuto_Checked(object sender, RoutedEventArgs e)
        {
            if (!_finishedInit)
            {
                return;
            }

            _setting.ControlValue = CameraControlFlags.Auto;
            _lblControl.Content = _setting.ControlValue;

            if (!_isSaving)
            {
                _isSaving = true;
                _setting.SaveCallback();
                _isSaving = false;
            }
        }

        private void _chkAuto_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!_finishedInit)
            {
                return;
            }

            _setting.ControlValue = CameraControlFlags.Manual;
            _lblControl.Content = _setting.ControlValue;

            if (!_isSaving)
            {
                _isSaving = true;
                _setting.SaveCallback();
                _isSaving = false;
            }
        }
    }
}
