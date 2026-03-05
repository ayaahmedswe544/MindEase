using Azure;
using Microsoft.AspNetCore.Identity;
using MindEase.DTOs.Journaling;
using MindEase.DTOs.Memory;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
using System;
using System.Security.Claims;

namespace MindEase.Service
{
    public class JournalService : IJournalService
    {
        private readonly IJournalRepo _journalRepo;
        private readonly UserManager<GeneralUser> _userManager;

        public JournalService(IJournalRepo journalRepo, UserManager<GeneralUser> userManager)
        {
            _journalRepo = journalRepo;
            _userManager = userManager;
        }

        public async Task<GeneralResponse<JournalingDto>> CreateJournalAsync(CreateJournalingDto input , string userId)
        {
            var journal = new Journal
            {
                Title = input.Title,
                Content = input.Content,
                UserId = userId
            };
            var response = await _journalRepo.CreateAsync(journal);

            if (!response.Success)
                return new GeneralResponse<JournalingDto>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoResponse = new JournalingDto
            {
                Id = response.Data!.Id,
                Title = response.Data.Title,
                Date = response.Data.Date,
                Content = response.Data.Content,
                UserId = response.Data!.UserId

            };

            return new GeneralResponse<JournalingDto>
            {
                Success = true,
                Data = dtoResponse,
                Message = response.Message
            };
        }



        public async Task<GeneralResponse<JournalingDto>> UpdateAsync(UpdateJournalingDto input, string userId)
        {

            var journal = new Journal
            {
                Id = input.Id,
                UserId = userId,
                Title = input.Title,
                Content = input.Content
            };


            var response = await _journalRepo.UpdateJournalAsync(journal);

            if (!response.Success)
                return new GeneralResponse<JournalingDto>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoResponse = new JournalingDto
            {
                Id = response.Data!.Id,
                Title = response.Data.Title,
                Content = response.Data.Content,
                Date = response.Data.Date,
                UserId = response.Data!.UserId
            };

            return new GeneralResponse<JournalingDto>
            {
                Success = true,
                Data = dtoResponse,
                Message = response.Message
            };
        }

        public async Task<GeneralResponse<JournalingDto>> GetJournalByIdAsync(int id)
        {
            var response = await _journalRepo.GetJournalByIdAsync(id);

            if (!response.Success)
                return new GeneralResponse<JournalingDto>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoResponse = new JournalingDto
            {
                Id = response.Data!.Id,
                Title = response.Data.Title,
                Date = response.Data.Date,
                Content = response.Data.Content,
                UserId = response.Data!.UserId
            };

            return new GeneralResponse<JournalingDto>
            {
                Success = true,
                Data = dtoResponse
            };
        }

        public async Task<GeneralResponse<List<JournalingDto>>> GetAllJournalsAsync(string userId)
        {
            var journalResponse = await _journalRepo.GetAllJournalsAsync(userId);


            if (!journalResponse.Success)
                return new GeneralResponse<List<JournalingDto>>
                {
                    Success = false,
                    Message = journalResponse.Message,
                    Errors = journalResponse.Errors
                };

            var dtoList = journalResponse.Data!.Select(m => new JournalingDto
            {
                Id = m.Id,
                Title = m.Title,
                Date = m.Date,
            }).ToList();

            return new GeneralResponse<List<JournalingDto>>
            {
                Success = true,
                Data = dtoList,
                Message = journalResponse.Message
            };
        }


        public async Task<GeneralResponse<bool>> DeleteAsync(int id)
        {
            return await _journalRepo.DeleteAsync(id);
        }

    }
}