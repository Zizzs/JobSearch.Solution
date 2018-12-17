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
            JobSearchClass.RunSearch();
            return View();
        }
    }
}
