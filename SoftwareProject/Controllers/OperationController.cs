using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SoftwareProject.Controllers
{
    public class OperationController : Controller
    {
        // GET: Operation
        MeDiagEntities12 db = new MeDiagEntities12();
        
        [Route("Operation/OperationIndex/{id}/{pid}")]
        public ActionResult OperationIndex(int id,int pid)
        {
            var infoDoctor = db.Doctor.FirstOrDefault(x => x.Id == id);
            var infoPatient = db.Patient.FirstOrDefault(x => x.Id == pid);
            
            var infoIlness = db.Illness.OrderByDescending(x=>x.Id).FirstOrDefault(x=>x.PId == pid);
            
            ViewModel viewModel = new ViewModel();
            viewModel.doctor = infoDoctor;
            viewModel.patient = infoPatient;
            viewModel.ilness = infoIlness;

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult OperationIndex(ViewModel viewModel)
        {
            if (viewModel.community != null) {
                string[] splitCommunity = viewModel.community.Split(' ');
                Op_Table op_Table = new Op_Table();
                op_Table.IllId = viewModel.ilness.Id;
                op_Table.Name = viewModel.ilness.Name;
                db.Op_Table.Add(op_Table);
                db.SaveChanges();

                Community community = new Community();
                community.Name = viewModel.community;
                community.Subject = splitCommunity[0];
                community.IllId = viewModel.ilness.Id;
                db.Community.Add(community);
                db.SaveChanges();
                ViewBag.Success = "Operation saved successfully!";
                if (viewModel.patient.Email.Contains("hotmail"))
                {
                    MailMessage message = new MailMessage();
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential("mediagnosis@hotmail.com", "caglacopurkaya1999");
                    client.Port = 587;
                    client.Host = "smtp.live.com";
                    client.EnableSsl = true;
                    message.To.Add(viewModel.patient.Email);
                    message.From = new MailAddress("mediagnosis@hotmail.com");
                    message.Subject = "Appointment";
                    message.Body = "Dear" + " " + viewModel.patient.Name + " " + viewModel.patient.Surname + " " + "your diagnosis was approved by our doctor and we have registerd you to " +
                       viewModel.community + ". Get well soon! ";
                    client.Send(message);
                }
                else if (viewModel.patient.Email.Contains("gmail"))
                {
                    var fromAddress = new MailAddress("denemexyzd@gmail.com");
                    var toAddress = new MailAddress(viewModel.patient.Email);
                    const string fromPassword = "deneme123";
                    const string subject = "Appointment";
                    string body = "Dear" + " " + viewModel.patient.Name + " " + viewModel.patient.Surname + " " + "your diagnosis was approved by our doctor and we have registerd you to " +
                       viewModel.community + ". Get well soon! ";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
            }
            else
            {
                ViewBag.Fail = "Select a community!";

            }
            
            

          
            

            return View(viewModel);
        }
    }
}