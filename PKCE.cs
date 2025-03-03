using System;
using System.Security.Cryptography;
using System.Text;

public class PKCE
{
    public static (string codeVerifier, string codeChallenge) GeneratePKCE()
    {
        // Generate a random code verifier (e.g., 43 characters long)
        var codeVerifier = GenerateRandomString(43);

        // Generate the code challenge (SHA256 of the code verifier)
        var codeChallenge = GenerateCodeChallenge(codeVerifier);

        return (codeVerifier, codeChallenge);
    }

    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";
        var randomBytes = new byte[length];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomBytes);
        }

        var randomString = new StringBuilder(length);
        foreach (var b in randomBytes)
        {
            randomString.Append(chars[b % chars.Length]);
        }
        return randomString.ToString();
    }

    private static string GenerateCodeChallenge(string codeVerifier)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(Encoding.ASCII.GetBytes(codeVerifier));
            var codeChallenge = Convert.ToBase64String(hash);
            return codeChallenge.TrimEnd('=').Replace('+', '-').Replace('/', '_'); // Base64URL encoding
        }
    }
}

