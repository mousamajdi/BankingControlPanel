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

        public ClientRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<ClientDTO>> GetAllClientsAsync(ClientQueryParams queryParams, string userId)
        {
            // Store the search parameters in the database
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

            var clientsQuery = _context.Clients.Include(c => c.Accounts).AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(queryParams.SearchTerm))
            {
                var searchTerm = queryParams.SearchTerm.ToLower();
                clientsQuery = clientsQuery.Where(c =>
                    c.FirstName.ToLower().Contains(searchTerm) ||
                    c.LastName.ToLower().Contains(searchTerm) ||
                    c.Email.ToLower().Contains(searchTerm));
            }

            // Calculate total count before applying pagination
            var totalCount = await clientsQuery.CountAsync();

            // Apply sorting
            switch (queryParams.SortBy)
            {
                case SortByOptions.FirstName:
                    clientsQuery = clientsQuery.OrderBy(c => c.FirstName);
                    break;
                case SortByOptions.FirstName_Desc:
                    clientsQuery = clientsQuery.OrderByDescending(c => c.FirstName);
                    break;
                case SortByOptions.LastName:
                    clientsQuery = clientsQuery.OrderBy(c => c.LastName);
                    break;
                case SortByOptions.LastName_Desc:
                    clientsQuery = clientsQuery.OrderByDescending(c => c.LastName);
                    break;
                case SortByOptions.Email:
                    clientsQuery = clientsQuery.OrderBy(c => c.Email);
                    break;
                case SortByOptions.Email_Desc:
                    clientsQuery = clientsQuery.OrderByDescending(c => c.Email);
                    break;
                default:
                    clientsQuery = clientsQuery.OrderBy(c => c.Id); // Default sort by Id
                    break;
            }

            // Apply paging
            var skip = (queryParams.Page - 1) * queryParams.PageSize;
            var clients = await clientsQuery.Skip(skip).Take(queryParams.PageSize).ToListAsync();

            // Calculate total pages
            var totalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize);



            var pagedClients = new PagedResult<Client>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = queryParams.Page,
                PageSize = queryParams.PageSize,
                Data = clients
            };

            // Map to PagedResult<ClientDTO>
            var result = _mapper.Map<PagedResult<ClientDTO>>(pagedClients);
            return result;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _context.Clients.Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddClientAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await GetClientByIdAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<QueryParams>> GetLastSearchParametersAsync(string userId)
        {
            var query = _context.QueryParams.AsQueryable();
            query = query.Where(q => q.UserId == userId);


            var lastThreeSearchParams = await query
                .OrderByDescending(q => q.CreatedAt)
                .Take(3)
                .ToListAsync();

            return lastThreeSearchParams;
        }
    }
}
