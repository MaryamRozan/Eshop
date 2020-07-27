using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class CompareItem {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
    }

    public class CompareListViewModel {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string[] Features { get; set; }
        public string[] ProductFeatures { get; set; }

    }
}
