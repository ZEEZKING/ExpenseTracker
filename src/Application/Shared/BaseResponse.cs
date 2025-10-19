
using System.Text.Json.Serialization;

namespace Application.Shared
{
    public class BaseResponse
    {
        [JsonPropertyOrder(0)]
        public string Message { get; set; } = default!;
        [JsonPropertyOrder(1)]
        public bool Success { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        [JsonPropertyOrder(2)]
        public T? Data { get; set; }

        public BaseResponse() { }

        public BaseResponse(string message, bool success, T? data = default)
        {
            Message = message;
            Success = success;
            Data = data;
        }
    }
}
