using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolRegistrationForm.Data;
using SchoolRegistrationForm.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SchoolRegistrationForm.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;


        public RegisterController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("success")]
        public IActionResult Success(string MerchantTransactionId)
        {
            return View();
        }


        [Route("Failed")]
        public IActionResult Failed()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index1(Register register)
        {
            if (ModelState.IsValid)
            {
                _context.Questions.Add(register);
                _context.SaveChanges();
                SendEmail("rachanakafle0@gmail.com","test", $"Hello {register.Name},  " +
                    $"Your have been registerd to itahari talent hunt." +
                    $" Your participate title is {register.ParticipateTitle}");
                //return RedirectToAction("Index");
                DateTime.Now.ToString("yyMMddHHmmssff");

               return Payment("0.01", DateTime.Now.ToString("yyMMddHHmmssff"));
            
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("yash-7@outlook.com", "Rachana");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Yash@671";
                    var sub = subject;
                    var body = message;
                    var smtp = new System.Net.Mail.SmtpClient
                    {
                        Host = "smtp.office365.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }

        public IActionResult Payment(string price, string orderID)
        {
            using (var client = new HttpClient())
            {
                var response = new Apiresponse();
                var endpoint = new Uri(_config["PaymentSetting:Url"]);
                var newPost = new Post()
                {
                    Amount = price,
                    OrderId = orderID,
                    UserName = _config["PaymentSetting:UserName"],
                    Password = _config["PaymentSetting:Password"],
                    MerchantId = _config["PaymentSetting:MerchantId"]
                };
                client.DefaultRequestHeaders.Add("API_KEY", _config["PaymentSetting:API_KEY"]);
                var newpostjson = JsonConvert.SerializeObject(newPost);
                var payload = new StringContent(newpostjson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;

                response = JsonConvert.DeserializeObject<Apiresponse>(result);
                if (response.Message == "Success")
                {
                    return Redirect(response.RedirectURL);
                };

            }
            return Ok();
        }
    }
}