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
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(IPathProvider connectionPathProvider)
            : base(connectionPathProvider)
        {
        }

        private PlaylistDto LoadPlayListDto(string file)
        {
            if (!File.Exists(file))
                return new PlaylistDto { Episodes = Array.Empty<EpisodeDto>() };

            var playListContent = File.ReadAllText(file);
            var playListDto =  JsonConvert.DeserializeObject<PlaylistDto>(playListContent) ?? new PlaylistDto { Episodes = Array.Empty<EpisodeDto>() };
            return new PlaylistDto { Episodes = playListDto.Episodes.Where(e => e.DatePublication <= DateTime.Today.AddDays(1).AddMinutes(-1)).ToArray() };
        }

        public Task<PlayList> LoadPlaylist(Enseignant enseignant)
        {
            var basePath = ConnectionPathProvider.GetSaveFileName(enseignant);
            var playlistDto = LoadPlayListDto(basePath);
            return Task.FromResult(playlistDto.ToPlayList());
        }
    }
}