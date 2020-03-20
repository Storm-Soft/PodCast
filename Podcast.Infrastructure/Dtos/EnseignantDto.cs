using Podcast.Domain.Equipe;

namespace Podcast.Infrastructure.Dtos
{
    public class EnseignantDto
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public Enseignant ToEnseignant() => new Enseignant(nom: Nom, prenom: Prenom);

        public static EnseignantDto CreateFromEnseignant(Enseignant enseignant) => new EnseignantDto
        {
            Nom = enseignant.Nom,
            Prenom = enseignant.Prenom,
        };
    }
}
