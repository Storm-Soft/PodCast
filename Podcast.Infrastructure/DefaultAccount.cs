using Microsoft.Extensions.Configuration;
using Podcast.Domain;
using Podcast.Domain.Comptes;
using Podcast.Domain.Equipe;

namespace Podcast.Infrastructure
{
    internal class DefaultAccount : IDefaultAccount
    {
        private readonly Compte adminCompte;
        public DefaultAccount(IConfiguration configuration)
        {
            adminCompte = new Compte("Admin", configuration.GetValue<string>("adminPassword"), true);
        }

        public Compte GetAdminAccount() => adminCompte;
    }
}
