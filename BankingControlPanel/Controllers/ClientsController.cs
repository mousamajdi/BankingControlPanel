using BankingControlPanel.DTOs;
using BankingControlPanel.Models.ClientModels;
using BankingControlPanel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankingControlPanel.Controllers
{
    [Route("api/Clients")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }


        /// <summary>
        /// Retrieves a paginated list of clients based on query parameters.
        /// </summary>
        /// <param name="queryParams">The filtering, sorting, and paging parameters.</param>
        /// <returns>An HTTP response with the paginated result of clients.</returns>
        [HttpGet]
        public async Task<IActionResult> GetClients([FromQuery] ClientQueryParams queryParams)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _clientService.GetAllClientsAsync(queryParams, userId);
            return Ok(result);
        }


        /// <summary>
        /// Retrieves a client by their ID.
        /// </summary>
        /// <param name="id">The ID of the client.</param>
        /// <returns>An HTTP response with the client data.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }


        /// <summary>
        /// Adds a new client to the system.
        /// </summary>
        /// <param name="clientDTO">The client data transfer object.</param>
        /// <returns>An HTTP response indicating the result of the operation.</returns>
        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientDTO clientDTO)
        {
            await _clientService.AddClientAsync(clientDTO);
            return CreatedAtAction(nameof(GetClient), new { id = clientDTO.Id }, clientDTO);
        }


        /// <summary>
        /// Updates an existing client's information.
        /// </summary>
        /// <param name="id">The ID of the client to update.</param>
        /// <param name="clientDTO">The client data transfer object.</param>
        /// <returns>An HTTP response indicating the result of the operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDTO clientDTO)
        {
            clientDTO.Id = id;
            await _clientService.UpdateClientAsync(clientDTO);
            return Ok("Client record updated successfully");
        }


        /// <summary>
        /// Deletes a client by their ID.
        /// </summary>
        /// <param name="id">The ID of the client to delete.</param>
        /// <returns>An HTTP response indicating the result of the operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return Ok("Client deleted successfully");
        }


        /// <summary>
        /// Retrieves the last three search parameters used by the current user.
        /// </summary>
        /// <returns>An HTTP response with the last three search parameters.</returns>
        [HttpGet("last-search-parameters")]
        public async Task<IActionResult> GetLastSearchParameters()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var lastThreeSearchParams = await _clientService.GetLastSearchParameters(userId);

            return Ok(lastThreeSearchParams);
        }

    }
}
