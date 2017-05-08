using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using System.Xml.Serialization;
using Beverages.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;

namespace Beverages.ViewModel
{
    class ProductViewModel : ViewModelBase
    {
        #region Paths
        private readonly string BeveragesPath=@"beverages.xml";
        private readonly string EN_Path = @"Beverages\Beverages\Properties\Resources.en-US.resx";
        private readonly string BG_Path = @"Beverages\Beverages\Properties\Resources.bg-BG.resx";
        #endregion

        #region PrivateVariables
        private XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ProductModel>));
        private ObservableCollection<ProductModel> beverages = new ObservableCollection<ProductModel>();
        private ProductModel selectedProduct = null;
        private XDocument xdoc;
        private string emptyString = "";
        #endregion

        public ProductViewModel()
        {
            LoadBeverages();
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

        public ProductModel SelectedProduct
        {
            get
            {
                return selectedProduct;
            }
            set
            {
                selectedProduct = value;                
                RaisePropertyChanged("SelectedProduct");
            }
        }
        #endregion 

        #region ICommands
        public ICommand AddNewProductCommand
        {
            get
            {
                return new RelayCommand<Object[]>(AddProduct, CanExecuteAdd);
            }
        }

        public ICommand DeleteSelectedProductCommand
        {
            get
            {
                return new RelayCommand(DeleteSelectedProduct, CanExecuteDelete);
            }
        }

        public ICommand ClearSelectedProductCommand
        {
            get
            {
                return new RelayCommand(ClearSelectedProduct, CanExecuteDelete);
            }
        }

        public ICommand SaveSelectedProductCommand
        {
            get
            {
                return new RelayCommand<object[]>(SaveSelectedProduct, CanExecuteSave);
            }
        }
        #endregion

        #region CanExecute
        private bool CanExecuteDelete()
        {
            return (SelectedProduct != null);
        }

        private bool CanExecuteAdd(object[] toAdd)
        {           
            return ((string)toAdd[0]!= emptyString) && 
                ((string)toAdd[1] != emptyString) && 
                ((string)toAdd[2] != emptyString);
        }

        private bool CanExecuteSave(object[] toAdd)
        {
                return ((string)toAdd[0] != emptyString) && 
                    ((string)toAdd[1] != emptyString) && 
                    ((string)toAdd[2] != emptyString);
        }
        #endregion

        #region PrivateMethods
        private void LoadBeverages()
        {
            using (TextReader reader =
                new StreamReader(BeveragesPath))
            {
                Beverages = (ObservableCollection<ProductModel>)serializer.Deserialize(reader);
            }
        }

        private void AddProduct(Object[] value)
        {
            if (!Beverages.Any(x => x.Name == value[0].ToString()))
            {
                xdoc = XDocument.Load(BeveragesPath);
                XElement root = new XElement("ProductModel");
                root.Add(new XElement("Name", value[0]));
                root.Add(new XElement("Price", double.Parse(value[2].ToString())));
                xdoc.Element("ArrayOfProductModel").Add(root);
                xdoc.Save(BeveragesPath);

                xdoc = XDocument.Load(EN_Path);
                root = new XElement("data", new XAttribute("name", value[0]));
                root.Add(new XElement("value", value[0]));
                xdoc.Element("root").Add(root);
                xdoc.Save(EN_Path);

                xdoc = XDocument.Load(BG_Path);
                root = new XElement("data", new XAttribute("name", value[0]));
                root.Add(new XElement("value", value[1]));
                xdoc.Element("root").Add(root);
                xdoc.Save(BG_Path);

                //ProductModel newProduct = new ProductModel();
                //newProduct.Name = value[0].ToString();
                //newProduct.Price = double.Parse(value[2].ToString());
                //Beverages.Add(newProduct);

                //Messenger.Default.Send<ObservableCollection<ProductModel>>(Beverages);
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("close"));
            }
            else
            {
                MessageBox.Show("Such product already exists");
            }
           
        }

        private void DeleteSelectedProduct()
        {
            xdoc = XDocument.Load(BeveragesPath);
            xdoc.Element("ArrayOfProductModel")
                 .Elements("ProductModel")
                 .Where(e => e.Element("Name").Value == SelectedProduct.Name.ToString()).Remove();

            xdoc.Save(BeveragesPath);

            xdoc = XDocument.Load(EN_Path);
            xdoc.Element("root")
                 .Elements("data")
                 .Where(e => e.Attribute("name").Value == SelectedProduct.Name.ToString()).Remove();
            xdoc.Save(EN_Path);

            xdoc = XDocument.Load(BG_Path);
            xdoc.Element("root")
                 .Elements("data")
                 .Where(e => e.Attribute("name").Value == SelectedProduct.Name.ToString()).Remove();
            xdoc.Save(BG_Path);

            Beverages.Remove(SelectedProduct);

            Messenger.Default.Send<ObservableCollection<ProductModel>>(Beverages);
        }

        private void ClearSelectedProduct()
        {
            SelectedProduct = null;
        }

        private void SaveSelectedProduct(object[] newValues)
        {
            xdoc = XDocument.Load(BeveragesPath);
            xdoc.Element("ArrayOfProductModel")
                 .Elements("ProductModel")
                 .Where(e => 
                     e.Element("Name").Value == 
                     SelectedProduct.Name.ToString()).Single().SetElementValue("Price",double.Parse(newValues[2].ToString()));
            xdoc.Element("ArrayOfProductModel")
                 .Elements("ProductModel")
                 .Where(e => 
                     e.Element("Name").Value == 
                     SelectedProduct.Name.ToString()).Single().SetElementValue("Name", newValues[0]);
            xdoc.Save(BeveragesPath);

            xdoc = XDocument.Load(EN_Path);
            xdoc.Element("root")
                 .Elements("data")
                 .Where(e => 
                     e.Attribute("name").Value == 
                     SelectedProduct.Name.ToString()).Single().SetElementValue("value", newValues[0]);
            xdoc.Element("root")
                 .Elements("data")
                 .Where(e => 
                     e.Attribute("name").Value == 
                     SelectedProduct.Name.ToString()).Single().SetAttributeValue("name", newValues[0]);
            xdoc.Save(EN_Path);

            xdoc = XDocument.Load(BG_Path);
            xdoc.Element("root")
                 .Elements("data")
                 .Where(e => 
                     e.Attribute("name").Value == 
                     SelectedProduct.Name.ToString()).Single().SetElementValue("value", newValues[1]);
            xdoc.Element("root")
                 .Elements("data")
                 .Where(e => 
                     e.Attribute("name").Value == 
                     SelectedProduct.Name.ToString()).Single().SetAttributeValue("name", newValues[0]);
            xdoc.Save(BG_Path);

            //Beverages.Where(x => 
            //    x.Name == SelectedProduct.Name).Single().Price = double.Parse(newValues[2].ToString());
            //Beverages.Where(x => x.Name == SelectedProduct.Name).Single().Name = newValues[0].ToString();
            
            //Messenger.Default.Send<ObservableCollection<ProductModel>>(Beverages);

            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("close"));
        }
        #endregion
    }
}
