//using Microsoft.AspNetCore.Mvc;
//using Shop.Controllers;
//using System.IO;
//using System.Net;
//using System.Security.Claims;

//namespace Shop.Controllers
//{
//    public class ApiController : Controller
//    {
//       // [ApiController]
//        [Route("api/[controller]")]

//        public IActionResult Get()
//        {
            
//            return View();
//        }
//        [HttpPost]
//        public IActionResult Deposit(string userId, decimal amount)
//        {
//            return View();
//        }
//        [HttpGet]
//        public IActionResult Deposit(string status)
//        {
//            WebClient client = new WebClient();
//            //WebResponse response = new WebResponse();


//            return Redirect(status);

//        }
//        public IActionResult SendDeposit(string userId, string amount)
//        {
//            //WebClient client = new WebClient();
//            //string adress = "https://localhost:44392/Payments/Deposit";
//            //client.Encoding = System.Text.Encoding.UTF8;
//            //client.UploadString(adress, amount.ToString());
            
//            //client.UploadString(adress, amount.ToString());
//            WebRequest request = WebRequest.Create("https://localhost:44392/Payments/Deposit");
//            string data = amount;
//            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
//            request.Method = "POST";
//            using (Stream dataStream = request.GetRequestStream())
//            {
//                dataStream.Write(byteArray, 0, byteArray.Length);
//            }
//            WebResponse response =  request.GetResponse();
//            using (Stream stream = response.GetResponseStream())
//            {
//                using (StreamReader reader = new StreamReader(stream))
//                {
//                    //Console.WriteLine(reader.ReadToEnd());
//                }
//            }
//            response.Close();

//            return Redirect("https://localhost:44392/Payments/Deposit");
//        }
        
//    }/// controller payments, action deposit, form, cancel; deposit , exept,  redirect 

//}
