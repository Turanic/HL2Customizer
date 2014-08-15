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
    public class BGsManager
    {
        public string[] LocalsBGs { get; set; }
        public string LastBGDirPath { get; private set; }
        public string LastBGName { get; private set; }
        public bool SmokeEffects { get; set; }
        public bool MapBG { get; set; }

        public BGsManager()
        {
            FillLocalBGs();
            LastBGDirPath = null;
            LastBGName = "default";
            SmokeEffects = false;
            MapBG = false;
        }

        public void FillLocalBGs()
        {
            LocalsBGs = new string[] { "2dbg_default.vtf" };
            if(Directory.Exists(LastBGDirPath))
                LocalsBGs.Concat(Directory.GetFiles(LastBGDirPath, "*.vtf"));
            LocalsBGs.Select(x => !x.Contains("widescreen"));
            for(int i = 0; i < LocalsBGs.Count(); i++)
            {
                LocalsBGs[i] = LocalsBGs[i].Substring(5);
                LocalsBGs[i] = LocalsBGs[i].Substring(0, LocalsBGs[i].Length - 4);
            }
        }

        public void ApplyBackground(ref UserPaths Paths, string BGname)
        {
            LastBGDirPath = Paths.BackgroundsPath;
            LastBGName = BGname;
            string file, bgfile;

            StreamReader sr;
            if(SmokeEffects)
                sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources.UnlitTwoTexture.vmt"));
            else
                sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources.UnlitGeneric.vmt"));

            file = Paths.BackgroundsPath + @"background01.vmt";
            bgfile = "2dbg_" + BGname + ".vmt";
            
            StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));
            
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("|BG|")) line = line.Replace("|BG|", bgfile);
                if (line.Contains("|EFFECT|")) line = line.Replace("|EFFECT|", "background01_effect_43.vtf");
                sw.WriteLine(line);
            }

            sw.Close();
            sr.DiscardBufferedData();
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            sr.BaseStream.Position = 0;

            file = Paths.BackgroundsPath + @"background01_widescreen.vmt";
            bgfile = "2dbg_" + BGname + "_widescreen.vmt";

            sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("|BG|")) line = line.Replace("|BG|", bgfile);
                if (line.Contains("|EFFECT|")) line = line.Replace("|EFFECT|", "background01_effect_1610.vtf");
                sw.WriteLine(line);
            }
            sw.Close();
        }
    }
}
