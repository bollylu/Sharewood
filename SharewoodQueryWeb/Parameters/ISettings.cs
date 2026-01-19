using SharewoodQueryWeb.Parameters;

namespace SharewoodQueryWeb;

public interface ISettings {
  Task<TSettingsData> GetSettingsAsync();
  Task SetSettingsAsync(TSettingsData data);

  Task<TSettingsData> ImportAsync(ISettings settings);
  Task ExportAsync(ISettings settings);

}
