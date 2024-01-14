using System.Security.Cryptography;

namespace AppDevCW1.Data;

public static class Func
{
    private const char _segmentDelimiter = ':';
    // hashing for encryption
    public static string HashSecret(string input)
    {
        var saltSize = 16;
        var iterations = 100_000;
        var keySize = 32;
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algorithm, keySize);

        return string.Join(
            _segmentDelimiter,
            Convert.ToHexString(hash),
            Convert.ToHexString(salt),
            iterations,
            algorithm
        );
    }
    // to verify the hash
    public static bool VerifyHash(string input, string hashString)
    {
        string[] segments = hashString.Split(_segmentDelimiter);
        byte[] hash = Convert.FromHexString(segments[0]);
        byte[] salt = Convert.FromHexString(segments[1]);
        int iterations = int.Parse(segments[2]);
        HashAlgorithmName algorithm = new(segments[3]);
        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            iterations,
            algorithm,
            hash.Length
        );

        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }

    //filepath to save the fenerated report
    public static string GetAppDirectoryPath()
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "Bislerium Cafe Report"
        );
    }

    public static string GetAppUsersFilePath()
    {
        return Path.Combine(GetAppDirectoryPath(), "users.json");
    }

    public static string GetAppCoffeesFilePath()
    {
        return Path.Combine(GetAppDirectoryPath(), "Coffees.json");
    }

    public static string GetAppAddInsFilePath()
    {
        return Path.Combine(GetAppDirectoryPath(), "addIns.json");
    }
    public static string GetAppOrdersFilePath()
    {
        return Path.Combine(GetAppDirectoryPath(), "orders.json");
    }
}