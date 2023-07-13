namespace Domain.Entity
{
    public class Message
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Recipient> Recipients { get; set; }
        public DateTime DateSend { get; set; }
        public List<MessageResult> Result { get; set; }
        public List<FailedMessage> Failed { get; set; }
    }
}
