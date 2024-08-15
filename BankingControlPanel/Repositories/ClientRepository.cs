using AutoMapper;
using BankingControlPanel.Data;
using BankingControlPanel.DTOs;
using BankingControlPanel.Entities;
using BankingControlPanel.Enums;
using BankingControlPanel.Models;
using BankingControlPanel.Models.ClientModels;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(ApplicationDbContext context, IMapper mapper, ILogger<ClientRepository> logger)
        {
            _context = context;
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
                // Store the search parameters
                await StoreSearchParametersAsync(queryParams, userId);

                // Start with a basic query
                var clientsQuery = _context.Clients.AsNoTracking()
                                                   .Include(c => c.Accounts)
                                                   .AsQueryable();

                // Apply filters
                clientsQuery = ApplyFiltering(clientsQuery, queryParams);

                // Apply sorting
                clientsQuery = ApplySorting(clientsQuery, queryParams.SortBy);

                // Apply pagination and retrieve the result
                return await GetPagedResultAsync(clientsQuery, queryParams);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving clients for user {UserId}.", userId);
                throw;
            }
        }

        private async Task StoreSearchParametersAsync(ClientQueryParams queryParams, string userId)
        {
            var dbParams = new QueryParams
            {
                UserId = userId,
                SearchTerm = queryParams.SearchTerm,
                SortBy = queryParams.SortBy.ToString(),
                Page = queryParams.Page,
                PageSize = queryParams.PageSize,
                CreatedAt = DateTime.UtcNow
            };
            _context.QueryParams.Add(dbParams);
            await _context.SaveChangesAsync();
        }
        private IQueryable<Client> ApplyFiltering(IQueryable<Client> query, ClientQueryParams queryParams)
        {
            if (!string.IsNullOrEmpty(queryParams.SearchTerm))
            {
                var searchTerm = queryParams.SearchTerm.ToLower();
                query = query.Where(c =>
                    c.FirstName.ToLower().Contains(searchTerm) ||
                    c.LastName.ToLower().Contains(searchTerm) ||
                    c.Email.ToLower().Contains(searchTerm));
            }
            return query;
        }
        private IQueryable<Client> ApplySorting(IQueryable<Client> query, SortByOptions? sortBy)
        {
            switch (sortBy)
            {
                case SortByOptions.FirstName:
                    query = query.OrderBy(c => c.FirstName);
                    break;
                case SortByOptions.FirstName_Desc:
                    query = query.OrderByDescending(c => c.FirstName);
                    break;
                case SortByOptions.LastName:
                    query = query.OrderBy(c => c.LastName);
                    break;
                case SortByOptions.LastName_Desc:
                    query = query.OrderByDescending(c => c.LastName);
                    break;
                case SortByOptions.Email:
                    query = query.OrderBy(c => c.Email);
                    break;
                case SortByOptions.Email_Desc:
                    query = query.OrderByDescending(c => c.Email);
                    break;
                default:
                    query = query.OrderBy(c => c.Id); // Default sort by Id
                    break;
            }
            return query;
        }
        private async Task<PagedResult<ClientDTO>> GetPagedResultAsync(IQueryable<Client> query, ClientQueryParams queryParams)
        {
            var totalCount = await query.CountAsync();

            var clients = await query.Skip((queryParams.Page - 1) * queryParams.PageSize)
                                     .Take(queryParams.PageSize)
                                     .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize);

            var pagedClients = new PagedResult<Client>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = queryParams.Page,
                PageSize = queryParams.PageSize,
                Data = clients
            };

            return _mapper.Map<PagedResult<ClientDTO>>(pagedClients);
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            try
            {
                return await _context.Clients.Include(c => c.Accounts)
                                             .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving client with ID {ClientId}.", id);
                throw;
            }
        }

        public async Task AddClientAsync(Client client)
        {
            try
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new client.");
                throw;
            }
        }

        public async Task UpdateClientAsync(Client client)
        {
            try
            {
                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating client with ID {ClientId}.", client.Id);
                throw;
            }
        }

        public async Task DeleteClientAsync(int id)
        {
            try
            {
                var client = await GetClientByIdAsync(id);
                if (client != null)
                {
                    _context.Clients.Remove(client);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting client with ID {ClientId}.", id);
                throw;
            }
        }

        public async Task<List<QueryParams>> GetLastSearchParametersAsync(string userId)
        {
            try
            {
                var query = _context.QueryParams.Where(q => q.UserId == userId);
                return await query.OrderByDescending(q => q.CreatedAt)
                                  .Take(3)
                                  .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving last search parameters for user {UserId}.", userId);
                throw;
            }
        }
    }
}
