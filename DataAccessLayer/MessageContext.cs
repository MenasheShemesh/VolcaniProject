using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Infrastructure;

namespace DataAccessLayer
{
    public class MessageContext
    {
        private static MessageContext instance;
        private Dal dal;
        private NameValueCollection config;

        public static MessageContext Instance
        {
            get
            {
                if (instance == null)
                    instance = new MessageContext();
                return instance;
            }
        }
        public MessageContext()
        {
            this.config = ConfigurationManager.AppSettings;
            this.dal = Dal.Instance;
        }

        public long InsertMessage(Message message)
        {
            SqlCommand command = dal.CreateSPCommand("InsertMessage", true);  
            
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = message.Worker.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = message.Worker.LastName;
            command.Parameters.Add("@City", SqlDbType.NVarChar).Value = message.Worker.Address.City;
            command.Parameters.Add("@Street", SqlDbType.NVarChar).Value = message.Worker.Address.Street;
            command.Parameters.Add("@HouseNumber", SqlDbType.NVarChar).Value = message.Worker.Address.HouseNumber;
            command.Parameters.Add("@PhonePrefix", SqlDbType.NVarChar).Value = message.Phone.Prefix;
            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = message.Phone.Number;
            command.Parameters.Add("@Content", SqlDbType.NVarChar).Value = message.Content;

            command.Parameters.Add("@Id", SqlDbType.BigInt).Direction = ParameterDirection.Output;

            if (dal.SqlExecuteNonQuery(command))
                return long.Parse(command.Parameters["@Id"].Value.ToString());
            else
                throw new Exception("Error on Insert Message To the Table");
        }

        public bool UpdateMessageStatus(long id, int status)
        {
            SqlCommand command = dal.CreateSPCommand("UpdateMessageStatus", true);

            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
            command.Parameters.Add("@Status", SqlDbType.Int).Value = status;


            return dal.SqlExecuteNonQuery(command);
        }

        public DataRow GetMessageStatus(long id)
        {
            SqlCommand command = dal.CreateSPCommand("GetMessageStatus");

            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

            return dal.FillDataTable(command).Rows?[0];
        }

        public DataTable GetMessagesByStatus(int status)
        {
            SqlCommand command = dal.CreateSPCommand("GetMessagesByStatus");

            command.Parameters.Add("@Status", SqlDbType.Int).Value = status;

            return dal.FillDataTable(command);
        }

        public DataTable GetAllMessageStatuses()
        {
            SqlCommand command = dal.CreateSPCommand("GetAllMessageStatuses");
            return dal.FillDataTable(command);
        }
    }
}
