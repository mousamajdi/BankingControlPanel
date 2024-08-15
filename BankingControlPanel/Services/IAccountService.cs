using BankingControlPanel.Entities;
using BankingControlPanel.Models.AccountModels;
using Microsoft.AspNetCore.Identity;

namespace BankingControlPanel.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterModel model);
        Task<string> LoginAsync(LoginModel model);
        Task<IdentityResult> EnsureRoleExistsAsync(string roleName);
        string GenerateJwtToken(ApplicationUser user);
    }
}
