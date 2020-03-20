using System;
using System.Collections.Generic;

namespace Podcast.Domain.Equipe
{
    public class EquipeEnseignante
    {
        public IList<Enseignant> Enseignants { get; }

        public EquipeEnseignante(IEnumerable<Enseignant> enseignants)
        {
            if (enseignants == null)
                throw new ArgumentNullException(nameof(enseignants));
            Enseignants = new List<Enseignant>(enseignants);
        }
    }
}
