namespace SharewoodQueryLib;

public interface ISettingsStorage {

  Task<ISettings?> ReadAsync();
  Task SaveAsync(ISettings settings);
  Task DeleteAsync();

  Task<ISettings?> ImportAsync(ISettingsStorage storage);
  Task ExportAsync(ISettingsStorage storage, ISettings settings);

}
