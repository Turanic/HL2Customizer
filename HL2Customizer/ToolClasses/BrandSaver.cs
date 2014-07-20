using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL2Customizer
{
    [Serializable]
    public class BrandSaver
    {
        public Boolean Enabled { get; private set; }

        private int _brandType;

        public BrandSaver ()
        {
            Enabled = false;
            _brandType = 1;
        }

        public Tuple<string, string>[] GetBrand (string color)
        {
            Tuple<string, string>[] Brand;
            Brand = new Tuple<string, string>[]{
                    Tuple.Create("controlName","Label"),
                    Tuple.Create("fieldName","SpeBrand"),
                    Tuple.Create("visible","1"),
                    Tuple.Create("enabled","1"),
                    Tuple.Create("labelText", _brandType.ToString()),
                    Tuple.Create("textAlignment","left"),
                    Tuple.Create("xpos","16"),
                    Tuple.Create("ypos","50"),
                    Tuple.Create("wide","128"),
                    Tuple.Create("tall","32"),
                    Tuple.Create("font","SpeBrand"),
                    Tuple.Create("fgcolor_override", color),
                    };
            return Brand;
        }

        public bool ApplyBrand(string pass, string color)
        {
            if(pass == "homeworld")
                _brandType = 0;
            else if (pass == "TrustSkill")
                _brandType = 1;
            else if (pass == "lambda")
                _brandType = 2;
            else if (pass == "D1ablot1ns")
                _brandType = 3;
            else
                return false;

            Enabled = true;
            return true;
        }
    }
}
