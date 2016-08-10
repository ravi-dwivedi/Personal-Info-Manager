using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Person.Shared;

namespace Company.Person.BLL
{
    public class BLL:IAgreementBLL
    {
        IAgreementDAL DALref;
        public BLL(string layer,string layerBLL)
        {
            DALref = UserRepository(layer,layerBLL);
        }

        static IAgreementDAL UserRepository(string layer,string layerBLL)
        {

            IAgreementDAL PersonDAL = (IAgreementDAL)Factory.CreatInstance(layerBLL,layer, new object[] { });
            return PersonDAL;
        }



        public IuserDTO ReadUser(int userID)
        {
            return DALref.ReadUser(userID);
        }
        public IList<IuserDTO> readAllUser()
        {
            return DALref.readAllUser();
        }

        public int insertUser(IuserDTO user)
        {
            return DALref.insertUser(user);

        }

        public bool deleteUser(int userId)
        {
            return DALref.deleteUser(userId);
        }

        public bool updateUserInfo(int id, IuserDTO user)
        {
            return DALref.updateUserInfo(id, user);
        }



    }
}
