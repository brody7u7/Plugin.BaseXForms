using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WaspApp.Controls;
using WaspApp.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace WaspApp.Droid.CustomRenderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        static readonly int[][] s_colorStates =
           { new[] { global::Android.Resource.Attribute.StateEnabled }, new[] { -global::Android.Resource.Attribute.StateEnabled } };

        public CustomDatePickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var picker = e.NewElement as CustomDatePicker;
                UpdateDisabledTextColor(picker);
                Draw(picker);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var picker = sender as CustomDatePicker;
            if (e.PropertyName == CustomDatePicker.DisabledTextColorProperty.PropertyName)
                UpdateDisabledTextColor(picker);

            Draw(picker);
        }

        private void Draw(CustomDatePicker picker)
        {
            if (picker.BorderType == CustomDatePickerBorderType.Frame)
                DrawFrame(picker);
            else if (picker.BorderType == CustomDatePickerBorderType.Line)
                DrawLine(picker);
            else
                DrawNone();
        }

        void DrawFrame(CustomDatePicker picker)
        {
            var bas = Control as TextView;

            //Get the measure to get the ViewSize
            bas.Measure(0, 0);

            //is common always get the heigh value instead of width, we can "pre define" this value as a reference
            var h = (picker.HeightRequest > 0 ? picker.HeightRequest : bas.MeasuredHeight / 2);

            var side = Math.Max(h, 0);
            var radius = side * picker.BorderRadius / 100;
            GradientDrawable gd = new GradientDrawable();
            gd.SetColor(picker.BackgroundColor.ToAndroid());//background
            gd.SetCornerRadius((float)radius);//Border radius
            gd.SetStroke(picker.BorderStroke, picker.BorderColor.ToAndroid());//stroke
            Control.SetBackground(gd);
        }

        private void DrawLine(CustomDatePicker picker)
        {
            // Background drawable
            GradientDrawable backgroundDrawable = new GradientDrawable();
            backgroundDrawable.SetShape(ShapeType.Rectangle);
            backgroundDrawable.SetColor(Color.Transparent.ToAndroid());

            // Bottom line normal
            GradientDrawable lineNormalDrawable = new GradientDrawable();
            lineNormalDrawable.SetShape(ShapeType.Rectangle);
            lineNormalDrawable.SetColor(picker.BorderColor.ToAndroid());
            lineNormalDrawable.SetSize((int)Element.Width, (int)Helpers.Utilities.DpToPixels(Context, 1));

            // Creates layer to contain background drawable and bottom line drawable
            int verticalPadding = (int)Helpers.Utilities.DpToPixels(Context, 10);
            int horizontalPadding = (int)Helpers.Utilities.DpToPixels(Context, 5);
            Drawable[] drawables = new Drawable[] { backgroundDrawable, lineNormalDrawable };
            LayerDrawable normalLayer = new LayerDrawable(drawables);
            normalLayer.SetPadding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
            normalLayer.SetLayerInset(1, horizontalPadding, 0, horizontalPadding, (int)Helpers.Utilities.DpToPixels(Context, 8));
            normalLayer.SetLayerGravity(1, Android.Views.GravityFlags.Bottom);

            // Creates focused bottom line drawable
            GradientDrawable lineFocusedDrawable = new GradientDrawable();
            lineFocusedDrawable.SetShape(ShapeType.Rectangle);
            lineFocusedDrawable.SetColor(picker.FocusedBorderColor.ToAndroid());
            lineFocusedDrawable.SetSize((int)Element.Width, (int)Helpers.Utilities.DpToPixels(Context, 2));

            // Creates layer to contain background and focus drawable layers
            drawables = new Drawable[] { backgroundDrawable, lineFocusedDrawable };
            LayerDrawable focusLayer = new LayerDrawable(drawables);
            focusLayer.SetPadding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
            focusLayer.SetLayerInset(1, horizontalPadding, 0, horizontalPadding, (int)Helpers.Utilities.DpToPixels(Context, 8));
            focusLayer.SetLayerGravity(1, Android.Views.GravityFlags.Bottom);

            // Creates drawable state list
            StateListDrawable state = new StateListDrawable();
            state.AddState(new int[] { Android.Resource.Attribute.StateFocused }, focusLayer);
            state.AddState(new int[] { }, normalLayer);

            Control.SetBackground(state);
        }

        private void DrawNone()
        {
            Control.SetBackground(null);
        }

        private void UpdateDisabledTextColor(CustomDatePicker picker)
        {
            var colors = Control.TextColors;
            var newColors = new Android.Content.Res.ColorStateList(s_colorStates, new int[]
            {
                colors.GetColorForState(s_colorStates[0], picker.TextColor.ToAndroid()),
                picker.DisabledTextColor.ToAndroid()
            });
            Control.SetTextColor(newColors);
        }
    }
}