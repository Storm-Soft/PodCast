using Newtonsoft.Json;
using Podcast.Domain;
using Podcast.Domain.Equipe;
using Podcast.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Podcast.Infrastructure.FileRepositories
{

    public class AdminRepository : BaseRepository, IAdminRepository
    {       

        public AdminRepository(IPathProvider connectionPathProvider) 
            : base(connectionPathProvider)
        {
        }
        
        public Task PublishEpisode(Enseignant enseignant, Episode episode)
        {
            var basePath = ConnectionPathProvider.GetSaveFileName(enseignant);
            var dtoToSave = EpisodeDto.CreateFromEpisode(episode);
            var playlistDto = LoadPlayListDto(basePath);
            playlistDto.Episodes = playlistDto.Episodes.Concat(new[] { dtoToSave }).ToArray();
            File.WriteAllText(basePath, JsonConvert.SerializeObject(playlistDto)); //ecrasera le fichier si il existe
            return Task.CompletedTask;
        }

        private PlaylistDto LoadPlayListDto(string file)
        {
            if (!File.Exists(file))
                return new PlaylistDto { Episodes = Array.Empty<EpisodeDto>() };

            var playListContent = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<PlaylistDto>(playListContent) ?? new PlaylistDto { Episodes = Array.Empty<EpisodeDto>() };
        }

        public Task<PlayList> LoadAllEpisodes(Enseignant enseignant)
        {
            if (enseignant == Enseignant.None)
                return Task.FromResult(new PlayList(Array.Empty<Episode>()));

            var basePath = ConnectionPathProvider.GetSaveFileName(enseignant);
            var playlistDto = LoadPlayListDto(basePath);
            return Task.FromResult(playlistDto.ToPlayList());
        }
    }
}