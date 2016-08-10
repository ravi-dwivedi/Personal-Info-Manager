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


namespace Company.Person.DAL.Connected
{
    public class DALLogic:IAgreementDAL
    {
        private string provider;
        private string connectionString;
        private DbConnection connection;
        private DbProviderFactory dfp;

        private void Connect()
        {
            this.provider = System.Configuration.ConfigurationManager.AppSettings["provider"];
            this.connectionString = "Data Source=RAVI4166;Initial Catalog=Person;Integrated Security=True";           //System.Configuration.ConfigurationSettings.AppSettings["connectionString"];
            this.dfp = DbProviderFactories.GetFactory(provider);
            this.connection = this.dfp.CreateConnection();
            DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
            builder.ConnectionString = this.connectionString;
            this.connection.ConnectionString = builder.ConnectionString;
            connection.Open();
        }
        private void Disconnect()
        {
            connectionString = null;
            provider = null;
            dfp = null;
            connection.Close();
        }
        public IuserDTO ReadUser(int userID)
        {
            Connect();
            DbCommand cmd = this.dfp.CreateCommand();
            string sql = "Select * FROM userInfo where Id=" + userID;
            cmd.Connection = this.connection;
            cmd.CommandText = sql;

            IuserDTO user;
            DbDataReader dr = cmd.ExecuteReader();
            {
                dr.Read();
                user = new userDTO(int.Parse(dr["Id"].ToString()));
                user.NAME = dr["Name"].ToString();
                user.AGE = int.Parse(dr["Age"].ToString());
                user.GENDER = int.Parse(dr["Gender"].ToString());
                user.MARRIAGESTATUS = int.Parse(dr["MarriageStatus"].ToString());
            }
            Disconnect();
            return user;
        }
        public IList<IuserDTO> readAllUser()
        {
            Connect();
            DbCommand cmd = this.dfp.CreateCommand();
            string sql = "Select * FROM userInfo";
            cmd.Connection = this.connection;
            cmd.CommandText = sql;
            IList<IuserDTO> Ilist = new List<IuserDTO>();
            IuserDTO user;
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                while(dr.Read())
                {
                    user = new userDTO(int.Parse(dr["Id"].ToString()));
                    user.NAME = dr["Name"].ToString();
                    user.AGE = int.Parse(dr["Age"].ToString());
                    user.GENDER = int.Parse(dr["Gender"].ToString());
                    user.MARRIAGESTATUS = int.Parse(dr["MarriageStatus"].ToString());
                    Ilist.Add(user);
                }
            }
            Disconnect();
            return Ilist;
        }

        public int insertUser(IuserDTO user)
        {
            Connect();
            DbCommand cmd = this.dfp.CreateCommand();
            string sql = string.Format("INSERT INTO userInfo (Id,Name,Age,Gender,MarriageStatus)" +
                " VALUES(67,'{0}',{1},{2},{3})", user.NAME, user.AGE, user.GENDER, user.MARRIAGESTATUS);
            cmd.Connection = this.connection;
            cmd.CommandText = sql;
            int rows = cmd.ExecuteNonQuery();
            if (rows != 1)
            {
                Disconnect();
                return -1;
            }
            else
            {
                sql = "SELECT ISNULL(@@IDENTITY,0)";
                cmd.CommandText = sql;
                int userID = int.Parse(cmd.ExecuteScalar().ToString());
                Disconnect();
                return userID;
            }

        }

        public bool deleteUser(int userId)
        {
            Connect();

            DbCommand cmd = this.dfp.CreateCommand();
            string sql = string.Format("DELETE FROM userInfo WHERE " +
                "Id={0}", userId);

            cmd.Connection = this.connection;
            cmd.CommandText = sql;
            int rows = cmd.ExecuteNonQuery();
            Disconnect();
            if (rows == 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updateUserInfo(int id, IuserDTO user)
        {
            Connect();

            DbCommand cmd = this.dfp.CreateCommand();
            string sql = string.Format("UPDATE userInfo SET " +
                "Name='{0}', Age={1}, Gender={2}, MarriageStatus={3}" +
                " WHERE Id={4}", user.NAME, user.AGE, user.GENDER, user.MARRIAGESTATUS, id);
            cmd.Connection = this.connection;
            cmd.CommandText = sql;
            int rows = cmd.ExecuteNonQuery();
            Disconnect();
            if (rows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
