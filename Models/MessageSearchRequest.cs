namespace Server.Models
{
    public class MessageSearchRequest
    {
        public string GroupId { get; set; } = string.Empty;
        public string Keyword { get; set; } = string.Empty;
    }
}