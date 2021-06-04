using System;
using System.Collections.Generic;
using System.Text;

namespace test_23_05_2021.models
{
    class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Author> Authors { get; set; }
        public string  Mark2 { get; set; }
    }
}
