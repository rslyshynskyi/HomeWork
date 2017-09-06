using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc.Internal;

namespace MatOrderingService.Services.Auth
{
    public class MatOsAuthHandler: AuthenticationHandler<MatOsAuthOptions>
    {
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var keyword = Options.AuthenticationScheme + " ";
            string autherization = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(autherization))
            {
                return await Task.FromResult(AuthenticateResult.Skip());
            }

            if (!autherization.StartsWith(keyword))
            {
                return await Task.FromResult(AuthenticateResult.Skip());
            }

            var authValue = autherization.Substring(keyword.Length).Trim();

            if (string.IsNullOrEmpty(authValue))
            {
                return await Task.FromResult(AuthenticateResult.Skip());
            }

            if (authValue.Equals(Options.SecurityKey))
            {
                return await Task.FromResult(AuthenticateResult.Success(
                    new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimsIdentity.DefaultRoleClaimType, Roles.MatRegisteredUser,
                                ClaimValueTypes.String),
                        }, Options.AuthenticationScheme)),
                        new AuthenticationProperties(),
                        Options.AuthenticationScheme
                    )));
            }
            else
            {
                return await Task.FromResult(AuthenticateResult.Fail("Incorrect key."));
            }
        }
    }
}