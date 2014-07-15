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
    public class ClientSchemeManager
    {
        public string MainColor { get; private set; }
        public string SecondaryColor { get; private set; }
        public string CrossColor { get; private set; }
        public Boolean GiantCrosshairs { get; private set; }
        public Boolean OutlinedCrosshairs { get; private set; }
        public Boolean BasicCrosshair { get; private set; }


        public ClientSchemeManager()
        {

            MainColor = "Orange";
            SecondaryColor = "Orange";
            CrossColor = "Orange";

            GiantCrosshairs = false;
            OutlinedCrosshairs = false;
            BasicCrosshair = false;
        }

        public void SetProperties(string maincolor, string secondarycolor, string crosshaircolor, bool outlined, bool giant, bool basicXhair)
        {
            MainColor = maincolor;
            SecondaryColor = secondarycolor;
            CrossColor = crosshaircolor;
            OutlinedCrosshairs = outlined;
            GiantCrosshairs = giant;
            BasicCrosshair = basicXhair;
        }

        public void WriteFile(ref UserPaths Paths)
        {

            string file = Paths.ResPath + "clientscheme.res";
            File.WriteAllText(file, "");
            StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources.clientscheme.res"));
            StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("|MAIN_COLOR|")) line = line.Replace("|MAIN_COLOR|", MainColor);
                if (line.Contains("|SECOND_COLOR|")) line = line.Replace("|SECOND_COLOR|", SecondaryColor);
                if (line.Contains("|CROSS_COLOR|")) line = line.Replace("|CROSS_COLOR|", CrossColor);
                if (line.Contains("|OUTLINED|")) line = line.Replace("|OUTLINED|", (OutlinedCrosshairs ? "1" : "0"));
                if (line.Contains("|ANTIALIASED|")) line = line.Replace("|ANTIALIASED|", (OutlinedCrosshairs ? "0" : "1"));
                if (line.Contains("|CROSS_SIZE|")) line = line.Replace("|CROSS_SIZE|", (GiantCrosshairs ? "128" : "40"));
                if (line.Contains("|QINFOS_SIZE|")) line = line.Replace("|QINFOS_SIZE|", (GiantCrosshairs ? "48" : "24"));
                if (line.Contains("|KEEPXHAIR|")) line = line.Replace("|KEEPXHAIR|", (BasicCrosshair ? "" : "[disabled]"));
                sw.WriteLine(line);
            }

            sr.Close();
            sw.Close();
        }

        public void AddFonts(ref UserPaths Paths)
        {
            File.WriteAllBytes(Paths.FontsPath + @"XHAIR.ttf", HL2Customizer.Resources.resfile.XHAIR);
        }
    }
}
