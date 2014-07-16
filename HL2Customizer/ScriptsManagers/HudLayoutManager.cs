using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace HL2Customizer
{
    [Serializable]
    public class HudLayoutManager
    {
        public List<HudElement> HudLayout { get; private set; }
        public char CrosshairType { get; set; }
        public bool AdvCrosshair { get; set; }
        public string[] AuxPowerConfig { get; set; }
        public string AuxPowerLabelPos { get; set; }

        public HudLayoutManager()
        {
            CrosshairType = 'b';
            AdvCrosshair = true;
            AuxPowerConfig = new string[] { "92","4","6","3"};
            AuxPowerLabelPos = "UP";
            Initiate();
        }

        public HudLayoutManager(HudLayoutManager h)
        {
            CrosshairType = h.CrosshairType;
            AdvCrosshair = h.AdvCrosshair;
            AuxPowerConfig = h.AuxPowerConfig;
            AuxPowerLabelPos = h.AuxPowerLabelPos;
            Initiate();
        }

        public void Initiate()
        {
            HudLayout = new List<HudElement>();
            StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources.hudlayout.res"));

            string line;
            bool definingHudElement = false;
            sr.ReadLine();
            int i = 0;
            while((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (line == "{" || line == "") continue;
                if ( line == "}" )
                {
                    definingHudElement = false;
                    i++;
                }
                else if (definingHudElement)
                {
                    string[] str = line.Split('\"');
                    HudLayout[i].Properties.Add(Tuple.Create(str[1], str[3]));
                }
                else
                {
                    HudLayout.Add(new HudElement(line));
                    definingHudElement = true;
                }
            }

            sr.Close();
        }

        public void ModifyElementProperty(string element, string property, string value)
        {
            for(int i = 0; i < HudLayout.Count(); i++)
            {
                if(HudLayout[i].Element == element)
                {
                    for (int j = 0; j < HudLayout[i].Properties.Count(); j++)
                    {
                        if (HudLayout[i].Properties[j].Item1 == property)
                            HudLayout[i].Properties[j] = Tuple.Create(property, value);
                    }
                    break;
                }
            }
        }

        public void AddElement(string element, Tuple<string,string>[] values)
        {
            HudElement e = new HudElement(element);
            for (int i = 0; i < values.Count(); i++)
                e.Properties.Add(Tuple.Create(values[i].Item1, values[i].Item2));
            HudLayout.Add(e);
        }


        public void WriteFile(ref UserPaths Paths)
        {
            string file = Paths.ScriptsPath + "hudlayout.res";
            File.WriteAllText(file, "");
            StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));
            string indent = "   ";

            sw.WriteLine("Resource/HudLayout.res");
            sw.WriteLine("{");
            sw.WriteLine("");

            foreach(HudElement e in HudLayout)
            {
                sw.WriteLine(indent + e.Element);
                sw.WriteLine(indent + "{");
                foreach (Tuple<string,string> p in e.Properties)
                    sw.WriteLine(indent + indent +"\"" + p.Item1 + "\"" +indent +"\"" + p.Item2 + "\"");
                sw.WriteLine(indent + "}");
            }

            sw.WriteLine("}");
            sw.Close();
        }
    }
}
