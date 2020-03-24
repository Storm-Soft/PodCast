using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Podcast.Mobile.Models;
using Podcast.Mobile.ViewModels;
using System.Timers;
using System.Globalization;
using System.Linq;

namespace Podcast.Mobile.Views
{  
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PodcastPlayerPage: ContentPage
    {
    
        bool polling = true;
        Timer playbackTimer;
        TimeSpan moveSpan = TimeSpan.Zero;

        public PodcastPlayerPage(PodcastPlayerViewModel viewModel)
            :this()
        {
            BindingContext = viewModel;
        }

        public PodcastPlayerPage()
        {
            InitializeComponent();
            playbackTimer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MediaElementAudio.StateRequested += MediaElementAudio_StateRequested;
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (MediaElementAudio.CurrentState == MediaElementState.Playing)
                    {                                               
                        var total = MediaElementAudio.Duration?.TotalSeconds ?? 0;
                        if (total == 0)
                            return;

                        MediaElementAudio.Position = MediaElementAudio.Position.Add(moveSpan);
                        moveSpan = TimeSpan.Zero;
                        var progress = (MediaElementAudio.Position.TotalSeconds / total);
                        if (progress > 1)
                            progress = 1;
                        ProgressBarProgress.Progress = progress;

                    }
                });
                return polling;
            });
        }




        async void ButtonClose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            polling = false;
            //}
            MediaElementAudio.StateRequested -= MediaElementAudio_StateRequested;
            if (MediaElementAudio.CurrentState == MediaElementState.Playing)
                MediaElementAudio.Stop();

#if !DEBUG
                        if (Navigation.ModalStack.Any())
                            await Navigation.PopModalAsync();
#endif
    }

        void Start()
        {

            //playbackTimer.Elapsed += OnPlaybackTimerElapsed;
            //playbackTimer.Start();
            //MediaElementAudio.Play();
        }

        void Stop()
        {
            playbackTimer.Elapsed -= OnPlaybackTimerElapsed;
            playbackTimer.Stop();
            MediaElementAudio.Pause();
        }

        private void MediaElementAudio_StateRequested(object sender, StateRequested e)
        {
            VisualStateManager.GoToState(PlayPauseButton,
                 (e.State == MediaElementState.Playing)
                 ? "playing"
                 : "paused");
        }

        void OnPlaybackTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateTimeDisplay();
        }

        void PlayPauseButton_Clicked(object sender, EventArgs e)
        {
            if (MediaElementAudio.CurrentState == MediaElementState.Playing)
                Stop();
            else
                Start();
        }

        void ForwardButton_Clicked(object sender, EventArgs e)
        {
            moveSpan = moveSpan.Add(TimeSpan.FromSeconds(10));
        }

        void RewindButton_Click(object sender, EventArgs e)
        {
            moveSpan = moveSpan.Add(TimeSpan.FromSeconds(-10));
        }

        void UpdateTimeDisplay()
        {

            //Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            //{
            //    var total = MediaElementAudio.Duration?.TotalSeconds ?? 0;
            //    if (total == 0)
            //        return;               

            //    //var progress = (MediaElementAudio.Position.TotalSeconds / total);
            //    //if (progress > 1)
            //    //    progress = 1;
            //    //ProgressBarProgress.Progress = progress;
            //});
        }

        //private void OnPositionSliderValueChanged(object sender, ValueChangedEventArgs e)
        //{

        //}
        //void OnPlayPauseButtonClicked(object sender, EventArgs args)
        //{
        //    if (MediaElementAudio.CurrentState == MediaElementState.Closed ||
        //        MediaElementAudio.CurrentState == MediaElementState.Stopped ||
        //        MediaElementAudio.CurrentState == MediaElementState.Paused)
        //    {
        //        MediaElementAudio.Play();
        //    }
        //    else if (MediaElementAudio.CurrentState == MediaElementState.Playing)
        //    {
        //        MediaElementAudio.Pause();
        //    }
        //}

        //void OnStopButtonClicked(object sender, EventArgs args)
        //{
        //    MediaElementAudio.Stop();
        //    positionSlider.Value = 0;
        //}

        //        void OnPositionSliderValueChanged(System.Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        //        {
        //            polling = false;
        //            MediaElementAudio.Position = positionSlider.Position;
        //            positionLabel.Text = MediaElementAudio.Position.ToString("hh\\:mm\\:ss");
        //            polling = true;
        //        }
    }
}