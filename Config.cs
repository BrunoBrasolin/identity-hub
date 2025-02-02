
using Duende.IdentityServer.Models;

namespace identity_hub;

public static class Config
{
	public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] { new IdentityResources.OpenId(), new IdentityResources.Profile() };

	public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] { };
	public static IEnumerable<Client> Clients =>
		new Client[]
		{
			new Client()
			{
				ClientId = "gamidas-portal",
				ClientSecrets = { new Secret("secret".Sha256()) },
				AllowedScopes = { "openid", "profile" },
				AllowedGrantTypes = GrantTypes.Code,
				RedirectUris = { ConfigurationHelper.config.GetSection("GamidasPortalUrl").Value }
			}
		};
}