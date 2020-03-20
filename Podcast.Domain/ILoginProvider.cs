using System;
using System.Threading.Tasks;
using Podcast.Domain.Equipe;

namespace Podcast.Domain
{
    public interface ILoginProvider
    {
        Task<bool> Login(string nom, string motdepasse);
        string GetLogedUser();
        bool IsLoged();
        bool IsAdmin();
        Task DisconnectAsync();
        void RegisterLoginChanged(Action<bool> loginChangedHandler);
    }
}
