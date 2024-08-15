using BankingControlPanel.DTOs;
using BankingControlPanel.Entities;
using BankingControlPanel.Models;
using BankingControlPanel.Models.ClientModels;

namespace BankingControlPanel.Repositories
{
    public interface IClientRepository
    {
        Task<PagedResult<ClientDTO>> GetAllClientsAsync(ClientQueryParams queryParams, string userId);
        Task<Client> GetClientByIdAsync(int id);
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
        Task<List<QueryParams>> GetLastSearchParametersAsync(string userId);
    }
}
