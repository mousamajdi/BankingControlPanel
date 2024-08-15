using AutoMapper;
using BankingControlPanel.DTOs;
using BankingControlPanel.Entities;
using BankingControlPanel.Models;
using BankingControlPanel.Models.ClientModels;
using BankingControlPanel.Repositories;

namespace BankingControlPanel.Services
{
    /// <summary>
    /// Service class for handling business logic related to clients.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientService> _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="clientRepository">The client repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger instance.</param>
        public ClientService(IClientRepository clientRepository, IMapper mapper, ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Retrieves a paginated list of clients based on query parameters.
        /// </summary>
        /// <param name="queryParams">The filtering, sorting, and paging parameters.</param>
        /// <param name="userId">The ID of the user making the request.</param>
        /// <returns>A paged result of clients as DTOs.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving clients.</exception>
        public async Task<PagedResult<ClientDTO>> GetAllClientsAsync(ClientQueryParams queryParams, string userId)
        {
            try
            {
                return await _clientRepository.GetAllClientsAsync(queryParams, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving clients for user {UserId}.", userId);
                throw;
            }
        }


        /// <summary>
        /// Retrieves a client by their ID.
        /// </summary>
        /// <param name="id">The ID of the client.</param>
        /// <returns>The client as a DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the client is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the client.</exception>
        public async Task<ClientDTO> GetClientByIdAsync(int id)
        {
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(id);
                if (client == null)
                {
                    _logger.LogWarning("Client with ID {ClientId} not found.", id);
                    throw new KeyNotFoundException($"Client with ID {id} not found.");
                }

                return _mapper.Map<ClientDTO>(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving client with ID {ClientId}.", id);
                throw;
            }
        }


        /// <summary>
        /// Adds a new client to the system.
        /// </summary>
        /// <param name="clientDTO">The client data transfer object.</param>
        /// <exception cref="Exception">Thrown when an error occurs while adding the client.</exception>
        public async Task AddClientAsync(ClientDTO clientDTO)
        {
            try
            {
                var client = _mapper.Map<Client>(clientDTO);
                await _clientRepository.AddClientAsync(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new client.");
                throw;
            }
        }


        /// <summary>
        /// Updates an existing client's information.
        /// </summary>
        /// <param name="clientDTO">The client data transfer object.</param>
        /// <exception cref="Exception">Thrown when an error occurs while updating the client.</exception>
        public async Task UpdateClientAsync(ClientDTO clientDTO)
        {
            try
            {
                var client = _mapper.Map<Client>(clientDTO);
                await _clientRepository.UpdateClientAsync(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating client with ID {ClientId}.", clientDTO.Id);
                throw;
            }
        }


        /// <summary>
        /// Deletes a client by their ID.
        /// </summary>
        /// <param name="id">The ID of the client to delete.</param>
        /// <exception cref="Exception">Thrown when an error occurs while deleting the client.</exception>
        public async Task DeleteClientAsync(int id)
        {
            try
            {
                await _clientRepository.DeleteClientAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting client with ID {ClientId}.", id);
                throw;
            }
        }


        /// <summary>
        /// Retrieves the last three search parameters used by the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of the last three search parameters.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving search parameters.</exception>
        public async Task<List<QueryParams>> GetLastSearchParameters(string userId)
        {
            try
            {
                return await _clientRepository.GetLastSearchParametersAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving last search parameters for user {UserId}.", userId);
                throw;
            }
        }
    }
}
