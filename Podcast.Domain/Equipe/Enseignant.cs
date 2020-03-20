using System;
using System.Collections.Generic;
using Value;

namespace Podcast.Domain.Equipe
{

    public class Enseignant : ValueType<Enseignant>
    {
        public static Enseignant None { get; } = new Enseignant();

        public string Nom { get; }
        public string Prenom { get; }


        private Enseignant() { }

        public Enseignant(string nom, string prenom)
        {
            Nom = !string.IsNullOrWhiteSpace(nom) ? nom : throw new ArgumentNullException(nameof(nom));
            Prenom = !string.IsNullOrWhiteSpace(prenom) ? prenom : throw new ArgumentNullException(nameof(prenom));
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return Nom;
            yield return Prenom;
        }
        public override string ToString() => $"{Nom} {Prenom}";
    }
}
