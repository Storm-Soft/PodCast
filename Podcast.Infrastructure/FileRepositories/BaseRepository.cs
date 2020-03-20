using System;
using Podcast.Domain;

namespace Podcast.Infrastructure.FileRepositories
{
    public abstract class BaseRepository
    {
        protected IPathProvider ConnectionPathProvider;

        protected BaseRepository(IPathProvider connectionPathProvider)
        {
            ConnectionPathProvider = connectionPathProvider ?? throw new ArgumentNullException(nameof(connectionPathProvider));
        }

    }
}
