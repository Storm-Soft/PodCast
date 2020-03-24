using System;
using System.Threading.Tasks;
using Podcast.Mobile.Models;

namespace Podcast.Mobile.Services
{
    internal sealed class PodcastClient : IPodcastClient
    {
        public Task<Enseignant[]> GetEquipeAsync()
       => Task.FromResult(new[] {
                new Enseignant {  Nom= "Enseignant1", Prenom="Prenom1"},
                new Enseignant {  Nom= "Enseignant2", Prenom="Prenom2"},
            });

        public Task<Episode[]> GetPlayListAsync(Enseignant enseignant)
       => Task.FromResult(enseignant.Nom == "Enseignant1" ?
            new[] {
                new Episode { Titre="TitreA", Nom= "EpisodeA",DatePublication=DateTime.Now.AddDays(-5), Url="http://www.hochmuth.com/mp3/Haydn_Cello_Concerto_D-1.mp3"},
                new Episode { Titre="TitreB", Nom= "EpisodeB",DatePublication=DateTime.Now.AddDays(-4), Url="http://www.hochmuth.com/mp3/Tchaikovsky_Rococo_Var_orch.mp3"},
            } :
            new[]{
                new Episode { Titre="TitreC", Nom= "EpisodeC",DatePublication=DateTime.Now.AddDays(-3), Url="http://www.hochmuth.com/mp3/Haydn_Adagio.mp3"},
                new Episode { Titre="TitreD", Nom= "EpisodeD",DatePublication=DateTime.Now.AddDays(-1), Url = "http://www.hochmuth.com/mp3/Boccherini_Concerto_478-1.mp3"},
            });
    }
}
