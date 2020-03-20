using System.Linq;
using Podcast.Domain.Comptes;
using Podcast.Infrastructure.Security;

namespace Podcast.Infrastructure.Dtos
{
    public class GestionnaireDeComptesDto
    {
        public CompteDto[] Comptes { get; set; }

        public GestionnaireDeComptes ToGestionnaireDeComptes(IEncryptionProvider encryptionProvider) 
            => new GestionnaireDeComptes(Comptes.Select(e => e.ToCompte(encryptionProvider)));

        public static GestionnaireDeComptesDto CreateFromGestionnaireDeComptes(GestionnaireDeComptes comptes, IEncryptionProvider encryptionProvider) => new GestionnaireDeComptesDto
        {
            Comptes = comptes.Comptes.Select(e => CompteDto.CreateFromCompte(e, encryptionProvider)).ToArray()
        };
    }
}
