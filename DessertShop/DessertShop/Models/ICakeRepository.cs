using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    interface ICakeRepository
    {
        IEnumerable<Cake> AllCakes { get; }
        IEnumerable<Cake> CakesOfTheWeek { get; }
        Cake GetCakeById(Guid CakeId);
        void CreateCake(Cake cake);
        void RemoveCake(Cake cake);
        void EditCake(Cake the_cake, Cake cake);
        void MakeCakeOfTheWeek(Cake cake);
    }
}
