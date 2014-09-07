using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace HL2Customizer
{
    [Serializable]
    public class LangManager
    {
        public int NoHealthIcon { get; set; }
        public int NoAmorIcon { get; set; }

        public LangManager()
        {
            NoHealthIcon = 2;
            NoAmorIcon = 2;
        }

        public void WriteFiles(ref UserPaths Paths)
        {
             //Language files creation
             File.WriteAllBytes(Paths.ResPath + @"valve_lang_files.zip", HL2Customizer.Resources.resfile.valve_lang_files);
             Extracter.Extract(Paths.ResPath, "valve_lang_files.zip");
             string[] files = Directory.GetFiles(Paths.ResPath, "*.txt");
             foreach (string file in files)
             {
                 if (file.Contains("valve_"))
                 {
                     StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources.valve_lang.txt"));
                     StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate), Encoding.Unicode);
                     
                     string line;
                     while ((line = sr.ReadLine()) != null)
                     {
                         if (line.Contains("|HEALTH_ICON|")) line = line.Replace("|HEALTH_ICON|", (NoHealthIcon *2 - 1).ToString());
                         if (line.Contains("|ARMOR_ICON|")) line = line.Replace("|ARMOR_ICON|", (NoAmorIcon * 2).ToString());

                         sw.WriteLine(line);
                     }

                     sr.Close();
                     sw.Close();
                 }
             }
        }
    }
}
