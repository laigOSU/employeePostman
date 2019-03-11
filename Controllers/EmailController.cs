using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using employeeRecognition.Extensions;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.Extensions.DependencyInjection;
using System.Text;


// https://www.youtube.com/watch?v=Y2X5wtuzuX4
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// https://www.youtube.com/watch?v=lnRBShlB9hA
// 1. instantiate mimemessage class
// 2. From address
// 3. To address
// 4. Subject
// 5. Body
// https://www.excitoninteractive.com/articles/read/69/asp-net-core2/sending-email-using-mailkit



// https://dotnetcoretutorials.com/2017/11/02/using-mailkit-send-receive-email-asp-net-core/
namespace employeeRecognition.Controllers
{
    public class EmailController : Controller
    {
        private Encoding mail;

        public IActionResult Index()
       {
            return View();
       }
       
       public IActionResult About()
       {
            ViewData["Message"] = "About Page";
            return View();
       }

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page";
            return View();
        }

        public IActionResult SendMail(string name, string email, string msg)
        {
            var message = new MimeMessage();

            // Specify sender email
            message.From.Add(new MailboxAddress("mail@employeerecognition.com"));

            // Specify recipient email
            message.To.Add(new MailboxAddress("laig@oregonstate.edu")); // replace recipient with {User.email}

            // Subject
            message.Subject = name;

            // Body, will be formatted in HTML format
            message.Body = new TextPart("html")
            {
                Text = "From: " + name + "<br>" +
                "Contact information " + email + "<br>" +
                "Message: " + msg
            };
            // Specify info about the service, how we're going to connect to it
              using (var client = new SmtpClient()) {
                client.Connect("asmtp.employeerecognition.com", 587);
                client.Authenticate("mail@employeerecognition.com", "errai");
                client.Send(message);
                client.Disconnect(false);
              }

              //When we've sent the mail, then return back to the contact page, or whatever
            return View("Contact");
        }

    }
}

