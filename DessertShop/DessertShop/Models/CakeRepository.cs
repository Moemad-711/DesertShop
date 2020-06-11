using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public class CakeRepository : ICakeRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStockItemRepository stockItemRepository;

        public CakeRepository(AppDbContext appDbContext, IStockItemRepository stockItemRepository)
        {
            _appDbContext = appDbContext;
            this.stockItemRepository = stockItemRepository;
        }
        public IEnumerable<Cake> CakesOfTheWeek
        {
            get
            {
                return _appDbContext.Cakes.Where(p => p.CakesOfTheWeek);
            }
        }
        public IEnumerable<Cake> AllCakes
        {
            get
            {
                return _appDbContext.Cakes;
            }
        }
        public Cake GetCakeById(Guid CakeId)
        {
            return _appDbContext.Cakes.FirstOrDefault(c => c.CakeId == CakeId);
        }

        public void CreateCake(Cake cake)
        {
            StockItem stockItem = new StockItem()
            {
                id = cake.CakeId,
                name = cake.CakeName,
                Price = cake.Price
            };
            _appDbContext.stockItems.Add(stockItem);
            _appDbContext.Cakes.Add(cake);
            _appDbContext.SaveChanges();
        }
        public void RemoveCake(Cake cake)
        {
            _appDbContext.Cakes.Remove(cake);
            _appDbContext.SaveChanges();
        }
        public void EditCake(Cake the_cake, Cake cake)
        {
            StockItem stockItem = stockItemRepository.GetStockItemByName(the_cake.CakeName);
            stockItem.name = cake.CakeName;
            stockItem.Price = cake.Price;
            the_cake.CakeName = cake.CakeName;
            the_cake.CakePhoto = cake.CakePhoto;
            the_cake.Price = cake.Price;
            the_cake.ShortDescreption = cake.ShortDescreption;
            the_cake.LongDescreption = cake.LongDescreption;
            the_cake.CategoryId = cake.CategoryId;

            _appDbContext.SaveChanges();
        }
        public void MakeCakeOfTheWeek(Cake cake)
        {
            cake.CakesOfTheWeek = true;
            _appDbContext.SaveChanges();
        }
    }
}
