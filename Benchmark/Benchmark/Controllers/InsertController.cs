/*
 * @author:    Kaleb Eberhart
 * @date:      04/20/2018
 * @course:    CST-247
 * @professor: Mark Reha
 * @project:   Benchmark v.1
 * @file:      InsertController.cs
 * @revision:  1
 * @synapsis:  This controller is used to handle the user's bible verse insertion attempts.
 */

using Benchmark.Models;
using Benchmark.Services.Business;
using System.Web.Mvc;

namespace Benchmark.Controllers
{
    public class InsertController : Controller
    {
        //Directs the user to the Insert view
        public ActionResult Index()
        {
            return View("Insert");
        }

        //This method checks the user's input with the model data validation and then calls
        //InsertVerse to put the verse in the database.
        [HttpPost]
        public ActionResult Insert(VerseModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            VerseService vs = new VerseService(model);
            vs.InsertVerse();
            return View();
        }
    }
}