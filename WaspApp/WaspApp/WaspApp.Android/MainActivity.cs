using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using WaspApp.Styles;

namespace WaspApp.Droid
{
    [Activity(Label = "WaspApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var x = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);
            var y = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            Layouts.DisplayYSizePX = x > y ? x : y;
            Layouts.DisplayXSizePX = x < y ? x : y;
            Layouts.DisplayScale = Resources.DisplayMetrics.Density;

            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}