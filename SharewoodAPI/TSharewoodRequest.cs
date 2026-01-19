namespace SharewoodAPI;

public class TSharewoodRequest {

  public const int MIN_TORRENTS_LIMITS = 1;
  public const int MAX_TORRENTS_LIMITS = 100;
  public const int DEFAULT_TORRENTS_LIMITS = 10;

  public string NameFilter { get; set; } = string.Empty;
  public ESharewoodCategory Category { get; set; } = ESharewoodCategory.None;
  public ESharewoodSubCategory SubCategory { get; set; } = ESharewoodSubCategory.None;
  public EResponseFormat Format { get; set; } = EResponseFormat.Json;
  public int Limit {
    get { return field; }
    set { field = value.WithinLimits(MIN_TORRENTS_LIMITS, MAX_TORRENTS_LIMITS); }
  } = DEFAULT_TORRENTS_LIMITS;

  public TSharewoodRequest() { }

  public override string ToString() {
    List<string> parameters = [];
    if (!string.IsNullOrWhiteSpace(NameFilter)) {
      parameters.Add($"name={Uri.EscapeDataString(NameFilter)}");
    }
    if (Category != ESharewoodCategory.None) {
      parameters.Add($"category={(int)Category}");
    }
    if (SubCategory != ESharewoodSubCategory.None) {
      parameters.Add($"subcategory={(int)SubCategory}");
    }
    parameters.Add($"limit={Limit}");
    if (Format == EResponseFormat.Xml) {
      parameters.Add("format=xml");
    }
    return string.Join("&", parameters);
  }
}
