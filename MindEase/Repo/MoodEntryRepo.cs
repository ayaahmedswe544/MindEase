using System.Linq;
using Microsoft.EntityFrameworkCore;
using MindEase.IRepo;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Repo

{
    public class MoodEntryRepo : IMoodEntryRepo
    {

        private readonly AppDbContext _context;
        public MoodEntryRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<GeneralResponse<MoodEntry>> CreateAsync(MoodEntry moodEntry)
        {
            if (moodEntry == null)
            {
                return new GeneralResponse<MoodEntry>
                {
                    Success = false,
                    Message = "Mood entry cannot be null."
                };
            }
            try
            {
                var isDuplicate = _context.MoodEntries
                    .Where(me => me.UserId == moodEntry.UserId && me.Date == moodEntry.Date);
                if (isDuplicate.Any())
                {
                    return new GeneralResponse<MoodEntry>
                    {
                        Success = false,
                        Message = "A mood entry for this date already exists."
                    };
                }
                else
                {
                    _context.MoodEntries.Add(moodEntry);
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse<MoodEntry>
                        {
                            Success = true,
                            Message = "Mood entry created successfully.",
                            Data = moodEntry
                        };
                    }
                    else
                    {
                        return new GeneralResponse<MoodEntry>
                        {
                            Success = false,
                            Message = "Failed to create mood entry."
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new GeneralResponse<MoodEntry>
                {
                    Success = false,
                    Message = $"An error occurred while creating the mood entry: {ex.Message}"
                };

            }
        }

       public async Task<GeneralResponse<bool>> DeleteAsync(int id, string userId)
        {
          if(id==0){
                return new GeneralResponse<bool>
                {
                    Success = false,
                    Message = "Invalid mood entry ID.",
                    Data = false

                };
            }
            try {
                var moodEntryDB = await _context.MoodEntries.FirstOrDefaultAsync(d=>d.Id==id&&d.UserId==userId);
                if (moodEntryDB == null)
                {
                   
                        return new GeneralResponse<bool>
                        {
                            Success = false,
                            Message = "Mood entry not found.",
                            Data = false
                        };

                }
                else
                {
                    _context.MoodEntries.Remove(moodEntryDB);
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
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
                            Message = "Failed to delete mood entry.",
                            Data = false
                        };
                    }
                }
            }
            catch( Exception ex) {

                return new GeneralResponse<bool> { 
                Success = false,
                Message = $"An error occurred while deleting the mood entry: {ex.Message}",
                Data = false
                };
                    
                    
            }
          
        }

        public async Task<GeneralResponse<List<MoodEntry>>> GetAllAsync(string userId)
        {
            if(userId == null)
            {
                return new GeneralResponse<List<MoodEntry>>
                {
                    Success = false,
                    Message = "User ID cannot be null."
                };
            }
            try
            {
                List<MoodEntry> moodEntries = await _context.MoodEntries.Where(me => me.UserId == userId).ToListAsync();

                return new GeneralResponse<List<MoodEntry>>
                {
                    Success = true,
                    Message = "Mood entries retrieved successfully.",
                    Data = moodEntries
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<MoodEntry>>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving mood entries: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse<MoodEntry>> GetByIdAsync(int id, string userId)
        {
            if(id == 0)
            {
                return new GeneralResponse<MoodEntry>
                {
                    Success = false,
                    Message = "Invalid mood entry ID.",
                    Data = null
                };
            }

            try
            {
                var moodEntry = await _context.MoodEntries.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);

                if(moodEntry == null)
                {
                    return new GeneralResponse<MoodEntry>
                    {
                        Success = false,
                        Message = "Mood entry not found.",
                        Data = null
                    };
                }
                else
                {
                    return new GeneralResponse<MoodEntry>
                    {
                        Success = true,
                        Message = "Mood entry retrieved successfully.",
                        Data = moodEntry
                    };
                }
            }
            catch (Exception ex)
            {
                return new GeneralResponse<MoodEntry>
                {
                    Success = false,
                    Message = $"An error occurred while retrieving the mood entry: {ex.Message}",
                    Data = null
                };
            }
        }

      public async Task<GeneralResponse<MoodEntry>>UpdateAsync(MoodEntry moodEntry, string userId)
        {
            if(moodEntry == null)
            {
                return new GeneralResponse<MoodEntry>
                {
                    Success = false,
                    Message = "Mood entry cannot be null.",
                    Data = null
                };
            }
            try
            {
               var moodEntryDB = await _context.MoodEntries.FirstOrDefaultAsync(me => me.Id == moodEntry.Id && me.UserId == userId);
                if(moodEntryDB == null)
                {
                    return new GeneralResponse<MoodEntry>
                    {
                        Success = false,
                        Message = "Mood entry not found.",
                        Data = null
                    };
                }
                _context.MoodEntries.Update(moodEntryDB);
                var result = await _context.SaveChangesAsync();
                if(result > 0)
                {
                    return new GeneralResponse<MoodEntry>
                    {
                        Success = true,
                        Message = "Mood entry updated successfully.",
                        Data = moodEntry
                    };
                }
                else
                {
                    return new GeneralResponse<MoodEntry>
                    {
                        Success = false,
                        Message = "Failed to update mood entry.",
                        Data = null
                    };
                }
            }
            catch(Exception ex)
            {
                return new GeneralResponse<MoodEntry>
                {
                    Success = false,
                    Message = $"An error occurred while updating the mood entry: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}
