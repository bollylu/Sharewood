using SharewoodQueryWeb.Parameters;

namespace SharewoodQueryWeb;

public interface ISettings {
  Task<TParametersData> GetSettingsAsync();
  Task SetSettingsAsync(TParametersData data);

  Task<TParametersData> ImportAsync(ISettings settings);
  Task ExportAsync(ISettings settings);

}
