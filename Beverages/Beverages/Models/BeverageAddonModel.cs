using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beverages.Models
{
    class BeverageAddonModel
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public OrderProductModel Parent { get; set; }
    }
}
