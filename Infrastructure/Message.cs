using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Infrastructure
{
    public enum MessageStatusCodeEnum
    {
        Pending = 0,
        Success = 1,
        Failed = 2
    }
    public class Message
    {
        public long Id { get; set; }

        [Required]
        public Worker Worker { get; set; }

        [Required]
        public Phone Phone { get; set; }

        [Required]
        [MaxLength(250)]
        public string Content { get; set; }
        public KeyValuePair<int, string>? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public Message()
        {
        }

        public Message(DataRow dataRow)
        {
            this.Id = long.Parse(dataRow["MESSAGE_ID"].ToString());
            this.Worker = new Worker(dataRow["WORKER_FIRST_NAME"].ToString(),
                                     dataRow["WORKER_LAST_NAME"].ToString(),
                                     dataRow["WORKER_CITY"].ToString(),
                                     dataRow["WORKER_STREET"].ToString(),
                                     int.Parse(dataRow["WORKER_HOUSE_NUMBER"].ToString()));
            this.Phone = new Phone(dataRow["MESSAGE_PHONE_PREFIX"].ToString(),
                                   dataRow["MESSAGE_PHONE_NUMBER"].ToString());
            this.Content = dataRow["MESSAGE_CONTENT"].ToString();
            this.Status = new KeyValuePair<int, string>(int.Parse(dataRow["MESSAGE_STATUS_ID"].ToString()),
                                                 dataRow["MESSAGE_STATUS_DESCRIPTION"].ToString());
            this.CreateTime = (DateTime)dataRow["MESSAGE_CREATE_TIME"];

            if (!dataRow.IsNull("MESSAGE_UPDATE_TIME"))
                this.UpdateTime = (DateTime)dataRow["MESSAGE_UPDATE_TIME"];
        }
    }
}
