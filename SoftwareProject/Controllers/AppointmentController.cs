using SoftwareProject.Models;
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
    public class AppointmentController : Controller
    {
        MeDiagEntities12 db = new MeDiagEntities12();
        // GET: Appointment
        [Authorize]
        public ActionResult AppointmentIndex(int id)
        {
            int findLastIllness = PatientHelper.id;
            var model = new ViewModel2();
            model.doctors = db.Doctor.ToList();
            model.hospitals = db.Hospital.ToList();
            model.departments = db.Department.ToList();
            model.dAppDates = db.DAppDate.ToList();
            model.patients = db.Patient.FirstOrDefault(x => x.Id == id);
            model.illness = db.Illness.OrderByDescending(x => x.Id).FirstOrDefault(x => x.PId == findLastIllness);



            List<Hospital> hospital = db.Hospital.ToList();

            ViewBag.HospitalNames = new SelectList(hospital, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        public ActionResult AppointmentIndex(ViewModel3 viewModel)
        {
            var findPatientId = db.Patient.FirstOrDefault(x => x.Email == viewModel.PatientEmail);
            var findDappId2 = db.DAppDate.FirstOrDefault(x => x.Time == viewModel.DappTime);


            if (ModelState.IsValid && viewModel.AppDate >= DateTime.Now)
            {
                var checkValidAppointment = db.Appointment.FirstOrDefault(x => x.Date == viewModel.AppDate && x.DId == findDappId2.Id);
                var findDoctorId = db.Doctor.FirstOrDefault(x => x.Name + " " + x.Surname == viewModel.DoctorName);
                var findIllnessId = db.Illness.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Name == viewModel.IllnessName && x.PId == findPatientId.Id);
                var findDappId = db.DAppDate.FirstOrDefault(x => x.Time == viewModel.DappTime && x.Doctor_id == findDoctorId.Id);
                if (checkValidAppointment == null && findDappId != null && findDoctorId!=null)
                {
                    Illness ıllness = new Illness();
                    ıllness.Name = viewModel.IllnessName;
                    ıllness.PId = findPatientId.Id;
                    db.Illness.Add(ıllness);
                    db.SaveChanges();


                    Hospital hospital = new Hospital();
                    hospital.Name = viewModel.HospitalName;
              
                    Appointment appointment = new Appointment();
                    appointment.IllId = findIllnessId.Id;
                    appointment.DId = findDappId.Id;
                    appointment.Name = viewModel.IllnessName;
                    appointment.Date = viewModel.AppDate;
                    appointment.PatientId = PatientHelper.id;
                    db.Appointment.Add(appointment);
                    db.SaveChanges();
                    TempData["success"] = "Your appointment is succesfully created!";

                    if (viewModel.PatientEmail.Contains("hotmail"))
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient client = new SmtpClient();
                        client.Credentials = new System.Net.NetworkCredential("mediagnosis@hotmail.com", "caglacopurkaya1999");
                        client.Port = 587;
                        client.Host = "smtp.live.com";
                        client.EnableSsl = true;
                        message.To.Add(viewModel.PatientEmail);
                        message.From = new MailAddress("mediagnosis@hotmail.com");
                        message.Subject = "Appointment";
                        message.Body = "Dear" + " " + viewModel.PatientName + " " + viewModel.PatientSurname + " " + "your appointment has been succesfully set up on " +
                            viewModel.AppDate.ToLongDateString() + " " + "at " + viewModel.HospitalName + " hospital from the department of " + viewModel.DepartmentName + " to " + viewModel.DoctorName + " " +
                            " doctor at " + viewModel.DappTime + ". We wish you a healty day!";
                        client.Send(message);
                    }
                    else if (viewModel.PatientEmail.Contains("gmail"))
                    {
                        var fromAddress = new MailAddress("denemexyzd@gmail.com");
                        var toAddress = new MailAddress(viewModel.PatientEmail);
                        const string fromPassword = "deneme123";
                        const string subject = "Appointment";
                        string body = "Dear" + " " + viewModel.PatientName + " " + viewModel.PatientSurname + " " + "your appointment has been succesfully set up on " +
                            viewModel.AppDate.ToLongDateString() + " " + "at " + viewModel.HospitalName + " hospital from the department of " + viewModel.DepartmentName + " to " + viewModel.DoctorName + " " +
                            " doctor at " + viewModel.DappTime + ". We wish you a healty day!";

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
                    viewModel.PatientId = PatientHelper.id;
                    return RedirectToAction("PastOperations", "Profile", viewModel);
                }
                else
                {
                    TempData["fail"] = "The appointment time may have been taken or the data you selected may be incompatible!";
                    return RedirectToAction("AppointmentIndex", findPatientId.Id);
                }


            }
            else
            {

                TempData["failed"] = "Please fill all blanks or select proper date!";
                return RedirectToAction("AppointmentIndex", findPatientId.Id);
            }


        }
    }
}