using MindEase.DTOs.Client;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IClientService
    {
        Task<GeneralResponse<ClientDto>> GetProfileAsync(string ClientId);
        Task<GeneralResponse<ClientDto>> UpdateProfileAsync(updateClientDto clientDto, string ID);
    }
}
