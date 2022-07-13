namespace BookActivity.Application.EventSourcedNormalizers.Models
{
    public class BaseHistoryData
    {
        public string Action { get; set; }
        public string Timestamp { get; set; }
        public string UserId { get; set; }
    }
}
