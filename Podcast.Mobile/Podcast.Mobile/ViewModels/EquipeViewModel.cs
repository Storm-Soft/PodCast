using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Podcast.Mobile.Models;
using Podcast.Mobile.Services;
using Xamarin.Forms;

namespace Podcast.Mobile.ViewModels
{
    class EquipeViewModel : BaseViewModel
    {
        public Command LoadEquipeCommand { get; }
        public ObservableCollection<Enseignant> Enseignants { get; } = new ObservableCollection<Enseignant>();

        public EquipeViewModel()
        {
            LoadEquipeCommand = new Command(async () => await LoadEquipeAsync());
        }

        async Task LoadEquipeAsync()
        {
            IsBusy = true;

            try
            {
                Enseignants.Clear();
                var enseignants = await PodcastClient.GetEquipeAsync();
                foreach (var enseignant in enseignants)
                {
                    Enseignants.Add(enseignant);
                }
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
