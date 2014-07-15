using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL2Customizer 
{
    [Serializable]
    public class SavedData
    {
        public HudInformations Infos {get; set;}
        public ClientSchemeManager Csm { get; set; }
        public SourceSchemeManager Ssm { get; set; }
        public HudLayoutManager Hlm { get; set; }
        public HudAnimationsManager Ham { get; set; }
        public GameMenuManager Gmm { get; set; }
        public CfgManager Cfgm { get; set; }
        public DSPManager Dspm { get; set; }

        public SavedData(HudInformations infos, ClientSchemeManager csm, SourceSchemeManager ssm,
            HudLayoutManager hlm, HudAnimationsManager ham, GameMenuManager gmm, CfgManager cfgm, DSPManager dspm)
        {
             this.Infos = infos;
             this.Csm = csm;
             this.Ssm = ssm;
             this.Hlm = hlm;
             this.Ham = ham;
             this.Gmm = gmm;
             this.Cfgm = cfgm;
             this.Dspm = dspm;
        }
    }
}
