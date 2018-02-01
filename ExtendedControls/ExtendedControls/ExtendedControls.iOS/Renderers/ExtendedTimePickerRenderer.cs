using CoreAnimation;
using CoreGraphics;
using ExtendedControls.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedControls.Controls.ExtendedTimePicker), typeof(ExtendedTimePickerRenderer))]
namespace ExtendedControls.iOS.Renderers
{
    class ExtendedTimePickerRenderer : TimePickerRenderer
    {
        public ExtendedControls.Controls.ExtendedTimePicker ExtendedTimePickerElement => Element as ExtendedControls.Controls.ExtendedTimePicker;

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);


            var view = Element as ExtendedDatePicker;

            if (view != null)
            {
                if (Control != null)
                {
                    SetFont(view);
                    //UpdateFont(view);
                    SetTextAlignment(view);
                    //SetBorder(view);
                    SetNullableText(view);
                    SetPlaceholderTextColor(view);
                    //SetTransparancy(view);
                    //ResizeHeight();

                    Control.BorderStyle = UITextBorderStyle.None;
                }
            }



            if (e.NewElement != null && Control != null)
            {              
                Control.BorderStyle = UITextBorderStyle.None;                
            }

            UpdateLineColor();
        }        

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = (ExtendedDatePicker)Element;

            if (e.PropertyName == ExtendedDatePicker.FontProperty.PropertyName)
                SetFont(view);
            else if (e.PropertyName == ExtendedDatePicker.FontSizeProperty.PropertyName)
                SetFont(view);
            else if (e.PropertyName == ExtendedDatePicker.FontFamilyProperty.PropertyName)
                SetFont(view);
            else if (e.PropertyName == ExtendedDatePicker.XAlignProperty.PropertyName)
                SetTextAlignment(view);
            else if (e.PropertyName == ExtendedDatePicker.NullableDateProperty.PropertyName)
                SetNullableText(view);
            else if (e.PropertyName == ExtendedDatePicker.PlaceholderTextColorProperty.PropertyName)
                SetPlaceholderTextColor(view);

            ResizeHeight();

            if (e.PropertyName.Equals(nameof(ExtendedControls.Controls.ExtendedTimePicker.LineColorToApply)))
            {
                UpdateLineColor();
            }            
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            LineLayer lineLayer = GetOrAddLineLayer();
            lineLayer.Frame = new CGRect(0, this.Frame.Size.Height - LineLayer.LineHeight, Control.Frame.Size.Width, LineLayer.LineHeight);          
        }
        private void UpdateLineColor()
        {
            LineLayer lineLayer = GetOrAddLineLayer();            
            lineLayer.BorderColor =  ExtendedTimePickerElement.LineColorToApply.ToCGColor();            
        }
        private LineLayer GetOrAddLineLayer()
        {
            var lineLayer = Control.Layer.Sublayers?.OfType<LineLayer>().FirstOrDefault();

            if (lineLayer == null)
            {
                lineLayer = new LineLayer();

                Control.Layer.AddSublayer(lineLayer);
                Control.Layer.MasksToBounds = true;
            }

            return lineLayer;
        }       

        class LineLayer : CALayer
        {
            public static nfloat LineHeight = 2f;

            public LineLayer()
            {
                BorderWidth = LineHeight;
            }
        }





        private void SetTextAlignment(ExtendedDatePicker view)
        {
            switch (view.XAlign)
            {
                case TextAlignment.Center:
                    Control.TextAlignment = UITextAlignment.Center;
                    break;
                case TextAlignment.End:
                    Control.TextAlignment = UITextAlignment.Right;
                    break;
                case TextAlignment.Start:
                    Control.TextAlignment = UITextAlignment.Left;
                    break;
            }
        }

        private void SetFont(ExtendedDatePicker view)
        {
            UIFont uiFont = null;
            float fontSize = view.FontSize != 0 ? (float)view.FontSize : 17f;

            if (!string.IsNullOrEmpty(view.FontFamily))
            {
                if (!UIFont.FamilyNames.Contains(view.FontFamily))
                    uiFont = UIFont.FromName(view.FontFamily, fontSize);
            }

            Control.Font = uiFont ?? UIFont.SystemFontOfSize(fontSize);

            //if (view.Font != Font.Default && (uiFont = view.Font.ToUIFont()) != null)
            //    Control.Font = uiFont;
            //else if (view.Font == Font.Default)
            //    Control.Font = UIFont.SystemFontOfSize(17f);

        }

        private void SetBorder(ExtendedDatePicker view)
        {
            //Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
        }

        private void SetNullableText(ExtendedDatePicker view)
        {
            if (view.NullableDate == null)
                Control.Text = string.Empty;
        }

        private void ResizeHeight()
        {
            if (Element.HeightRequest >= 0) return;

            var height = Math.Max(Bounds.Height,
                new UITextField { Font = Control.Font }.IntrinsicContentSize.Height) * 2;

            Control.Frame = new CGRect(0.0f, 0.0f, (nfloat)Element.Width, (nfloat)height);

            Element.HeightRequest = height;
        }

        private void SetPlaceholderTextColor(ExtendedDatePicker view)
        {
            if (!string.IsNullOrEmpty(view.Placeholder))
            {
                var foregroundUIColor = view.PlaceholderTextColor.ToUIColor(CheckColorExtensions.SeventyPercentGrey);
                var backgroundUIColor = view.BackgroundColor.ToUIColor();
                var targetFont = Control.Font;
                Control.AttributedPlaceholder = new NSAttributedString(view.Placeholder, targetFont, foregroundUIColor, backgroundUIColor);
            }
        }
    }
}
