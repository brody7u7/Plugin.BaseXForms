using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace WaspApp.Droid.Helpers
{
    public class Utilities
    {
        public static float DpToPixels(Context context, float dp)
        {
            var metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, metrics);
        }
    }
}