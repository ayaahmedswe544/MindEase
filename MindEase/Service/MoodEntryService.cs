using MindEase.DTOs.MoodEntry;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Service
{
    public class MoodEntryService : IMoodEntryService
    {
        private readonly IMoodEntryRepo _repo;
        public MoodEntryService(IMoodEntryRepo repo)
        {
            _repo = repo;
        }
        public async Task<GeneralResponse<MoodEntryDto>> CreateAsync(CreateMoodEntryDto moodEntrydto, string userId)
        {
            if (moodEntrydto == null)
            {
                return new GeneralResponse<MoodEntryDto>
                {
                    Success = false,
                    Message = "Mood entry data is required.",
                    Data = null
                };
            }
            try
            {
                var newMoodEntry = new MoodEntry
                {
                    MoodType = moodEntrydto.MoodType,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    UserId = userId
                };
                var result = await _repo.CreateAsync(newMoodEntry);
                if (result.Success)
                {
                    var moodEntryDto = new MoodEntryDto
                    {
                        Id = result.Data.Id,
                        MoodType = result.Data.MoodType,
                        Date = result.Data.Date


                    };
                    return new GeneralResponse<MoodEntryDto>
                    {
                        Success = true,
                        Message = "Mood entry created successfully.",
                        Data = moodEntryDto
                    };
                }
                else
                {
                    return new GeneralResponse<MoodEntryDto>
                    {
                        Success = false,
                        Message = result.Message,
                        Data = null
                    };

                }
            }
            catch (Exception ex)
            {
                return new GeneralResponse<MoodEntryDto>
                {
                    Success = false,
                    Message = $"An error occurred while creating the mood entry: {ex.Message}",
                    Data = null

                };
            }
        }

        public async Task<GeneralResponse<bool>> DeleteAsync(int id,string userId)
        {
            if(id <= 0)
            {
                return new GeneralResponse<bool>
                {
                    Success = false,
                    Message = "Invalid mood entry ID.",
                    Data = false
                };
            }
            else
            {
                try
                {
                    var result = await _repo.DeleteAsync(id, userId);
                    if (result.Success)
                    {
                        return new GeneralResponse<bool>
                        {
                            Success = true,
                            Message = "Mood entry deleted successfully.",
                            Data = true
                        };
                    }
                    else
                    {
                        return new GeneralResponse<bool>
                        {
                            Success = false,
                            Message = result.Message,
                            Data = false
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new GeneralResponse<bool>
                    {
                        Success = false,
                        Message = $"An error occurred while deleting the mood entry: {ex.Message}",
                        Data = false
                    };
                }
            }
        }

        public async Task<GeneralResponse<List<MoodEntryDto>>> GetAllAsync(string userId)
        {
            if (userId == null)
            {
                return new GeneralResponse<List<MoodEntryDto>>
                {
                    Success = false,
                    Message = "User ID is required.",
                    Data = null
                };
            }
            try
            {
                var result = await _repo.GetAllAsync(userId);
                if (result.Success)
                {
                    var moodEntryDtos = result.Data.Select(m => new MoodEntryDto
                    {
                        Id = m.Id,
                        MoodType = m.MoodType,
                        Date = m.Date
                    }).ToList();
                    return new GeneralResponse<List<MoodEntryDto>>
                    {
                        Success = true,
                        Message = "Mood entries retrieved successfully.",
                        Data = moodEntryDtos
                    };
                }
                else
                {
                    return new GeneralResponse<List<MoodEntryDto>>
                    {
                        Success = false,
                        Message = result.Message,
                        Data = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<MoodEntryDto>>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving mood entries: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse<MoodEntryDto>> GetByIdAsync(int id,string userId)
        {
            if (id == 0)
            {
                return new GeneralResponse<MoodEntryDto>
                {
                    Success = false,
                    Message = "Mood entry ID is required.",
                    Data = null
                };
            }
            try
            {
                

                var result = await _repo.GetByIdAsync(id, userId);
                if (result.Success)
                {
                    var moodEntryDto = new MoodEntryDto
                    {
                        Id = result.Data.Id,
                        MoodType = result.Data.MoodType,
                        Date = result.Data.Date
                    };
                    return new GeneralResponse<MoodEntryDto>
                    {
                        Success = true,
                        Message = "Mood entry retrieved successfully.",
                        Data = moodEntryDto
                    };
                }
                else
                {
                    return new GeneralResponse<MoodEntryDto>
                    {
                        Success = false,
                        Message = result.Message,
                        Data = null
                    };

                }
            }
            catch (Exception ex)
            {
                return new GeneralResponse<MoodEntryDto>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving the mood entry: {ex.Message}",
                    Data = null
                };
            }
        }
        public async Task<GeneralResponse<MoodEntryDto>>UpdateAsync(updateMoodEntryDto moodEntry,string userId)
        {
            if(moodEntry == null)
            {
                return new GeneralResponse<MoodEntryDto>
                {
                    Success = false,
                    Message = "Mood entry data is required.",
                    Data = null
                };
            }
            if (string.IsNullOrEmpty(userId))
            {
                return new GeneralResponse<MoodEntryDto>
                {
                    Success = false,
                    Message = "User ID is required.",
                    Data = null
                };
            }
            try
            {
                var moodEntryDb = new MoodEntry
                {
                    Id = moodEntry.Id,
                    MoodType = moodEntry.MoodType,
                    UserId = userId
                };

                var result = await _repo.UpdateAsync(moodEntryDb, userId);
                if (result.Success)
                {
                    var moodEntryDto = new MoodEntryDto
                    {
                        Id = result.Data.Id,
                        MoodType = result.Data.MoodType,
                        Date = result.Data.Date
                    };
                    return new GeneralResponse<MoodEntryDto>
                    {
                        Success = true,
                        Message = "Mood entry updated successfully.",
                        Data = moodEntryDto
                    };
                }
                else
                {
                    return new GeneralResponse<MoodEntryDto>
                    {
                        Success = false,
                        Message = result.Message,
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new GeneralResponse<MoodEntryDto>
                {
                    Success = false,
                    Message = $"An error occurred while updating the mood entry: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}
