using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Podcast.Domain.Equipe;

namespace Podcast.Domain
{

    public interface IAdminRepository
    {
        Task PublishEpisode(Enseignant enseignant, Episode episode);
        Task<PlayList> LoadAllEpisodes(Enseignant enseignant);       
    }
}
