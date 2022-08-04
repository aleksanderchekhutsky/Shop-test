using Shop.Data.Models;
using System.Collections.Generic;

namespace Shop.Data.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
