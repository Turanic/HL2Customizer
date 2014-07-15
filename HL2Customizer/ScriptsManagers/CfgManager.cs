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
    public class CfgManager
    {
        private string _autoexec;
        private int _rate = 33000, _updaterate = 33, _cmdrate = 33;
        private float _interp = 0.1f;

        public bool Autoswitch { get; private set; }
        public bool DisableMotd { get; private set; }
        public bool QuickInfos { get; private set; }
        public bool ConsoleAtstart { get; private set; }
        public bool DisableSpray { get; private set; }
        public bool EnableConsoleFilter { get; private set; }
        public bool DontModifyRates { get; set; }
        public string Model { get; private set; }
        public string StartWeapon { get; private set; }

        public CfgManager()
        {
            DontModifyRates = true;
            Autoswitch = true;
            ConsoleAtstart = false;
            DisableMotd = false;
            QuickInfos = false;
            DisableSpray = false;
            EnableConsoleFilter = false;
            Model = "models/combine_soldier_prisonguard.mdl";
            StartWeapon = "weapon_physcannon";
        }

        public void SetAutoexec(ref UserPaths Paths)
        {
            _autoexec = Paths.CfgPath + "valve.rc";
        }
        public void SetProperties(bool autoswitch, bool motd, bool console, bool sprays, bool filter, string mdl, string startweapon)
        {
            Autoswitch = autoswitch;
            ConsoleAtstart = console;
            DisableMotd = motd;
            DisableSpray = sprays;
            EnableConsoleFilter = filter;
            Model = mdl;
            StartWeapon = startweapon;
        }

        public void SetQuickInfos(bool value)
        {
            QuickInfos = value;
        }

        public void SetRate(int rate, int upd, int cmd, float interp)
        {
            _rate = rate;
            _updaterate = upd;
            _cmdrate = cmd;
            _interp = interp;
        }

        public void ConfigAutoExec()
        {
            if (!File.Exists(_autoexec)) File.Create(_autoexec).Close();

            File.WriteAllText(_autoexec, "");

            StreamWriter sw  = new StreamWriter(File.Open(_autoexec, System.IO.FileMode.Open));
            StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("HL2Customizer.Resources.autoexec.cfg"));

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("|BRACKETS|")) line = line.Replace("|BRACKETS|", (QuickInfos ? "1" : "0"));
                if (line.Contains("|SPRAY|")) line = line.Replace("|SPRAY|", (DisableSpray ? "1" : "0")); 
                if (line.Contains("|AUTOSWITCH|")) line = line.Replace("|AUTOSWITCH|", (Autoswitch ? "1" : "0"));
                if (line.Contains("|MOTD|")) line = line.Replace("|MOTD|", (DisableMotd ? "1" : "0"));
                if (line.Contains("|CONSOLE|")) line = line.Replace("|CONSOLE|", (ConsoleAtstart ? "showconsole" : "hideconsole"));
                if (line.Contains("|MODELNAME|")) line = line.Replace("|MODELNAME|", Model);
                if (line.Contains("|WEAPONNAME|")) line = line.Replace("|WEAPONNAME|", StartWeapon);
                if (line.Contains("|FILTER|")) line = line.Replace("|FILTER|", (EnableConsoleFilter ? "1" : "0"));
                if (DontModifyRates && line.Contains("rate ")) continue;
                else
                {
                    if (line.Contains("|RATE|")) line = line.Replace("|RATE|", _rate.ToString());
                    if (line.Contains("|UPD|")) line = line.Replace("|UPD|", _updaterate.ToString());
                    if (line.Contains("|CMD|")) line = line.Replace("|CMD|", _cmdrate.ToString());
                    if (line.Contains("|INTERP|")) line = line.Replace("|INTERP|", _interp.ToString().Replace(',', '.'));
                }
                sw.WriteLine(line);
            }

            sr.Close();
            sw.Close();
        }
    }
}
