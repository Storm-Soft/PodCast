using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Podcast.Mobile.Services;
using Podcast.Mobile.Views;
using System.Collections.Generic;
using Podcast.Mobile.Styles;
using Podcast.Mobile.Helpers;

namespace Podcast.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            Xamarin.Forms.Device.SetFlags(new[] {
                    "StateTriggers_Experimental",
                    "IndicatorView_Experimental",
                    "CarouselView_Experimental",
                    "MediaElement_Experimental"
                });
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<PodcastClient>();
            DependencyService.Register<StreamingService>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            ThemeHelper.ChangeTheme(Settings.ThemeOption, true);
        }
    }
}
