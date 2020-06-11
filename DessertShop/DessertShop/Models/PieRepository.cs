using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStockItemRepository stockItemRepository;

        public PieRepository(AppDbContext appDbContext, IStockItemRepository stockItemRepository)
        {
            _appDbContext = appDbContext;
            this.stockItemRepository = stockItemRepository;
        }
        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _appDbContext.Pies.Where(p => p.PiesOfTheWeek);
            }
        }
        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _appDbContext.Pies;
            }
        }
        public Pie GetPieById(Guid pieId)
        {
            return _appDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }

        public void CreatePie(Pie pie)
        {
            StockItem stockItem = new StockItem()
            {
                id = pie.PieId,
                name = pie.PieName,
                Price = pie.Price
            };
            _appDbContext.stockItems.Add(stockItem);
            _appDbContext.Pies.Add(pie);
            _appDbContext.SaveChanges();
        }
        public void RemovePie(Pie pie)
        {
            _appDbContext.Pies.Remove(pie);
            _appDbContext.SaveChanges();
        }
        public void EditPie(Pie the_pie, Pie pie)
        {
            StockItem stockItem = stockItemRepository.GetStockItemByName(the_pie.PieName);
            stockItem.name = pie.PieName;
            stockItem.Price = pie.Price;
            the_pie.PieName = pie.PieName;
            the_pie.PiePhoto = pie.PiePhoto;
            the_pie.Price = pie.Price;
            the_pie.ShortDescreption = pie.ShortDescreption;
            the_pie.LongDescreption = pie.LongDescreption;
            the_pie.CategoryId = pie.CategoryId;

            _appDbContext.SaveChanges();
        }
        public void MakePieOfTheWeek(Pie pie)
        {
            pie.PiesOfTheWeek = true;
            _appDbContext.SaveChanges();
        }
    }
}
