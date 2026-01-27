using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using SharewoodQueryLib;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<TProtectedStorageService>();
builder.Services.AddSingleton<TSettingsLocalStorage>(sp =>
  new TSettingsLocalStorage(sp.GetRequiredService<TProtectedStorageService>())
);

await builder.Build().RunAsync();
