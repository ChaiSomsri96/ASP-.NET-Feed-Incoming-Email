using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GMailFeed.Models;
using ImapX;

namespace GMailFeed.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            ImapClient imapClient = new ImapClient("imap.gmail.com", 993, true);
            if (imapClient.Connect())
            {
                if (imapClient.Login("user@gmail.com", "Password"))
                {
                    var inboxMails = imapClient.Folders.Inbox.Search("UNSEEN", ImapX.Enums.MessageFetchMode.Tiny, 10); //Forcing to only get 10 messages

                    foreach (var inboxMail in inboxMails)
                    {
                        customers.Add(new CustomerModel
                        {
                            MessageContent = inboxMail.Body.Html,
                            Subject = inboxMail.Subject,
                            From = inboxMail.From.DisplayName
                        });
                        inboxMail.Seen = true;
                    }
                }
            }
            ViewBag.Customers = customers.ToArray();
            return View();
        }

        public IActionResult Privacy()
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
