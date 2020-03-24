﻿using Podcast.Domain.Episodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Podcast.Infrastructure.Dtos
{
    public class EpisodeDto
    {
        public string TitreEpisode { get; set; }
        public string NomEpisode { get; set; }
        public DateTime DatePublication { get; set; }
        public string Chemin { get; set; }

        public Episode ToEpisode() => new Episode(new EpisodeName(NomEpisode), new EpisodeTitle(TitreEpisode), new PublicationDate(DatePublication), Chemin);

        public static EpisodeDto CreateFromEpisode(Episode episode)
            => new EpisodeDto
            {
                NomEpisode = episode.NomEpisode,
                TitreEpisode = episode.TitreEpisode,
                DatePublication = episode.DatePublication,
                Chemin = episode.Chemin
            };
    }
}
