using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SharewoodQueryWeb;

public class TParametersJson : IParameters {
  private readonly string _filePath;
  private readonly string _password;
  private const string SALT_BASE64 = "1q2w3e4r5t6y7u8i9o0p1q2w3e4r5t6y";
  private const string IV_BASE64 = "q2w3e4r5t6y7u8i9o0p1q2w3e4r5t6y";
  private const string DEFAULT_PARAMETERS_FILE = "parameters.json";
  private const string DEFAULT_PARAMETERS_PATH = "sharewood";
  public string ApiKey { get; set; } = string.Empty;

  public TParametersJson(string fileName = DEFAULT_PARAMETERS_FILE, string password = "") {
    string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    string sharewoodPath = Path.Combine(appDataPath, DEFAULT_PARAMETERS_PATH);

    if (!Directory.Exists(sharewoodPath)) {
      Directory.CreateDirectory(sharewoodPath);
    }

    _filePath = Path.Combine(sharewoodPath, fileName);
    _password = password;
  }

  public void Read() {
    try {
      if (File.Exists(_filePath)) {
        string jsonContent = File.ReadAllText(_filePath);
        TParametersJsonData data = JsonSerializer.Deserialize<TParametersJsonData>(jsonContent) ?? new TParametersJsonData();

        ApiKey = string.IsNullOrEmpty(_password)
          ? data.ApiKey
          : DecryptValue(data.ApiKey);
      }
    } catch (Exception ex) {
      System.Diagnostics.Debug.WriteLine($"Error reading parameters from JSON: {ex.Message}");
      ApiKey = string.Empty;
    }
  }

  public void Write() {
    try {
      string encryptedApiKey = string.IsNullOrEmpty(_password)
        ? ApiKey
        : EncryptValue(ApiKey);

      TParametersJsonData data = new TParametersJsonData {
        ApiKey = encryptedApiKey
      };

      JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
      string jsonContent = JsonSerializer.Serialize(data, options);
      File.WriteAllText(_filePath, jsonContent);
    } catch (Exception ex) {
      System.Diagnostics.Debug.WriteLine($"Error writing parameters to JSON: {ex.Message}");
    }
  }

  private string EncryptValue(string plaintext) {
    byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
    byte[] salt = Convert.FromBase64String(SALT_BASE64);
    byte[] iv = Convert.FromBase64String(IV_BASE64);

    using (Aes aes = Aes.Create()) {
      aes.KeySize = 256;
      aes.Mode = CipherMode.CBC;
      aes.Padding = PaddingMode.PKCS7;

      byte[] key = DeriveKeyFromPassword(_password, salt);
      aes.Key = key;
      aes.IV = iv;

      using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV)) {
        byte[] ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
        return Convert.ToBase64String(ciphertext);
      }
    }
  }

  private string DecryptValue(string encryptedValue) {
    if (string.IsNullOrEmpty(encryptedValue)) {
      return encryptedValue;
    }

    byte[] ciphertext = Convert.FromBase64String(encryptedValue);
    byte[] salt = Convert.FromBase64String(SALT_BASE64);
    byte[] iv = Convert.FromBase64String(IV_BASE64);

    using (Aes aes = Aes.Create()) {
      aes.KeySize = 256;
      aes.Mode = CipherMode.CBC;
      aes.Padding = PaddingMode.PKCS7;

      byte[] key = DeriveKeyFromPassword(_password, salt);
      aes.Key = key;
      aes.IV = iv;

      using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV)) {
        byte[] plaintext = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
        return Encoding.UTF8.GetString(plaintext);
      }
    }
  }

  private byte[] DeriveKeyFromPassword(string password, byte[] salt) {
    return Rfc2898DeriveBytes.Pbkdf2(password, salt, 10000, HashAlgorithmName.SHA256, 32);
  }
}

internal class TParametersJsonData {
  public string ApiKey { get; set; } = string.Empty;
}