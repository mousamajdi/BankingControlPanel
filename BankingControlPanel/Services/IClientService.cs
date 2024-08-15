using BankingControlPanel.DTOs;
using BankingControlPanel.Models;
using BankingControlPanel.Models.ClientModels;

namespace BankingControlPanel.Services
{
    public interface IClientService
    {
        Task<PagedResult<ClientDTO>> GetAllClientsAsync(ClientQueryParams queryParams);
        Task<ClientDTO> GetClientByIdAsync(int id);
        Task AddClientAsync(ClientDTO clientDTO);
        Task UpdateClientAsync(ClientDTO clientDTO);
        Task DeleteClientAsync(int id);
    }
}
