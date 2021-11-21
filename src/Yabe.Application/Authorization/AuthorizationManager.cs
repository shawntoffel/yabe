using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Yabe.Application.Configuration;

namespace Yabe.Application.Authorization
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly YabeOptions _options;

        public AuthorizationManager(AuthenticationStateProvider authenticationStateProvider, IOptions<YabeOptions> options)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _options = options.Value;
        }

        public bool CanEdit()
        {
            var t = GetUser();
            t.Wait();

            return t.Result?.IsInRole(_options.EditorRole) ?? false;
        }

        public async Task<ClaimsPrincipal> GetUser()
        {
            var auth = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return auth?.User;
        }
    }
}
