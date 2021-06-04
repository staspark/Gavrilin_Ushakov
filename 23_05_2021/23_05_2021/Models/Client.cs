using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace test_23_05_2021.models
{
    class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //1a attributes(annotantion)
        [MinLength(7)] 
        public string Email { get; set; }
        public List<Order> Orders { get; set; }
    }
}
