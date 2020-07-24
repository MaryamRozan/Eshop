using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.ViewModels;

namespace MyEshop.Controllers
{
    public class ShopController : Controller
    {
        EshopUnitOfWork db = new EshopUnitOfWork();
        // GET: Shop
        public ActionResult ShowBasket()
        {
            List<DataLayer.ViewModels.ShopCartItemViewModel> list = new List<DataLayer.ViewModels.ShopCartItemViewModel>();

            if (Session["shopCart"] != null)
            {
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
                        Count = item.Count,
                        ProductID = item.ProductID,
                        ImageName = product.ImageName,
                        Title = product.ProductTitle
                    });
                }
            }

            return PartialView(list);
        }

        public ActionResult Index() {
            return View();
        }

        public ActionResult ShowOrder() {
        
            return PartialView(getOrderList());
        }


        public ActionResult CommandOrder(int id, int count) {
            List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;
            int index = listShop.FindIndex(p => p.ProductID == id);
            if (count != 0) {
               
                listShop[index].Count =count;

            }
            else {
                listShop.RemoveAt(index);
            }
            Session["shopCart"] = listShop;
            return PartialView("ShowOrder",getOrderList());
        }

        [Authorize]
        public ActionResult Payment()
        {

            int userid = db.UserRepository.GetByUsername(User.Identity.Name.ToString()).UserID;
            Orders order = new Orders() {
            UserID=userid,
            Date=DateTime.Now,
            IsFinally=false
            };
            db.OrdersRepository.Insert(order);
            List<ShowOrderViewModel> listshop = getOrderList();
            foreach (var item in listshop) {
            OrderDetails orderDetails = new OrderDetails() {
            ProductID=item.ProductID,
            Price=item.Price,
            Count=item.Count,
            OrderID=order.OrderID
            };
                db.OrderDetailsRepository.Insert(orderDetails);
            }
            db.Save();
            return null;
        }

         List<ShowOrderViewModel> getOrderList()
        {
            List<ShowOrderViewModel> list = new List<ShowOrderViewModel>();
            if (Session["shopCart"] != null)
            {
                List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;
                foreach (var item in listShop)
                {
                    var product = db.ProductRepository.GetAll(p => p.ProductID == item.ProductID).Select(p => new { p.ImageName, p.Price, p.ProductTitle }).Single();
                    list.Add(new ShowOrderViewModel()
                    {
                        ProductID = item.ProductID,
                        Count = item.Count,
                        Title = product.ProductTitle,
                        ImageName = product.ImageName,
                        Price = product.Price,
                        Sum = (product.Price) * item.Count
                    });
                }

            }
            return list;
        }



    }
}