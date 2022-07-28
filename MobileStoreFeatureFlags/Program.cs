using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.FeatureManagement;
using MobileStoreFeatureFlags.Data;
using MobileStoreFeatureFlags.Handlers;
using MobileStoreFeatureFlags.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddFeatureManagement()
    .UseDisabledFeaturesHandler(new DisabledFeaturesHandler());

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IMobileDataService, MobileDataServiceMock>();

builder.Configuration.AddAzureAppConfiguration(options =>
{
    string? endpoint = Environment.GetEnvironmentVariable("AZURE_APP_CONFIG_ENDPOINT");

    if (endpoint is null)
    {
        throw new ArgumentNullException("APP CONFIG ENDPOINT was not added");
    }

    var credentials = new DefaultAzureCredential();

    options.Connect(new Uri(endpoint), credentials);

    //we need this to access key vault from Azure App Config
    options.ConfigureKeyVault(x =>
    {
        x.SetCredential(credentials);
    });

    options.ConfigureRefresh(refreshOptions =>
    {
        refreshOptions.Register("Settings:Sentinel", refreshAll: true)
            .SetCacheExpiration(new TimeSpan(0, 0, 0));
    });

    options.UseFeatureFlags();
});

//public static void ConfigureKeyVault(this IConfigurationBuilder config)
//{
//    string? keyVaultEndpoint = Environment.GetEnvironmentVariable("KEYVAULT_ENDPOINT");

//    if (keyVaultEndpoint is null)
//        throw new InvalidOperationException("Store the Key Vault endpoint in a KEYVAULT_ENDPOINT environment variable.");

//    var secretClient = new SecretClient(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
//    config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
//}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
