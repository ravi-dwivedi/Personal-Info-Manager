using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Person.Shared
{
    public interface IAgreementDAL
    {
        IuserDTO ReadUser(int userID);
        IList<IuserDTO> readAllUser();
        int insertUser(IuserDTO user);
        bool deleteUser(int userId);
        bool updateUserInfo(int id, IuserDTO user);
    }
}
