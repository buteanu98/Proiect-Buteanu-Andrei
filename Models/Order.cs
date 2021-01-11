using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Buteanu_Andrei.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int GameID { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public Game Game { get; set; }
    }
}
