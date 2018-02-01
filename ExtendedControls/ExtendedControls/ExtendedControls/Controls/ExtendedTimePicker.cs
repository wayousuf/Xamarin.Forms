using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExtendedControls.Controls
{
    public class ExtendedTimePicker : TimePicker
    {
        private Color _lineColorToApply;
        public ExtendedTimePicker()
        {
            Focused += OnFocused;
            Unfocused += OnUnfocused;

            ResetLineColor();
        }
        public Color LineColorToApply
        {
            get => _lineColorToApply;
            private set
            {
                _lineColorToApply = value;
                OnPropertyChanged(nameof(LineColorToApply));
            }
        }


        public static readonly BindableProperty LineColorProperty =
            BindableProperty.Create("LineColor", typeof(Color), typeof(ExtendedTimePicker), Color.Default);
        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }


        public static readonly BindableProperty FocusLineColorProperty =
            BindableProperty.Create("FocusLineColor", typeof(Color), typeof(ExtendedTimePicker), Color.Default);
        public Color FocusLineColor
        {
            get { return (Color)GetValue(FocusLineColorProperty); }
            set { SetValue(FocusLineColorProperty, value); }
        }

        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create("IsValid", typeof(bool), typeof(ExtendedTimePicker), true);
        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }


        public static readonly BindableProperty InvalidLineColorProperty =
            BindableProperty.Create("InvalidLineColor", typeof(Color), typeof(ExtendedTimePicker), Color.Default);
        public Color InvalidLineColor
        {
            get { return (Color)GetValue(InvalidLineColorProperty); }
            set { SetValue(InvalidLineColorProperty, value); }
        }



        public static readonly BindableProperty FontProperty =
            BindableProperty.Create(nameof(Font), typeof(Font), typeof(ExtendedTimePicker), default(Font));

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ExtendedTimePicker), default(double));

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ExtendedTimePicker), default(string));

        public static readonly BindableProperty NullableTimeProperty =
            BindableProperty.Create(nameof(NullableTime), typeof(TimeSpan?), typeof(ExtendedTimePicker), null, BindingMode.TwoWay);

        public static readonly BindableProperty XAlignProperty =
          BindableProperty.Create(nameof(XAlign), typeof(TextAlignment), typeof(ExtendedTimePicker),
          TextAlignment.Start);

        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(ExtendedTimePicker), true);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ExtendedTimePicker), string.Empty, BindingMode.OneWay);

        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create(nameof(PlaceholderTextColor), typeof(Color), typeof(ExtendedTimePicker), Color.Default);

        public static readonly BindableProperty ReturnButtonProperty =
            BindableProperty.Create(nameof(ReturnButton), typeof(ReturnButtonType), typeof(ExtendedTimePicker), ReturnButtonType.None);

        public ReturnButtonType ReturnButton
        {
            get { return (ReturnButtonType)GetValue(ReturnButtonProperty); }
            set { SetValue(ReturnButtonProperty, value); }
        }

        public static readonly BindableProperty NextViewProperty =
            BindableProperty.Create(nameof(NextView), typeof(View), typeof(ExtendedTimePicker));

        public View NextView
        {
            get { return (View)GetValue(NextViewProperty); }
            set { SetValue(NextViewProperty, value); }
        }

        public Font Font
        {
            get { return (Font)GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
        }
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public TimeSpan? NullableTime
        {
            get { return (TimeSpan?)GetValue(NullableTimeProperty); }
            set
            {
                if (value != NullableTime)
                {
                    SetValue(NullableTimeProperty, value);
                    UpdateDate();
                }
            }
        }

        public TextAlignment XAlign
        {
            get { return (TextAlignment)GetValue(XAlignProperty); }
            set { SetValue(XAlignProperty, value); }
        }

        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public Color PlaceholderTextColor
        {
            get { return (Color)GetValue(PlaceholderTextColorProperty); }
            set { SetValue(PlaceholderTextColorProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsFocusedProperty.PropertyName)
            {
                if (IsFocused)
                {
                    if (!NullableTime.HasValue)
                    {
                        Time = (TimeSpan)TimeProperty.DefaultValue;
                    }
                }
                else
                {
                    OnPropertyChanged(TimeProperty.PropertyName);
                }
            }
            //});

            if (propertyName == TimeProperty.PropertyName)
            {
                NullableTime = Time;
            }

            if (propertyName == NullableTimeProperty.PropertyName)
            {
                if (NullableTime.HasValue)
                {
                    Time = NullableTime.Value;
                }
            }
            if (propertyName == IsValidProperty.PropertyName)
            {
                CheckValidity();
            }
        }

        private void UpdateDate()
        {
            if (NullableTime.HasValue)
            {
                Time = NullableTime.Value;
            }
            else
            {
                Time = (TimeSpan)TimeProperty.DefaultValue;
            }
        }


        private void OnFocused(object sender, FocusEventArgs e)
        {
            IsValid = true;
            LineColorToApply = FocusLineColor != Color.Default
                ? FocusLineColor
                : GetNormalStateLineColor();
        }

        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            ResetLineColor();
        }

        private void CheckValidity()
        {
            if (!IsValid)
            {
                LineColorToApply = InvalidLineColor;
            }
        }
        private void ResetLineColor()
        {
            LineColorToApply = GetNormalStateLineColor();
        }
        private Color GetNormalStateLineColor()
        {
            return LineColor != Color.Default
                ? LineColor
                : TextColor;
        }
        public void OnNext()
        {
            NextView?.Focus();
        }

    }
}
