using BankingControlPanel.DTOs;
using BankingControlPanel.Entities;
using BankingControlPanel.Models;
using BankingControlPanel.Models.ClientModels;

namespace BankingControlPanel.Services
{
    public interface IClientService
    {
        Task<PagedResult<ClientDTO>> GetAllClientsAsync(ClientQueryParams queryParams, string userId);
        Task<ClientDTO> GetClientByIdAsync(int id);
        Task AddClientAsync(ClientDTO clientDTO);
        Task UpdateClientAsync(ClientDTO clientDTO);
        Task DeleteClientAsync(int id);
        Task<List<QueryParams>> GetLastSearchParameters(string userId);
    }
}
