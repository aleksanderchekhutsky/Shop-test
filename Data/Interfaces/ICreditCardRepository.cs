using Shop.Data.Models;
namespace Shop.Data.Interfaces
{
    public interface ICreditCardRepository
    {
        void SaveCard(string cardNumber, string expiration, int cvv, string cardName, string userId);
    }
}
