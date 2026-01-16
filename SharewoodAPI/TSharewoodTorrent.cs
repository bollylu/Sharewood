using System.Text.Json.Serialization;

namespace SharewoodAPI;

public class TSharewoodTorrent {
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("info_hash")]
  public string InfoHash { get; set; } = string.Empty;

  [JsonPropertyName("type")]
  public string Type { get; set; } = string.Empty;

  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  [JsonPropertyName("slug")]
  public string Slug { get; set; } = string.Empty;

  [JsonPropertyName("tags")]
  public string Tags { get; set; } = string.Empty;

  [JsonPropertyName("size")]
  public long Size { get; set; }

  [JsonPropertyName("leechers")]
  public int Leechers { get; set; }

  [JsonPropertyName("seeders")]
  public int Seeders { get; set; }

  [JsonPropertyName("times_completed")]
  public int TimesCompleted { get; set; }

  [JsonPropertyName("category_id")]
  public int CategoryId { get; set; }

  [JsonPropertyName("subcategory_id")]
  public int SubCategoryId { get; set; }

  [JsonPropertyName("language")]
  public string Language { get; set; } = string.Empty;

  [JsonPropertyName("free")]
  public int Free { get; set; }

  [JsonPropertyName("doubleup")]
  public int DoubleUp { get; set; }

  [JsonPropertyName("created_at")]
  public string CreatedAt { get; set; } = string.Empty;

  [JsonPropertyName("download_url")]
  public string DownloadUrl { get; set; } = string.Empty;

  [JsonPropertyName("is_downloaded")]
  public bool IsDownloaded { get; set; }
}
