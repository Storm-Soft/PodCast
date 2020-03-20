using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

namespace Podcast.Infrastructure.Security
{
    /// <summary>
    /// Implémentation de fournisseur d'encryption basé sur l'algorithme AES
    /// </summary>
    internal sealed class EncryptionProvider : IEncryptionProvider
    { 
            internal sealed class EncryptionKey
            {
                public byte[] Key { get; set; }
                public byte[] IV { get; set; }
            }

            private readonly EncryptionKey encryptionKey;

            public EncryptionProvider(IConfiguration configuration)
            {
                var encodedKey = configuration.GetValue<string>("Encryptionkey");
                var keyBytes = Convert.FromBase64String(encodedKey);
                var key = Encoding.UTF8.GetString(keyBytes);
                encryptionKey = JsonConvert.DeserializeObject<EncryptionKey>(key);
            }

        public string Encrypt(string data)
        {
            using (var algorithm = Aes.Create())
            {
                var dataBytes = Encoding.UTF8.GetBytes(data);
                var transform = algorithm.CreateEncryptor(encryptionKey.Key, encryptionKey.IV);
                var outputBuffer = transform.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
                return Convert.ToBase64String(outputBuffer);
            }
        }

        public string Decrypt(string data)
        {
            using (var algorithm = Aes.Create())
            {
                var transform = algorithm.CreateDecryptor(encryptionKey.Key, encryptionKey.IV);
                var inputbuffer = Convert.FromBase64String(data);
                var outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
                return Encoding.UTF8.GetString(outputBuffer);
            }
        }
    }
}
