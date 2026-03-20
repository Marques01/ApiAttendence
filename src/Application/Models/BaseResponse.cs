using System.Net;

namespace Application.Models
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public object Model { get; set; } = new object();
    }
}
