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
    public class WeaponScriptManager
    {
        public WeaponScript[] Weapons { get; set; }
        public bool KeepXhair { private get; set; }

        public WeaponScriptManager()
        {
            Weapons = new WeaponScript[]
            {
                new WeaponScript("Gravity gun", "physcannon", ' '),
                new WeaponScript("Crowbar", "crowbar", ' '),
                new WeaponScript("Stunstick", "stunstick", ' '),
                new WeaponScript("Frag grenade", "frag", ' '),
                new WeaponScript("S.L.A.M", "slam", ' '),
                new WeaponScript("9mm Pistol", "pistol", ' '),
                new WeaponScript("Sub machine gun", "smg1", ' '),
                new WeaponScript("Rifle", "ar2", ' '),
                new WeaponScript("Shotgun", "shotgun", ' '),
                new WeaponScript("357 Magnum", "357", ' '),
                new WeaponScript("Crossbow", "crossbow", ' '),
                new WeaponScript("Rocket launcher", "rpg", ' '),
            };
        }

        public WeaponScriptManager(WeaponScriptManager loadedWSM)
        {
            Weapons = loadedWSM.Weapons;
        }

        public void WriteFiles(ref UserPaths Paths)
        {
            for (int i = 0; i < Weapons.Count(); i++)
            {
                string fileName = "weapon_" + Weapons[i].ScriptFile + ".txt";
                string file = Paths.ScriptsPath + fileName;
                File.WriteAllText(file, "");

                StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources." + fileName));
                StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("|NAME|")) line = line.Replace("|NAME|", Weapons[i].Name);
                    if (line.Contains("|KEEPXHAIR|")) line = line.Replace("|KEEPXHAIR|", (KeepXhair ? "Crosshairs" : Weapons[i].ScriptFile+"_crosshair"));
                    if (line.Contains("|CROSSHAIR|")) line = line.Replace("|CROSSHAIR|", (KeepXhair ? "Q" : Weapons[i].Crosshair.ToString()));
                    sw.WriteLine(line);
                }

                sr.Close();
                sw.Close();

            }
        }
    }
}
