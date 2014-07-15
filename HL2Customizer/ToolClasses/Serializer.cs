using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HL2Customizer
{
    class Serializer
    {

        public static void SerializeHudData(SavedData datas)
        {
            string filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HL2Customizer\\" + datas.Infos.Name + ".hcd";
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, datas);
            stream.Close();
        }

        public static SavedData DeSerializeHudData(string hudname)
        {
            string filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HL2Customizer\\" + hudname + ".hcd";
            SavedData loadedHud;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            loadedHud = (SavedData)bFormatter.Deserialize(stream);
            stream.Close();
            return loadedHud;
        }
    }
}
