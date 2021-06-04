using System;
using System.Collections.Generic;
using System.Text;

namespace test_23_05_2021.models
{
    class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
