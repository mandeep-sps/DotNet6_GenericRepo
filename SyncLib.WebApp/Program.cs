using SyncLib.Model.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add ApplicationSettings configurations
builder.Services.Configure<ApplicationSettingsModel>(
    builder.Configuration.GetSection(ApplicationSettingsModel.ApplicationSettings));


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(2); //Just for testing we are adding 2 minutes only
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
