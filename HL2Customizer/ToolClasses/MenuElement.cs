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
        public int Id { get; private set; }
        public string Label { get; private set; }
        private string _command;
        private bool _onlyInGame;
        public MenuElement(int no, string lbl, string command, bool onlyingame)
        {
            Id = no;
            Label = lbl;
            _command = command;
            _onlyInGame = onlyingame;
        }
        public void MakeScript(StreamWriter sw)
        {
            string indent = "	";
            sw.WriteLine(indent + "\"" + Id + "\"");
            sw.WriteLine(indent + "{");
            sw.WriteLine(indent + indent + "\"label\"  \"" + Label + "\"");
            sw.WriteLine(indent + indent + "\"command\"  \"" + _command + "\"");
            sw.WriteLine(indent + indent + "\"OnlyInGame\"  \"" + (_onlyInGame ? "1" : "0" )+ "\"");
            sw.WriteLine(indent + "}");
        }
    }
}
