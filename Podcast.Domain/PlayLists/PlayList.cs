using System;
using System.Collections.Generic;

namespace Podcast.Domain
{
    public class PlayList
    {
        public IEnumerable<Episode> Episodes { get; }

        public PlayList(IEnumerable<Episode> episodes)
        {
            Episodes = episodes ?? throw new ArgumentNullException(nameof(episodes));
        }
    }
}
