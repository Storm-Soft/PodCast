using System;
using System.Collections.Generic;

namespace Podcast.Domain.Comptes
{
    public class GestionnaireDeComptes
    {
        public IList<Compte> Comptes { get; }

        public GestionnaireDeComptes(IEnumerable<Compte> comptes)
        {
            if (comptes == null)
                throw new ArgumentNullException(nameof(comptes));
            Comptes = new List<Compte>(comptes);
        }
    }
}
