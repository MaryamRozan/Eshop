using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyEshop.Controllers
{
    public class ShopCartController : ApiController
    {
        // GET: api/ShopCart
        public int Get()
        {
            List<DataLayer.ViewModels.ShopCartItem> list = new List<DataLayer.ViewModels.ShopCartItem>();
            var sessions = HttpContext.Current.Session;
            if (sessions["shopCart"] != null) {
            list=(List<DataLayer.ViewModels.ShopCartItem>)sessions["shopCart"];
            }
            return list.Sum(l=>l.Count);
        }

        // GET: api/ShopCart/5
        public int Get(int id)
        {
            List<DataLayer.ViewModels.ShopCartItem> list = new List<DataLayer.ViewModels.ShopCartItem>();
            var sessions = HttpContext.Current.Session;
            if (sessions["shopcart"] != null) {
                list = (List<DataLayer.ViewModels.ShopCartItem>)sessions["shopcart"];
            }
            if (list.Any(l => l.ProductID == id))
            {
                int index = list.FindIndex(p => p.ProductID == id);
                list[index].Count += 1;
            }
            else {
                list.Add(new DataLayer.ViewModels.ShopCartItem()
                {
                    Count=1,
                    ProductID=id
                });
            }

            sessions["shopCart"] = list;

            return Get();
        }

    }
}
