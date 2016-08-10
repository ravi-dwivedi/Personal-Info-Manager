using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Person.Shared
{
    public interface IuserDTO
    {

        int ID
        {
            get;
        }

        string NAME
        {
            get;
            set;
        }

        int AGE
        {
            get;
            set;
        }

        int GENDER
        {
            get;
            set;
        }

        int MARRIAGESTATUS
        {
            get;
            set;
        }
    }
}
