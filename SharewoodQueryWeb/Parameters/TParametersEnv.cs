
using SharewoodQueryWeb.Parameters;

namespace SharewoodQueryWeb;

public class TParametersEnv : ISettings {

  public TParametersEnv() {
  }

  public Task<TParametersData> GetSettingsAsync() {
    throw new NotImplementedException();
  }

  public Task SetSettingsAsync(TParametersData data) {
    throw new NotImplementedException();
  }

  public Task<TParametersData> ImportAsync(ISettings settings) {
    throw new NotImplementedException();
  }

  public Task ExportAsync(ISettings settings) {
    throw new NotImplementedException();
  }
}
