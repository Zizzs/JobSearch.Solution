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
<<<<<<< HEAD
            List<Indeed> newList = Indeed.RunSearch();
            return View(newList);
=======
            return View();
>>>>>>> 15be08b12a98c323d60f2fd474c52e82498f125b
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
