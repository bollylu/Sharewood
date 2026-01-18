namespace SharewoodQueryWeb;

public class TParametersEnv : IParameters {
  public string ApiKey { get; set; } = string.Empty;

  public TParametersEnv() {
  }

  public void Read() {
    ApiKey = Environment.GetEnvironmentVariable("SHAREWOOD_API_KEY") ?? string.Empty;
  }

  public void Write() {
    Environment.SetEnvironmentVariable("SHAREWOOD_API_KEY", ApiKey);
  }

}
