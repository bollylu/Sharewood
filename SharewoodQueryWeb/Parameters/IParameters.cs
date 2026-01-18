namespace SharewoodQueryWeb;

public interface IParameters {
  void Read();
  void Write();

  string ApiKey { get; set; }
}
