using CoreAnimation;
using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UIKit;
using WaspApp.Controls;
using WaspApp.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace WaspApp.iOS.CustomRenderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.Focused -= FocusedEvent;
                e.OldElement.Unfocused -= UnfocusedEvent;
            }

            if (e.NewElement != null)
            {
                e.NewElement.Focused += FocusedEvent;
                e.NewElement.Unfocused += UnfocusedEvent;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomEditor.BorderTypeProperty.PropertyName)
                SetNeedsDisplay();
        }

        public override void Draw(CGRect e)
        {
            if ((Element as CustomEditor).BorderType == CustomEditorBorderType.Frame)
                DrawFrame(Element as CustomEditor);
            else if ((Element as CustomEditor).BorderType == CustomEditorBorderType.Line)
                DrawLine(Element as CustomEditor);
            else
                DrawNone();
        }

        void DrawFrame(CustomEditor editor)
        {
            using (var context = UIGraphics.GetCurrentContext())
            {
                //Get the current UIView size, it allow us to don't get a nullptr/overflow when we try to make the corner radious :P
                var currentSizeValue = this.SizeThatFits(new CGSize() { Height = 0, Width = 0 });

                //need to be transparent, if not, the original control color will override the "canvas" layout color
                this.SetBackgroundColor(Color.Transparent);
                //TintColor = baseEditor.TextColor.ToUIColor();

                var rc = this.Bounds.Inset(1, 1);
                var minSide = Math.Min((float)currentSizeValue.Width, (float)currentSizeValue.Height) / 2;

                var BorderRadius = minSide * editor.BorderRadius / 100;

                //must be less than the half of the smaller side and >=0
                BorderRadius = Math.Max(Math.Min(BorderRadius, minSide - 1), 0);
                var path = CGPath.FromRoundedRect(rc, BorderRadius, BorderRadius);
                
                //this.TintColor = baseEditor.TextColor.ToUIColor();//if we can set the "hint" as a different color e.e
                context.SetFillColor(editor.BackgroundColor.ToCGColor());
                context.SetStrokeColor(editor.BorderColor.ToCGColor());
                context.SetLineWidth((float)editor.BorderStroke);
                context.AddPath(path);
                context.DrawPath(CGPathDrawingMode.FillStroke);
            }
        }

        void DrawLine(CustomEditor editor)
        {
            // Creates normal layer
            var normalLayer = new CANormalLayer();
            var rect = new CGRect(NativeView.Bounds.X, Control.Frame.Height - 3, NativeView.Bounds.Width, 1f);
            normalLayer.Frame = rect;
            normalLayer.BackgroundColor = editor.BorderColor.ToCGColor();

            // Creates focus layer
            var focusLayer = new CAFocusLayer();
            rect = new CGRect(NativeView.Bounds.X, Control.Frame.Height - 4, NativeView.Bounds.Width, 2f);
            focusLayer.Frame = rect;
            focusLayer.BackgroundColor = editor.FocusedBorderColor.ToCGColor();

            // REMPLAZA LA ANTERIOR CAPA NORMAL SI EXISTE
            if (NativeView.Layer.Sublayers[0] is CANormalLayer)
                NativeView.Layer.ReplaceSublayer(NativeView.Layer.Sublayers[0], normalLayer);
            // AGREGA LA CAPA NORMAL
            else
                NativeView.Layer.InsertSublayer(normalLayer, 0);

            // REMPLAZA EL ANTERIOR GRADIENTE SI EXISTE
            if (NativeView.Layer.Sublayers[1] is CAFocusLayer)
                NativeView.Layer.ReplaceSublayer(NativeView.Layer.Sublayers[1], focusLayer);
            // AGREGA LA CAPA DEL GRADIENTE
            else
                NativeView.Layer.InsertSublayer(focusLayer, 1);

            // OCULTA LA CAPA DEL GRADIENTE
            NativeView.Layer.Sublayers[1].Hidden = true;
        }

        void DrawNone()
        {
            // Remove native borders
            //Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }

        void FocusedEvent(object sender, FocusEventArgs e)
        {
            if ((Element as CustomEditor).BorderType != CustomEditorBorderType.Line) return;
            // Shows focus line
            NativeView.Layer.Sublayers[1].Hidden = false;
        }

        void UnfocusedEvent(object sender, FocusEventArgs e)
        {
            if ((Element as CustomEditor).BorderType != CustomEditorBorderType.Line) return;
            // Hide focus line
            NativeView.Layer.Sublayers[1].Hidden = true;
        }

        /// <summary>
        /// Custom class for normal layer
        /// </summary>
        private class CANormalLayer : CALayer { }

        /// <summary>
        /// Custome class for focus layer
        /// </summary>
        private class CAFocusLayer : CALayer { }
    }
}