using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Podcast.Domain;
using Podcast.Domain.Episodes;
using Podcast.Domain.Equipe;
using Podcast.Web.Models;

namespace Podcast.Web.Components
{
    public class AddPodcastComponent : ComponentBase
    {
        [Inject] IAdminRepository adminRepository { get; set; }
        [Inject] IPathProvider pathProvider { get; set; }

        protected string Result { get; set; }
        [Parameter] public Enseignant Enseignant { get; set; }
        [Parameter] public EventCallback<Enseignant> EnseignantChanged { get; set; }
        [Parameter] public EventCallback<Enseignant> OnPodcastAdded { get; set; }

        protected AddPodcastModel Model { get; set; } = new Models.AddPodcastModel();

        protected async Task HandleUploadPodcast()
        {
            var audioDir = pathProvider.GetTeacherSaveFolder(Enseignant);
            var readDir = pathProvider.GetTeacherBrowsingFolder(Enseignant);
            Model.Status = "Chargement...";
            var validFiles = Model.Files.Where(f => f.IsValid).ToList();
            await Task.WhenAll(validFiles.Select(f =>
                Task.Run(async () =>
                {
                    var path = Path.Combine(audioDir, f.FileEntry.Name);
                    using var fileStream = new FileStream(path, FileMode.Create);
                    await f.FileEntry.Data.CopyToAsync(fileStream);
                })
            ));

            var titreAppend = string.Empty;
            var fileIndexForThisPodcast = 1;
            foreach (var file in Model.Files)
            {
                await adminRepository.PublishEpisode(Enseignant, new Episode(nomEpisode: new EpisodeName(Path.GetFileNameWithoutExtension(file.FileEntry.Name)),
                                                                             titreEpisode: new EpisodeTitle($"{Model.TitreEpisode}{titreAppend}"),
                                                                             datePublication: new PublicationDate(Model.DatePublication),
                                                                             chemin: $"{readDir}/{file.FileEntry.Name}"));
                titreAppend = $"({fileIndexForThisPodcast++})";
            }

            Model.Status = AddPodcastModel.DefaultStatus;
            await OnPodcastAdded.InvokeAsync(Enseignant);
            Result = "Podcast Ajouté";
            await Task.Delay(2000);
            Result = string.Empty;
        }
    }
}
