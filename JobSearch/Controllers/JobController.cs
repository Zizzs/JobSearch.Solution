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
            return View();
        }

        [HttpGet("/jobs/all")]
        public ActionResult All()
        {
            List<IndeedClass> model = new List<IndeedClass>();
            return View(model);
        }

        [HttpGet("/jobs/stackoverflow")]
        public ActionResult ShowStackOverflow()
        {
            List<StackOverflow> model = new List<StackOverflow> { };
            return View("StackOverflow", model);
        }

        [HttpPost("/jobs/stackoverflow")]
        public ActionResult StackOverflowSearch(string jobName, string jobLocation)
        {
            List<StackOverflow> model = StackOverflow.RunSearch(jobName, jobLocation);
            return View("StackOverflow", model);
        }
        [HttpGet("/jobs/indeed")]
        public ActionResult Indeed()
        {
            List<IndeedClass> model = new List<IndeedClass>();
            return View(model);
        }

        [HttpPost("/jobs/indeed")]
        public ActionResult IndeedSearch(string jobName, string jobLocation)
        {
            List<IndeedClass> model = IndeedClass.RunSearch(jobName, jobLocation);
            return View("Indeed", model);
        }

    }
}
