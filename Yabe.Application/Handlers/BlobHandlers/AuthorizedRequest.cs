using System.Security.Claims;

namespace Yabe.Application.Handlers.BlobHandlers
{
    public class AuthorizedRequest
    {
        public ClaimsPrincipal User { get; set; }
    }
}
