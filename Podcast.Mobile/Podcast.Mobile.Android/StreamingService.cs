//using System;
//using System.Threading.Tasks;
//using Android.Content;
//using Android.Media;
//using Podcast.Mobile.Services;
//using Xamarin.Forms;

//[assembly: Dependency(typeof(Podcast.Mobile.Droid.StreamingService))]
//namespace Podcast.Mobile.Droid
//{
//    public class StreamingService : IStreamingService
//    {
//        private static Context Context;
//        private readonly MediaPlayer mediaPlayer = new MediaPlayer();

//        internal static void Init(Context context)
//        {
//            Context = context;
//        }

//        public StreamingService()
//        { 
//            mediaPlayer.Prepared += MediaPlayer_Prepared;
//        }

//        private void MediaPlayer_Prepared(object sender, EventArgs e)
//        {
//            mediaPlayer.Start();
//        }
               
//        public Task PauseAsync() => Task.Run(() => mediaPlayer.Pause());
        
//        public async Task PlayAsync(Uri fileUrl)
//        {
//            mediaPlayer.SetAudioStreamType(Stream.Music);
//            await mediaPlayer.SetDataSourceAsync(fileUrl.ToString());
//            mediaPlayer.PrepareAsync();
//        }

//        public  Task StopAsync() => Task.Run(() => mediaPlayer.Stop());          
//    }
//}