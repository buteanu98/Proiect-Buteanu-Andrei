using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proiect_Buteanu_Andrei.Models;

namespace Proiect_Buteanu_Andrei.Data
{
    public class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            context.Database.EnsureCreated();
            if (context.Games.Any())
            {
                return; // BD a fost creata anterior
            }
            var games = new Game[]
            {
 new Game{Title="Metro Exodus",Developer="4A Games",Publisher="Deep Silver",Price=Decimal.Parse("40")},
 new Game{Title="Dark Souls",Developer="FROM Software",Publisher="Bandai Namco",Price=Decimal.Parse("18")},
 new Game{Title="BloodBorne",Developer="FROM Software",Publisher="Sony",Price=Decimal.Parse("27")}
            };
            foreach (Game s in games)
            {
                context.Games.Add(s);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {

 new Customer{CustomerID=1050,Name="Popescu Marcela",BirthDate=DateTime.Parse("1979-09-01")},
 new Customer{CustomerID=1045,Name="Mihailescu Cornel",BirthDate=DateTime.Parse("1969-07-08")},

 };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
                 new Order{GameID=1,CustomerID=1050},
                 new Order{GameID=3,CustomerID=1045},
                 new Order{GameID=1,CustomerID=1045},
                 new Order{GameID=2,CustomerID=1050},
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();
        }
    }
}
  

