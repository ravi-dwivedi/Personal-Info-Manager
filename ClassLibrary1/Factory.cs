using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Company.Person.Shared
{
   public class Factory
    {
        public static object CreatInstance(string path, string typeName, object[] args)
        {
            Assembly sourceAssembly = Assembly.LoadFrom(DLLConstants.OutputBinPath + path);
            object instance = sourceAssembly.CreateInstance(typeName, true,
                BindingFlags.CreateInstance, null, args, null, null);

            return instance;
        }
    }
}