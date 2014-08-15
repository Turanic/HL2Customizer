using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HL2Customizer
{
    [Serializable]
    public class BGsManager
    {
        public string[] LocalsBGs { get; set; }
        public string LastBGDirPath { get; private set; }
        public string LastBGName { get; private set; }
        public bool SmokeEffects { get; set; }
        public bool MapBG { get; set; }

        public BGsManager()
        {
            FillLocalBGs();
            LastBGDirPath = null;
            LastBGName = "default";
            SmokeEffects = false;
            MapBG = false;
        }

        public void FillLocalBGs()
        {
            if(Directory.Exists(LastBGDirPath))
                LocalsBGs = Directory.GetFiles(LastBGDirPath, "*.vtf");
        }

        public void ApplyBackground(ref UserPaths Paths, string BGname)
        {

        }
    }
}
