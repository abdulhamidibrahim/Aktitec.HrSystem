using System.Security.Cryptography;
using Aktitic.HrProject.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace Aktitic.HrProject.DAL.Helpers;


public static class PasswordHelper
{
    private const int SaltSize = 16; // 128 bit
    private const int KeySize = 32;  // 256 bit
    private const int Iterations = 10000; // Number of iterations for the hashing algorithm

    public static string HashPassword(string password)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            var hash = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hash, 0, SaltSize);
            Array.Copy(key, 0, hash, SaltSize, KeySize);

            return Convert.ToBase64String(hash);
        }
    }
}