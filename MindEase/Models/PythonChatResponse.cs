namespace MindEase.Models
{
    public class PythonChatResponse
    {
        public string? reply { get; set; }
        public string? emotion { get; set; }
        public List<string>? practice { get; set; }

        // FastAPI returns this if the try/catch in chat_service.py fails
        public string? error { get; set; }
    }
}
