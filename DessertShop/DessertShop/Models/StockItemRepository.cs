using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public class StockItemRepository: IStockItemRepository
    {
        private readonly AppDbContext _appDbContext;
        public StockItemRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<StockItem> AllPies => throw new NotImplementedException();


        public StockItem GetStockItemById(Guid id)
        {
            return _appDbContext.stockItems.FirstOrDefault(p => p.id == id);
        }

        public StockItem GetStockItemByName(string name)
        {
            return _appDbContext.stockItems.FirstOrDefault(p => p.name == name);
        }
    }
}
