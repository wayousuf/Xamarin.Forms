using System;
using System.ComponentModel;
using System.Drawing;
using CustomActivityIndicator.iOS.Renderers.ActivityIndicator;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ActivityIndicator), typeof(ExtendedActivityIndicatorRenderer))]
namespace CustomActivityIndicator.iOS.Renderers.ActivityIndicator
{
    public class ExtendedActivityIndicatorRenderer : ActivityIndicatorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ActivityIndicator> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new ExtendedActivityIndicator(UIImage.FromFile("custom_spinner.png"), RectangleF.Empty)
                    { ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.WhiteLarge });
                }
            }

            base.OnElementChanged(e);
        }
    }
}
