namespace NETCORE.Shared
{
    public class StatusResponseSimple
    {
        public bool Success { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }

        public StatusResponseSimple(bool success, string title)
        {
            TraceId = Guid.NewGuid().ToString();
            Success = success;
            Title = title;
        }
    }
}
