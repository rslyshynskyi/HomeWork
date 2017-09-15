using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4;
using System.Security.Claims;
using IdentityModel;

namespace IdentityServer
{
    public class Configs
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "Web Api")
                {
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                },
                new ApiResource
                {
                    Name = "customAPI",
                    DisplayName = "Custom API",
                    Description = "Custom API Access",
                    UserClaims = new List<string> {"role"},
                    ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                    Scopes = new List<Scope> {
                        new Scope("customAPI.read"),
                        new Scope("customAPI.write")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        "api",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email
                    }
                },
                new Client
                {
                    ClientId = "openIdConnectClient",
                    ClientName = "Example Implicit Client Application",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "customAPI.write"
                    },
                    RedirectUris = new List<string> {"http://localhost:12514/signin-oidc"},
                    PostLogoutRedirectUris = new List<string> { "https://localhost:12514" }
                }
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "roman",
                    Password = "111111",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "slyshynsky94@gmail.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}
