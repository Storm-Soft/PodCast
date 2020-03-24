using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Podcast.Mobile.Models;
using Podcast.Mobile.Views;
using Podcast.Mobile.ViewModels;

namespace Podcast.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EquipePage : ContentPage
    {
        EquipeViewModel viewModel;

        public EquipePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new EquipeViewModel();
           
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var enseignant = (Enseignant)layout.BindingContext;
            await Navigation.PushAsync(new EnseignantPlaylistPage(new EnseignantPlaylistViewModel(enseignant)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Enseignants.Count == 0)
                //viewModel.IsBusy = true;
            Task.Run(() => viewModel.LoadEquipeCommand.Execute(null));
        }
    }
}