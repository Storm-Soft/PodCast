using System.Linq;
using Podcast.Domain.Equipe;
using Podcast.Infrastructure.Security;

namespace Podcast.Infrastructure.Dtos
{
    public class EquipeEnseignanteDto
    {
        public EnseignantDto[] Enseignants { get; set; }

        public EquipeEnseignante ToEquipeEnseignante() => new EquipeEnseignante(Enseignants.OrderBy(e => e.Nom).Select(e => e.ToEnseignant()));

        public static EquipeEnseignanteDto CreateFromEquipeEnseignante(EquipeEnseignante equipe) => new EquipeEnseignanteDto
        {
            Enseignants = equipe.Enseignants.Select(e => EnseignantDto.CreateFromEnseignant(e)).ToArray()
        };
    }
}
