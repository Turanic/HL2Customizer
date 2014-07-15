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
    public class HudAnimationsManager
    {
        public int RedScreenDegree { get; private set; }

        public HudAnimationsManager()
        {
            RedScreenDegree = 200;
        }

        public void SetProperties(int redAmount)
        {
            RedScreenDegree = redAmount;
        }

        public void WriteFile(ref UserPaths Paths)
        {

            string file = Paths.ScriptsPath + "hudanimations.txt";
            File.WriteAllText(file, "");
            StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources.hudanimations.txt"));
            StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("$redScreenLow")) line = line.Replace("$redScreenLow", (RedScreenDegree/4).ToString());
                if (line.Contains("$redScreen")) line = line.Replace("$redScreen", RedScreenDegree.ToString());
                sw.WriteLine(line);
            }

            sr.Close();
            sw.Close();
        }
    }
}
