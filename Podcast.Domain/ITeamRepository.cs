using System.Threading.Tasks;
using Podcast.Domain.Equipe;

namespace Podcast.Domain
{

    public interface ITeamRepository
    {
        Task AddOrUpdateEnseignant(EquipeEnseignante equipe, Enseignant enseignant);
        Task<EquipeEnseignante> LoadEnseignants();
    }
}
