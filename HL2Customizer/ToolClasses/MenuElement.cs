using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HL2Customizer
{
    [Serializable]
    public class MenuElement
    {
        public int id;
        public string label;
        private string _command;
        private bool _OnlyInGame;
        public MenuElement(int no, string lbl, string command, bool onlyingame)
        {
            id = no;
            label = lbl;
            _command = command;
            _OnlyInGame = onlyingame;
        }
        public void MakeScript(StreamWriter sw)
        {
            string indent = "	";
            sw.WriteLine(indent + "\"" + id + "\"");
            sw.WriteLine(indent + "{");
            sw.WriteLine(indent + indent + "\"label\"  \"" + label + "\"");
            sw.WriteLine(indent + indent + "\"command\"  \"" + _command + "\"");
            sw.WriteLine(indent + indent + "\"OnlyInGame\"  \"" + (_OnlyInGame ? "1" : "0" )+ "\"");
            sw.WriteLine(indent + "}");
        }
    }
}
