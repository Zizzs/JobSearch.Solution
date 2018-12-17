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
    public class StackOverflow
    {
        private string _title;
        private string _url;
        public StackOverflow(string title, string url)
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

            // Get the page elements
            var searchForm = driver.FindElementById("q");
            // Get the location
            var searchFormLocation = driver.FindElementById("l");

            // Type user name and password
            searchForm.SendKeys(jobName);
            searchFormLocation.SendKeys(jobLocation);

            // and click the login button
            searchForm.Submit();

            List<StackOverflow> stackOverflowJobs = new List<StackOverflow> { };
            string tempTitle = "";
            string tempLink = "";

            try
            {
                for (int i = 1; i < 5; i++)
                {
                    var tempListing = driver.FindElementByXPath("//*[@id='mainbar']/div[2]/div/div[4]/div[3]/div[1]/h2/a");
                    tempTitle = tempListing.Text;
                    tempLink = tempListing.GetAttribute("href");
                    StackOverflow tempJob = new StackOverflow(tempTitle, tempLink);
                    stackOverflowJobs.Add(tempJob);
                }
            }
            catch
            {
                for (int i = 1; i < 5; i++)
                {
                    var tempListing = driver.FindElementByXPath("//*[@id='mainbar']/div[2]/div/div[4]/div[3]/div[1]/h2/a");
                    tempTitle = tempListing.Text;
                    tempLink = tempListing.GetAttribute("href");
                    StackOverflow tempJob = new StackOverflow(tempTitle, tempLink);
                    stackOverflowJobs.Add(tempJob);
                }
            }
            return stackOverflowJobs;
            // Extract the text and save it into result.txt
            // File.WriteAllText("result.txt", resultText);


        }
    }
}