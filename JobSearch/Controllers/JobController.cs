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
            Dictionary<string, object> model = new Dictionary<string, object>();
            List<GlassdoorClass> glassdoor = new List<GlassdoorClass>();
            List<CraigslistClass> craigslist = new List<CraigslistClass>();
            List<MonsterClass> monster = new List<MonsterClass>();
            model.Add("glassdoor" , glassdoor);
            model.Add("craigslist" , craigslist);
            model.Add("monster" , monster);
            return View(model);
        }

        [HttpPost("/jobs/all")]
        public ActionResult AllSearch(string jobName, string jobLocation)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            List<GlassdoorClass> glassdoor = GlassdoorClass.RunSearch(jobName, jobLocation);
            List<CraigslistClass> craigslist = CraigslistClass.RunSearch(jobName);
            List<MonsterClass> monster = MonsterClass.RunSearch(jobName, jobLocation);
            model.Add("glassdoor" , glassdoor);
            model.Add("craigslist" , craigslist);
            model.Add("monster" , monster);
            return View("All" , model);
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

        [HttpGet("/jobs/glassdoor")]
        public ActionResult Glassdoor()
        {
            List<IndeedClass> model = new List<IndeedClass>();
            return View(model);
        }

        [HttpPost("/jobs/glassdoor")]
        public ActionResult GlassdoorSearch(string jobName, string jobLocation)
        {
            List<GlassdoorClass> model = GlassdoorClass.RunSearch(jobName, jobLocation);
            return View("Glassdoor", model);
            
        }

        [HttpGet("/jobs/craigslist")]
        public ActionResult Craigslist()
        {
            List<CraigslistClass> model = new List<CraigslistClass>();
            return View(model);
        }

        [HttpPost("/jobs/craigslist")]
        public ActionResult CraigslistSearch(string jobName)
        {
            List<CraigslistClass> model = CraigslistClass.RunSearch(jobName);
            return View("Craigslist", model);
        }

        [HttpGet("/jobs/monster")]
        public ActionResult Monster()
        {
            List<MonsterClass> model = new List<MonsterClass>();
            return View(model);
        }

        [HttpPost("/jobs/monster")]
        public ActionResult MonsterSearch(string jobName, string jobLocation)
        {
            List<MonsterClass> model = MonsterClass.RunSearch(jobName, jobLocation);
            return View("Monster", model);
        }

        [HttpGet("/jobs/angellist")]
        public ActionResult Angellist()
        {
            List<AngelListClass> model = new List<AngelListClass>();
            return View(model);
        }

        [HttpPost("/jobs/angellist")]
        public ActionResult AngellistSearch(string jobName, string jobLocation)
        {
            List<AngelListClass> model = AngelListClass.RunSearch(jobName, jobLocation);
            return View("Angellist", model);
        }

    }
}
