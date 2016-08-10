using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Person.Shared
{
    public class userDTO:IuserDTO
    {
        private int id;
        private string Name;
        private int Age;
        private int Gender;
        private int MarriageStatus;

        public userDTO()
        {
            this.ID = 0;
        }

        public userDTO(int id)
        {
            this.ID = id;
        }
        public int ID
        {
            get
            {
                return this.id;
            }
            private set
            {
                this.id = value;
            }
        }

        public string NAME
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }

        public int AGE
        {
            get
            {
                return this.Age;
            }
            set
            {
                this.Age = value;
            }
        }

        public int GENDER
        {
            get
            {
                return this.Gender;
            }
            set
            {
                this.Gender = value;
            }
        }

        public int MARRIAGESTATUS
        {
            get
            {
                return this.MarriageStatus;
            }
            set
            {
                this.MarriageStatus = value;
            }
        }
        public override string ToString()
        {
            string marriage, gender;
            if (this.GENDER == 1)
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }
            if (this.MARRIAGESTATUS == 1)
            {
                marriage = "Married";
            }
            else
            {
                marriage = "Single";
            }
            return string.Format("Name = {0}\n Age = {1}\nGender = {2}\nMarriage status ={3}\n", this.NAME, this.AGE, gender, marriage);
        }
    }
}
