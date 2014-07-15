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
    public class DSPManager
    {
        public float SoundVolume { get; set; }

        public DSPManager()
        {
            SoundVolume = 1.6f;
        }

        public void WriteFile(ref UserPaths Paths)
        {

            string file = Paths.ScriptsPath + "dsp_presets.txt";
            File.WriteAllText(file, "");
            StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources.dsp_presets.txt"));
            StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("|VALUE|")) line = line.Replace("|VALUE|", SoundVolume.ToString().Replace(',', '.'));
                sw.WriteLine(line);
            }

            sr.Close();
            sw.Close();
        }
    }
}
