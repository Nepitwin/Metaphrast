using System.Security.Cryptography;
using System.Text;

namespace Metaphrast.Translation;
public class Text
{
    public string TranslationText { get; }

    public string Sha256 { get; }

    public Text(string text, string? sha256 = null)
    {
        if(string.IsNullOrEmpty(text))
        {
            text = "";
        }

        TranslationText = text;
        sha256 ??= ComputeSha256Hash(text);
        Sha256 = sha256;
    }

    private static string ComputeSha256Hash(string text)
    {
        using var sha256Hash = SHA256.Create();
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }
        return builder.ToString();
    }

}
