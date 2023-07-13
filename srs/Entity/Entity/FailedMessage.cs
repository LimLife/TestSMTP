using System.Text.Json.Serialization;

namespace Domain.Entity
{
    public class FailedMessage
    {
        public int ID { get; set; }   
        public int MessageID { get; set; }
        public Message Message { get; set; }
        public string MessageFiled { get; set; }
    }
}
