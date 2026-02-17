namespace MindEase.Models.Response
{
    public class GeneralResponse<T>
    {

            public bool Success { get; set; } = true;
            public string? Message { get; set; }
            public T? Data { get; set; } 
            public Dictionary<string, string[]>? Errors { get; set; }

    }
}
