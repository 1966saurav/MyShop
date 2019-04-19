using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.Services;
using MyShop.WebUI.Controllers;
using MyShop.WebUI.Tests.Mock;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            //Setup
            IRepository<Basket> basket = new MockContext<Basket>();
            IRepository<Product> product = new MockContext<Product>();


            var httpContext = new MockHttpContext();

            IBasketService basketService = new BasketService(product, basket);
            var controller = new BasketController(basketService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            // basketService.AddToBasket(httpContext, "1");

            controller.AddToBasket("1");

            Basket baskets = basket.Collection().FirstOrDefault();

            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, baskets.BasketItems.Count);
            Assert.AreEqual("1", baskets.BasketItems.ToList().FirstOrDefault().ProductId);
        }
        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Basket> basket = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();

            products.Insert(new Product() { Id = "1", Price = 300.00m });
            products.Insert(new Product() { Id = "2", Price = 500.00m });

            Basket baskets = new Basket();
            baskets.BasketItems.Add(new BasketItem() { ProductId = "1", Quanity = 2 });
            baskets.BasketItems.Add(new BasketItem() { ProductId = "2", Quanity = 1 });
            basket.Insert(baskets);

            IBasketService basketService = new BasketService(products, basket);

            var controller = new BasketController(basketService);
            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = baskets.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);


            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(1100.00m, basketSummary.BasketTotal);
        }
    }
}
