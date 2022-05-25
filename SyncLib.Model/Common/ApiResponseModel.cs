using System.Net;

namespace SyncLib.Model.Common
{
    public class ApiResponseModel
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public dynamic Data { get; set; }
        public ApiResponseModel(HttpStatusCode code, string message, Exception exception, dynamic data = null)
        {
            Success = (int)code >= 200 && (int)code < 300;
            StatusCode = code;
            Message = message;
            Exception = exception;
            Data = data;
        }
    }
}
