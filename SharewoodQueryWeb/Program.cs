using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using SharewoodQueryLib;

using SharewoodQueryWeb;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<TProtectedStorageService>();
builder.Services.AddScoped<TSettingsLocalStorage>(sp => new TSettingsLocalStorage(sp.GetRequiredService<TProtectedStorageService>()));

await builder.Build().RunAsync();
