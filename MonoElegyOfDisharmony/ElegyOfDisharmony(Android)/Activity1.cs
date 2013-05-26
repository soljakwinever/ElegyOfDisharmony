using Android.App;
using Android.Content.PM;
using Android.OS;

namespace ElegyOfDisharmony_Android_
{
    [Activity(Label = "ElegyOfDisharmony(Android)"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ElegyGame.Activity = this;
            var g = new ElegyGame();
            SetContentView(g.Window);
            g.Run();
        }
    }
}

