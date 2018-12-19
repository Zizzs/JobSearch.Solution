using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using JobSearch;
using System.IO;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace JobSearch.Models
{
    public class CraigslistClass
    {
        private string _title;
        private string _url;

        CraigslistClass(string title, string url)
        {
            _title = title;
            _url = url;
        }

        public string GetTitle()
        {
            return _title;
        }

        public string GetUrl()
        {
            return _url;
        }
        public static List<CraigslistClass> RunSearch(string jobName)
        {

            // Check operating system
            string driverLocation = "";
            string osName = System.Runtime.InteropServices.RuntimeInformation.OSDescription.ToLower();

            Console.WriteLine(osName);

            if (osName.Contains("windows"))
            {
                driverLocation = "..\\JobSearch\\wwwroot\\drivers-win";
            }
            else
            {
                driverLocation = "../JobSearch.Solution/JobSearch/wwwroot/drivers";
            }
            // Initialize the Chrome Driver

            ChromeDriver driver = new ChromeDriver(driverLocation);

            // Go to the home page
            driver.Navigate().GoToUrl("https://seattle.craigslist.org/d/jobs/search/jjj");

            // Get the page elements
            var searchForm = driver.FindElementById("query");
            //var locationForm = driver.FindElementById("LocationSearch");


            // Type user name and password
            searchForm.SendKeys(jobName);
            //locationForm.SendKeys("");
            // for (int i = 0; i < 20; i++)
            // {
            //     locationForm.SendKeys(Keys.Backspace);
            // }
            //locationForm.SendKeys(jobLocation);

            // and click the login button
            searchForm.Submit();

            List<CraigslistClass> craigslistJobs = new List<CraigslistClass> { };
            string tempTitle = "";
            string tempLink = "";


            IReadOnlyCollection<IWebElement> anchors = driver.FindElements(By.ClassName("hdrlnk"));
            foreach (IWebElement link in anchors)
            {
                tempTitle = link.Text;
                tempLink = link.GetAttribute("href");
                CraigslistClass tempjob = new CraigslistClass(tempTitle, tempLink);
                craigslistJobs.Add(tempjob);
            }

            return craigslistJobs;
        }
    }
}