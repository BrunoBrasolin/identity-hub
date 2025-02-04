using Serilog;

namespace identity_hub
{
	internal static class HostingExtensions
	{
		public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddRazorPages();

			builder.Services.AddIdentityServer(options =>
				{
					options.EmitStaticAudienceClaim = true;
				})
				.AddInMemoryIdentityResources(Config.IdentityResources)
				.AddInMemoryApiScopes(Config.ApiScopes)
				.AddInMemoryClients(Config.Clients)
				.AddTestUsers(TestUsers.Users);

			return builder.Build();
		}

		public static WebApplication ConfigurePipeline(this WebApplication app)
		{
			app.UseSerilogRequestLogging();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();

			app.UseIdentityServer();

			app.UseAuthorization();
			app.MapRazorPages().RequireAuthorization();

			return app;
		}
	}
}
