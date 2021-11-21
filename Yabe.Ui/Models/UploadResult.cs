namespace Yabe.Ui.Models
{
    public class UploadResult
    {
        public bool Success { get; set; }
        public int Count { get; set; }
        public string Message { get; set; }

        public static UploadResult SuccessResult(int count) => new()
        {
            Success = true,
            Count = count
        };

        public static UploadResult ErrorResult(string message) => new()
        {
            Success = false,
            Message = message
        };
    }
}
