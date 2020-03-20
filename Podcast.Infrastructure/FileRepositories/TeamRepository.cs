using Newtonsoft.Json;
using Podcast.Domain;
using Podcast.Domain.Equipe;
using Podcast.Infrastructure.Dtos;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Podcast.Infrastructure.FileRepositories
{

    public class TeamRepository : ITeamRepository
    {       
        private readonly IPathProvider pathProvider;


        public TeamRepository(IPathProvider pathProvider)
        {
            this.pathProvider = pathProvider ?? throw new ArgumentException(nameof(pathProvider));
  
        }

        public Task AddOrUpdateEnseignant(EquipeEnseignante equipe, Enseignant enseignant)
        {
            var basePath = pathProvider.GetTeamFileName();
            var equipeDto = LoadEquipeEnseignanteDto(basePath);
            var dtoToSave = EnseignantDto.CreateFromEnseignant(enseignant);
            var foundEnseignant = equipeDto.Enseignants.FirstOrDefault(x => x.Nom == dtoToSave.Nom);           
            equipeDto.Enseignants = equipeDto.Enseignants.Except(new[] { foundEnseignant })
                                                         .Concat(new[] { dtoToSave })
                                                         .ToArray();
            File.WriteAllText(basePath, JsonConvert.SerializeObject(equipeDto)); //ecrasera le fichier si il existe
            return Task.CompletedTask;
        }

        private EquipeEnseignanteDto LoadEquipeEnseignanteDto(string file)
        {
            if (!File.Exists(file))
                return new EquipeEnseignanteDto { Enseignants = Array.Empty<EnseignantDto>() };

            var equipeContent = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<EquipeEnseignanteDto>(equipeContent) ?? new EquipeEnseignanteDto() { Enseignants = Array.Empty<EnseignantDto>() };
        }


        public Task<EquipeEnseignante> LoadEnseignants()
        {
            if (!File.Exists(pathProvider.GetTeamFileName()))
                return Task.FromResult(new EquipeEnseignante(Array.Empty<Enseignant>()));

            var equipe = File.ReadAllText(pathProvider.GetTeamFileName());
            var equipeDto = JsonConvert.DeserializeObject<EquipeEnseignanteDto>(equipe);
            if (equipeDto == null)
                return Task.FromResult(new EquipeEnseignante(Array.Empty<Enseignant>()));
            return Task.FromResult(equipeDto.ToEquipeEnseignante());
        }
    }
}