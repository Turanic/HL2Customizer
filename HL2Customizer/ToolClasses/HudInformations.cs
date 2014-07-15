using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL2Customizer
{
    [Serializable]
    public class HudInformations
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Creator { get; set; }
        public string HudVersion { get; set; }

        public HudInformations(string name)
        {
            Name = name;
            Date = DateTime.Now.ToString("M/d/yyyy");
            Creator = Environment.UserName;
            if (Creator == "Arthu_000")
                Creator = "Turanic";
            HudVersion = Resources.resfile.Version;
        }
    }
}
