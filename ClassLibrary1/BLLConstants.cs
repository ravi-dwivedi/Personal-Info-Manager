using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Person.Shared
{
    public class BLLConstants
    {
        public static readonly string OutputBinPath = GetAssembliesSourceOutputBinPath();

        public const string PersonBLL = "Company.Person.BLL.BLL";
        public const string PersonBLLDll = "Company.Person.BLL.dll";

        private static string GetAssembliesSourceOutputBinPath()
        {
            string baseDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string pathToCheck = string.Empty;
            bool isPathFound = false;
            while (!isPathFound)
            {
                DirectoryInfo dInfo = Directory.GetParent(baseDir);
                if (dInfo != null)
                {
                    baseDir = dInfo.FullName;
                    pathToCheck = baseDir + "\\" + System.Configuration.ConfigurationManager.AppSettings["outputBinFolderName"];

                    if (Directory.Exists(pathToCheck))
                    {
                        isPathFound = true;
                    }
                }
            }

            return pathToCheck + Path.DirectorySeparatorChar;
        }
    }
}
