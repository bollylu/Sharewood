using System.Text.Json;

namespace SharewoodQueryLib;

public class TSettingsLocalStorageService {

  private const string KEY_SETTINGS = "shw-settings";

  private readonly TProtectedStorageService _storage;

  public TSettingsLocalStorageService(TProtectedStorageService storage) {
    _storage = storage;
  }

  public async Task<TSettingsData> ReadAsync() {
    try {
      string Data = await _storage.GetFromLocalStorage(KEY_SETTINGS) ?? string.Empty;
      TSettingsData Result = JsonSerializer.Deserialize<TSettingsData>(Data) ?? new TSettingsData();
      return Result;
    } catch {
      return new TSettingsData();
    }
  }

  public async Task SaveAsync(TSettingsData data) {
    string JsonData = JsonSerializer.Serialize(data);
    await _storage.SetToLocalStorage(KEY_SETTINGS, JsonData);
  }

    public Task<TSettingsData> ImportAsync(ISettings settings) {
        throw new NotImplementedException();
    }

    public Task ExportAsync(ISettings settings) {
        throw new NotImplementedException();
    }
}
