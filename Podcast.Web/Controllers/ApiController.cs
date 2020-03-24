using Microsoft.AspNetCore.Mvc;
using Podcast.Domain;
using Podcast.Web.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podcast.Web.Controllers
{
    [ApiController]
    [Route("api/{ControllerName}")]
    public class ApiController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IAdminRepository adminRepository;
        private readonly ITeamRepository teamRepository;

        public ApiController(IStudentRepository studentRepository, IAdminRepository adminRepository, ITeamRepository teamRepository)
        {
            this.studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            this.adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
            this.teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
        }
    

        [Route("podcast/{enseignant}")]
        [HttpGet]
        public async Task<Episode[]> GetPlayListAsync(Enseignant enseigant)
        {
            var equipe = await teamRepository.LoadEnseignants();
            var foundEnseignant = equipe.Enseignants.FirstOrDefault(x => x.Nom == enseigant.Nom && x.Prenom == enseigant.Prenom);
            var playList = await studentRepository.LoadPlaylist(foundEnseignant);
            return playList.Episodes.Select(e => new Episode
            {
                Titre = e.TitreEpisode,
                Nom = e.NomEpisode,
                DatePublication = e.DatePublication
            }).ToArray();
        }

        [Route("equipe")]
        [HttpGet]
        public async Task<Enseignant[]> GetTeam()
        {
            var equipe = await teamRepository.LoadEnseignants();
            return equipe.Enseignants.Select(e => new Enseignant { Nom = e.Nom, Prenom = e.Prenom }).ToArray();
        }

    }
}
