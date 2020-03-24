using System;
using System.Collections.Generic;

using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Podcast.Mobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(Podcast.Mobile.Droid.Services.Environment))]
namespace Podcast.Mobile.Droid.Services
{
        public class Environment : IEnvironment
        {
            public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
            {
                if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                    return;

                var activity = Platform.CurrentActivity;
                var window = activity.Window;
                window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
                window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                window.SetStatusBarColor(color.ToPlatformColor());

                if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                {
                    var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
                    window.DecorView.SystemUiVisibility = darkStatusBarTint ? flag : 0;
                }
            }
        }
    }