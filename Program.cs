using Duende.IdentityServer.Services;
using identity_hub;
using Serilog;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateBootstrapLogger();

Log.Information("Starting up");

try
{
	var builder = WebApplication.CreateBuilder(args);
	ConfigurationHelper.Initialize(builder.Configuration);

	builder.Host.UseSerilog((ctx, lc) => lc
		.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
		.Enrich.FromLogContext()
		.ReadFrom.Configuration(ctx.Configuration));

	builder.Services.AddSingleton<ICorsPolicyService>(container =>
	{
		var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
		return new DefaultCorsPolicyService(logger) { AllowedOrigins = { ConfigurationHelper.config.GetSection("GamidasPortalUrl").Value } };
	});

	var app = builder
		.ConfigureServices()
		.ConfigurePipeline();

	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Unhandled exception");
}
finally
{
	Log.Information("Shut down complete");
	Log.CloseAndFlush();
}