namespace HexaCleanHybArch.Template.Shared.DTOs
{
    public class BaseResponse<T>
    {
        public int code { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public object? Details { get; set; }
    }
}
