using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;

namespace Podcast.Web.Models
{
    public class AddPodcastModel
    {
        [Required]
        public string TitreEpisode { get; set; }

        [Required]
        public DateTime DatePublication { get; set; }

        public IFileListEntry[] Files { get; set; }

        public void HandleFileSelected(IFileListEntry[] files) => Files = files;
    }
}
