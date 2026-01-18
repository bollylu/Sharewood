using System.Text.Json;

namespace SharewoodAPI;

public class TSharewoodClient : IDisposable {
  private readonly HttpClient _httpClient;
  private ILogger Logger { get; init { field = value ?? new TTraceLogger<TSharewoodClient>(); } } = new TTraceLogger<TSharewoodClient>();
  public string ApiKey { get; set; }
  public string ServerAddress { get; set; }

  public HttpStatusCode LastStatusCode { get; set; } = HttpStatusCode.OK;
  public string LastStatusDescription { get; set; } = "OK";

  public string ServerUrl => $"https://{ServerAddress}/api/{ApiKey}";

  public TSharewoodClient(string apiKey, string serverAddress) {
    ApiKey = apiKey;
    ServerAddress = serverAddress;
    //HttpClientHandler handler = new() {
    //  AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
    //};

    //_httpClient = new HttpClient(handler);
    _httpClient = new HttpClient();
    _httpClient.DefaultRequestHeaders.Add("User-Agent", "Sharewood API Client/1.0");
    _httpClient.Timeout = TimeSpan.FromSeconds(30);
  }

  public async Task<TSharewoodResponse?> GetTorrentsAsync(TSharewoodRequest request) {
    string fullRequest = $"{ServerUrl}/last-torrents?{request}";
    Logger.LogDebug(fullRequest);
    HttpResponseMessage responseMessage = new HttpResponseMessage();
    try {
      using (CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(15))) {
        HttpRequestMessage requestMessage = new(HttpMethod.Get, fullRequest) {
          Version = HttpVersion.Version11
        };
        responseMessage = await _httpClient.SendAsync(requestMessage, cts.Token);
        if (responseMessage.IsSuccessStatusCode) {
          //Logger.LogDebug("Request successful");
          string content = await responseMessage.Content.ReadAsStringAsync();
          //Logger.LogDebug($"Response content length: {content.Length}");
          //Logger.LogDebug(content);
          //Logger.LogDebug("Deserializing response");
          return JsonSerializer.Deserialize<TSharewoodResponse>(content);
        } else {
          Logger.LogWarning($"Request failed: {responseMessage.StatusCode} - {responseMessage.ReasonPhrase ?? string.Empty}");
          return null;
        }
      }
    } catch (Exception ex) {
      Logger.LogErrorBox("Error fetching torrents", ex);
      return null;
    } finally {
      LastStatusCode = responseMessage.StatusCode;
      LastStatusDescription = responseMessage.ReasonPhrase ?? string.Empty;
    }
  }

  public void Dispose() {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    if (disposing) {
      _httpClient?.Dispose();
    }
  }

  ~TSharewoodClient() {
    Dispose(false);
  }
}

