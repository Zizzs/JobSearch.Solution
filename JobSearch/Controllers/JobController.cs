using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using JobSearch.Models;

namespace JobSearch.Controllers
{
    public class JobController : Controller
    {
        [HttpGet("/jobs")]
        public ActionResult Index()
        {
            List<IndeedClass> newList = IndeedClass.RunSearch();
            return View(newList);
        }

        [HttpGet("/jobs/all")]
        public ActionResult All()
        {
            return View();
        }

        [HttpGet("/jobs/stackoverflow")]
        public ActionResult StackOverflow()
        {
            return View();
        }

        [HttpGet("/jobs/indeed")]
        public ActionResult Indeed()
        {
            return View();
        }

    }
}
