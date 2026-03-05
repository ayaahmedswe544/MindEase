using MindEase.DTOs.Client;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientRepo _repo;
        public ClientService(IClientRepo repo)
        {
            _repo = repo;
        }
        public async Task<GeneralResponse<ClientDto>> GetProfileAsync(string ClientId)
        {
            if (string.IsNullOrWhiteSpace(ClientId))
            {
                return new GeneralResponse<ClientDto>
                {
                    Success = false,
                    Message = "Client ID cannot be null or empty.",
                    Data = null,
                    Errors = new Dictionary<string, string[]>
                    {
                        { "ClientId", new[] { "Client ID is required." } }
                    }
                };
            }
            var response = await _repo.ClientProfileAsync(ClientId);
            if (response.Success)
            {
                var clientDto = new ClientDto
                {
                    Id = response.Data.Id,
                    FullName = response.Data.FullName,
                    Email = response.Data.Email,
                    Gender = response.Data.Gender,
                    Age = response.Data.Age
                    //ProfilePicture = response.Data!.Image
                };

                return new GeneralResponse<ClientDto>
                {
                    Success = true,
                    Message = "Client profile retrieved successfully.",
                    Data = clientDto,
                    Errors = null

                };

            }
            return new GeneralResponse<ClientDto>
            {
                Success = false,
                Message = response.Message,
                Errors = response.Errors
            };
        }

        public async Task<GeneralResponse<ClientDto>> UpdateProfileAsync(updateClientDto clientDto, string ID)
        {
            if (clientDto == null)
            {
                return new GeneralResponse<ClientDto>
                {
                    Success = false,
                    Message = "Client data cannot be null.",
                    Data = null,
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Client", new[] { "Client data is required." } }
                    }
                };
            }
            User client = new User();
            client.Id = ID;
            client.FullName = clientDto.FullName;
            client.Email = clientDto.Email;
            var resoponse = await _repo.UpdateProfileAsync(client, clientDto.ProfilePicture);
            if (resoponse.Success)
            {
                var updatedClientDto = new ClientDto
                {
                    Id = resoponse.Data.Id,
                    FullName = resoponse.Data.FullName,
                    Email = resoponse.Data.Email,
                    ProfilePicture = resoponse.Data.Image,
                    Gender = resoponse.Data.Gender,
                    Age = resoponse.Data.Age

                };

                return new GeneralResponse<ClientDto>
                {
                    Success = true,
                    Message = "Client profile updated successfully.",
                    Data = updatedClientDto,
                    Errors = null
                };
            }
            else
            {
                return new GeneralResponse<ClientDto>
                {
                    Success = false,
                    Message = resoponse.Message,
                    Data = null,
                    Errors = resoponse.Errors
                };
            }

        }

         
    }
}