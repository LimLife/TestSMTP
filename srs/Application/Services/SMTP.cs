namespace Application.Services
{
    /// <summary>
    /// Get to applications.json Params, for SMTP
    /// </summary>
    public class SMTP
    {      
        public string SmtpServer { get; set; }
       
        public int SmtpPort { get; set; }
        
        public string SmtpUsername { get; set; }
       
        public string SmtpPassword { get; set; }
    }
}
