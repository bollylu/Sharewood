using BLTools.Core;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using SharewoodQueryWeb;
using SharewoodQueryWeb.Services;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<TProtectedStorageService>();
builder.Services.AddSingleton<TSettingsLocalStorageService>(sp => new TSettingsLocalStorageService(sp.GetRequiredService<TProtectedStorageService>()));

await builder.Build().RunAsync();
