using PayManAPI.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PayManAPI.Security
{
    public interface IAuthService
    {
        Task<(UserModel, string)> AuthenticationAsync(string username, string password);
        Task<UserModel> GetAuthenticatedUserAsync(string username, string password);
        string GetUserIdFromToken(ClaimsPrincipal claimsPrincipal);
    }
}
