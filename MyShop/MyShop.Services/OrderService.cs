using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class OrderServices : IOrderService
    {
        IRepository<Order> orderContext;
        public OrderServices(IRepository<Order> OrderContext)
        {
            this.orderContext = OrderContext;
        }

        public void CreateOrder(Order baseOrder, List<BasketItemViewmodel> basketItem)
        {
            foreach( var item in basketItem)
            {
                baseOrder.OrderItems.Add(new OrderItems()
                {
                   ProductId = item.Id,
                   Image = item.Image,
                   Price = item.Price,
                   ProductName = item.ProductName,
                   Quanity = item.Quanity
                });
            }
            orderContext.Insert(baseOrder);
            orderContext.Commit();
        }
    }
}
