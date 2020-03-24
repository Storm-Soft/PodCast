using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MediaManager;
using Android.Media;

namespace Podcast.Mobile.Droid
{

    [Activity(Label = "Podcast.Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //MediaPlayer player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CrossMediaManager.Current.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //StreamingService.Init(this);
            //player = MediaPlayer.Create(this, Resource.Layout.MainPage);
            //Button button = FindViewById<Button>(Resource.Id.MyButton);
            //button.Click += delegate {
            //    player.Start();
            //};
            //var videoView = FindViewById<VideoView>(Resource.Id.videoView1);
            //var uri = Android.Net.Uri.Parse("https://www.youtube.com/watch?v=wg-kEWsL6Xc");
            //videoView.SetVideoURI(uri);
            //videoView.Start();  
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}