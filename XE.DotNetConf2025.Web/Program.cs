using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XE.DotNetConf2025.Models;
using XE.DotNetConf2025.Web;
using XE.DotNetConf2025.Web.Components;
using XE.DotNetConf2025.Web.Components.Account;
using XE.DotNetConf2025.Web.Data;
using XE.DotNetConf2025.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.AddRedisDistributedCache(connectionName: "cache");
builder.Services.AddHybridCache();

builder.Services.AddScoped<WeatherStateService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .RegisterPersistentService<WeatherStateService>(RenderMode.InteractiveServer);

builder.Services.Configure<CircuitOptions>(options =>
{
    options.PersistedCircuitInMemoryRetentionPeriod = TimeSpan.FromHours(2);
    options.PersistedCircuitDistributedRetentionPeriod = TimeSpan.FromHours(8);
    options.PersistedCircuitInMemoryMaxRetained = 1000;
});

builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
    });


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies();

builder.AddSqlServerDbContext<ApplicationDbContext>("identitydb");
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddValidationForTypesInModels();
builder.Services.AddValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();
app.MapDefaultEndpoints();

app.Run();
