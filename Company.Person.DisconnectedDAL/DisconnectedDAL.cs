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


namespace Company.Person.DAL.Disconnected
{
    public class DisconnectedDAL : IAgreementDAL
    {
       
        private string connectionString = string.Empty;

        private string provider;
        private DbConnection connection = null;

        public DisconnectedDAL()
        {
            this.connectionString = "Data Source=RAVI4166;Initial Catalog=Person;Integrated Security=True";
            this.provider = "System.Data.SqlClient";
        }

        public IuserDTO ReadUser(int userID)
        {
            IuserDTO user = new userDTO();
            using (this.connection = DbProviderFactories.GetFactory(provider).CreateConnection())
            {
                DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
                builder.ConnectionString = this.connectionString;
                this.connection.ConnectionString = builder.ConnectionString;
                connection.Open();
                DataSet person = new DataSet();
                DbDataAdapter tableAdapter = DbProviderFactories.GetFactory(provider).CreateDataAdapter();
                DbCommand cmd = DbProviderFactories.GetFactory(provider).CreateCommand();
                string sql = "Select * FROM userInfo where Id=" + userID;
                cmd.Connection = this.connection;
                cmd.CommandText = sql;

                tableAdapter.SelectCommand = cmd;

                tableAdapter.Fill(person, "Person");

                if (person != null & person.Tables.Count > 0)
                {
                    foreach (DataRow dataRow in person.Tables["Person"].Rows)
                    {
                        user = new userDTO((int)dataRow["Id"]);
                        user.AGE = (int)dataRow["Age"];
                        user.GENDER = (int)dataRow["Gender"];
                        user.NAME = (string)dataRow["Name"];
                        user.MARRIAGESTATUS = (int)dataRow["MarriageStatus"];
                    }
                }


            }
            return user;
        }
        public IList<IuserDTO> readAllUser()
        {
            IList<IuserDTO> Ilist = new List<IuserDTO>();

            using (this.connection = DbProviderFactories.GetFactory(provider).CreateConnection())
            {
                DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
                builder.ConnectionString = this.connectionString;
                this.connection.ConnectionString = builder.ConnectionString;
                connection.Open();
                DataSet person = new DataSet();
                DbDataAdapter tableAdapter = DbProviderFactories.GetFactory(provider).CreateDataAdapter();
                DbCommand cmd = DbProviderFactories.GetFactory(provider).CreateCommand();
                cmd.Connection = this.connection;
                cmd.CommandText = "Select * FROM userInfo";
                tableAdapter.SelectCommand = cmd;

                tableAdapter.Fill(person, "Person");

                if (person != null & person.Tables.Count > 0)
                {
                    foreach (DataRow dataRow in person.Tables["Person"].Rows)
                    {
                        IuserDTO user = new userDTO((int)dataRow["Id"]);
                        user.AGE = (int)dataRow["Age"];
                        user.GENDER = (int)dataRow["Gender"];
                        user.NAME = (string)dataRow["Name"];
                        user.MARRIAGESTATUS = (int)dataRow["MarriageStatus"];
                        Ilist.Add(user);
                    }
                }
            }
            return Ilist;
        }

        public int insertUser(IuserDTO user)
        {
            var retVal = default(int);
            using (this.connection = DbProviderFactories.GetFactory(provider).CreateConnection())
            {
                using (DbDataAdapter tableAdapter = DbProviderFactories.GetFactory(provider).CreateDataAdapter())
                {
                    DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
                    builder.ConnectionString = this.connectionString;
                    this.connection.ConnectionString = builder.ConnectionString;
                    connection.Open();
                     DbCommand cmd = DbProviderFactories.GetFactory(provider).CreateCommand();
                     cmd.Connection = this.connection;
                cmd.CommandText = string.Format("INSERT INTO userInfo (Name,Age,Gender,MarriageStatus)" +
                " VALUES('{0}',{1},{2},{3})", user.NAME, user.AGE, user.GENDER, user.MARRIAGESTATUS);
                tableAdapter.InsertCommand = cmd;
                retVal = (int)tableAdapter.InsertCommand.ExecuteScalar();
                }
            }
            return retVal;
        }

        public bool deleteUser(int userId)
        {
            int retVal;
            using (this.connection = DbProviderFactories.GetFactory(provider).CreateConnection())
            {
                DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
                builder.ConnectionString = this.connectionString;
                this.connection.ConnectionString = builder.ConnectionString;
                connection.Open();

                    DbDataAdapter tableAdapter = DbProviderFactories.GetFactory(provider).CreateDataAdapter();
                    DbCommand cmd = DbProviderFactories.GetFactory(provider).CreateCommand();
                    cmd.Connection = this.connection;
                    cmd.CommandText = string.Format("DELETE FROM userInfo WHERE " + "Id={0}", userId);
                    tableAdapter.DeleteCommand = cmd;

                    retVal = tableAdapter.DeleteCommand.ExecuteNonQuery();
                   
                
                
            }

            if (retVal == 1)
                return true;
            else
                return false;
        }

        public bool updateUserInfo(int id, IuserDTO user)
        {
            int retVal;
            using (this.connection = DbProviderFactories.GetFactory(provider).CreateConnection())
            {
                using (DbDataAdapter tableAdapter = DbProviderFactories.GetFactory(provider).CreateDataAdapter())
                {
                    DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
                    builder.ConnectionString = this.connectionString;
                    this.connection.ConnectionString = builder.ConnectionString;
                    connection.Open();

                    DbCommand cmd = DbProviderFactories.GetFactory(provider).CreateCommand();
                    cmd.Connection = this.connection;
                    cmd.CommandText = string.Format("UPDATE userInfo SET " +
                "Name='{0}', Age={1}, Gender={2}, MarriageStatus={3}" +
                " WHERE Id={4}", user.NAME, user.AGE, user.GENDER, user.MARRIAGESTATUS, id);
                    tableAdapter.UpdateCommand = cmd;
                   retVal =  tableAdapter.UpdateCommand.ExecuteNonQuery();
                }
            }
            if (retVal == 1)
                return true;
            else
                return false;
        }
    }
}
