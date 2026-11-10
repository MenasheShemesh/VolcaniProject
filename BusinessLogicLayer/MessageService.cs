using DataAccessLayer;
using Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class MessageService
    {
        private static MessageService instance;
        private MessageContext context;

        public static MessageService Instance
        {
            get
            {
                if (instance == null)
                    instance = new MessageService();
                return instance;
            }
        }
        public MessageService()
        {
            context = MessageContext.Instance;
        }

        public async Task<bool> AddMessage(Message message)
        {
            try
            {
                message.Id = context.InsertMessage(message);
            }
            catch (Exception exception)
            {
                throw new Exception("Error on Insert Message To the Table", exception);
            }

            try
            {
                message.Status = new KeyValuePair<int, string>(await SendMessageSms(message), null);
            }
            catch (Exception exception)
            {
                throw new Exception("Error on Send External SMS", exception);
            }

            try
            {
                return context.UpdateMessageStatus(message.Id, message.Status.Value.Key);
            }
            catch (Exception exception)
            {
                throw new Exception("Error on Update Message Status", exception);
            }

        }

        public KeyValuePair<int, string> GetMessageStatus(long id)
        {
            DataRow messageRow = context.GetMessageStatus(id);
            return new KeyValuePair<int, string>(int.Parse(messageRow["MESSAGE_STATUS_ID"].ToString()),
                                                 messageRow["MESSAGE_STATUS_DESCRIPTION"].ToString());
        }

        public List<Message> GetMessagesByStatus(int status)
        {
            DataTable messageTable = context.GetMessagesByStatus(status);
            List<Message> messageList = new List<Message>();

            foreach (DataRow dataRow in messageTable.Rows)
                messageList.Add(new Message(dataRow));

            return messageList;
        }

        public Dictionary<int, string> GetAllMessageStatuses()
        {
            DataTable messageStatusesTable = context.GetAllMessageStatuses();
            Dictionary<int, string> messageStatusesDictionary = new Dictionary<int, string>();

            foreach (DataRow dataRow in messageStatusesTable.Rows)
                messageStatusesDictionary.Add(int.Parse(dataRow["STATUS_ID"].ToString()),
                                              dataRow["STATUS_DESCRIPTION"].ToString());

            return messageStatusesDictionary;
        }

        private async Task<int> SendMessageSms(Message message)
        {
            //Random status for the Sample - more than 90% for Success
            Random random = new Random();
            return random.Next(10, 20) / 10;

            HttpClient httpClient = new HttpClient();
            Task<HttpResponseMessage> httpResponse = httpClient.PostAsync($"requestUri/{message.Phone.Prefix}-{message.Phone.Number}", new StringContent(message.Content, Encoding.UTF8, "application/json"));
            if (httpResponse.Result.IsSuccessStatusCode)
                return (int)MessageStatusCodeEnum.Success;
            else
                return (int)MessageStatusCodeEnum.Failed;
        }

    }
}
