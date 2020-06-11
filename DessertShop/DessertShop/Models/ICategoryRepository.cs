using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        public void CreateCategory(Category category);
        public void RemoveCategory(Category category);
    }
}
