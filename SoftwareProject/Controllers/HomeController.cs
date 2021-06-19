using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftwareProject.Controllers
{    
    public class HomeController : Controller
    {
        MeDiagEntities12 db = new MeDiagEntities12();
        // GET: Home


        public ActionResult Index()
        {
            Patient patient = new Patient();
            patient.Id = PatientHelper.id;

            return View(patient);
        }

    }
        
}
