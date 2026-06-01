using FreerunZufuss.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace FreerunZufuss.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> SendMessage(string Name, string Email, string Message)
        {
            try
            {
                var fromAddress = new MailAddress("hael1977@seznam.cz");
                var toAddress = new MailAddress("hael1977@seznam.cz");

                const string fromPassword = "  ";

                var smtp = new SmtpClient("smtp.seznam.cz", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using var mail = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Zpráva z webu FreerunZufuss",
                    Body = $"Jméno: {Name}\nEmail: {Email}\n\nZpráva:\n{Message}"
                };

                await smtp.SendMailAsync(mail);

                return RedirectToAction("Contact");
            }
            catch (Exception ex)
            {
                return Content("Mail error: " + ex.Message);
            }
        }


        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Contribution()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
