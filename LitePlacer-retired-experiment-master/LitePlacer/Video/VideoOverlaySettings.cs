using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class VideoOverlaySettings : INotifyPropertyChanged {

    private bool _Threshold, _GrayScale, _Invert, _DrawCross, _DrawSidemarks,
        _DrawDashedCross, _FindCircles, _FindRectangles, _FindFiducial, _Draw1mmGrid, _FindComponent, 
        _TakeSnapshot, _Draw_Snapshot, _PauseProcessing, _TestAlgorithm, _DrawBox;

    public bool Threshold { get { return _Threshold; } set { _Threshold = value; Notify("Threshold");}}
    public bool GrayScale { get { return _GrayScale; } set { _GrayScale = value; Notify("GrayScale");}}
    public bool Invert { get { return _Invert; } set { _Invert = value; Notify("Invert");}}
    public bool DrawCross { 
        get { return _DrawCross; } 
        set { _DrawCross = value; Notify("DrawCross");}
    }
    public bool DrawSidemarks { get { return _DrawSidemarks; } set { _DrawSidemarks = value; Notify("DrawSidemarks");}}
    public bool DrawDashedCross { get { return _DrawDashedCross; } set { _DrawDashedCross = value; Notify("DrawDashedCross");}}
    public bool FindCircles { get { return _FindCircles; } set { _FindCircles = value; Notify("FindCircles");}}
    public bool FindRectangles { get { return _FindRectangles; } set { _FindRectangles = value; Notify("FindRectangles");}}
    public bool FindFiducial { get { return _FindFiducial; } set { _FindFiducial = value; Notify("FindFiducial");}}
    public bool Draw1mmGrid { get { return _Draw1mmGrid; } set { _Draw1mmGrid = value; Notify("Draw1mmGrid");}}
    public bool FindComponent { get { return _FindComponent; } set { _FindComponent = value; Notify("FindComponent");}}
    public bool TakeSnapshot { get { return _TakeSnapshot; } set { _TakeSnapshot = value; Notify("TakeSnapshot");}}
    public bool Draw_Snapshot { get { return _Draw_Snapshot; } set { _Draw_Snapshot = value; Notify("Draw_Snapshot");}}
    public bool PauseProcessing { get { return _PauseProcessing; } set { _PauseProcessing = value; Notify("PauseProcessing");}}
    public bool TestAlgorithm { get { return _TestAlgorithm; } set { _TestAlgorithm = value; Notify("TestAlgorithm");}}
    public bool DrawBox { get { return _DrawBox; } set { _DrawBox = value; Notify("DrawBox");}}


    public event PropertyChangedEventHandler PropertyChanged;
    private void Notify(string name) {
        if (PropertyChanged != null) {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
