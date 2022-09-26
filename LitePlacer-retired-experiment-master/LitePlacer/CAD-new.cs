using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//using System.Web.Script.Serialization;

namespace LitePlacer {
    public class CAD {
        public SortableBindingList<PhysicalComponent> ComponentData = new SortableBindingList<PhysicalComponent>();
        public SortableBindingList<JobData> JobData = new SortableBindingList<JobData>();

        FormMain MainForm;

        public CAD(FormMain f) {
            MainForm = f;
        }

        /// <summary>
        /// Copy the list of compoents found in the job data into the list of components
        /// </summary>
        public void CopyComponentsFromJob() {
            ComponentData.Clear();
            foreach (var p in JobData) {
                foreach (var c in p.Components) {
                    c.PropertyChanged += (s,e) => {if (e.PropertyName.Equals("IsPlaced")) MainForm.ReColorComponentsTable(); };
                    ComponentData.Add(c);
                }
            }
            MainForm.ReColorComponentsTable();
        }

        // i think this is my favorite linq implementation especially how terse it is. so clean but very legible.
        public JobData FindJobDataThatContainsComponent(PhysicalComponent component) {
            return JobData.FirstOrDefault(j => Enumerable.Contains(j.Components, component));
        }

        public static void MoveItem<T>(BindingList<T> list, int index, int offset) {
            if (index + offset < 0 || index + offset >= list.Count) return;
            var item = list[index];
            list.RemoveAt(index);
            list.Insert((index + offset), item);
        }

        public bool CADdataToMMs_m() {
            foreach (var x in ComponentData) {
                x.nominal.X /= 2.54;
                x.nominal.Y /= 2.54;
            }
            return true;
        }

        public void AutoFillJobEntry() {
            JobData.Clear();
            var partTypes = ComponentData.Select(x => x.TypePlusFootprint).Distinct().ToArray();
            foreach (var part in partTypes) {
                var job = new JobData();
                job.AddComponent(ComponentData.Where(x => x.TypePlusFootprint.Equals(part)).ToArray());
                JobData.Add(job);
            }
        }


        public PhysicalComponent GetComponentByDesignator(string designator) {
            return ComponentData.Where(x => x.Designator.Equals(designator.Trim())).First();
        }

        
        // =================================================================================
        // ParseKiCadData_m()
        // =================================================================================
        public  bool ParseKiCadData_m(String[] AllLines) {
            // Convert KiCad data to regular CSV
            int i = 0;
            bool inches = false;
            // Skip headers until find one starting with "## "
            while (!(AllLines[i].StartsWith("## "))) {
                i++;
            }

            // inches vs mms
            if (AllLines[i++].Contains("inches")) {
                inches = true;
            }
            i++; // skip the "Side" line
            List<string> KiCadLines = new List<string>();
            KiCadLines.Add(AllLines[i++].Substring(2));  // add header, skip the "# " start
            // add rest of the lines
            while (!(AllLines[i]).StartsWith("## End")) {
                KiCadLines.Add(AllLines[i++]);
            }
            // parse the data
            string[] KicadArr = KiCadLines.ToArray();
            if (!ParseCadData_m(KicadArr, true)) {
                return false;
            }
            // convert to mm'f if needed
            if (inches) {
                return (CADdataToMMs_m());
            }
            return true;
        }

        // =================================================================================
        // ParseCadData_m(): main function called from file open
        // =================================================================================

        // =================================================================================
        // FindDelimiter_m(): Tries to find the difference with comma and semicolon separated files  
        bool FindDelimiter_m(String Line, out char delimiter) {

            int commas = Line.Count(c => c == ',');
            int semicolons = Line.Count(c => c == ';');

            if ((commas == 0) && (semicolons > 4)) {
                delimiter = ';';
                return true;
            }
            if ((semicolons == 0) && (commas > 4)) {
                delimiter = ',';
                return true;
            }
            Global.Instance.DisplayText("File Headers Parse Failure");
            delimiter = ',';
            return false;
        }

        public bool ParseCadData_m(String[] AllLines, bool KiCad) {
            int ComponentIndex=-1;
            int ValueIndex=-1;
            int FootPrintIndex=-1;
            int X_Nominal_Index=-1;
            int Y_Nominal_Index=-1;
            int RotationIndex=-1;
            int LayerIndex = -1;
            bool LayerDataPresent = false;
            int i;
            int LineIndex = 0;

            // Parse header. Skip empty lines and comment lines (starting with # or "//")
            Regex skip_header = new Regex(@"^$|^#.*|//.*");
            foreach (string s in AllLines) {
                if (skip_header.IsMatch(s)) { LineIndex++; continue; }
                break;
            }

            char delimiter;
            if (KiCad) {
                delimiter = ' ';
            } else {
                if (!FindDelimiter_m(AllLines[0], out delimiter)) {
                    return false;
                };
            }
            
            // Determien which column is what 
            string[] h = SplitCSV(AllLines[LineIndex++], delimiter).ToArray();
            Regex header_regexp = new Regex(@"^(designator|part|refdes|ref|component)$", RegexOptions.IgnoreCase);
            Regex value_regexp = new Regex(@"^(value|val|comment)$", RegexOptions.IgnoreCase);
            Regex footprint_regexp = new Regex(@"^(footprint|package|pattern)$", RegexOptions.IgnoreCase);
            Regex x_regexp = new Regex(@"^(x|x \(mm\)|posx|ref x)$", RegexOptions.IgnoreCase);
            Regex y_regexp = new Regex(@"^(y|y \(mm\)|posy|ref y)$", RegexOptions.IgnoreCase);
            Regex rotate_regexp = new Regex(@"^(rotation|rot|rotate)$", RegexOptions.IgnoreCase);
            Regex layer_regexp = new Regex(@"^(layer|side|tb)$", RegexOptions.IgnoreCase);


            for (int j=0; j<h.Length; j++) {
                if (header_regexp.IsMatch(h[j])) ComponentIndex = j;
                if (value_regexp.IsMatch(h[j]))  ValueIndex = j;
                if (footprint_regexp.IsMatch(h[j])) FootPrintIndex = j;
                if (x_regexp.IsMatch(h[j])) X_Nominal_Index = j;
                if (y_regexp.IsMatch(h[j])) Y_Nominal_Index = j;
                if (rotate_regexp.IsMatch(h[j])) RotationIndex = j;
                if (layer_regexp.IsMatch(h[j])) {LayerIndex = j; LayerDataPresent=true;}
            }

            if (ComponentIndex == -1 || ValueIndex == -1 || FootPrintIndex == -1 || X_Nominal_Index == -1 ||
                Y_Nominal_Index == -1 || RotationIndex == -1) {
                Global.Instance.DisplayText("Headers not set correctly - unable to parse cad file");
                return false;
            }

            // Parse data
            string peek;
            Regex skip_lines_regex = new Regex("^$|^\"\"|^#.*|^//.*");
            Regex top_regex = new Regex(@"top|f\.cu|t", RegexOptions.IgnoreCase);
            Regex bot_regex = new Regex(@"bottom|b\.cu|b|bot", RegexOptions.IgnoreCase);
            for (i = LineIndex; i < AllLines.Count(); i++) {  // for each component
            
                peek = AllLines[i];
                // Skip: empty lines and comment lines (starting with # or "//")
                if (skip_lines_regex.IsMatch(AllLines[i])) continue;

                List<String> Line = SplitCSV(AllLines[i], delimiter);
                // If layer is indicated and the component is not on this layer, skip it
                //if (LayerDataPresent &  MainForm.Bottom_checkBox.Checked & top_regex.IsMatch(Line[LayerIndex])) continue;
                //if (LayerDataPresent & !MainForm.Bottom_checkBox.Checked & bot_regex.IsMatch(Line[LayerIndex])) continue;


                PhysicalComponent p = new PhysicalComponent();
                p.Designator = Line[ComponentIndex];
                p.Type = Line[ValueIndex];
                p.Footprint = Line[FootPrintIndex];
                p.nominal.X = double.Parse(Line[X_Nominal_Index].Replace("mm", "").Replace(",", "."));
                if (LayerDataPresent && MainForm.Bottom_checkBox.Checked) {
                    p.nominal.X *= -1;
                }
                p.nominal.Y = double.Parse(Line[Y_Nominal_Index].Replace("mm", "").Replace(",", "."));
                p.Rotation = double.Parse(Line[RotationIndex]);
                p.PropertyChanged += (s, e) => { if (e.PropertyName.Equals("IsPlaced")) MainForm.ReColorComponentsTable(); };
                ComponentData.Add(p);
            }
            MainForm.ReColorComponentsTable();
            return true;
        }   // end ParseCadData



        // =================================================================================
        // Helper function for ParseCadData() (and some others, hence public static)
        // from http://stackoverflow.com/questions/3147836/c-sharp-regex-split-commas-outside-quotes
        public static List<string> SplitCSV(string Line, char delimiter) {
            return Regex.Split(Line, delimiter + "(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)").Select(x=>x.Replace("\"","")).ToList();
        }


    }
}
