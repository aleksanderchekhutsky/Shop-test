using Shop.Data.Models;
namespace Shop.Data.Interfaces
{
    public interface IOrdersRepository
    {
        void CreateOrder(Order order);
    }
}
