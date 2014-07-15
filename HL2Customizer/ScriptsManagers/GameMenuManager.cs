using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HL2Customizer
{
    [Serializable]
    public class GameMenuManager
    {
        public MenuElement[] MenuLabels { get; set; }
        public bool[] Options { get; set; }

        public GameMenuManager()
        {
            MenuLabels = new MenuElement[50];
            Options = new bool[7];
            Options[0] = true;
        }

        public void WriteFile(ref UserPaths Paths)
        {
            string file = Paths.ResPath + "gamemenu.res";
            if (MenuLabels.Count(s => s != null) > 0)
            {
                File.WriteAllText(file, "");
                StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));
                sw.WriteLine("GameMenu");
                sw.WriteLine("{");
                for (int i = 49; i > 0; i--)
                {
                    MenuElement e = MenuLabels[i];
                    if (e != null)
                        e.MakeScript(sw);
                }
                sw.WriteLine("}");
                sw.Close();
            }
            else if (File.Exists(file))
                File.Delete(file);
        }
    }
}
