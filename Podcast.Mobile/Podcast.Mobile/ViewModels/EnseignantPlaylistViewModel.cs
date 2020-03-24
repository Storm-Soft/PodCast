using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Podcast.Mobile.Models;
using Xamarin.Forms;

namespace Podcast.Mobile.ViewModels
{
    public class EnseignantPlaylistViewModel : BaseViewModel
    {

        public Command LoadEpisodesCommand { get; }
        public ObservableCollection<Episode> Episodes { get; } = new ObservableCollection<Episode>();

        private readonly Enseignant enseignant;

        public EnseignantPlaylistViewModel(Enseignant enseignant)
        {
            this.enseignant = enseignant ?? throw new ArgumentNullException(nameof(enseignant));
            Title = $"Episodes de {enseignant.Prenom} {enseignant.Nom}";
            LoadEpisodesCommand = new Command(async () => await LoadEpisodesAsync());
        }

        async Task LoadEpisodesAsync()
        {
            IsBusy = true;

            try
            {
                Episodes.Clear();
                var episodes = await PodcastClient.GetPlayListAsync(enseignant) ?? Array.Empty<Episode>();
                foreach (var episode in episodes)
                    Episodes.Add(episode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
