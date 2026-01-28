namespace SharewoodQueryLib;

public interface ISettings {

  string ApiKey { get; }
  string SharewoodUrl { get; }
  bool AutoRefresh { get; }
  int RefreshIntervalInSec { get; }
  int TorrentsLimit { get; }
  ESharewoodCategory DefaultCategory { get; }
  ESharewoodSubCategory DefaultSubCategory { get; }

}
