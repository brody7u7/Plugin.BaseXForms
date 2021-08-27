using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WaspApp.Controls;
using WaspApp.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]
namespace WaspApp.Droid.CustomRenderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var searchBar = e.NewElement as CustomSearchBar;
                UpdateIconColor(searchBar.IconColor);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomSearchBar.IconColorProperty.PropertyName)
                UpdateIconColor((sender as CustomSearchBar).IconColor);
        }

        void UpdateIconColor(Color color)
        {
            int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
            var icon = Control.FindViewById(searchIconId) as ImageView;
            icon?.SetColorFilter(color.ToAndroid());
        }
    }
}