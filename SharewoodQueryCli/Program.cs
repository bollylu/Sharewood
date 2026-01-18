using SharewoodAPI;

ISplitArgs Args = new SplitArgs();
Args.Parse(args);

string ParamApiKey = Args.GetValue("apikey", string.Empty);
string ParamServerAddress = Args.GetValue("server", "www.sharewood.tv");

ILogger Logger = new TFileLogger<Program>("SharewoodQueryCli.log");

if (string.IsNullOrEmpty(ParamApiKey)) {
  Logger.LogError("API Key is required.");
  Environment.Exit(1);
}

using (TSharewoodClient Client = new TSharewoodClient(ParamApiKey, ParamServerAddress)) {
  Console.WriteLine("Fetching data from Sharewood...");

  TSharewoodRequest RequestLastAnimes = new TSharewoodRequest() {
    Category = ESharewoodCategory.Video,
    SubCategory = ESharewoodSubCategory.VideoSeriesAnimation,
    Limit = 10
  };

  TSharewoodRequest RequestLastEBooks = new TSharewoodRequest() {
    Category = ESharewoodCategory.EBooks,
    SubCategory = ESharewoodSubCategory.EbookLivres,
    Limit = 10
  };

  TSharewoodResponse? Result = await Client.GetTorrentsAsync(RequestLastAnimes);
  if (Result is null) {
    Console.WriteLine("No result");
    return;
  }

  foreach (TSharewoodTorrent Torrent in Result) {
    Console.WriteLine($"{Torrent.CreatedAt} - {Torrent.Name} - {Torrent.DownloadUrl}");
  }

}

await ConsoleExtension.Pause();
