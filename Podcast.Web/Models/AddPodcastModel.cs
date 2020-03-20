using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlazorInputFile;

namespace Podcast.Web.Models
{
    public class AddPodcastModel
    {
        public class File
        {
            public IFileListEntry FileEntry { get; set; }
            public string Status { get; set; }
            public bool IsValid { get; set; }
        }

        public const string DefaultStatus = "Faire glisser un ou plusieurs fichiers audio";
        public const string UnsupportedFormat = "Seuls les fichiers de type Audio (.wav|.mp3|.mp4|.mov) sont supportés";

        [Required]
        public string TitreEpisode { get; set; }

        [Required]
        public DateTime DatePublication { get; set; } = DateTime.Now;

        public string Status { get; set; } = DefaultStatus;
        public File[] Files { get; set; } = Array.Empty<File>();

        public async void HandleFileSelected(IFileListEntry[] files)
        {
            Files = files.Select(f =>
            {
                var isValid = Regex.IsMatch(Path.GetExtension(f.Name), @"\.(?:wav|mp3|mp4|mov)$");
                return new File
                {
                    FileEntry = f,
                    Status = isValid ? "En attente" : "Format incorrect",
                    IsValid = isValid
                };
            }).ToArray();
         
            if (Files.Length > 0)
                Status = "Cliquer sur Envoyer";
            else
                Status = DefaultStatus;
        }
    }
}
