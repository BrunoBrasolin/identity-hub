using Duende.IdentityModel;
using System.Security.Claims;
using Duende.IdentityServer.Test;

namespace identity_hub;

public static class TestUsers
{
	public static List<TestUser> Users
	{
		get
		{
			return new List<TestUser>
			{
				new TestUser
				{
					SubjectId = "1",
					Username = "leticia",
					Password = "123",
					Claims =
					{
						new Claim(JwtClaimTypes.Name, "Leticia Oliveira"),
						new Claim(JwtClaimTypes.GivenName, "Letícia"),
						new Claim(JwtClaimTypes.FamilyName, "Oliveira"),
						new Claim(JwtClaimTypes.Email, "leticia.ferr123@gmail.com"),
						new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
					}
				},
				new TestUser
				{
					SubjectId = "2",
					Username = "bruno",
					Password = "123",
					Claims =
					{
						new Claim(JwtClaimTypes.Name, "Bruno Brasolin"),
						new Claim(JwtClaimTypes.GivenName, "Bruno"),
						new Claim(JwtClaimTypes.FamilyName, "Brasolin"),
						new Claim(JwtClaimTypes.Email, "brunobrasolinc@gmail.com"),
						new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
					}
				}
			};
		}
	}
}