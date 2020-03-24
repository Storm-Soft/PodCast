using System;
using System.Collections.Generic;
using System.Text;

namespace Podcast.Mobile.Models
{
    public class Episode
    {
        public string Titre { get; set; }
        public string Nom { get; set; }
        public DateTime DatePublication { get; set; }
        public string Url { get; set; }
    }
}
