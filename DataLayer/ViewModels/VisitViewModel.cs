using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
   public class VisitSiteViewModel
    {
        public int VisitID { get; set; }
        public int TodayVisit { get; set; }
        public int YesterdayVisit { get; set; }
        public int SumVisit { get; set; }
        public int OnlineUser { get; set; }
    }
}
