using AutoMapper;
using BankingControlPanel.DTOs;
using BankingControlPanel.Entities;
using BankingControlPanel.Models;
using BankingControlPanel.Models.ClientModels;
using BankingControlPanel.Repositories;

namespace BankingControlPanel.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ClientDTO>> GetAllClientsAsync(ClientQueryParams queryParams)
        {
            var result = await _clientRepository.GetAllClientsAsync(queryParams);
            return result;
        }

        public async Task<ClientDTO> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            return _mapper.Map<ClientDTO>(client);
        }

        public async Task AddClientAsync(ClientDTO clientDTO)
        {
            var client = _mapper.Map<Client>(clientDTO);
            await _clientRepository.AddClientAsync(client);
        }

        public async Task UpdateClientAsync(ClientDTO clientDTO)
        {
            var client = _mapper.Map<Client>(clientDTO);
            await _clientRepository.UpdateClientAsync(client);
        }

        public async Task DeleteClientAsync(int id)
        {
            await _clientRepository.DeleteClientAsync(id);
        }
    }
}
