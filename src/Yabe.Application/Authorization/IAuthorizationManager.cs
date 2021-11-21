using System.Security.Claims;
using System.Threading.Tasks;

namespace Yabe.Application.Authorization
{
    public interface IAuthorizationManager
    {
        Task<ClaimsPrincipal> GetUser();
        bool CanEdit();
    }
}
