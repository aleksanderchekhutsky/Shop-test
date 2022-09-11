namespace Shop.Data.Interfaces
{
    public interface IPayRepository
    {
        void Pay(string userId, decimal carPrice, string operationType);
        decimal GetUserBalance(string userId);
        decimal GetTotalPrice (string CartId);
    }
}
