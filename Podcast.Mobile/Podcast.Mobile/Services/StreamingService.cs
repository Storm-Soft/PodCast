using System;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Library;
using Plugin.Media.Abstractions;

namespace Podcast.Mobile.Services
{
    class StreamingService : IStreamingService
    {
        public StreamingService()
        {
            CrossMediaManager.Current.MediaItemFinished += OnMediaItemFinished;
            CrossMediaManager.Current.MediaItemFailed += OnMediaItemFailed;
        }

        private void OnMediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
        }

        private void OnMediaItemFailed(object sender, MediaManager.Media.MediaItemFailedEventArgs e)
        {

        }

        private void OnMediaPositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
        }

        public Task PlayAsync(Uri fileUrl) => CrossMediaManager.Current.Play(fileUrl);
        public Task PauseAsync() => CrossMediaManager.Current.Pause();
        public Task StopAsync() => CrossMediaManager.Current.Stop();
    }
}
