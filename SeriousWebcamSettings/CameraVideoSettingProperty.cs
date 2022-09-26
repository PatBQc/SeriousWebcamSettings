using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriousWebcamSettings
{
    /// <summary>
    /// Control both Camera controls and Video Proc Amp
    /// </summary>
    public enum CameraVideoSettingProperty
    {
        /// <summary>
        /// Pan control.
        /// </summary>
        Pan = 0,
        /// <summary>
        /// Tilt control.
        /// </summary>
        Tilt,
        /// <summary>
        /// Roll control.
        /// </summary>
        Roll,
        /// <summary>
        /// Zoom control.
        /// </summary>
        Zoom,
        /// <summary>
        /// Exposure control.
        /// </summary>
        Exposure,
        /// <summary>
        /// Iris control.
        /// </summary>
        Iris,
        /// <summary>
        /// Focus control.
        /// </summary>
        Focus,

        /// <summary>
        /// Brightness control.
        /// </summary>
        Brightness,

        /// <summary>
        /// Contrast control.
        /// </summary>
        Contrast,

        /// <summary>
        /// Hue control.
        /// </summary>
        Hue,

        /// <summary>
        /// Saturation control.
        /// </summary>
        Saturation,

        /// <summary>
        /// Sharpness control.
        /// </summary>
        Sharpness,

        /// <summary>
        /// Gamma control.
        /// </summary>
        Gamma,

        /// <summary>
        /// ColorEnable control.
        /// </summary>
        ColorEnable,

        /// <summary>
        /// WhiteBalance control.
        /// </summary>
        WhiteBalance,

        /// <summary>
        /// BacklightCompensation control.
        /// </summary>
        BacklightCompensation,

        /// <summary>
        /// Gain control.
        /// </summary>
        Gain
    }
}
