using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;
using Company.Person.Shared;

namespace Company.Person.Console
{
    class Presentation
    {


        static void Main(string[] args)
        {

            int layerType = Convert.ToInt16(ConfigurationManager.AppSettings["layerType"]);
            string layer = string.Empty;
            string layerDLL = string.Empty;
            switch (layerType)
            {
                case 1:
                    layer = DLLConstants.PersonDALConnectedType;
                    layerDLL = DLLConstants.PersonConnectedDALDll;
                    break;
                case 2:
                    layer = DLLConstants.PersonDALDisconnectedType;
                    layerDLL = DLLConstants.PersonDisconnectedDALDll;
                    break;
                case 3:
                    layer = DLLConstants.PersonDALEntityFramework;
                    layerDLL = DLLConstants.PersonEntityFrameworkDALDll;
                    break;
            }
             IAgreementBLL BLLref =   UserRepository(layer,layerDLL);

             char again = 'y';
             while (again == 'y')
             {
                 System.Console.WriteLine(MessageConstants.options);
                 int option = 0;
                 while (option < 1 || option > 6)
                 {
                     bool success = Int32.TryParse(System.Console.ReadLine(), out option);
                     if (success == false)
                     {
                         System.Console.WriteLine(MessageConstants.inputInt);
                     }
                     else if (option < 1 || option > 6)
                     {
                         System.Console.WriteLine(MessageConstants.rangeInput);
                     }
                 }

                 switch (option)
                 {
                     case 1:
                         int id = 0;
                         System.Console.WriteLine("Enter the Id of the User");
                         int.TryParse(System.Console.ReadLine(), out id);
                         IuserDTO user = BLLref.ReadUser(id);
                         System.Console.WriteLine(user);
                         System.Console.WriteLine("------------------------------------");

                         break;
                     case 2:
                         IList<IuserDTO> IList = BLLref.readAllUser();
                         foreach (userDTO u in IList)
                         {
                             System.Console.WriteLine(u);
                             System.Console.WriteLine("------------------------------------");
                         }
                         break;
                     case 3:
                         userDTO newUser = new userDTO();
                         System.Console.WriteLine("Enter the name of the user");
                         newUser.NAME = System.Console.ReadLine();
                         System.Console.WriteLine("Enter the Age of the user");
                         newUser.AGE = int.Parse(System.Console.ReadLine());
                         System.Console.WriteLine("Enter the Gender of the user 1:Male 0:Female");
                         newUser.GENDER = int.Parse(System.Console.ReadLine());
                         System.Console.WriteLine("Enter the MaritalStatus of the user 1:Married 0:Single");
                         newUser.MARRIAGESTATUS = int.Parse(System.Console.ReadLine());
                         int userId = BLLref.insertUser(newUser);
                         System.Console.WriteLine("Id of the new user is : " + userId);
                         System.Console.WriteLine("------------------------------------");
                         break;
                     case 4:
                         System.Console.WriteLine("Enter the Id of the user to delete");
                         int deleteId = int.Parse(System.Console.ReadLine());
                         if (BLLref.deleteUser(deleteId))
                         {
                             System.Console.WriteLine("User Deleted");
                         }
                         else
                         {
                             System.Console.WriteLine("Such user does not exist");
                         }
                         System.Console.WriteLine("------------------------------------");
                         break;
                     case 5:
                         System.Console.WriteLine("Enter the Id of the user to Update");
                         int updateId = int.Parse(System.Console.ReadLine());
                         userDTO updateUser = new userDTO();
                         System.Console.WriteLine("Enter the newname of the user");
                         updateUser.NAME = System.Console.ReadLine();
                         System.Console.WriteLine("Enter the newAge of the user");
                         updateUser.AGE = int.Parse(System.Console.ReadLine());
                         System.Console.WriteLine("Enter the newGender of the user 1:Male 0:Female");
                         updateUser.GENDER = int.Parse(System.Console.ReadLine());
                         System.Console.WriteLine("Enter the newMaritalStatus of the user 1:Married 0:Single");
                         updateUser.MARRIAGESTATUS = int.Parse(System.Console.ReadLine());
                         if (BLLref.updateUserInfo(updateId, updateUser))
                         {
                             System.Console.WriteLine("Information updated");
                         }
                         else
                         {
                             System.Console.WriteLine("Such user does not exist");
                         }

                         System.Console.WriteLine("------------------------------------");

                         break;
                     case 6:
                         System.Environment.Exit(0);
                         break;
                 }
                 System.Console.WriteLine(MessageConstants.again);

                 char.TryParse(System.Console.ReadLine(), out again);
                 again = char.ToLower(again);
             }
            System.Console.ReadLine();

        }

        static IAgreementBLL UserRepository(string layer,string layerDLL)
        {

            IAgreementBLL PersonBll = (IAgreementBLL)Factory.CreatInstance(BLLConstants.PersonBLLDll,BLLConstants.PersonBLL, new object[] { layer,layerDLL });
            return PersonBll;


        }

    }
}
