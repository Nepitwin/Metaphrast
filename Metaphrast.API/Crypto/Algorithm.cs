using System.Security.Cryptography;
using System.Text;

namespace Metaphrast.Crypto;

public class Algorithm
{
    private const int Sha256ByteSize = 32;

    public static byte[] Sha256ToBytes(string text)
    {
        using var sha256Hash = SHA256.Create();
        return sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
    }

    public static string Sha256ToString(byte[] bytes)
    {
        if (bytes.Length != Sha256ByteSize)
        {
            throw new ArgumentException();
        }

        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }
        return builder.ToString();
    }

    public static byte[] Sha256Xor(byte[] inputA, byte[] inputB)
    {
        if (inputA.Length != inputB.Length)
        {
            throw new ArgumentException();
        }

        var result = new byte[inputA.Length];

        for (var i = 0; i < inputA.Length; i++)
        {
            result[i] = (byte)(inputA[i] ^ inputB[i]);
        }

        return result;
    }

    public static byte[] Sha256StringToByteArray(string hex)
    {
        if (hex.Length % 2 == 1)
        {
            throw new ArgumentException("The binary key cannot have an odd number of digits");
        }
            
        var arr = new byte[hex.Length >> 1];

        for (var i = 0; i < hex.Length >> 1; ++i)
        {
            arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
        }

        return arr;
    }

    private static int GetHexVal(char hex)
    {
        int val = hex;
        return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
    }
}
