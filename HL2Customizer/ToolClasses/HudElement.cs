using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL2Customizer
{
    [Serializable]
    public struct HudElement
    {
        public string Element { get; private set; }
        public List<Tuple<string, string>> Properties { get; private set; }
        public HudElement(string elmt) : this()
        {
            Element = elmt;
            Properties = new List<Tuple<string, string>>();
        }
    }
}
