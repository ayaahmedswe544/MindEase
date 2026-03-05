using Microsoft.EntityFrameworkCore;
using MindEase.Models;
using MindEase.IRepo;
using MindEase.Models.Response;
using System;

namespace MindEase.Repo
{
    public class JournalRepo : IJournalRepo
    {
        private readonly AppDbContext _context;

        public JournalRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse<Journal>> CreateAsync(Journal journal)
        {
            try
            {
                _context.Journals.Add(journal);
                await _context.SaveChangesAsync();

                return new GeneralResponse<Journal>
                {
                    Success = true,
                    Data = journal,
                    Message = "Journal created successfully."
                };
            }
            catch (Exception ex)
            {

                return new GeneralResponse<Journal>
                {
                    Success = false,
                    Message = "Failed to create journal.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }


        public async Task<GeneralResponse<Journal>> UpdateJournalAsync(Journal journal)
        {
            try
            {
                Journal existedJournal = await _context.Journals.FindAsync(journal.Id);
                if (existedJournal == null)
                {
                    return new GeneralResponse<Journal>
                    {
                        Success = false,
                        Message = "journal not found."
                    };
                }
                if (existedJournal.UserId != journal.UserId)
                {
                    return new GeneralResponse<Journal>
                    {
                        Success = false,
                        Message = "Unauthorized to update this journal."
                    };
                }
                if (journal.Title != null)
                {
                    existedJournal.Title = journal.Title;
                }
                if (journal.Content != null)
                {
                    existedJournal.Content = journal.Content;
                }

                 
                await _context.SaveChangesAsync();

                return new GeneralResponse<Journal>
                {
                    Success = true,
                    Data = existedJournal,
                    Message = "Journal Updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<Journal>
                {
                    Success = false,
                    Message = "Failed to Update journal.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }


        }

        public async Task<GeneralResponse<Journal>> GetJournalByIdAsync(int id)
        {
            try
            {
                var journal = await _context.Journals.FindAsync(id);

                if (journal == null)
                {
                    return new GeneralResponse<Journal>
                    {
                        Success = false,
                        Message = "Journal not found."
                    };
                }

                return new GeneralResponse<Journal>
                {
                    Success = true,
                    Data = journal
                };

            }
            catch (Exception ex)
            {

                return new GeneralResponse<Journal>
                {
                    Success = false,
                    Message = "Failed to retrieve journal.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }

        public async Task<GeneralResponse<List<Journal>>> GetAllJournalsAsync(string userId)
        {
            try
            {

            var journalsList = await _context.Journals
                .Where(journal => journal.UserId == userId)
                .ToListAsync(); // تصفية السجلات بناءً على `UserId`

            return new GeneralResponse<List<Journal>>
            {
                Success = true,
                Data = journalsList,
                Message = "Journals retrieved successfully."
            };

            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<Journal>>
                {
                    Message = "Failed to retrieve journals.",
                    Success = false,
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
                var journal = await _context.Journals.FindAsync(id);
                if (journal == null)
                {
                    return new GeneralResponse<bool>
                    {
                        Success = false,
                        Message = "Journal not found."
                    };
                }

                _context.Journals.Remove(journal);
                await _context.SaveChangesAsync();

                return new GeneralResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Journal deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<bool>
                {
                    Success = false,
                    Message = "Failed to delete journal.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }

    }
}