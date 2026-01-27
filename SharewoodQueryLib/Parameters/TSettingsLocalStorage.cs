using System.Text.Json;

namespace SharewoodQueryLib;

public class TSettingsLocalStorage : ASettings {

  private const string KEY_SETTINGS = "shw-settings";
  private readonly TProtectedStorageService? _storage;

  public TSettingsLocalStorage() {
  }

  public TSettingsLocalStorage(ISettings settings) : base(settings) { }

  public TSettingsLocalStorage(TProtectedStorageService storage) {
    _storage = storage;
  }

  public override async Task ReadAsync() {
    try {
      if (_storage == null) {
        return;
      }
      string Data = await _storage.GetFromLocalStorage(KEY_SETTINGS) ?? string.Empty;
      TSettingsLocalStorage Result = JsonSerializer.Deserialize<TSettingsLocalStorage>(Data) ?? new TSettingsLocalStorage(_storage);
      this.ApiKey = Result.ApiKey;
      this.SharewoodUrl = Result.SharewoodUrl;
      this.AutoRefresh = Result.AutoRefresh;
      this.RefreshIntervalInSec = Result.RefreshIntervalInSec;
      this.DefaultCategory = Result.DefaultCategory;
      this.DefaultSubCategory = Result.DefaultSubCategory;
      this.TorrentsLimit = Result.TorrentsLimit;
      return;
    } catch {
      return;
    }
  }

  public override async Task SaveAsync() {
    if (_storage == null) {
      return;
    }
    string JsonData = JsonSerializer.Serialize(this);
    await _storage.SetToLocalStorage(KEY_SETTINGS, JsonData);
  }

  public override Task ImportAsync(ISettings settings) {
    throw new NotImplementedException();
  }

  public override Task ExportAsync(ISettings settings) {
    throw new NotImplementedException();
  }

}
