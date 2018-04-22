/*
 * @author:    Kaleb Eberhart
 * @date:      04/20/2018
 * @course:    CST-247
 * @professor: Mark Reha
 * @project:   Benchmark v.1
 * @file:      VerseService.cs
 * @revision:  1
 * @synapsis:  This class calls the database class and returns the user to the controller.
 */

using Benchmark.Models;
using Benchmark.Services.Data;
using Benchmark.Services.Utility;
using System.Collections.Generic;

namespace Benchmark.Services.Business
{
    public class VerseService
    {
        private VerseDAO vd;

        //This constructor just instantiates the VerseDAO object
        public VerseService(VerseModel model)
        {
            //The VerseDAO requires the model, so it's just pushed through here.
            vd = new VerseDAO(model);
        }

        //This method calls the VerseDAO search method and returns the list retrieved.
        public List<string> SearchVerses()
        {
            BenchLogger.getInstance().Info("User invoked SearchVerses method in the VerseService class");
            List<string> verse = vd.SearchVerse();
            if (verse[0] != "No results found!")
            {
                BenchLogger.getInstance().Info("User search returned the verse: " + verse[4] + ". (Note this will fail" +
                    " if the user's search did not yield results).");
            }
            else
                BenchLogger.getInstance().Info("User's search yielded 0 results");
            BenchLogger.getInstance().Info("Leaving SearchVerses method in service");
            return verse;
        }

        //This method calls the insert method from the DAO and returns true or false.
        public bool InsertVerse()
        {
            BenchLogger.getInstance().Info("Invoked InsertVerse method in the verse service");
            if (vd.CheckExisting())
            {
                vd.InsertVerse();
                BenchLogger.getInstance().Info("Leaving InsertVerse method in the verse service. User's verse added.");
                return true;
            }
            else
            {
                BenchLogger.getInstance().Error("Leaving InsertVerse method in the verse service. Verse existent.");
                return false;
            }
        }
    }
}