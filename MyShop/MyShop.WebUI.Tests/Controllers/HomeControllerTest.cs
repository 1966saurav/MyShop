using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.WebUI;
using MyShop.WebUI.Controllers;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            //    Arrange
            //   HomeController controller = new HomeController();

            //    Act
            //   ViewResult result = controller.Index() as ViewResult;

            //    Assert
            //    Assert.IsNotNull(result);
            //}

            IRepository<Product> productContext = new Mock.MockContext<Product>();
            IRepository<ProductCategory> productCategoryContext = new Mock.MockContext<ProductCategory>();

            productContext.Insert(new Product());

            HomeController controller = new HomeController(productContext , productCategoryContext);

            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Product.Count());
        }
    }
}