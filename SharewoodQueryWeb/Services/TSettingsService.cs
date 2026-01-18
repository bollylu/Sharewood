using System.Text.Json;

using SharewoodQueryWeb.Parameters;

namespace SharewoodQueryWeb.Services;

public class TSettingsService {

  private const string KEY_SETTINGS = "shw-settings";

  public string ApiKey { get; private set; } = string.Empty;
  private readonly TProtectedStorageService _storage;

  public TSettingsService(TProtectedStorageService storage) {
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

  public async Task SetParametersAsync(TParametersData data) {
    string JsonData = JsonSerializer.Serialize(data);
    await _storage.SetToLocalStorage(KEY_SETTINGS, JsonData);
  }
}
