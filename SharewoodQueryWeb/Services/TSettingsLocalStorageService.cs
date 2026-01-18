using System.Text.Json;

using SharewoodQueryWeb.Parameters;

namespace SharewoodQueryWeb.Services;

public class TSettingsLocalStorageService : ISettings {

  private const string KEY_SETTINGS = "shw-settings";

  private readonly TProtectedStorageService _storage;

  public TSettingsLocalStorageService(TProtectedStorageService storage) {
    _storage = storage;
  }

  public async Task<TParametersData> GetSettingsAsync() {
    try {
      string Data = await _storage.GetFromLocalStorage(KEY_SETTINGS) ?? string.Empty;
      TParametersData Result = JsonSerializer.Deserialize<TParametersData>(Data) ?? new TParametersData();
      return Result;
    } catch {
      return new TParametersData();
    }
  }

  public async Task SetSettingsAsync(TParametersData data) {
    string JsonData = JsonSerializer.Serialize(data);
    await _storage.SetToLocalStorage(KEY_SETTINGS, JsonData);
  }

    public Task<TParametersData> ImportAsync(ISettings settings) {
        throw new NotImplementedException();
    }

    public Task ExportAsync(ISettings settings) {
        throw new NotImplementedException();
    }
}
