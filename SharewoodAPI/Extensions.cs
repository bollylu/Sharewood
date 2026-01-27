namespace SharewoodAPI;

public static class Extensions {

  public static IEnumerable<ESharewoodSubCategory> GetSubCategories(this ESharewoodCategory category) {
    return Enum.GetValues<ESharewoodSubCategory>()
        .Where(subcategory => subcategory.ToString().StartsWith(category.ToString()));
  }
}
