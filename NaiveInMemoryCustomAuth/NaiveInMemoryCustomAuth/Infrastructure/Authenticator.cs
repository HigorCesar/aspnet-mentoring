using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NaiveInMemoryCustomAuth.Infrastructure
{
    public class Authenticator
    {
        private readonly Dictionary<string, byte[]> userPasswordsDatabase;
        private readonly HashSet<string> validKeys;
        public Authenticator()
        {
            userPasswordsDatabase = new Dictionary<string, byte[]>();
            validKeys = new HashSet<string>();
            userPasswordsDatabase.Add("higor", HashPassword("abc"));
            userPasswordsDatabase.Add("georgia", HashPassword("123"));
        }
        public byte[] HashPassword(string password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            return sha1.ComputeHash(Encoding.ASCII.GetBytes(password));
        }

        public bool Authenticate(string user, string password)
        {
            var storedPassword = userPasswordsDatabase.GetValueOrDefault(user);
            var hashedPassword = HashPassword(password);
            return storedPassword.SequenceEqual(hashedPassword);
        }
        public bool Authenticate(string authKey)
        {
            return validKeys.Contains(authKey);
        }

        public string CreateAuthKey()
        {
            var newKey = Guid.NewGuid().ToString("N");
            validKeys.Add(newKey);
            return newKey;
        }

        public void DeleteAuthKey(string authKey)
        {
            validKeys.Remove(authKey);
        }

    }
}
