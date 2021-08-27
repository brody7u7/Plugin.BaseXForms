using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WaspApp.Controls;
using WaspApp.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace WaspApp.Droid.CustomRenderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        static readonly int[][] s_colorStates =
            { new[] { global::Android.Resource.Attribute.StateEnabled }, new[] { -global::Android.Resource.Attribute.StateEnabled } };

        public CustomEditorRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var editor = e.NewElement as CustomEditor;
                UpdateDisabledTextColor(editor);
                Draw(editor);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var editor = sender as CustomEditor;
            if (e.PropertyName == CustomEditor.DisabledTextColorProperty.PropertyName)
                UpdateDisabledTextColor(editor);

            Draw(editor);
        }

        void Draw(CustomEditor editor)
        {
            if (editor.BorderType == CustomEditorBorderType.Frame)
                DrawFrame(editor);
            else if (editor.BorderType == CustomEditorBorderType.Line)
                DrawLine(editor);
            else
                DrawNone();
        }

        void DrawFrame(CustomEditor editor)
        {
            var bas = Control as TextView;

            //Get the measure to get the ViewSize
            bas.Measure(0, 0);

            //is common always get the heigh value instead of width, we can "pre define" this value as a reference
            var h = (editor.HeightRequest > 0 ? editor.HeightRequest : bas.MeasuredHeight / 2);

            var side = Math.Max(h, 0);
            var radius = side * editor.BorderRadius / 100;
            GradientDrawable gd = new GradientDrawable();
            gd.SetColor(editor.BackgroundColor.ToAndroid());//background
            gd.SetCornerRadius((float)radius);//Border radius
            gd.SetStroke(editor.BorderStroke, editor.BorderColor.ToAndroid());//stroke
            Control.SetBackground(gd);
        }

        void DrawLine(CustomEditor editor)
        {
            // Background drawable
            GradientDrawable backgroundDrawable = new GradientDrawable();
            backgroundDrawable.SetShape(ShapeType.Rectangle);
            backgroundDrawable.SetColor(Color.Transparent.ToAndroid());

            // Bottom line normal
            GradientDrawable lineNormalDrawable = new GradientDrawable();
            lineNormalDrawable.SetShape(ShapeType.Rectangle);
            lineNormalDrawable.SetColor(editor.BorderColor.ToAndroid());
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
            lineFocusedDrawable.SetColor(editor.FocusedBorderColor.ToAndroid());
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

        void DrawNone()
        {
            Control.SetBackground(null);
        }

        private void UpdateDisabledTextColor(CustomEditor editor)
        {
            var colors = Control.TextColors;
            var newColors = new Android.Content.Res.ColorStateList(s_colorStates, new int[]
            {
                colors.GetColorForState(s_colorStates[0], editor.TextColor.ToAndroid()),
                editor.DisabledTextColor.ToAndroid()
            });
            Control.SetTextColor(newColors);

            // Placeholder 
            var hintColors = Control.HintTextColors;
            var newHintColors = new Android.Content.Res.ColorStateList(s_colorStates, new int[]
            {
                hintColors.GetColorForState(s_colorStates[0], editor.PlaceholderColor.ToAndroid()),
                editor.DisabledTextColor.ToAndroid()
            });
            Control.SetHintTextColor(newHintColors);
        }
    }
}