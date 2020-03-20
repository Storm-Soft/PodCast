using Newtonsoft.Json;
using Podcast.Domain;
using Podcast.Domain.Comptes;
using Podcast.Infrastructure.Dtos;
using Podcast.Infrastructure.Security;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Podcast.Infrastructure.FileRepositories
{
    public class AccountManagementRepository: IAccountManagementRepository
    {        
        private readonly IEncryptionProvider encryptionProvider; 
        private readonly IPathProvider pathProvider;
        private readonly IDefaultAccount defaultAccount;

        public AccountManagementRepository(IPathProvider pathProvider, IEncryptionProvider encryptionProvider, IDefaultAccount defaultAccount)
        {
            this.pathProvider = pathProvider ?? throw new ArgumentException(nameof(pathProvider));
            this.encryptionProvider = encryptionProvider ?? throw new ArgumentException(nameof(encryptionProvider));
            this.defaultAccount = defaultAccount ?? throw new ArgumentException(nameof(defaultAccount));
        }

        public Task CreateOrUpdateLogin(GestionnaireDeComptes gestionnaireDeCompte, Compte compte)
        {
            var basePath = pathProvider.GetAccountsFileName();
            var comptesDto = LoadGestionnairedeComptesDto(basePath);
            var dtoToSave = CompteDto.CreateFromCompte(compte, encryptionProvider);
            var foundCompte = comptesDto.Comptes.FirstOrDefault(x => x.Nom == dtoToSave.Nom);
            comptesDto.Comptes = comptesDto.Comptes.Except(new[] { foundCompte })
                                                         .Concat(new[] { dtoToSave })
                                                         .ToArray();
            File.WriteAllText(basePath, JsonConvert.SerializeObject(comptesDto)); //ecrasera le fichier si il existe
            return Task.CompletedTask;
        }

        private GestionnaireDeComptesDto CreateDefaultGestionnaireDto()
            => new GestionnaireDeComptesDto { Comptes = Array.Empty<CompteDto>() };

        private GestionnaireDeComptes CreateDefaultGestionnaire()
            => new GestionnaireDeComptes(new[] { defaultAccount.GetAdminAccount() });

        private GestionnaireDeComptesDto LoadGestionnairedeComptesDto(string file)
        {
            if (!File.Exists(file))
                return CreateDefaultGestionnaireDto();

            var comptesContent = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<GestionnaireDeComptesDto>(comptesContent) ?? CreateDefaultGestionnaireDto();
        }

        public Task<GestionnaireDeComptes> LoadAccounts()
        {
            if (!File.Exists(pathProvider.GetAccountsFileName()))
                return Task.FromResult(CreateDefaultGestionnaire());


            var equipe = File.ReadAllText(pathProvider.GetAccountsFileName());
            var comptesDto = JsonConvert.DeserializeObject<GestionnaireDeComptesDto>(equipe);
            if (comptesDto == null)
                return Task.FromResult(CreateDefaultGestionnaire());
            
            return Task.FromResult(new GestionnaireDeComptes(new[] { defaultAccount.GetAdminAccount() }.Concat(comptesDto.ToGestionnaireDeComptes(encryptionProvider).Comptes))); //on insère le compte admin
        }
    }
}