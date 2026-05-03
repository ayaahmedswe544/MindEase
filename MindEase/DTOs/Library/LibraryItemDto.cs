using MindEase.Models;

namespace MindEase.DTOs.Library
{
    public class LibraryItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public string ContentUrl { get; set; } = string.Empty;

        public ContentType Type { get; set; }

        public LibraryMood Mood { get; set; }
    }
}
