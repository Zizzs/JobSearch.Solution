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
            List<Indeed> newList = Indeed.RunSearch();
            return View(newList);
        }
    }
}
