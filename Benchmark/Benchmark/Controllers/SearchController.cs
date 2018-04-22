/*
 * @author:    Kaleb Eberhart
 * @date:      04/20/2018
 * @course:    CST-247
 * @professor: Mark Reha
 * @project:   Benchmark v.1
 * @file:      SearchController.cs
 * @revision:  1
 * @synapsis:  This controller handles the traffic for the user's search attempts.
 */

using Benchmark.Models;
using Benchmark.Services.Business;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Benchmark.Controllers
{
    public class SearchController : Controller
    {
        //This method sends the user to the Search view
        public ActionResult Index()
        {
            return View("Search");
        }

        //This method handles the user's form data and calls the VerseService to search
        //for a verse.
        [HttpPost]
        public ActionResult Search(VerseModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Search");
            }
            VerseService vs = new VerseService(model);
            List<string> text = vs.SearchVerses();
            return View("SearchResults", text);
        }
    }
}