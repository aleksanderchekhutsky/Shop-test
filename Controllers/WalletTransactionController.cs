using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class WalletTransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
