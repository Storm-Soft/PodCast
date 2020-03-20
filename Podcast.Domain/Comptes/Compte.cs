using System;
using System.Collections.Generic;
using Podcast.Domain.Equipe;
using Value;

namespace Podcast.Domain.Comptes
{
    public class Compte : ValueType<Compte>
    {
        public string Nom { get; }
        public string MotDePasse { get; }
        public bool IsAdmin { get; }
        public Compte(string nom, string motDePasse, bool isAdmin)
        {
            Nom = !string.IsNullOrWhiteSpace(nom) ? nom : throw new ArgumentNullException(nameof(nom));
            MotDePasse = !string.IsNullOrWhiteSpace(motDePasse) ? motDePasse : throw new ArgumentNullException(nameof(motDePasse));
            IsAdmin = isAdmin;
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return Nom;
            yield return MotDePasse;
        }
    }
}
