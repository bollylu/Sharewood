using Microsoft.JSInterop;

using SharewoodQueryLib;
using SharewoodQueryWeb2.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
  .AddRazorComponents()
  .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<TProtectedStorageService>();
builder.Services.AddScoped<TSettingsLocalStorage>(sp =>
  new TSettingsLocalStorage(sp.GetRequiredService<TProtectedStorageService>())
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseWebAssemblyDebugging();
} else {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    //.AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(SharewoodQueryWeb2.Client._Imports).Assembly);

app.Run();
