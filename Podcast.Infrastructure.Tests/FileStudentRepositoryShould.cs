using NUnit.Framework;
using Podcast.Domain;
using Podcast.Infrastructure.FileRepositories;
using System;
using System.Linq;
using System.IO;
using Podcast.Infrastructure.Security;
using Podcast.Domain.Equipe;
using Podcast.Domain.Episodes;

namespace Podcast.Infrastructure.Tests
{
    public class FileStudentRepositoryShould
    {
        private IPathProvider pathProvider;
        private IStudentRepository studentRepository;
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
            studentRepository = new StudentRepository(pathProvider);
            adminRepository = new AdminRepository(pathProvider);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(pathProvider.GetSaveFileName(Enseignant1)))
                File.Delete(pathProvider.GetSaveFileName(Enseignant1));
            if (File.Exists(pathProvider.GetSaveFileName(Enseignant2)))
                File.Delete(pathProvider.GetSaveFileName(Enseignant2));
        }

        [Test]
        public void RetrieveOnePublishedEpisode()
        {
            

            var episode = new Episode(new EpisodeName("Test01"), new EpisodeTitle("Episode 1"), new PublicationDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1)),"");

            adminRepository.PublishEpisode(Enseignant1, episode);

            var actualPlaylist = studentRepository.LoadPlaylist(Enseignant1).GetAwaiter().GetResult();

            Assert.IsTrue(actualPlaylist.Episodes.Contains(episode));
        }

        [Test]
        public void RetrieveTodayPublishedEpisode()
        {
            var episode = new Episode(new EpisodeName("Test01"), new EpisodeTitle("Episode 1"), new PublicationDate(DateTime.Now), "");

            adminRepository.PublishEpisode(Enseignant1, episode);

            var actualPlaylist = studentRepository.LoadPlaylist(Enseignant1).GetAwaiter().GetResult();

            Assert.IsTrue(actualPlaylist.Episodes.Contains(episode));
        }

        [Test]
        public void RetrieveOrderedEpisode()
        {
            var episode1 = new Episode(new EpisodeName("Test01"), new EpisodeTitle("Episode 1"), new PublicationDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 10)),"");
            var episode2 = new Episode(new EpisodeName("Test02"), new EpisodeTitle("Episode 2"), new PublicationDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1)),"");

            adminRepository.PublishEpisode(Enseignant1, episode1);
            adminRepository.PublishEpisode(Enseignant1, episode2);

            var actualPlaylist = studentRepository.LoadPlaylist(Enseignant1).GetAwaiter().GetResult();

            Assert.IsTrue(actualPlaylist.Episodes.Contains(episode1));
            Assert.IsTrue(actualPlaylist.Episodes.Contains(episode2));
            Assert.IsTrue(actualPlaylist.Episodes.ElementAt(0) == episode2);
            Assert.IsTrue(actualPlaylist.Episodes.ElementAt(1) == episode1);

        }

        [Test]
        public void NotRetrievePendingEpisode()
        {
            var episode = new Episode(new EpisodeName("Test01"), new EpisodeTitle("Episode 1"), new PublicationDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 10)), "");

            adminRepository.PublishEpisode(Enseignant1, episode);

            var actualPlaylist = studentRepository.LoadPlaylist(Enseignant1).GetAwaiter().GetResult();

            Assert.IsFalse(actualPlaylist.Episodes.Any());
        }

        [Test]
        public void RetrieveEmptyPlaylistWithNoFile()
        {
            var actualPlaylist = studentRepository.LoadPlaylist(Enseignant1).GetAwaiter().GetResult();

            Assert.IsFalse(actualPlaylist.Episodes.Any());
        }

        [Test]
        public void OnlyRetrievePlaylistFromExpectedTeacher()
        {

            var episode1 = new Episode(new EpisodeName("Test01"), new EpisodeTitle("Episode 1"), new PublicationDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 10)), "");
            var episode2 = new Episode(new EpisodeName("Test02"), new EpisodeTitle("Episode 2"), new PublicationDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1)), "");

            adminRepository.PublishEpisode(Enseignant2, episode1);
            adminRepository.PublishEpisode(Enseignant1, episode2);

            var currentPlaylistForEnseignant1 = studentRepository.LoadPlaylist(Enseignant1).GetAwaiter().GetResult();
            var currentPlaylistForEnseignant2 = studentRepository.LoadPlaylist(Enseignant2).GetAwaiter().GetResult();

            Assert.IsTrue(currentPlaylistForEnseignant1.Episodes.Contains(episode2));
            Assert.IsFalse(currentPlaylistForEnseignant1.Episodes.Contains(episode1));
            Assert.IsTrue(currentPlaylistForEnseignant2.Episodes.Contains(episode1));
            Assert.IsFalse(currentPlaylistForEnseignant2.Episodes.Contains(episode2));
        }

    }
}