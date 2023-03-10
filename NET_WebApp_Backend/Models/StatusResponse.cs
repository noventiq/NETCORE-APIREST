namespace NET_WebApp_Backend.Models
{
    public class StatusResponse<T> : StatusResponseSimple
    {
        public T Data { get; set; }
        public StatusResponse(bool success, string title) : base(success, title)
        {
        }
    }
}
