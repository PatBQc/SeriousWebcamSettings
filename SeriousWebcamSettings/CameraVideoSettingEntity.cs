using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriousWebcamSettings
{
    public class CameraVideoSettingEntity
    {
        public CameraVideoSettingEntity()
        {
            SaveCallback = () => { };
        }

        public CameraVideoSettingProperty Setting { get; set; }

        public int Value { get; set; }
        public int Min { get; set; }

        public int Max { get; set; }

        public int Default { get; set; }

        public int StepSize { get; set; }

        public CameraControlFlags ControlValue { get; set; }
        public CameraControlFlags ControlRange { get; set; }

        public Action SaveCallback { get; set; }

        public bool IsAuto
        {
            get => ControlValue.HasFlag(CameraControlFlags.Auto);
        }
    }
}
