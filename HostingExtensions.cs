using Serilog;

namespace identity_hub
{
	internal static class HostingExtensions
	{
		public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddRazorPages();

			builder.Services.AddIdentityServer()
				.AddInMemoryIdentityResources(Config.IdentityResources)
				.AddInMemoryApiScopes(Config.ApiScopes)
				.AddInMemoryClients(Config.Clients)
				.AddTestUsers(TestUsers.Users);

			return builder.Build();
		}

		public static WebApplication ConfigurePipeline(this WebApplication app)
		{
			app.UseSerilogRequestLogging();
			app.UseDeveloperExceptionPage();

			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();

			app.Use((context, next) =>
			{
				context.Request.IsHttps = true;
				if (app.Environment.IsProduction())
				{
					context.Request.Host = new HostString("api.gamidas.dev.br");
					context.Request.PathBase = new PathString("/identity-hub");
				}
				return next();
			});

			app.UseIdentityServer();

			app.UseAuthorization();
			app.MapRazorPages().RequireAuthorization();

			if (app.Environment.IsProduction())
				app.UseCors("CorsPolicy");

			return app;
		}
	}
}
