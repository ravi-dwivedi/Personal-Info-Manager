using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Company.Person.Shared;



namespace Company.Person.DAL.EntityFrameworkDAL
{
    public class EntityFrameworkDAL : IAgreementDAL
    {

        public IuserDTO ReadUser(int userID)
        {
            IuserDTO user = null;
            using (var database = new Company.Person.EntityFrameworkDAL.PersonEntities())
            {
                var result = (from row in database.userInfoes
                              where row.Id == userID
                              select row).FirstOrDefault();
                if (result != null)
                {
                    user = new userDTO(result.Id);
                    user.NAME = result.Name;
                    user.AGE = result.Age;
                    user.GENDER = result.Gender;
                    user.MARRIAGESTATUS = result.MarriageStatus;
                }
            }
            return user;
        }
        public IList<IuserDTO> readAllUser()
        {
            List<IuserDTO> userList = new List<IuserDTO>();
            using (var database = new Company.Person.EntityFrameworkDAL.PersonEntities())
            {
                database.Database.Connection.Open();

                    var result = from row in database.userInfoes
                                 select row;

                    foreach (var row in result)
                    {
                        IuserDTO user = new userDTO(row.Id);
                        user.NAME = row.Name;
                        user.AGE = row.Age;
                        user.GENDER = row.Gender;
                        user.MARRIAGESTATUS = row.MarriageStatus;
                        userList.Add(user);
                    }
               
            }
            return userList;


        }

        public int insertUser(IuserDTO user)
        {
            using (var database = new Company.Person.EntityFrameworkDAL.PersonEntities())
            {
                int maxID = (from row in database.userInfoes
                             select row.Id).Max();
                Person.EntityFrameworkDAL.userInfo newuser = new Person.EntityFrameworkDAL.userInfo();
                newuser.Id = maxID + 1;
                newuser.Name = user.NAME;
                newuser.MarriageStatus = user.MARRIAGESTATUS;
                newuser.Age = user.AGE;
                newuser.Gender = user.GENDER;
                database.userInfoes.Add(newuser);
                return maxID + 1;
            }

        }

        public bool deleteUser(int userId)
        {

            bool isDeleted = false;

            using (var database = new Company.Person.EntityFrameworkDAL.PersonEntities())
            {
                var result = (from row in database.userInfoes
                              where row.Id == userId
                              select row).FirstOrDefault();
                if (result != null)
                {
                    database.userInfoes.Remove(result);
                    database.SaveChanges();
                    isDeleted = true;
                }
            }
            return isDeleted;
        }

        public bool updateUserInfo(int id, IuserDTO user)
        {
            
                bool isUpdated = false;
            using (var database = new Company.Person.EntityFrameworkDAL.PersonEntities())
            {
               
                    var result = (from row in database.userInfoes
                                  where row.Id == id
                                  select row).FirstOrDefault();
                    if (result != null)
                    {
                        result.Name = user.NAME;
                        result.Age = user.AGE;
                        result.Gender = user.GENDER;
                        result.MarriageStatus = user.MARRIAGESTATUS;
                        database.SaveChanges();
                        isUpdated = true;
                    }
                }
                return isUpdated;
            }


        }
    }


