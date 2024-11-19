namespace App.Shared.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }

        public ApiResponse(bool success, List<string> errors, T data)
        {
            Success = success;
            Errors = errors;
            Data = data;
        }
    }

}
