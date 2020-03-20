using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Podcast.Domain;
using Podcast.Domain.Comptes;
using Podcast.Domain.Equipe;
using Podcast.Infrastructure.FileRepositories;
using Podcast.Infrastructure.Security;

namespace Podcast.Infrastructure.Tests
{
    public class AccountManagementRepositoryShould
    {
        private IPathProvider pathProvider;
        private IAccountManagementRepository accountManagementRepository;
        private IDefaultAccount defaultAccount;

        private ITeamRepository teamRepository;
        private readonly Enseignant Enseignant1 = new Enseignant("nom", "prenom");
        private readonly Enseignant Enseignant2 = new Enseignant("nom2", "prenom2");

        private class FakeEncryptionProvider : IEncryptionProvider
        {
            public string Decrypt(string encryptedData) => encryptedData;
            public string Encrypt(string data) => data;
        }

        private class FakePathProvider : IPathProvider
        {
            public string GetAccountsFileName() => "accounts.save";

            public string GetSaveFileName(Enseignant enseignant) => $"{enseignant.Nom}.save";

            public string GetTeacherBrowsingFolder(Enseignant enseignant) => string.Empty;
            public string GetTeacherSaveFolder(Enseignant enseignant) => string.Empty;

            public string GetTeamFileName() => $"team.save";
        }

        private class FakeDefaultAccount : IDefaultAccount
        {
            private readonly Compte compte= new Compte("admin", "password", true);
            public Compte GetAdminAccount() => compte;
        }

        [SetUp]
        public void Setup()
        {
            pathProvider = new FakePathProvider();
            defaultAccount = new FakeDefaultAccount();
            accountManagementRepository = new AccountManagementRepository(pathProvider, new FakeEncryptionProvider(), defaultAccount);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(pathProvider.GetAccountsFileName()))
                File.Delete(pathProvider.GetAccountsFileName());
        }

        [Test]
        public async Task GestionnaireContientAuMoinsLeCompteAdmin()
        {
            var comptes = await accountManagementRepository.LoadAccounts();
            Assert.IsTrue(comptes.Comptes.Contains(defaultAccount.GetAdminAccount()));
        }

        [Test]
        public async Task GestionnaireContientLeCompteAdminEtLesAutres()
        {
            var comptes = new GestionnaireDeComptes(Array.Empty<Compte>());
            var compte = new Compte("nom", "mot de passe", false); 
            await accountManagementRepository.CreateOrUpdateLogin(comptes, compte);
            comptes = await accountManagementRepository.LoadAccounts();
            Assert.IsTrue(comptes.Comptes.Contains(defaultAccount.GetAdminAccount()));
            Assert.IsTrue(comptes.Comptes.Contains(compte));
        }

        [Test]
        public async Task MiseAJourMotDePasse()
        {
            var comptes = new GestionnaireDeComptes(Array.Empty<Compte>());
            var compte = new Compte("nom", "mot de passe", false);
            await accountManagementRepository.CreateOrUpdateLogin(comptes, compte);
            comptes = await accountManagementRepository.LoadAccounts();

            Assert.IsTrue(comptes.Comptes.Contains(compte));            
            var updatedCompte = new Compte("nom", "mot de passe 2", false);

            await accountManagementRepository.CreateOrUpdateLogin(comptes, updatedCompte);
            comptes = await accountManagementRepository.LoadAccounts();

            Assert.IsFalse(comptes.Comptes.Contains(compte));
            Assert.IsTrue(comptes.Comptes.Contains(updatedCompte));
        }

    }
}
