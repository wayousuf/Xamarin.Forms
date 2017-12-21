using System;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace CustomActivityIndicator.iOS.Renderers.ActivityIndicator
{
    public class ExtendedActivityIndicator : UIActivityIndicatorView
    {
        const string ANIMATION_KEY = "animation";
        UIImageView imageView;
        CABasicAnimation animation;

        UIImage image;

        public ExtendedActivityIndicator(UIImage image, CGRect frame) : base(frame)
        {
            this.image = image;

            DefineAnimation();
            HideDefaultActivityIndicator();
            CreateAndAddImageViewInSubview();
        }

        void DefineAnimation()
        {
            animation = CABasicAnimation.FromKeyPath("transform.rotation.z");
            animation.From = NSNumber.FromFloat(0.0f);
            animation.To = NSNumber.FromFloat(2f * (float)System.Math.PI);
            animation.Duration = 1.0f;
            animation.RepeatCount = int.MaxValue;
        }

        void HideDefaultActivityIndicator()
        {
            if (Subviews.Count() > 0)
            {
                Subviews[0].Hidden = true;
            }
        }

        void CreateAndAddImageViewInSubview()
        {
            imageView = new UIImageView(image);
            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            imageView.ClipsToBounds = true;

            var calculatedWidth = Bounds.Size.Width - imageView.Frame.Size.Width / 4;
            var calculatedHight = Bounds.Size.Height - imageView.Frame.Size.Height / 4;

            imageView.Frame = new CGRect(new CGPoint(calculatedWidth, calculatedHight), imageView.Frame.Size);

            AddSubview(imageView);
        }

        public override void StartAnimating()
        {
            base.StartAnimating();

            if (imageView != null)
            {
                imageView.Hidden = false;
                imageView.Layer.AddAnimation(animation, ANIMATION_KEY);
            }

        }

        public override void StopAnimating()
        {
            base.StopAnimating();

            if (imageView != null)
            {
                imageView.Hidden = true;
                imageView.Layer.RemoveAllAnimations();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                imageView?.Dispose();
                animation?.Dispose();
                image?.Dispose();
            }
        }
    }
}
