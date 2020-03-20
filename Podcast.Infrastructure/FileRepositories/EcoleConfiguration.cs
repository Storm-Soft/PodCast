using Microsoft.Extensions.Configuration;
using Podcast.Domain;

namespace Podcast.Infrastructure.FileRepositories
{
    public class EcoleConfiguration : IEcoleConfiguration
    {
        private readonly string ecole;
        private readonly string photoPath;
        private readonly string ecoleWebSite;

        public EcoleConfiguration(IConfiguration configuration)
        {
            ecole = configuration.GetValue<string>("ecole");
            photoPath = configuration.GetValue<string>("photo");
            ecoleWebSite = configuration.GetValue<string>("website");
        }
        public string GetEcole() => ecole;
        public string GetPhotoPath() => photoPath;
        public string GetEcoleWebSite() => ecoleWebSite;
    }
}