using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;


namespace HL2Customizer
{
    class Extracter
    {

        public static void Extract(string sourceDirectory, string archiveName)
        {
            try
            {
                ZipFile.ExtractToDirectory(sourceDirectory + archiveName, sourceDirectory);
            }
            catch
            {
                // Probaby an overwrite bug, not rly important
            }
            File.Delete(sourceDirectory + archiveName);
        }

    }
}
