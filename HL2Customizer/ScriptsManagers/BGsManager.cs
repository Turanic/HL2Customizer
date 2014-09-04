using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Net;

namespace HL2Customizer
{
    [Serializable]
    public class BGsManager
    {
        public string[] Locals2dBGs { get; private set; }
        public string[] LocalsMapBGs { get; private set; }
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
            this.Locals2dBGs = new string[] { "2dbg_default.vtf" };
            if (Directory.Exists(LastBGDirPath))
            {
                Locals2dBGs = Directory.GetFiles(LastBGDirPath, "*.vtf");
                for (int i = 0; i < Locals2dBGs.Count(); i++)
                {
                    string[] aux = Locals2dBGs[i].Split('/');
                    Locals2dBGs[i] = aux[aux.Count() - 1];
                    if (Locals2dBGs[i].Contains("widescreen") || Locals2dBGs[i].Contains("effect"))
                        Locals2dBGs[i] = null;
                }
            }
            Locals2dBGs = Locals2dBGs.Where(x => x != null).ToArray();
            for (int i = 0; i < Locals2dBGs.Count(); i++)
            {
                Locals2dBGs[i] = Locals2dBGs[i].Substring(5);
                Locals2dBGs[i] = Locals2dBGs[i].Substring(0, Locals2dBGs[i].Length - 4);
            }
        }

        public void Apply2dBackground(ref UserPaths Paths, string BGname)
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
            bgfile = "2dbg_" + BGname + ".vtf";

            File.WriteAllText(file, "");
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
            File.WriteAllText(file, "");
            bgfile = "2dbg_" + BGname + "_widescreen.vtf";

            sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("|BG|")) line = line.Replace("|BG|", bgfile);
                if (line.Contains("|EFFECT|")) line = line.Replace("|EFFECT|", "background01_effect_1610.vtf");
                sw.WriteLine(line);
            }
            sw.Close();
        }

        public void DownloadBG(string BGname, ref UserPaths Paths)
        {
            if (!this.Locals2dBGs.Contains(BGname))
            {
                System.Windows.MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("You're going to download \"" + BGname + "\".",
                    "Download confirmation", System.Windows.MessageBoxButton.OKCancel);
                if (messageBoxResult == System.Windows.MessageBoxResult.OK)
                {
                    WebClient webClient = new WebClient();
                    string onlineFile = "http://turanic.com/HL2Customizer/dlable_content/2dbg/" + BGname + ".zip";
                    webClient.DownloadFile(onlineFile, Paths.BackgroundsPath + BGname + ".zip");
                    Extracter.Extract(Paths.BackgroundsPath, BGname + ".zip");
                }
            }
        }

        public void DownloadMapBG(string Mapname, ref UserPaths Paths)
        {
            if (true/*!this.LocalsMapBGs.Contains(Mapname)*/)
            {
                System.Windows.MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("You're going to download \"" + Mapname + "\".",
                            "Download confirmation", System.Windows.MessageBoxButton.OKCancel);
                if (messageBoxResult == System.Windows.MessageBoxResult.OK)
                {
                    WebClient webClient = new WebClient();
                    string onlineFile = "http://turanic.com/HL2Customizer/dlable_content/3dbg/" + Mapname + ".zip";
                    webClient.DownloadFile(onlineFile, Paths.MapsPath + Mapname + ".zip");
                    Extracter.Extract(Paths.MapsPath, Mapname + ".zip");
                }            
            }
        }
    }
}
