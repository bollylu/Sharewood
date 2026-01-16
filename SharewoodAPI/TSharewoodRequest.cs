namespace SharewoodAPI;

public class TSharewoodRequest {

  public string NameFilter { get; set; } = string.Empty;
  public ESharewoodCategory Category { get; set; } = ESharewoodCategory.None;
  public ESharewoodSubCategory SubCategory { get; set; } = ESharewoodSubCategory.None;
  public EResponseFormat Format { get; set; } = EResponseFormat.Json;
  public int Limit {
    get {
      return field;
    }
    set {
      field = value.WithinLimits(1, 100);
    }
  } = 10;

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
