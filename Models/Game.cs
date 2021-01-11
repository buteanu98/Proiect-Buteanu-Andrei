using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Buteanu_Andrei.Models
{
    public class Game
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
    

       
        public ICollection<Order> Orders { get; set; }

    }
}
