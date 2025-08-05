using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebSıtesı.Models;

namespace WebSıtesı.Controllers
{
    public class EmailController : Controller
    {
        //private readonly EmailService _emailService;

        //public EmailController()
        //{
        //    _emailService = new EmailService();
        //}

        [HttpGet]
        public ActionResult Contact()
        {
            return View("~/Pages/Contact.cshtml");
        }
        [HttpPost]
        public IActionResult Contact(string name, string userEmail, string subject, string message)
        {
            // Önceki gönderilen bilgileri al
            string? previousEmail = HttpContext.Session.GetString("PreviousEmail");
            string? previousMessage = HttpContext.Session.GetString("PreviousMessage");

            // Eğer aynı kullanıcı aynı mesajı tekrar yollamışsa engelle
            if (previousEmail == userEmail && previousMessage == message)
            {
                ViewBag.Message = "Bu mesajı zaten gönderdiniz. Lütfen farklı bir mesaj deneyin.";
                return View("~/Views/Pages/Contact.cshtml");
            }

            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential("kecelerimizikacirdik@gmail.com", "vzqrbmhauqiuqftv")
                };

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("kecelerimizikacirdik@gmail.com", "İletişim Formu");
                mailMessage.To.Add("kecelerimizikacirdik@gmail.com");

                mailMessage.Subject = subject;
                mailMessage.Body =
                    $"Gönderen Adı: {name}\n" +
                    $"Gönderen E-posta: {userEmail}\n\n" +
                    $"Mesaj:\n{message}";

                mailMessage.ReplyToList.Add(new MailAddress(userEmail));
                client.Send(mailMessage);

                // Bu bilgileri Session'a kaydet
                HttpContext.Session.SetString("PreviousEmail", userEmail);
                HttpContext.Session.SetString("PreviousMessage", message);

                ViewBag.Message = "Mesajınız başarıyla gönderildi!";
                return View("~/Views/Pages/Contact.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Bir hata oluştu: " + ex.Message;
                return View();
            }
        }
    }
}