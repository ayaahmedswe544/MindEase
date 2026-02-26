namespace MindEase.Service
{
    using MindEase.DTOs.Memory;
    using MindEase.IRepo;
    using MindEase.IService;
    using MindEase.Models;
    using MindEase.Models.Response;
    public class MemoryService : IMemoryService
    {
        private readonly IMemoryRepository _repo;

        public MemoryService(IMemoryRepository repo)
        {
            _repo = repo;
        }
        //public MemoryService()
        //{

        //}

        public async Task<GeneralResponse<MemoryResponseDto>> CreateAsync(CreateMemoryDto dto, string userId)
        {
            var memory = new Memory
            {
                Title = dto.Title,
                MoodState = dto.MoodState,
                UserId = userId,
                Date = DateTime.Now
            };

            var response = await _repo.CreateAsync(memory, dto.Image);

            if (!response.Success)
                return new GeneralResponse<MemoryResponseDto>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoResponse = new MemoryResponseDto
            {
                Id = response.Data!.Id,
                Title = response.Data.Title,
                MoodState = response.Data.MoodState,
                Date = response.Data.Date,
                ImageUrl = response.Data.Image
            };

            return new GeneralResponse<MemoryResponseDto>
            {
                Success = true,
                Data = dtoResponse,
                Message = response.Message
            };
        }

        public async Task<GeneralResponse<MemoryResponseDto>> GetByIdAsync(int id)
        {
            var response = await _repo.GetByIdAsync(id);

            if (!response.Success)
                return new GeneralResponse<MemoryResponseDto>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoResponse = new MemoryResponseDto
            {
                Id = response.Data!.Id,
                Title = response.Data.Title,
                MoodState = response.Data.MoodState,
                Date = response.Data.Date,
                ImageUrl = response.Data.Image
            };

            return new GeneralResponse<MemoryResponseDto>
            {
                Success = true,
                Data = dtoResponse
            };
        }

        public async Task<GeneralResponse<bool>> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<GeneralResponse<List<MemoryResponseDto>>> GetByUserIdAsync(string userId)
        {
            var response = await _repo.GetByUserIdAsync(userId);

            if (!response.Success)
                return new GeneralResponse<List<MemoryResponseDto>>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoList = response.Data!.Select(m => new MemoryResponseDto
            {
                Id = m.Id,
                Title = m.Title,
                MoodState = m.MoodState,
                Date = m.Date,
                ImageUrl = m.Image
            }).ToList();

            return new GeneralResponse<List<MemoryResponseDto>>
            {
                Success = true,
                Data = dtoList,
                Message = response.Message
            };
        }

        public async Task<GeneralResponse<MemoryResponseDto>> UpdateAsync(UpdateMemoryDto dto, string userId)
        {

            var memory = new Memory { Id = dto.Id, UserId = userId,
                Title = dto.Title,
                MoodState = (MoodState)dto.MoodState,

            };


            var response = await _repo.UpdateAsync(memory, dto.Image);

            if (!response.Success)
                return new GeneralResponse<MemoryResponseDto>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoResponse = new MemoryResponseDto
            {
                Id = response.Data!.Id,
                Title = response.Data.Title,
                MoodState = response.Data.MoodState,
                Date = response.Data.Date,
                ImageUrl = response.Data.Image
            };

            return new GeneralResponse<MemoryResponseDto>
            {
                Success = true,
                Data = dtoResponse,
                Message = response.Message
            };
    } 
    } 
}

