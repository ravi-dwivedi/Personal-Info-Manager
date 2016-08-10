using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Person.Shared
{
    public class DLLConstants
    {
        public static readonly string OutputBinPath = GetAssembliesSourceOutputBinPath();

        public const string PersonDALConnectedType = "Company.Person.DAL.Connected.DALLogic";
        public const string PersonDALDisconnectedType = "Company.Person.DAL.Disconnected.DisconnectedDAL";
        public const string PersonDALEntityFramework = "Company.Person.DAL.EntityFrameworkDAL.EntityFrameworkDAL";


        public const string PersonConnectedDALDll = "Company.Person.ConnectedDAL.dll";
        public const string PersonDisconnectedDALDll = "Company.Person.DisconnectedDAL.dll";
        public const string PersonEntityFrameworkDALDll = "Company.Person.EntityFrameworkDAL.dll";

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
