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
           var trans =  TransactionStatusCheck(MerchantTransactionId);
           var regDetail = _context.Questions.FirstOrDefault(x => x.TransactionId == MerchantTransactionId);
            var status = "Pending";


            if (trans.Status == 1)
            {
                status = "Success";
            }
            else if (trans.Status == 2)
            {
                status = "Failed";
            }
            else if (trans.Status == 3)
            {
                status = "Cancelled";
            }
            else if (trans.Status == 4)
            {
                status = "Pending";
            }
            else
            {
                status = "Incomplete";
            }

            //var reg = new Register
            //{
            //    Name = regDetail.Name,
            //    Address = regDetail.Address,
            //    Email = regDetail.Email,
            //    Institution = regDetail.Institution,
            //    EducationLevel = regDetail.EducationLevel,
            //    Gender = regDetail.Gender,
            //    ParticipateTitle = regDetail.ParticipateTitle,
            //    OrderId = regDetail.OrderId,
            //    TransactionId = regDetail.TransactionId,
            //    Status = status,
            //};
            regDetail.Status = status;
           
          
            //_context.Questions.Add(reg);
            _context.SaveChanges();
            SendEmail(regDetail.Email, "test", $"Hello {regDetail.Name}, Congratulations!!! " +
                    $"Your have been registerd to Itahari Talent Hunt." +
                    $" Your participate title is {regDetail.ParticipateTitle}");
            //return RedirectToAction("Index");
            return View();
        }


        [Route("Failed")]
        public IActionResult Failed(string MerchantTransactionId)
        {
            var trans = TransactionStatusCheck(MerchantTransactionId);
            var regDetail = _context.Questions.FirstOrDefault(x => x.TransactionId == MerchantTransactionId);
            var status = "Pending";

            if (trans.Status == 1)
            {
                status = "Success";
            }
            else if (trans.Status == 2)
            {
                status = "Failed";
            }
            else if (trans.Status == 3)
            {
                status = "Cancelled";
            }
            else if (trans.Status == 4)
            {
                status = "Pending";
            }
            else
            {
                status = "Incomplete";
            }
            regDetail.Status = status;
            _context.SaveChanges();
            return View();
        }

        [HttpPost]
        public IActionResult Index1(Register register)
        {
            if (ModelState.IsValid)
            {
               var orderId =  DateTime.Now.ToString("yyMMddHHmmssffff");
                var pay = Payment("0.01", orderId);
                if(pay != null)
                {
                    var reg = new Register
                    {
                        Name = register.Name,
                        Address = register.Address,
                        Email = register.Email,
                        Institution = register.Institution,
                        EducationLevel = register.EducationLevel,
                        Gender = register.Gender,
                        ParticipateTitle = register.ParticipateTitle,
                        OrderId = orderId,
                        TransactionId = pay.MerchantTransactionId,
                        Status = "Pending"
                    };

                    _context.Questions.Add(reg);
                    _context.SaveChanges();
                    return Redirect(pay.RedirectURL);
                }
           

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

        public Apiresponse Payment(string price, string orderID)
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
                //if (response.Message == "Success")
                //{
                //    return Redirect(response.RedirectURL);
                //};
                if (response != null)
                {
                    return response;
                }
                return null;
            }
           
        }

        public ApiTrxresponse? TransactionStatusCheck(string TransectionId)
        {
            using (var client = new HttpClient())
            {
                var response = new ApiTrxresponse();
                var endpoint = new Uri(_config["PaymentSetting:UrlCheck"]);
                var trxID = new TransactionID()
                {
                    MerchantTransactionId = TransectionId,
                };


                client.DefaultRequestHeaders.Add("API_KEY", _config["PaymentSetting:API_KEY"]);
                var newpostjson = JsonConvert.SerializeObject(trxID);
                var payload = new StringContent(newpostjson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;

                response = JsonConvert.DeserializeObject<ApiTrxresponse>(result);

                if (response != null) return response;

                return null;

            }

        }
    } 
}