using SyncLib.Model.Common;
using SyncLib.WebAPI.Common;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
ConfigurationExtension.AddAllServices(builder.Services, builder.Configuration);

// Add ApplicationSettings configurations
builder.Services.Configure<ApplicationSettingsModel>(
    builder.Configuration.GetSection(ApplicationSettingsModel.ApplicationSettings));

var app = builder.Build();

//Configuring all middlewares
ConfigurationExtension.ConfigureMiddleWares(app);
app.Run();

