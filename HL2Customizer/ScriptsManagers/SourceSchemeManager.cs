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
    public class SourceSchemeManager
    {
        public Tuple<string, byte, byte, byte> TxtColor1 { get; set; }
        public Tuple<string, byte, byte, byte> TxtColor2 { get; set; }
        public Tuple<string, byte, byte, byte> BgColor { get; set; }

        public string TitleFont;

        public SourceSchemeManager()
        {
            TxtColor1 = Tuple.Create("White", (byte)160, (byte)160, (byte)160);
            TxtColor2 = Tuple.Create("White", (byte)160, (byte)160, (byte)160);
            BgColor = Tuple.Create("White", (byte)160, (byte)160, (byte)160);
            TitleFont = "Verdana";
        }

        public void SetProperties(Tuple<string, byte, byte, byte> txtColor1, Tuple<string, byte, byte, byte> txtColor2, Tuple<string, byte, byte, byte> bgColor)
        {
            TxtColor1 = txtColor1;
            TxtColor2 = txtColor2;
            BgColor = bgColor;
        }

        public void WriteFile(ref UserPaths Paths)
        {

            string file = Paths.ResPath + "sourcescheme.res";
            File.WriteAllText(file, "");
            StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources.sourcescheme.res"));
            StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("|ttltxt+|"))
                    line = line.Replace("|ttltxt+|", (TxtColor1.Item2 * 1.2).ToString() +
                    " " + (TxtColor1.Item3 * 1.2).ToString() +
                    " " + (TxtColor1.Item4 * 1.2).ToString());
                if (line.Contains("|ttltxt-|"))
                    line = line.Replace("|ttltxt-|", (TxtColor1.Item2 * 0.8).ToString() +
                    " " + (TxtColor1.Item3 * 0.8).ToString() +
                    " " + (TxtColor1.Item4 * 0.8).ToString());

                if (line.Contains("|txt+|"))
                    line = line.Replace("|txt+|", (TxtColor2.Item2 * 1.5).ToString() +
                    " " + (TxtColor2.Item3 * 1.5).ToString() +
                    " " + (TxtColor2.Item4 * 1.5).ToString());
                if (line.Contains("|txt=|"))
                    line = line.Replace("|txt=|", (TxtColor2.Item2).ToString() +
                    " " + (TxtColor2.Item3).ToString() +
                    " " + (TxtColor2.Item4).ToString());
                if (line.Contains("|txt-|"))
                    line = line.Replace("|txt-|", (TxtColor2.Item2 * 0.90).ToString() +
                    " " + (TxtColor2.Item3 * 0.90).ToString() +
                    " " + (TxtColor2.Item4 * 0.90).ToString());

                if (line.Contains("|background+|"))
                    line = line.Replace("|background+|", (BgColor.Item2 * 1.3).ToString() +
                    " " + (BgColor.Item3 * 1.3).ToString() +
                    " " + (BgColor.Item4 * 1.3).ToString());
                if (line.Contains("|background=|"))
                    line = line.Replace("|background=|", (BgColor.Item2).ToString() +
                    " " + (BgColor.Item3).ToString() +
                    " " + (BgColor.Item4).ToString());
                if (line.Contains("|background-|"))
                    line = line.Replace("|background-|", (BgColor.Item2 * 0.5).ToString() +
                    " " + (BgColor.Item3 * 0.5).ToString() +
                    " " + (BgColor.Item4 * 0.5).ToString());

                if (line.Contains("|TITLE_FONT|")) line = line.Replace("|TITLE_FONT|", TitleFont);

                sw.WriteLine(line);
            }

            sr.Close();
            sw.Close();
        }

    }
}
