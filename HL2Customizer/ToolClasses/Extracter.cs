using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ionic.Zip;

namespace HL2Customizer
{
    class Extracter
    {

        public static void Extract(string sourceDirectory, string archiveName)
        {
            using (ZipFile zip1 = ZipFile.Read(sourceDirectory+archiveName))
            {
                foreach (ZipEntry e in zip1)
                {
                    e.Extract(sourceDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            File.Delete(sourceDirectory + archiveName);
        }

    }
}
