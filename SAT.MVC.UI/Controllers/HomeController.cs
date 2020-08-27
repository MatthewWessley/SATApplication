using SAT.MVC.UI.Models;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace SAT.MVC.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel usersContactRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(usersContactRequest);
            }

            MailMessage msg = new MailMessage("no-reply@example.com", "email@example.com", usersContactRequest.Subject, usersContactRequest.Message);

            msg.CC.Add("yourccemail@gmail.com");

            SmtpClient client = new SmtpClient("mail.example.com");
            client.Credentials = new NetworkCredential("no-reply@example.com", "yourpasswordhere");

        

            client.Port = 8889;

            try
            {
                client.Send(msg);
            }
            catch (System.Exception ex)
            {

                ViewBag.ErrorMessage = "There was a problem. Please try at a later time. <br/>" + ex.StackTrace;

                return View(usersContactRequest);
            }

            return View("EmailConfirmation", usersContactRequest);

        }
    }
}
