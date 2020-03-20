using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Podcast.Domain.Equipe;

namespace Podcast.Domain
{
    public interface IStudentRepository
    {
        Task<PlayList> LoadPlaylist(Enseignant enseignant);
    }
}
