public class ChatMessage
{
    public string Id { get; set; }= string.Empty;
    public string GroupId { get; set; }= string.Empty;
    public string UserName { get; set; }= string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
