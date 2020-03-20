using NUnit.Framework;
using Podcast.Domain;
using Podcast.Infrastructure.FileRepositories;
using System;
using System.Linq;
using System.IO;
using Podcast.Infrastructure.Security;
using Podcast.Domain.Equipe;

namespace Podcast.Infrastructure.Tests
{
    public class FileAdminRepositoryShould
    {
        private IPathProvider pathProvider;
        private IAdminRepository adminRepository;
        private readonly Enseignant Enseignant1 = new Enseignant("nom", "prenom");
        private readonly Enseignant Enseignant2 = new Enseignant("nom2", "prenom2");
        private class FakePathProvider : IPathProvider
        {
            public string GetAccountsFileName() => "accounts.save";

            public string GetSaveFileName(Enseignant enseignant) => $"{enseignant.Nom}.save";

            public string GetTeacherBrowsingFolder(Enseignant enseignant) => string.Empty;
            public string GetTeacherSaveFolder(Enseignant enseignant) => string.Empty;

            public string GetTeamFileName() => $"team.save";
        }

        [SetUp]
        public void Setup()
        {
            pathProvider = new FakePathProvider();
            adminRepository = new AdminRepository(pathProvider);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(pathProvider.GetSaveFileName(Enseignant1)))
                File.Delete(pathProvider.GetSaveFileName(Enseignant1));
            if (File.Exists(pathProvider.GetSaveFileName(Enseignant2)))
                File.Delete(pathProvider.GetSaveFileName(Enseignant2));
            if (File.Exists(pathProvider.GetTeamFileName()))
                File.Delete(pathProvider.GetTeamFileName());
        }

        [Test]
        public void RetrieveEmptyPlaylistWithNoFile()
        {
            var actualPlaylist = adminRepository.LoadAllEpisodes(Enseignant1).GetAwaiter().GetResult();

            Assert.IsFalse(actualPlaylist.Episodes.Any());
        }

        [Test]
        public void NotThrowExceptionWhenTryingToSaveEpisodeOnEmptyFile()
        {

            var episode = new Episode(new EpisodeName("Test01"), new EpisodeTitle("Episode 1"), new PublicationDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1)), "");

            Assert.DoesNotThrow(() => adminRepository.PublishEpisode(Enseignant1, episode));
            Assert.IsTrue(File.Exists(pathProvider.GetSaveFileName(Enseignant1)));
            Assert.Greater(new FileInfo(pathProvider.GetSaveFileName(Enseignant1)).Length, 0);
        }

        [Test]
        public void NotThrowExceptionWhenTryingToSaveEpisodeOnNonEmptyFile()
        {

            var episode1 = new Episode(new EpisodeName("Test01"), new EpisodeTitle("Episode 1"), new PublicationDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1)), "");
            var episode2 = new Episode(new EpisodeName("Test02"), new EpisodeTitle("Episode 1"), new PublicationDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 10)), "");

            Assert.DoesNotThrow(() => adminRepository.PublishEpisode(Enseignant1, episode1));
            Assert.DoesNotThrow(() => adminRepository.PublishEpisode(Enseignant1, episode2));
            Assert.IsTrue(File.Exists(pathProvider.GetSaveFileName(Enseignant1)));
            Assert.Greater(new FileInfo(pathProvider.GetSaveFileName(Enseignant1)).Length, 0);
        }

        [Test]
        public void SaveAndLoadEpisode()
        {
            var episode1 = new Episode(new EpisodeName("Test01"), new EpisodeTitle("Episode 1"), new PublicationDate(new DateTime(2020, 1, 1)),"");
            var episode2 = new Episode(new EpisodeName("Test02"), new EpisodeTitle("Episode 1"), new PublicationDate(new DateTime(2020, 1, 1)), "");

            adminRepository.PublishEpisode(Enseignant1, episode1);
            adminRepository.PublishEpisode(Enseignant1, episode2);

            var playlist = adminRepository.LoadAllEpisodes(Enseignant1).GetAwaiter().GetResult();
            Assert.IsTrue(playlist.Episodes.Contains(episode1));
            Assert.IsTrue(playlist.Episodes.Contains(episode2));
        }      
    }
}