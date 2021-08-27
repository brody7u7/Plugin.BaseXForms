using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using WaspApp.Styles;

namespace WaspApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var x = (int)UIScreen.MainScreen.Bounds.Width;
            var y = (int)UIScreen.MainScreen.Bounds.Height;
            Layouts.DisplayYSizePX = x > y ? x : y;
            Layouts.DisplayXSizePX = x < y ? x : y;
            Layouts.DisplayScale = (float)UIScreen.MainScreen.Scale;

            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
