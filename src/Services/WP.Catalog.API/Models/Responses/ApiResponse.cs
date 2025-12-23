namespace WP.Catalog.API.Models.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse()
        {
            Errors = new List<string>();
        }

        public ApiResponse(T data, string message = null)
        {
            Success = true;
            Data = data;
            Message = message;
            Errors = new List<string>();
        }

        public ApiResponse(string message, List<string> errors = null)
        {
            Success = false;
            Message = message;
            Errors = errors ?? new List<string>();
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation completed successfully")
        {
            return new ApiResponse<T>(data, message);
        }

        public static ApiResponse<T> ErrorResponse(string message, List<string> errors = null)
        {
            return new ApiResponse<T>(message, errors);
        }
    }
}
