using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MultiSelectionSegmented.Control
{
    public class SegmentControl : ContentView
    {
        public static readonly BindableProperty SelectedItemsIndexProperty = BindableProperty.Create(nameof(SelectedItemsIndex), typeof(List<int>), typeof(SegmentControl), new List<int>());
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(List<string>), typeof(SegmentControl), null);
        public static readonly BindableProperty ItemColorProperty = BindableProperty.Create(nameof(ItemColor), typeof(Color), typeof(SegmentControl), Color.White);
        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create(nameof(ItemWidth), typeof(double), typeof(SegmentControl), 0.0);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(SegmentControl), Color.DarkGray);
        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(SegmentControl), Color.Black);
        public static readonly BindableProperty SelectedItemColorProperty = BindableProperty.Create(nameof(SelectedItemColor), typeof(Color), typeof(SegmentControl), Color.Blue);
        public static readonly BindableProperty SeperatorBorderSizeProperty = BindableProperty.Create(nameof(SeperatorBorderSize), typeof(int), typeof(SegmentControl), 5);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(SegmentControl), Color.Black);
        public static readonly BindableProperty OuterBorderSizeProperty = BindableProperty.Create(nameof(OuterBorderSize), typeof(int), typeof(SegmentControl), 5);
        public static readonly BindableProperty IsMultiSelectProperty = BindableProperty.Create(nameof(IsMultiSelect), typeof(bool), typeof(SegmentControl), false);

        public List<int> SelectedItemsIndex 
        { 
            get => (List<int>)GetValue(SelectedItemsIndexProperty); 
            set =>  SetValue(SelectedItemsIndexProperty, value); 
        } 
        public List<string> Items
        {
            get => (List<string>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }
        public Color ItemColor
        {
            get => (Color)GetValue(ItemColorProperty);
            set => SetValue(ItemColorProperty, value);
        }
        public double ItemWidth
        {
            get => (double)GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }
        public Color TextColor 
        { 
            get => (Color)GetValue(TextColorProperty);
            set =>  SetValue(TextColorProperty, value); 
        }
        public Color SelectedTextColor 
        { 
            get => (Color)GetValue(SelectedTextColorProperty); 
            set => SetValue(SelectedTextColorProperty, value); 
        }
        public Color SelectedItemColor
        {
            get => (Color)GetValue(SelectedItemColorProperty);
            set => SetValue(SelectedItemColorProperty, value);
        }
        public int SeperatorBorderSize
        {
            get => (int)GetValue(SeperatorBorderSizeProperty);
            set => SetValue(SeperatorBorderSizeProperty, value);
        }
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        public int OuterBorderSize
        {
            get => (int)GetValue(OuterBorderSizeProperty);
            set => SetValue(OuterBorderSizeProperty, value);
        }

        public bool IsMultiSelect
        {
            get => (bool)GetValue(IsMultiSelectProperty);
            set => SetValue(IsMultiSelectProperty, value);
        }

        public Action<List<int>> SelectedItemAction;

        StackLayout _container;
        StackLayout _lastSelectedItem;

        void PopulateItems(){
            _container.Children.Clear();

            for (int i = 0; i < Items.Count; i++)
            {
                var stackLayout = new StackLayout
                {
                    Padding = new Thickness(10),
                    BackgroundColor = ItemColor,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    ClassId = Boolean.FalseString
                };

                if (ItemWidth > 0)
                {
                    stackLayout.WidthRequest = ItemWidth;
                }

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (sender, e) =>  {
                    SelectedItems((StackLayout)sender);
                };

                stackLayout.GestureRecognizers.Add(tapGestureRecognizer);

                var label = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    TextColor = TextColor,
                    Text = Items[i]
                };

                stackLayout.Children.Add(label);
                _container.Children.Add(stackLayout);
            }

            this.Content = _container;
        }

        void SelectedItems(StackLayout selectedItem)
        {
            selectedItem.ClassId = selectedItem.ClassId == Boolean.FalseString ? Boolean.TrueString : Boolean.FalseString;
            var index = _container.Children.IndexOf(selectedItem);

            if (!IsMultiSelect)
            {
                SelectedItemsIndex.Clear();

                if (_lastSelectedItem != null)
                {
                    _lastSelectedItem.BackgroundColor = ItemColor;
                    ((Label)_lastSelectedItem.Children[0]).TextColor = TextColor;
                    _lastSelectedItem.ClassId = Boolean.FalseString;
                }

                if (selectedItem.ClassId == Boolean.TrueString)
                {
                    selectedItem.BackgroundColor = SelectedItemColor;
                    ((Label)selectedItem.Children[0]).TextColor = SelectedTextColor;
                    SelectedItemsIndex.Add(index);

                } else {
                    selectedItem.BackgroundColor = ItemColor;
                    ((Label)selectedItem.Children[0]).TextColor = TextColor;
                }

                _lastSelectedItem = _lastSelectedItem == selectedItem ? null : selectedItem;

            } else {

                if (selectedItem.ClassId == Boolean.TrueString)
                {
                    selectedItem.BackgroundColor = SelectedItemColor;
                    ((Label)selectedItem.Children[0]).TextColor = SelectedTextColor;
                    if (!SelectedItemsIndex.Contains(index))                    {
                        SelectedItemsIndex.Add(index);
                    }
                }
                else
                {
                    selectedItem.BackgroundColor = ItemColor;
                    ((Label)selectedItem.Children[0]).TextColor = TextColor;
                    if (SelectedItemsIndex.Contains(index))
                    {
                        SelectedItemsIndex.Remove(index);
                    }
                }

                SelectedItemsIndex.Sort();
            }

            SelectedItemAction?.Invoke(SelectedItemsIndex);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ItemsProperty.PropertyName && Items != null) {
                PopulateItems();
            } else if (propertyName == BorderColorProperty.PropertyName) {
                _container.BackgroundColor = BorderColor;
            } else if (propertyName == OuterBorderSizeProperty.PropertyName) {
                _container.Padding = new Thickness(OuterBorderSize);
            } else if (propertyName == SeperatorBorderSizeProperty.PropertyName) {
                _container.Spacing = SeperatorBorderSize;
            } else if (propertyName == SelectedItemsIndexProperty.PropertyName) {
                if (SelectedItemsIndex.Count > 0)
                {
                    if (IsMultiSelect)
                    {
                        for (int i = 0; i < SelectedItemsIndex.Count; i++)
                        {
                            SelectedItems((StackLayout)_container.Children[SelectedItemsIndex[i]]);
                        }
                    } else {
                        SelectedItems((StackLayout)_container.Children[SelectedItemsIndex[0]]);

                    }
                }
            }
        }

        public SegmentControl()
        {
            _container = new StackLayout
            { 
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Spacing = SeperatorBorderSize,
                Padding = new Thickness(OuterBorderSize),
                BackgroundColor = BorderColor,
            };
        }
    }
}
