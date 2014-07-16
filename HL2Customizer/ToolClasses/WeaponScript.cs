using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL2Customizer
{
    [Serializable]
    public struct WeaponScript
    {
        public string Name { get; private set; }
        public WeaponScript(string name) : this()
        {
            Name = name;
        }
    }
}
