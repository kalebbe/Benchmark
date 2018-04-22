/*
 * @author:    Kaleb Eberhart
 * @date:      04/20/2018
 * @course:    CST-247
 * @professor: Mark Reha
 * @project:   Benchmark v.1
 * @file:      HomeController.cs
 * @revision:  1
 * @synapsis:  This controller is only used to send the user to the main page.
 */

using System.Web.Mvc;

namespace Benchmark.Controllers
{
    public class HomeController : Controller
    {
        //Redirects the user to the main page
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}