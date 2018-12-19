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
        private string _location;
        private string _date;

        CraigslistClass(string title, string url, string location, string date)
        {
            _title = title;
            _url = url;
            _location = location;
            _date = date;
        }

        public string GetTitle()
        {
            return _title;
        }

        public string GetUrl()
        {
            return _url;
        }

        public string GetLocation()
        {
            return _location;
        }

        public string GetDate()
        {
            return _date;
        }
        // Initialize the Chrome Driver
        public static List<CraigslistClass> RunSearch(string jobName, string jobLocation)
        {

            ChromeDriver driver = new ChromeDriver("/Users/Guest/Desktop/JobSearch.Solution/JobSearch/wwwroot/drivers");

            // Go to the home page
            driver.Navigate().GoToUrl("https://" + jobLocation + ".craigslist.org/d/jobs/search/jjj");

            // Get the page elements
            var searchForm = driver.FindElementById("query");

            // Type user name and password
            searchForm.SendKeys(jobName);

            searchForm.Submit();

            List<CraigslistClass> craigslistJobs = new List<CraigslistClass> { };
            string tempTitle = "";
            string tempLink = "";
            string tempLocation = "";
            string tempDate = "";

            IList<IWebElement> anchors = driver.FindElements(By.ClassName("hdrlnk"));
            for (int i = 1; i < anchors.Count; i++)
            {
                tempTitle = anchors[i].Text;
                tempLink = anchors[i].GetAttribute("href");
                IWebElement location = driver.FindElement(By.XPath("//*[@id='sortable-results']/ul/li[" + i + "]/p/span[2]/span[1]"));
                tempLocation = location.Text;
                IWebElement date = driver.FindElement(By.XPath("//*[@id='sortable-results']/ul/li[" + i + "]/p/time"));
                tempDate = date.Text;

                CraigslistClass tempjob = new CraigslistClass(tempTitle, tempLink, tempLocation, tempDate);
                craigslistJobs.Add(tempjob);
            }

            return craigslistJobs;
        }
    }
}