using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Podcast.Mobile.Models;

namespace Podcast.Mobile.Services
{
    public interface IPodcastClient
    {
        Task<Episode[]> GetPlayListAsync(Enseignant enseignant);
        Task<Enseignant[]> GetEquipeAsync();
    }
}
