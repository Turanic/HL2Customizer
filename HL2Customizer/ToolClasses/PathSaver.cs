using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HL2Customizer
{
    class PathSaver
    {
        public static string LoadPath()
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/HL2Customizer/";
            string file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/HL2Customizer/PathSave.dat";
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            if (!File.Exists(file)) File.Create(file).Close();
            using(StreamReader sr = new StreamReader(file))
            {
                return sr.ReadLine();
            }

        }

        public static void SavePath(string path)
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/HL2Customizer/";
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            string file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/HL2Customizer/PathSave.dat";
            StreamWriter sw = new StreamWriter(File.Open(file, System.IO.FileMode.OpenOrCreate));
            sw.WriteLine(path);
            sw.Close();
        }
    }
}
