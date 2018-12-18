using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using JobSearch;
using System.IO;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;


namespace JobSearch.Models
{
    public class StackOverflow
    {
        private string _title;
        private string _url;

        StackOverflow(string title, string url)
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
        // Initialize the Chrome Driver
        public static List<StackOverflow> RunSearch(string jobName, string jobLocation)
        {

            ChromeDriver driver = new ChromeDriver("/Users/Guest/Desktop/JobSearch.Solution/JobSearch/wwwroot/drivers");

            // Go to the home page
            driver.Navigate().GoToUrl("https://stackoverflow.com/jobs");

            Thread.Sleep(2000);
            // Get the page elements
            var searchForm = driver.FindElementById("q");
            var locationForm = driver.FindElementById("l");


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

            List<StackOverflow> stackOverflowJobs = new List<StackOverflow> { };
            string tempTitle = "";
            string tempLink = "";
            Thread.Sleep(1000);
            IReadOnlyCollection<IWebElement> anchors = driver.FindElements(By.ClassName("-job-summary"));
            int count = anchors.Count;
            for(int i = 1; i < count; i++)
            {
                if (i==3)
                {
                    i=4;
                }
                else
                {
                    if (driver.FindElement(By.TagName("div")).Text.Contains("This isn't exactly what you searched for, but you might be interested in these jobs:") == true)
                    {
                        tempTitle = "There are no more of those jobs on Stack Overflow.";
                        tempLink = "http://www.google.com";
                        StackOverflow tempjob = new StackOverflow(tempTitle, tempLink);
                        stackOverflowJobs.Add(tempjob);
                        i = 1000;
                    }
                    else
                    {
                        IWebElement single = driver.FindElement(By.XPath("//*/div["+i+"]/div[3]/div[1]/h2/a"));

                        tempTitle = single.Text;
                        tempLink = single.GetAttribute("href");
                        StackOverflow tempjob = new StackOverflow(tempTitle, tempLink);
                        stackOverflowJobs.Add(tempjob);
                    }
                }
            }
            return stackOverflowJobs;
        }
    }
}

//*[@id="mainbar"]/div[2]/div/div[2]/text()