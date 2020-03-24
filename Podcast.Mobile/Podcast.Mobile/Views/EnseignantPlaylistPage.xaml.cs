using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Podcast.Mobile.Models;
using Podcast.Mobile.ViewModels;
using System.Threading.Tasks;

namespace Podcast.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EnseignantPlaylistPage : ContentPage
    {
        EnseignantPlaylistViewModel viewModel;

        public EnseignantPlaylistPage(EnseignantPlaylistViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public EnseignantPlaylistPage()
        {
            InitializeComponent();

            var enseignant = new Enseignant
            {
                Nom = "Nom temp",
                Prenom="Prenom tem",
            };

            viewModel = new EnseignantPlaylistViewModel(enseignant);
            BindingContext = viewModel;
        }
        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var episode = (Episode)layout.BindingContext;
            await Navigation.PushAsync(new PodcastPlayerPage(new PodcastPlayerViewModel(episode)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();           
            Task.Run(() => viewModel.LoadEpisodesCommand.Execute(null));
        }
    }
}