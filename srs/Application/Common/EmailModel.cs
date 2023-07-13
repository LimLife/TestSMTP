using System.Text.Json.Serialization;


namespace Application.Common
{
    /// <summary>
    /// Map to json object
    /// </summary>
    public class EmailModel
    {
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("recipients")]
        public List<string> Recipients { get; set; }
    }
}
