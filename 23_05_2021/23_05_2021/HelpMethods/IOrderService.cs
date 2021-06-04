using System;
using System.Collections.Generic;
using System.Text;
using test_23_05_2021;
using test_23_05_2021.models;

namespace _23_05_2021.HelpMethods
{
    interface IOrderService
    {
        public int CreateOrUpdateOrder(Order order, Context context);
    }
}
