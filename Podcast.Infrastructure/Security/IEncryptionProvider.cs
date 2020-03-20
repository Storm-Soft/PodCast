namespace Podcast.Infrastructure.Security
{
    /// <summary>
    /// Fournisseur d'encryption 
    /// </summary>
    public interface IEncryptionProvider
    {
        string Encrypt(string data);

        string Decrypt(string encryptedData);
    }
}
