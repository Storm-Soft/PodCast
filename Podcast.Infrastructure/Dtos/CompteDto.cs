using Podcast.Domain.Comptes;
using Podcast.Domain.Equipe;
using Podcast.Infrastructure.Security;

namespace Podcast.Infrastructure.Dtos
{

    public class CompteDto
    {
        public string Nom { get; set; }
        public string MotDePasse { get; set; }
        public bool IsAdmin { get; set; }

        public Compte ToCompte(IEncryptionProvider encryptionProvider) => new Compte(nom: Nom, motDePasse: encryptionProvider.Decrypt(MotDePasse), IsAdmin);

        public static CompteDto CreateFromCompte(Compte compte, IEncryptionProvider encryptionProvider) => new CompteDto
        {
            Nom = compte.Nom,
            MotDePasse = encryptionProvider.Encrypt(compte.MotDePasse),
            IsAdmin = compte.IsAdmin
        };
    }
}
