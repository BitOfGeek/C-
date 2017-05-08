using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Beverages.Behaviors
{
    class ListBoxScrollBehavior
    {
        static readonly Dictionary<ListBox, Capture> Associations = new Dictionary<ListBox, Capture>();

        public static bool GetScrollOnNewItem(DependencyObject dpo)
        {
            return (bool)dpo.GetValue(ScrollOnNewItemProperty);
        }

        public static void SetScrollOnNewItem(DependencyObject dpo, bool value)
        {
            dpo.SetValue(ScrollOnNewItemProperty, value);
        }

        public static readonly DependencyProperty ScrollOnNewItemProperty =
            DependencyProperty.RegisterAttached(
                "ScrollOnNewItem",
                typeof(bool),
                typeof(ListBoxScrollBehavior),
                new UIPropertyMetadata(false, OnScrollOnNewItemChanged));

        public static void OnScrollOnNewItemChanged(
            DependencyObject dpo,
            DependencyPropertyChangedEventArgs dpoEvent)
        {
            var listBox = dpo as ListBox;
            bool oldValue = (bool)dpoEvent.OldValue, newValue = (bool)dpoEvent.NewValue;

            if (newValue == oldValue || listBox == null)
            {
                return;
            }

            if (newValue)
            {
                listBox.Loaded += ListBox_Loaded;
                listBox.Unloaded += ListBox_Unloaded;

                var itemsSourcePropertyDescriptor = 
                    TypeDescriptor.GetProperties(listBox)["ItemsSource"];
                itemsSourcePropertyDescriptor.AddValueChanged(listBox, ListBox_ItemsSourceChanged);
            }
            else
            {
                listBox.Loaded -= ListBox_Loaded;
                listBox.Unloaded -= ListBox_Unloaded;

                if (Associations.ContainsKey(listBox))
                {
                    Associations[listBox].Dispose();
                }
                   
                var itemsSourcePropertyDescriptor = 
                    TypeDescriptor.GetProperties(listBox)["ItemsSource"];
                itemsSourcePropertyDescriptor.RemoveValueChanged(listBox, ListBox_ItemsSourceChanged);
            }
        }

        private static void ListBox_ItemsSourceChanged(object sender, EventArgs e)
        {
            var listBox = (ListBox)sender;

            if (Associations.ContainsKey(listBox))
            {
                Associations[listBox].Dispose();
            }
                
            Associations[listBox] = new Capture(listBox);
        }

        static void ListBox_Unloaded(object sender, RoutedEventArgs e)
        {
            var listBox = (ListBox)sender;

            if (Associations.ContainsKey(listBox))
            {
                Associations[listBox].Dispose();
            }
                
            listBox.Unloaded -= ListBox_Unloaded;
        }

        static void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            var listBox = (ListBox)sender;
            var incc = listBox.Items as INotifyCollectionChanged;

            if (incc == null)
            {
                return;
            }

            listBox.Loaded -= ListBox_Loaded;
            Associations[listBox] = new Capture(listBox);
        }

        class Capture : IDisposable
        {
            private readonly ListBox listBox;
            private readonly INotifyCollectionChanged lbItemSource;

            public Capture(ListBox listBox)
            {
                this.listBox = listBox;
                lbItemSource = listBox.ItemsSource as INotifyCollectionChanged;

                if (lbItemSource != null)
                {
                    lbItemSource.CollectionChanged += CollectionChanged;
                }
            }

            void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    listBox.ScrollIntoView(e.NewItems[0]);
                    
                }
            }

            public void Dispose()
            {
                if (lbItemSource != null)
                {
                    lbItemSource.CollectionChanged -= CollectionChanged;
                }                   
            }

        }

    }
}
