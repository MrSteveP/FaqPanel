using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace FaqPanel.Views.Controls
{
    /// <summary>
    /// https://martynnw.wordpress.com/2017/05/14/xamarin-forms-repeater-view/
    /// https://github.com/Martynnw/Trailmaker.UI
    /// </summary>
    public class Repeater : StackLayout
    {
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(Repeater), null);
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create("ItemSource", typeof(IEnumerable<object>), typeof(Repeater), propertyChanging: ItemSourceChanging);
        public static readonly BindableProperty IsStripedProperty = BindableProperty.Create("IsStriped", typeof(bool), typeof(Repeater), false);
        public static readonly BindableProperty WithSeparatorProperty = BindableProperty.Create("WithSeparator", typeof(bool), typeof(Repeater), false);
        public static readonly BindableProperty StripedColorProperty = BindableProperty.Create("StripedColor", typeof(Color), typeof(Repeater), (Color)App.Current.Resources["StripeColor"]);

        public Repeater()
        {
        }

        public event EventHandler<ItemTappedEventArgs> ItemTapped;

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public IEnumerable<object> ItemSource
        {
            get { return (IEnumerable<object>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public bool IsStriped
        {
            get { return (bool)GetValue(IsStripedProperty); }
            set { SetValue(WithSeparatorProperty, value); }
        }

        public bool WithSeparator
        {
            get { return (bool)GetValue(WithSeparatorProperty); }
            set { SetValue(WithSeparatorProperty, value); }
        }

        public Color StripedColor
        {
            get { return (Color)GetValue(StripedColorProperty); }
            set { SetValue(StripedColorProperty, value); }
        }

        private static void ItemSourceChanging(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != null && oldValue is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)oldValue).CollectionChanged -= ((Repeater)bindable).OnCollectionChanged;
            }

            if (newValue != null && newValue is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)newValue).CollectionChanged += ((Repeater)bindable).OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            Populate();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ItemTemplateProperty.PropertyName || propertyName == ItemSourceProperty.PropertyName)
            {
                this.Populate();
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.Populate();
        }

        public void Populate()
        {
            if (ItemSource != null)
            {
                Children.Clear();
                var source = ItemSource.ToList();

                foreach (var item in source)
                {
                    var content = ItemTemplate.CreateContent();
                    var viewCell = content as ViewCell;


                    if (viewCell != null)
                    {
                        if (IsStriped)
                        {
                            if (source.IndexOf(item) % 2 != 1)
                            {
                                viewCell.View.BackgroundColor = StripedColor;
                            }
                        }

                        Children.Add(viewCell.View);

                        viewCell.View.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(() =>
                            {
                                if (ItemTapped != null)
                                {
                                    ItemTapped.Invoke(viewCell.View, new ItemTappedEventArgs(viewCell.View, item));
                                }
                            })
                        });

                        viewCell.BindingContext = item;

                        if (this.WithSeparator && source.Count > 0 && source.IndexOf(item) != source.Count - 1)
                        {
                            Children.Add(new Separator());
                        }
                    }
                }
            }
        }
    }
}
