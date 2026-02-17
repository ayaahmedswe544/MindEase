using Microsoft.EntityFrameworkCore;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Repo
{
    public class MemoryRepository : IMemoryRepository
    {


            private readonly AppDbContext _context;
            private readonly IImageService _imageService;

            public MemoryRepository(AppDbContext context, IImageService imageService)
            {
                _context = context;
                _imageService = imageService;
            }

            public async Task<GeneralResponse<Memory>> CreateAsync(Memory memory, IFormFile? image)
            {
                try
                {
                    if (image != null)
                    {
                        memory.Image = await _imageService.UploadImageAsync(image, "memories");
                    }

                    _context.Memories.Add(memory);
                    await _context.SaveChangesAsync();

                    return new GeneralResponse<Memory>
                    {
                        Success = true,
                        Data = memory,
                        Message = "Memory created successfully."
                    };
                }
                catch (Exception ex)
                {
                    return new GeneralResponse<Memory>
                    {
                        Success = false,
                        Message = "Failed to create memory.",
                        Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                    };
                }
            }

            public async Task<GeneralResponse<Memory>> GetByIdAsync(int id)
            {
                try
                {
                    var memory = await _context.Memories.FindAsync(id);
                    if (memory == null)
                    {
                        return new GeneralResponse<Memory>
                        {
                            Success = false,
                            Message = "Memory not found."
                        };
                    }

                    return new GeneralResponse<Memory>
                    {
                        Success = true,
                        Data = memory
                    };
                }
                catch (Exception ex)
                {
                    return new GeneralResponse<Memory>
                    {
                        Success = false,
                        Message = "Failed to retrieve memory.",
                        Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                    };
                }
            }

            public async Task<GeneralResponse<bool>> DeleteAsync(int id)
            {
                try
                {
                    var memory = await _context.Memories.FindAsync(id);
                    if (memory == null)
                    {
                        return new GeneralResponse<bool>
                        {
                            Success = false,
                            Message = "Memory not found."
                        };
                    }

                    _context.Memories.Remove(memory);
                    await _context.SaveChangesAsync();

                    return new GeneralResponse<bool>
                    {
                        Success = true,
                        Data = true,
                        Message = "Memory deleted successfully."
                    };
                }
                catch (Exception ex)
                {
                    return new GeneralResponse<bool>
                    {
                        Success = false,
                        Message = "Failed to delete memory.",
                        Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                    };
                }
            }

            public async Task<GeneralResponse<List<Memory>>> GetByUserIdAsync(string userId)
            {
                try
                {
                    var memories = await _context.Memories
                        .Where(m => m.UserId == userId)
                        .ToListAsync();

                    return new GeneralResponse<List<Memory>>
                    {
                        Success = true,
                        Data = memories,
                        Message = "User memories retrieved successfully."
                    };
                }
                catch (Exception ex)
                {
                    return new GeneralResponse<List<Memory>>
                    {
                        Success = false,
                        Message = "Failed to retrieve user memories.",
                        Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                    };
                }
            }
        }
    }
