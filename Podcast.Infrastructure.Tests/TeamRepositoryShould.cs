using NUnit.Framework;
using Podcast.Domain;
using Podcast.Infrastructure.FileRepositories;
using System;
using System.Linq;
using System.IO;
using Podcast.Infrastructure.Security;
using System.Threading.Tasks;
using Podcast.Domain.Equipe;

namespace Podcast.Infrastructure.Tests
{
    public class TeamRepositoryShould
    {
        private IPathProvider pathProvider;
        private ITeamRepository teamRepository;
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
            teamRepository = new TeamRepository(pathProvider);
        }

        [TearDown]
        public void TearDown()
        {          
            if (File.Exists(pathProvider.GetTeamFileName()))
                File.Delete(pathProvider.GetTeamFileName());
        }     

        [Test]
        public void RetrieveEmptyTeamWithNoFile()
        {
            var currentTeam = teamRepository.LoadEnseignants().GetAwaiter().GetResult();

            Assert.IsFalse(currentTeam.Enseignants.Any());
        }

        [Test]
        public void NotThrowExceptionWhenTryingToSaveTeacherOnEmptyFile()
        {
            Assert.IsFalse(File.Exists(pathProvider.GetTeamFileName()));
            var equipe = new EquipeEnseignante(Array.Empty<Enseignant>());
            Assert.DoesNotThrow(() => teamRepository.AddOrUpdateEnseignant(equipe, Enseignant1));
            Assert.IsTrue(File.Exists(pathProvider.GetTeamFileName()));
            Assert.Greater(new FileInfo(pathProvider.GetTeamFileName()).Length, 0);
        }

        [Test]
        public void NotThrowExceptionWhenTryingToSaveTeacherOnNonEmptyFile()
        {            
            var equipe = new EquipeEnseignante(Array.Empty<Enseignant>());
            var baseLength = 0L;
            foreach (var enseignant in new[] { Enseignant1, Enseignant2 })
            {
                Assert.DoesNotThrow(() => teamRepository.AddOrUpdateEnseignant(equipe, enseignant));
                Assert.IsTrue(File.Exists(pathProvider.GetTeamFileName()));
                var fileSize = new FileInfo(pathProvider.GetTeamFileName()).Length;
                Assert.Greater(new FileInfo(pathProvider.GetTeamFileName()).Length, baseLength);
                baseLength += fileSize;
            }
        }

        [Test]
        public void SaveAndLoadTeachers()
        {
            var equipe = new EquipeEnseignante(Array.Empty<Enseignant>());

            teamRepository.AddOrUpdateEnseignant(equipe,Enseignant1);
            teamRepository.AddOrUpdateEnseignant(equipe,Enseignant2);

            equipe = teamRepository.LoadEnseignants().GetAwaiter().GetResult();
            Assert.IsTrue(equipe.Enseignants.Contains(Enseignant1));
            Assert.IsTrue(equipe.Enseignants.Contains(Enseignant2));
        }
    }
}