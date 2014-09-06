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
        public string SecCrossColor { get; private set; }
        public string TitleColor { get; private set; }

        public string TxtFont { get; set; }
        public string NbrFont { get; set; }
        public string ChatFont { get; set; }

        public Boolean OutlinedCrosshairs { get; set; }
        public Boolean KeepXhair { private get; set; }

        public Char CrosshairSize { get; set; }
        public Boolean[] OutlinedAdditionnalCrosshairs { get; set; }

        public ClientSchemeManager()
        {

            MainColor = "Orange";
            SecondaryColor = "Orange";
            CrossColor = "Orange";
            SecCrossColor = "White";

            TxtFont = "Verdana";
            NbrFont = "Verdana";
            ChatFont = "Verdana";

            CrosshairSize = 'S';
            OutlinedCrosshairs = false;

            OutlinedAdditionnalCrosshairs = new Boolean[12];
        }

        public void SetProperties(string maincolor, string secondarycolor, string crosshaircolor, string seccrosshaircolor, string titlecolor, bool outlined)
        {
            MainColor = maincolor;
            SecondaryColor = secondarycolor;
            CrossColor = crosshaircolor;
            SecCrossColor = seccrosshaircolor;
            OutlinedCrosshairs = outlined;
            TitleColor = titlecolor;
        }

        public void WriteFile(ref UserPaths Paths)
        {

            string file = Paths.ResPath + "clientscheme.res";
            File.WriteAllText(file, "");
            StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources.clientscheme.res"));
            StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));

            int csize;
            switch (CrosshairSize)
            {
                case 'S': csize = 40; break;
                case 'M': csize = 62; break;
                case 'L': csize = 78; break;
                case 'G': csize = 128; break;
                default: goto case 'S';
            }

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("|MAIN_COLOR|")) line = line.Replace("|MAIN_COLOR|", MainColor);
                if (line.Contains("|SECOND_COLOR|")) line = line.Replace("|SECOND_COLOR|", SecondaryColor);
                if (line.Contains("|CROSS_COLOR|")) line = line.Replace("|CROSS_COLOR|", (KeepXhair ? CrossColor : SecCrossColor));
                if (line.Contains("|TITLE_COLOR|")) line = line.Replace("|TITLE_COLOR|", TitleColor);
                if (line.Contains("|OUTLINED|")) line = line.Replace("|OUTLINED|", (OutlinedCrosshairs ? "1" : "0"));
                if (line.Contains("|ANTIALIASED|")) line = line.Replace("|ANTIALIASED|", (OutlinedCrosshairs ? "0" : "1"));
                if (line.Contains("|CROSS_SIZE|")) line = line.Replace("|CROSS_SIZE|", csize.ToString());
                if (line.Contains("|CROSS_SIZE2|")) line = line.Replace("|CROSS_SIZE2|", csize.ToString());
                if (line.Contains("|QINFOS_SIZE|")) line = line.Replace("|QINFOS_SIZE|", (csize/2).ToString());

                if (line.Contains("|TEXT_FONT|")) line = line.Replace("|TEXT_FONT|", TxtFont);
                if (line.Contains("|NBR_FONT|")) line = line.Replace("|NBR_FONT|", NbrFont);

                if (line.Contains("|PHYSCANNON_ANTIALIASED|")) line = line.Replace("|PHYSCANNON_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[0] ? "0" : "1"));
                if (line.Contains("|PHYSCANNON_OUTLINED|")) line = line.Replace("|PHYSCANNON_OUTLINED|", (OutlinedAdditionnalCrosshairs[0] ? "1" : "0"));
                if (line.Contains("|CROWBAR_ANTIALIASED|")) line = line.Replace("|CROWBAR_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[1] ? "0" : "1"));
                if (line.Contains("|CROWBAR_OUTLINED|")) line = line.Replace("|CROWBAR_OUTLINED|", (OutlinedAdditionnalCrosshairs[1] ? "1" : "0"));
                if (line.Contains("|STUNSTICK_ANTIALIASED|")) line = line.Replace("|STUNSTICK_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[2] ? "0" : "1"));
                if (line.Contains("|STUNSTICK_OUTLINED|")) line = line.Replace("|STUNSTICK_OUTLINED|", (OutlinedAdditionnalCrosshairs[2] ? "1" : "0"));
                if (line.Contains("|FRAG_ANTIALIASED|")) line = line.Replace("|FRAG_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[3] ? "0" : "1"));
                if (line.Contains("|FRAG_OUTLINED|")) line = line.Replace("|FRAG_OUTLINED|", (OutlinedAdditionnalCrosshairs[3] ? "1" : "0"));
                if (line.Contains("|SLAM_ANTIALIASED|")) line = line.Replace("|SLAM_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[4] ? "0" : "1"));
                if (line.Contains("|SLAM_OUTLINED|")) line = line.Replace("|SLAM_OUTLINED|", (OutlinedAdditionnalCrosshairs[4] ? "1" : "0"));
                if (line.Contains("|PISTOL_ANTIALIASED|")) line = line.Replace("|PISTOL_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[5] ? "0" : "1"));
                if (line.Contains("|PISTOL_OUTLINED|")) line = line.Replace("|PISTOL_OUTLINED|", (OutlinedAdditionnalCrosshairs[5] ? "1" : "0"));
                if (line.Contains("|SMG1_ANTIALIASED|")) line = line.Replace("|SMG1_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[6] ? "0" : "1"));
                if (line.Contains("|SMG1_OUTLINED|")) line = line.Replace("|SMG1_OUTLINED|", (OutlinedAdditionnalCrosshairs[6] ? "1" : "0"));
                if (line.Contains("|AR2_ANTIALIASED|")) line = line.Replace("|AR2_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[7] ? "0" : "1"));
                if (line.Contains("|AR2_OUTLINED|")) line = line.Replace("|AR2_OUTLINED|", (OutlinedAdditionnalCrosshairs[7] ? "1" : "0"));
                if (line.Contains("|SHOTGUN_ANTIALIASED|")) line = line.Replace("|SHOTGUN_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[8] ? "0" : "1"));
                if (line.Contains("|SHOTGUN_OUTLINED|")) line = line.Replace("|SHOTGUN_OUTLINED|", (OutlinedAdditionnalCrosshairs[8] ? "1" : "0"));
                if (line.Contains("|357_ANTIALIASED|")) line = line.Replace("|357_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[9] ? "0" : "1"));
                if (line.Contains("|357_OUTLINED|")) line = line.Replace("|357_OUTLINED|", (OutlinedAdditionnalCrosshairs[9] ? "1" : "0"));
                if (line.Contains("|CROSSBOW_ANTIALIASED|")) line = line.Replace("|CROSSBOW_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[10] ? "0" : "1"));
                if (line.Contains("|CROSSBOW_OUTLINED|")) line = line.Replace("|CROSSBOW_OUTLINED|", (OutlinedAdditionnalCrosshairs[10] ? "1" : "0"));
                if (line.Contains("|RPG_ANTIALIASED|")) line = line.Replace("|RPG_ANTIALIASED|", (OutlinedAdditionnalCrosshairs[11] ? "0" : "1"));
                if (line.Contains("|RPG_OUTLINED|")) line = line.Replace("|RPG_OUTLINED|", (OutlinedAdditionnalCrosshairs[11] ? "1" : "0"));

                sw.WriteLine(line);
            }

            sr.Close();
            sw.Close();
        }

        public void WriteChatScheme(ref UserPaths Paths)
        {
            string file = Paths.ResPath + "chatscheme.res";
            File.WriteAllText(file, "");
            StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources.ChatScheme.res"));
            StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("|CHAT_FONT|")) line = line.Replace("|CHAT_FONT|", ChatFont);

                sw.WriteLine(line);
            }

            sr.Close();
            sw.Close();
        }

        public void AddFonts(ref UserPaths Paths)
        {
            File.WriteAllBytes(Paths.FontsPath + @"XHAIR.ttf", HL2Customizer.Resources.resfile.XHAIR);
            File.WriteAllBytes(Paths.FontsPath + @"brands.ttf", HL2Customizer.Resources.resfile.brands);
            File.WriteAllBytes(Paths.FontsPath + @"Defused.ttf", HL2Customizer.Resources.resfile.Defused);
            File.WriteAllBytes(Paths.FontsPath + @"Dodger.ttf", HL2Customizer.Resources.resfile.Dodger);
            File.WriteAllBytes(Paths.FontsPath + @"DS-DIGIT.ttf", HL2Customizer.Resources.resfile.DS_DIGIT);
            File.WriteAllBytes(Paths.FontsPath + @"Icons.ttf", HL2Customizer.Resources.resfile.Icons);
            File.WriteAllBytes(Paths.FontsPath + @"Turok.ttf", HL2Customizer.Resources.resfile.Turok);
        }
    }
}