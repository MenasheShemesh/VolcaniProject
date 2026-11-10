using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DataAccessLayer
{
    public class Dal
    {
        private static Dal instance;

        public static Dal Instance
        {
            get
            {
                if (instance == null)
                    instance = new Dal();
                return instance;
            }
        }
        public Dal()
        {
        }

        private SqlConnection CreateConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"];
            return connection;
        }

        public DataTable FillDataTable(SqlCommand command)
        {
            try
            {
                command.Connection = CreateConnection();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                return dataTable;
            }

            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool SqlExecuteNonQuery(SqlCommand command)
        {
            try
            {
                command.Connection = CreateConnection();
                command.Connection.Open();

                command.ExecuteNonQuery();

                return int.Parse(command.Parameters["@RowsAffected"].Value.ToString()) > 0;
            }

            catch(Exception exception)
            {
                throw exception;
            }

            finally
            {
                command.Connection.Close();
            }
        }

        public SqlCommand CreateSPCommand(string procedureName, bool isRowsAffected = false)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;

            if(isRowsAffected)
                command.Parameters.Add("@RowsAffected", SqlDbType.Int).Direction = ParameterDirection.Output;

            return command;
        }
    }
}
