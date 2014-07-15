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
        public string Element;
        public List<Tuple<string, string>> properties;
        public HudElement(string element)
        {
            Element = element;
            properties = new List<Tuple<string, string>>();
        }
    }
}
