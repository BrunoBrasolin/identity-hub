
using Duende.IdentityServer;
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
				ClientName = "Gamidas Portal",
				ClientSecrets = { new Secret("secret".Sha256()) },
				AllowedScopes = {
					IdentityServerConstants.StandardScopes.OpenId,
					IdentityServerConstants.StandardScopes.Profile
				},
				AllowedGrantTypes = GrantTypes.Code,
				RedirectUris = { ConfigurationHelper.config.GetSection("GamidasPortalUrl").Value + "/login" },
				AllowedCorsOrigins = { ConfigurationHelper.config.GetSection("GamidasPortalUrl").Value }
			}
		};
}