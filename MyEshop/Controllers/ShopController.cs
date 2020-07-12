using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Controllers
{
    public class ShopController : Controller
    {
        EshopUnitOfWork db = new EshopUnitOfWork();
        // GET: Shop
        public ActionResult ShowBasket()
        {
            List<DataLayer.ViewModels.ShopCartItemViewModel> list = new List<DataLayer.ViewModels.ShopCartItemViewModel>();
            
            if (Session["shopCart"] != null) {
                List<DataLayer.ViewModels.ShopCartItem> listshop = new List<DataLayer.ViewModels.ShopCartItem>();
                listshop = (List<DataLayer.ViewModels.ShopCartItem>)Session["shopCart"];
                foreach (var item in listshop)
                {
                    var product = db.ProductRepository.GetAll(p => p.ProductID == item.ProductID).Select(p => new
                    {
                        p.ImageName,
                        p.ProductTitle
                    }).Single();
                    list.Add(new DataLayer.ViewModels.ShopCartItemViewModel()
                    {
                        Count=item.Count,
                        ProductID=item.ProductID,
                        ImageName=product.ImageName,
                        Title=product.ProductTitle
                    });
                }
            }
         
            return PartialView(list);
        }
    }
}