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
    public class GlassdoorClass
    {
        private string _title;
        private string _url;

        GlassdoorClass(string title, string url)
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
        public static List<GlassdoorClass> RunSearch(string jobName, string jobLocation)
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
                driverLocation = "../JobSearch/wwwroot/drivers";
            }
            // Initialize the Chrome Driver

            ChromeDriver driver = new ChromeDriver(driverLocation);

            // Go to the home page
            driver.Navigate().GoToUrl("https://www.glassdoor.com/index.htm");
            driver.Manage().Cookies.DeleteAllCookies();
            // Get the page elements
            var searchForm = driver.FindElementById("KeywordSearch");
            var locationForm = driver.FindElementById("LocationSearch");


            // Type user name and password
            searchForm.SendKeys(jobName);
            locationForm.SendKeys("");
            for (int i = 0; i < 20; i++)
            {
                locationForm.SendKeys(Keys.Backspace);
            }
            locationForm.SendKeys(jobLocation);

            // and click the login button
            searchForm.Submit();

            List<GlassdoorClass> glassdoorJobs = new List<GlassdoorClass> { };
            string tempTitle = "";
            string tempLink = "";


            IReadOnlyCollection<IWebElement> anchors = driver.FindElements(By.ClassName("jobLink"));
            foreach (IWebElement link in anchors)
            {
                tempTitle = link.Text;
                tempLink = link.GetAttribute("href");
                GlassdoorClass tempjob = new GlassdoorClass(tempTitle, tempLink);
                glassdoorJobs.Add(tempjob);
            }

            return glassdoorJobs;
        }
    }
}