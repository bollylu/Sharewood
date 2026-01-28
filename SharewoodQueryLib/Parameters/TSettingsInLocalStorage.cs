using System.Diagnostics;
using System.Text.Json;

namespace SharewoodQueryLib;

public class TSettingsInLocalStorage : ISettingsStorage {

  private const string KEY_SETTINGS = "shw-settings";
  private readonly TProtectedStorageService? _storage;

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  public TSettingsInLocalStorage() {
  }

  public TSettingsInLocalStorage(TSettingsInLocalStorage settings) {
    _storage = settings._storage;
  }

  public TSettingsInLocalStorage(TProtectedStorageService storage) {
    _storage = storage;
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  public async Task<ISettings?> ReadAsync() {
    #region === Validate parameters ===
    if (_storage == null) {
      Debug.WriteLine("TSettingsLocalStorage.ReadAsync: Storage service is null.");
      return null;
    }
    #endregion === Validate parameters ===

    try {
      string JsonData = await _storage.GetFromLocalStorage(KEY_SETTINGS) ?? string.Empty;
      TSettings? Result = JsonSerializer.Deserialize<TSettings>(JsonData);
      if (Result == null) {
        Debug.WriteLine("TSettingsLocalStorage.ReadAsync: Failed to deserialize settings.");
        return null;
      }
      TSettings RetVal = new TSettings {
        ApiKey = Result.ApiKey,
        SharewoodUrl = Result.SharewoodUrl,
        AutoRefresh = Result.AutoRefresh,
        RefreshIntervalInSec = Result.RefreshIntervalInSec,
        DefaultCategory = Result.DefaultCategory,
        DefaultSubCategory = Result.DefaultSubCategory,
        TorrentsLimit = Result.TorrentsLimit
      };
      return RetVal;
    } catch (Exception ex) {
      Debug.WriteLine($"TSettingsLocalStorage.ReadAsync: {ex.Message}");
      return null;
    }
  }

  public async Task SaveAsync(ISettings settings) {
    if (_storage == null) {
      Debug.WriteLine("TSettingsLocalStorage.SaveAsync: Storage service is null.");
      return;
    }
    string JsonData = JsonSerializer.Serialize(settings);
    await _storage.SetToLocalStorage(KEY_SETTINGS, JsonData);
  }

  public async Task DeleteAsync() {
    if (_storage == null) {
      Debug.WriteLine("TSettingsLocalStorage.DeleteAsync: Storage service is null.");
      return;
    }
    await _storage.RemoveFromLocalStorage(KEY_SETTINGS);
  }

  public async Task<ISettings?> ImportAsync(ISettingsStorage storage) {
    if (_storage == null) {
      Debug.WriteLine("TSettingsLocalStorage.ImportAsync: Storage service is null.");
      return null;
    }
    ISettings? Settings = await storage.ReadAsync();
    if (Settings == null) {
      Debug.WriteLine("TSettingsLocalStorage.ImportAsync: Failed to read settings.");
      return null;
    }
    return Settings;
  }

  public async Task ExportAsync(ISettingsStorage storage, ISettings settings) {
    if (_storage == null) {
      Debug.WriteLine("TSettingsLocalStorage.ExportAsync: Storage service is null.");
      return;
    }
    await storage.SaveAsync(settings);
  }

}
