using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftwareProject.Controllers
{
    public class PredictionController : Controller
    {
        MeDiagEntities12 db = new MeDiagEntities12();
        // GET: Prediction
        [Authorize]
        public ActionResult Index(int id)
        {
            var findPatient = db.Patient.FirstOrDefault(x => x.Id == id);

            return View(findPatient);
        }
        [HttpPost]
        public ActionResult Index(string variable,int id)
        {
            string[] variableTrimmed = variable.Split(' ');
            Illness ıllness = new Illness();
            ıllness.Name = variableTrimmed[0];
            ıllness.PId = id;
            db.Illness.Add(ıllness);
            db.SaveChanges();
            var getPid = ıllness.Id;
            TempData["getPID"] = getPid;
            return Json(Url.Action("AppointmentIndex", "Appointment", new { @id = id }));
        }

       

    }
}