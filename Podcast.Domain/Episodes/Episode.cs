using System;
using System.Collections.Generic;
using Value;

namespace Podcast.Domain.Episodes
{

    public class Episode : ValueType<Episode>
    {
        public EpisodeName NomEpisode { get; }
        public EpisodeTitle TitreEpisode { get; }
        public PublicationDate DatePublication { get; }
        public string Chemin { get; }

        public Episode(EpisodeName nomEpisode, EpisodeTitle titreEpisode, PublicationDate datePublication, string chemin)
        {
            NomEpisode = nomEpisode ?? throw new ArgumentNullException(nameof(nomEpisode));
            TitreEpisode = titreEpisode ?? throw new ArgumentNullException(nameof(titreEpisode));
            DatePublication = datePublication ?? throw new ArgumentNullException(nameof(datePublication));
            Chemin = chemin ?? throw new ArgumentNullException(nameof(chemin));
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
            => new object[] { NomEpisode, TitreEpisode };
    }
}
