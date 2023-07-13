using Domain.Enums;
using Domain.Services;
using System.Text.Json.Serialization;

namespace Domain.Entity
{
    public class MessageResult
    {
        public int Id { get; set; }
        public int MessageID { get; set; }
        public Message Message { get; set; }
        public StateResult State { get; set; }
    }
}
