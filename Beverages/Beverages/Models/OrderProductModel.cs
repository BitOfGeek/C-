using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Beverages.Models
{
    class OrderProductModel 
    {

        private IList<BeverageAddonModel> addons = new ObservableCollection<BeverageAddonModel>();

        public string Name { get; set; }
        public double Price { get; set; }

        public IList<BeverageAddonModel> AddonsToBeverage
        {
            get
            {
                return addons;
            }
            set
            {
                addons = value;
            }
        }
    }
}
