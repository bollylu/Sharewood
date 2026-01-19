using BLTools.Core;

using SharewoodAPI;

namespace SharewoodQueryWeb.Parameters;

public class TSettingsData {

  public TSettingsData() { }

  public const int MIN_REFRESH_DELAY_IN_SECS = 5;
  public const int MAX_REFRESH_DELAY_IN_SECS = 300;
  public const int DEFAULT_REFRESH_DELAY_IN_SECS = 5;

  public const int MIN_TORRENTS_LIMITS = 1;
  public const int MAX_TORRENTS_LIMITS = 100;
  public const int DEFAULT_TORRENTS_LIMITS = 10;

  public string ApiKey { get; set; } = string.Empty;
  public string SharewoodUrl { get; set; } = "www.sharewood.tv";
  public bool AutoRefresh { get; set; } = false;
  public int RefreshIntervalInSec {
    get { return field; }
    set { field = value.WithinLimits(MIN_REFRESH_DELAY_IN_SECS, MAX_REFRESH_DELAY_IN_SECS); }
  } = DEFAULT_REFRESH_DELAY_IN_SECS;
  public int TorrentsLimit {
    get { return field; }
    set { field = value.WithinLimits(MIN_TORRENTS_LIMITS, MAX_TORRENTS_LIMITS); }
  } = DEFAULT_TORRENTS_LIMITS;

  public ESharewoodCategory DefaultCategory { get; set; } = ESharewoodCategory.Video;
  public ESharewoodSubCategory DefaultSubCategory { get; set; } = ESharewoodSubCategory.VideoFilms;

}
