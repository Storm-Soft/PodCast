using System.Threading.Tasks;
using Podcast.Domain.Comptes;

namespace Podcast.Domain
{

    public interface IAccountManagementRepository
    {
        Task CreateOrUpdateLogin(GestionnaireDeComptes gestionnaireDeComptes, Compte compte);
        Task<GestionnaireDeComptes> LoadAccounts();
    }
}
