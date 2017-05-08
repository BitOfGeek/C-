using Beverages.Mediator;
using Beverages.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows.Input;
using System.Xml.Serialization;
using WPFLocalizeExtension.Engine;
using System.Linq;
using System.Windows;
using Beverages.Properties;

namespace Beverages.ViewModel
{
    class BeveragesViewModel : ViewModelBase
    {
        #region Paths
        private readonly string BeveragesPath = @"C:\Users\Yo-Yo\Documents\Visual Studio 2013\Projects\Beverages\Beverages\beverages.xml";
        private readonly string AddonsPath = @"C:\Users\Yo-Yo\Documents\Visual Studio 2013\Projects\Beverages\Beverages\addons.xml";
        #endregion

        #region PrivateVariables
        private MessageMediator mediator = new MessageMediator();
        private XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ProductModel>));
        private ObservableCollection<OrderProductModel> orders = new ObservableCollection<OrderProductModel>();
        private ObservableCollection<ProductModel> beverages = new ObservableCollection<ProductModel>();
        private ObservableCollection<ProductModel> addons = new ObservableCollection<ProductModel>();
        private OrderProductModel selectedOrder = null;
        private BeverageAddonModel selectedAddon = null;
        private double total = 0;
        #endregion

        public BeveragesViewModel()
        {
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            ChangeLanguage("bg-BG");            
  
            using (TextReader reader = 
                new StreamReader(BeveragesPath))
            {
                Beverages =(ObservableCollection<ProductModel>)serializer.Deserialize(reader);
            }

            using (TextReader reader =
                new StreamReader(AddonsPath))
            {
                Addons = (ObservableCollection<ProductModel>)serializer.Deserialize(reader);
            }

            Messenger.Default.Register<ObservableCollection<ProductModel>>(this, RemoveProduct);
        }

        #region Properties
        public ObservableCollection<ProductModel> Beverages 
        {
            get
            {
                return beverages;
            }
            set
            {
                beverages = value;
                RaisePropertyChanged("Beverages");
            } 
        }

        public ObservableCollection<ProductModel> Addons
        {
            get
            {
                return addons;
            }
            set
            {
                addons = value;
                RaisePropertyChanged("Addons");
            }
        }

        public ObservableCollection<OrderProductModel> Orders
        {
            get
            {
                return orders;
            }
            set
            {
                orders = value;
                RaisePropertyChanged("Orders");
            }
        }

        public double Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
                RaisePropertyChanged("Total");
            }
        }

        public OrderProductModel SelectedOrder
        {
            get
            {
                return selectedOrder;
            }
            set
            {
                selectedOrder = value;
                RaisePropertyChanged("SelectedOrder");

                if (SelectedAddon != null && selectedOrder != null)
                {
                    SelectedAddon = null;
                }
            }
        }

        public BeverageAddonModel SelectedAddon
        {
            get
            {
                return selectedAddon;
            }
            set
            {
                selectedAddon = value;
                RaisePropertyChanged("SelectedAddon");

                if (SelectedOrder != null && selectedAddon != null)
                {
                    //TODO: find parent value

                    SelectedOrder = null;
                }
            }
        }
        #endregion

        #region ICommands
        public ICommand ChangeLanguageCommand
        {
            get
            {
                return new RelayCommand<string>(ChangeLanguage);
            }
        }

        public ICommand EditProductsCommand
        {
            get
            {
                return new RelayCommand(EditProduct);
            }
        }

        public ICommand ChosenProductCommand
        {
            get
            {
                return new RelayCommand<ProductModel>(ChosenProduct);
            }
        }

        public ICommand DeleteSelectedItemCommand
        {
            get
            {
                return new RelayCommand(DeleteOrder, CanExecuteDelete);
            }
        }

        public ICommand DeleteAllCommand
        {
            get
            {
                return new RelayCommand(DeleteAll, CanExecuteDeleteAll);
            }
        }

        public ICommand FinalizeCommand
        {
            get
            {
                return new RelayCommand(DeleteAll, CanExecuteDeleteAll);
            }
        }
        #endregion

        #region CanExecute
        private bool CanExecuteDelete()
        {
            return (selectedOrder != null || selectedAddon != null);
        }

        private bool CanExecuteDeleteAll()
        {
            return orders.Count != 0;
        }
        #endregion

        #region PrivateMethods
        private void ReLoadItems()
        {            
            RaisePropertyChanged("Total");

            ObservableCollection<ProductModel> tempProduct = Beverages;
            Beverages = null;
            Beverages = tempProduct;
            tempProduct = Addons;
            Addons = null;
            Addons = tempProduct;

            ObservableCollection<OrderProductModel> tempOrders = Orders;
            Orders = null;
            Orders = tempOrders;
        }

        private void EditProduct()
        {
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("activate"));
        }

        private void RemoveProduct(ObservableCollection<ProductModel> newBeverages)
        {
            Beverages = newBeverages;
        }

        private void ChangeLanguage(string language)
        {
            LocalizeDictionary.Instance.Culture = new CultureInfo(language);      
            ReLoadItems();
        }

        private void ChosenProduct(ProductModel product)
        {
            bool contains = false;

            if (selectedOrder != null)
            {
                contains = Beverages.Any(p => p.Name == selectedOrder.Name);
            }

            if (contains && Addons.Contains(product))
            {
                selectedOrder.AddonsToBeverage.Add(new BeverageAddonModel
                {
                    Name = product.Name,
                    Price = product.Price,
                    Parent = SelectedOrder
                });
            }
            else
            {
                orders.Add(new OrderProductModel { Name = product.Name, Price = product.Price });
                SelectedOrder = orders.Last();
            }

            Total += product.Price;
        }

        private void DeleteOrder()
        {

            if (selectedAddon != null)
            {
                Total -= selectedAddon.Price;

                selectedOrder = selectedAddon.Parent;
                selectedOrder.AddonsToBeverage.Remove(selectedAddon);
                selectedOrder = null;
            }

            if (selectedOrder != null)
            {
                double addonsTotalPrice = 0;

                foreach (var price in selectedOrder.AddonsToBeverage)
                {
                    addonsTotalPrice += price.Price;
                }

                Total -= (selectedOrder.Price + addonsTotalPrice);

                orders.Remove(selectedOrder);
            }
        }

        private void DeleteAll()
        {
            orders.Clear();
            Total = 0;
        }

        #endregion

    }
}