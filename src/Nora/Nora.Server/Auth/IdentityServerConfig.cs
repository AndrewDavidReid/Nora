using System.Collections.Generic;
using IdentityServer4.Models;

namespace Nora.Server.Auth
{
  public static class IdentityServerConfig
  {
    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
      new IdentityResources.OpenId(),
      new IdentityResources.Profile(),
      new IdentityResources.Email()
    };

    public static IEnumerable<ApiResource> Apis => new[]
    {
      new ApiResource("nora-api")
      {
        Scopes = new[]
        {
          "nora:api"
        }
      }
    };

    public static IEnumerable<ApiScope> Scopes => new[]
    {
      new ApiScope("nora:api")
    };

    public static IEnumerable<Client> Clients => new[]
    {
      new Client
      {
        ClientId = "nora-client",
        AllowedGrantTypes = GrantTypes.Code,
        RequirePkce = true,
        AllowedCorsOrigins = new[]
        {
          ""
        },
        RedirectUris = new[]
        {
          ""
        },
        PostLogoutRedirectUris = new[]
        {
          ""
        },
        AllowedScopes = new[]
        {
          "openid",
          "profile",
          "email",
          "nora:api"
        }
      }
    };
  }
}
