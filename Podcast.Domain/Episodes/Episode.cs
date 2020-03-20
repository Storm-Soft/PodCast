using System;
using System.Collections.Generic;
using Value;

namespace Podcast.Domain
{

    public class Episode : ValueType<Episode>
    {
        public EpisodeName NomEpisode { get; }
        public EpisodeTitle TitreEpisode { get; }
        public PublicationDate DatePublication { get; }

        public Episode(EpisodeName nomEpisode, EpisodeTitle titreEpisode, PublicationDate datePublication)
        {
            NomEpisode = nomEpisode ?? throw new ArgumentNullException(nameof(nomEpisode));
            TitreEpisode = titreEpisode ?? throw new ArgumentNullException(nameof(titreEpisode));
            DatePublication = datePublication ?? throw new ArgumentNullException(nameof(datePublication));
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
            => new object[] { NomEpisode, TitreEpisode };
    }
}
