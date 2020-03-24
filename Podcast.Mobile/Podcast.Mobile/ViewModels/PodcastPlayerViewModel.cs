using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Podcast.Mobile.Models;
using Podcast.Mobile.Services;
using Xamarin.Forms;

namespace Podcast.Mobile.ViewModels
{
    public class PodcastPlayerViewModel : BaseViewModel
    {
        IStreamingService streamingService = DependencyService.Get<IStreamingService>();
        public ICommand Add { get; }
        private int i=0;
        public int Index
        {
            get => i;
            set => SetProperty(ref i, value);
        }
        private string index;

        public string currentIndex
        {
            get { return index; }
            set => SetProperty(ref index, value);
        }

        public Episode Episode { get; }

        private string playText = "Play";
        public string PlayText
        {
            get => playText;
            set => SetProperty(ref playText, value);
        }

        public string StopText => "Stop";
        public PodcastPlayerViewModel() { }
        public ICommand PlayResumeCommand { get; }
        public ICommand StopCommand { get; }
        public PodcastPlayerViewModel(Episode episode)
        {
            Add = new Command(() => currentIndex = $"&#x{++Index:X};");
            Episode = episode;
            PlayResumeCommand = new Command(async () => await PlayOrResumeAsync());
            StopCommand = new Command(async () => await StopAsync());
        }

        private async Task PlayOrResumeAsync()
        {
            if (PlayText == "Play")
            {
                PlayText = "Pause";
                await PlayAsync();
            }
            else
            {
                PlayText = "Play";
                await PauseAsync();
            }
        }
        private Task PlayAsync() => streamingService.PlayAsync(new Uri(Episode.Url));
        private Task PauseAsync() => streamingService.PauseAsync();
        private Task StopAsync() => streamingService.StopAsync();
    }
}
