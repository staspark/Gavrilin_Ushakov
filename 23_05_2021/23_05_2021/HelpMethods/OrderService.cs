using System;
using System.Collections.Generic;
using System.Text;
using test_23_05_2021;
using test_23_05_2021.models;

namespace _23_05_2021.HelpMethods
{
    internal static class OrderService
    {
        public static int CreateOrUpdateOrder(Order order, Context context)
        {
            context.ChangeTracker.DetectChanges();
            var orderData = context.Orders.Find(order.Id);
            if (orderData == null)
            {
                context.Orders.Add(order);
                return context.SaveChanges();
            }
            else
            {
                //context.Entry(order).CurrentValues.SetValues(order);
                orderData.Books.AddRange(order.Books);
                orderData.Sum += order.Sum;
                return context.SaveChanges();
            }
        }
    }
}
