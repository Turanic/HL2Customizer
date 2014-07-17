using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL2Customizer
{
    [Serializable]
    public class WeaponScript
    {
        public string Name { get; set; }
        public string ScriptFile { get; private set; }
        public WeaponScript(string name, string file)
        {
            Name = name;
            ScriptFile = file;

        }
    }
}
