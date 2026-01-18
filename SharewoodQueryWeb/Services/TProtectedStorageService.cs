using Microsoft.JSInterop;

namespace SharewoodQueryWeb.Services;

public class TProtectedStorageService(IJSRuntime jsRuntime) {

  private IJSRuntime jsr = jsRuntime;

  public async ValueTask SetToLocalStorage(string key, string value) => await jsr.InvokeVoidAsync("localStorage.setItem", key, value);

  public async ValueTask<string?> GetFromLocalStorage(string key, string? defaultValue = null) => await jsr.InvokeAsync<string>("localStorage.getItem", key) ?? defaultValue;

  public async ValueTask RemoveFromLocalStorage(string key) => await jsr.InvokeVoidAsync("localStorage.removeItem", key);

  public async ValueTask SetToSessionStorage(string key, string value) => await jsr.InvokeVoidAsync("sessionStorage.setItem", key, value);

  public async ValueTask<string?> GetFromSessionStorage(string key, string? defaultValue = null) => await jsr.InvokeAsync<string>("sessionStorage.getItem", key) ?? defaultValue;

  public async ValueTask RemoveFromSessionStorage(string key) => await jsr.InvokeVoidAsync("sessionStorage.removeItem", key);

}
