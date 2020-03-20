using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Podcast.Web.Models
{
    public class LoginModel
    {
        [Required]
        public string Nom { get; set; }

        [Required]
        public string MotDePasse { get; set; }
    }
       
}
