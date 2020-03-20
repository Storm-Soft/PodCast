using Podcast.Domain.Comptes;

namespace Podcast.Domain
{
    public interface IDefaultAccount
    {
        Compte GetAdminAccount();
    }
}
