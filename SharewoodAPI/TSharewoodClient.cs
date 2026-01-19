using System.Text.Json;

namespace SharewoodAPI;

public class TSharewoodClient : IDisposable {

  public const int TIMEOUT_IN_SEC = 30;

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
    _httpClient = new HttpClient();
    _httpClient.DefaultRequestHeaders.Add("User-Agent", "Sharewood API Client/1.0");
    _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
    _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
    _httpClient.DefaultRequestHeaders.Add("Origin", "vpn.sharenet.be");
  }

  public async Task<TSharewoodResponse?> GetLastTorrentsAsync(TSharewoodRequest request) {
    string fullRequest = $"{ServerUrl}/last-torrents?{request}";
    Logger.LogDebug(fullRequest);
    HttpResponseMessage responseMessage = new HttpResponseMessage();
    try {
      using (CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(TIMEOUT_IN_SEC))) {
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

  public async Task<TSharewoodResponse?> SearchTorrentsAsync(TSharewoodRequest request) {
    string fullRequest = $"{ServerUrl}/search?{request}";
    Logger.LogDebug(fullRequest);
    HttpResponseMessage responseMessage = new HttpResponseMessage();
    try {
      using (CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(TIMEOUT_IN_SEC))) {
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

