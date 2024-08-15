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

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientDTO clientDTO)
        {
            await _clientService.AddClientAsync(clientDTO);
            return CreatedAtAction(nameof(GetClient), new { id = clientDTO.Id }, clientDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDTO clientDTO)
        {
            clientDTO.Id = id;
            await _clientService.UpdateClientAsync(clientDTO);
            return Ok("Client record updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return Ok("Client deleted successfully");
        }

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
