using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Win32;

namespace LitePlacer {
    public class NamedLocation : PartLocation {
        private string _name = "";
        public string Name {get { return _name; } set { _name = value; Notify("Name");}}
        public NamedLocation(double x, double y, string name) : base(x,y) {Name = name;}
        public NamedLocation(PartLocation p, string name) : base(p) {Name = name;}
        public NamedLocation() {}
        public override string ToString() {
            return String.Format("{0} ({1:F2},{2:F2})", Name, X, Y);
        }
    }

    public delegate void LocationChangeEventHandler(object sender, LocationChangeEventArgs e);
    public class LocationChangeEventArgs: EventArgs {
        public NamedLocation NamedLocation;
        public string PropertyName;
    }

    public class LocationManager {
        public event LocationChangeEventHandler LocationChangeEvent;
        private const string LocationsSaveName = "Locations.xml";
        private readonly string[] _defaultLocations = {"Park", "Max Machine", "Up Camera", "PCB Zero", "Pickup", "Needle Zeroing", "Component Dropoff"};
        private readonly SortableBindingList<NamedLocation> _locations;
        private static string FileName { get { return Global.BaseDirectory + @"\" + LocationsSaveName;  }}

        public LocationManager() {
            //if we have saved data, load it
            _locations = File.Exists(FileName) ? Global.DeSerialization<SortableBindingList<NamedLocation>>(FileName) : new SortableBindingList<NamedLocation>();

            //ensure default locations are present
            foreach (var x in _defaultLocations.Where(x => !LocationIsSet(x))) {
                AddLocation(0, 0, x);
            }

            //setup event handling
            foreach (var x in _locations) x.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ChildPropertyChangedCallback);          
        }

        void ChildPropertyChangedCallback(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (LocationChangeEvent == null) return;
            var x = new LocationChangeEventArgs();
            x.NamedLocation = (NamedLocation)sender;
            x.PropertyName = e.PropertyName;
            LocationChangeEvent(this, x);
        } 
        
        public bool IsLocationMandatory(string name) {
            return _defaultLocations.Any(n => name.Equals(n));
        }

        public SortableBindingList<NamedLocation> GetList() {
            return _locations;
        } 

        public void Save() {
            Global.Instance.DisplayText("Saving Updated Locations To Disk", System.Drawing.Color.Pink);
            Global.Serialization(_locations, FileName);
        }

        public void AddLocation(PartLocation p, string name) {
            var nl = new NamedLocation(p, name);
            nl.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ChildPropertyChangedCallback);
            _locations.Add(nl);
        }

        public void AddLocation(double x, double y, string name) {
            var nl = new NamedLocation(x, y, name);
            nl.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ChildPropertyChangedCallback);
            _locations.Add(nl);
        }

        public void DeleteIndex(int index) {
            _locations.RemoveAt(index);
        }

        /// <summary>
        /// If this location exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool LocationIsSet(string name) {
            try {
                GetLocation(name);
                return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Get the named location
        /// </summary>
        /// <param name="name">location name</param>
        /// <returns>NamedLocation corresponding to the location</returns>
        public NamedLocation GetLocation(string name) {
            foreach (var x in _locations) {
                if (x.Name.ToLower() == name.ToLower()) return x;
            }
            throw new Exception("Requested Location " + name + " does not exist");
        }

        public string[] GetNames() {
            return _locations.Select(x => x.Name).ToArray();
        } 
    }
}
