using Podcast.Domain.Equipe;

namespace Podcast.Domain
{
    public interface IPathProvider
    {
        string GetSaveFileName(Enseignant enseignant);
        string GetTeacherSaveFolder(Enseignant enseignant);
        string GetTeacherBrowsingFolder(Enseignant enseignant);
        string GetTeamFileName(); 
        string GetAccountsFileName();
    }
}
