using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Podcast.Domain;
using Podcast.Domain.Comptes;
using Podcast.Domain.Equipe;

namespace Podcast.Infrastructure
{

    internal class LoginProvider : ILoginProvider
    {
        private readonly IAccountManagementRepository accountManagementRepository;
        private readonly HashSet<Action<bool>> loginChangedHandlers = new HashSet<Action<bool>>();
        private Compte logedUser;

        public LoginProvider(IAccountManagementRepository accountManagementRepository)
        {
            this.accountManagementRepository = accountManagementRepository ?? throw new ArgumentNullException(nameof(accountManagementRepository));
        }

        public string GetLogedUser() => logedUser?.Nom ?? string.Empty;
        public bool IsLoged() => logedUser != null;
        public bool IsAdmin() => logedUser?.IsAdmin ?? false;

        public async Task<bool> Login(string nom, string motdepasse)
        {
            var accounts = await accountManagementRepository.LoadAccounts();
            var isLoged = accounts.Comptes.Contains(new Compte(nom, motdepasse, false));
            if (isLoged)
                logedUser = accounts.Comptes.FirstOrDefault(x => x.Nom == nom);
            await FireLoginChangedAsync();
            return isLoged;
        }

        public async Task DisconnectAsync()
        {
            logedUser = null;
            await FireLoginChangedAsync();
        }

        public void RegisterLoginChanged(Action<bool> loginChangedHandler)
        {
            if (!loginChangedHandlers.Contains(loginChangedHandler))
                loginChangedHandlers.Add(loginChangedHandler);
        }

        private async Task FireLoginChangedAsync()
        {
            var firingTasks = loginChangedHandlers.Select(handler => Task.Run(() => handler.Invoke(IsLoged())));
            await Task.WhenAll(firingTasks);
        }
    }
}
