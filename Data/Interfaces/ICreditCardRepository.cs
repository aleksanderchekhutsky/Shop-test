using Shop.Data.Models;
namespace Shop.Data.Interfaces
{
    public interface ICreditCardRepository
    {
        void SaveCard(int cardNumber, string expiration, int cvv, string cardName, string userId);
    }
}
