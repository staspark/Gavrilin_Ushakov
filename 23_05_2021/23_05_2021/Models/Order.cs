using System;
using System.Collections.Generic;
using System.Text;

namespace test_23_05_2021.models
{
    class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public decimal Sum { get; set; }
        public List<Book> Books { get; set; }
    }
}
