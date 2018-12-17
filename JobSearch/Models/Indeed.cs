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
    public class IndeedClass
    {
        private string _title;
        private string _url;

        IndeedClass(string title, string url)
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
        public static List<IndeedClass> RunSearch(string jobName, string jobLocation)
        {

            ChromeDriver driver = new ChromeDriver("/Users/Guest/Desktop/JobSearch.Solution/JobSearch/wwwroot/drivers");
            
            // Go to the home page
            driver.Navigate().GoToUrl("http://www.indeed.com");

            // Get the page elements
            var searchForm = driver.FindElementByName("q");
            var locationForm = driver.FindElementByName("l");
            

            // Type user name and password
            searchForm.SendKeys(jobName);
            locationForm.SendKeys("");
            for(int i =0; i <20; i++)
            {
               locationForm.SendKeys(Keys.Backspace);
            }
            locationForm.SendKeys(jobLocation);

            // and click the login button
            searchForm.Submit();
            
            List<IndeedClass> indeedJobs = new List<IndeedClass>{};
            string tempTitle ="";
            string tempLink = "";
            
            for(int i =1; i < 5; i++)
            {
                var tempListing = driver.FindElementById("sja" +i); 
                tempTitle = tempListing.Text;
                tempLink = tempListing.GetAttribute("href");
                IndeedClass tempJob = new IndeedClass(tempTitle, tempLink);
                indeedJobs.Add(tempJob);
           }

          


            return indeedJobs;
   
            // // Extract the text and save it into result.txt
            // var result = driver.FindElementByXPath("//div[@id='case_login']/h3").Text;
            // File.WriteAllText("result.txt", result);
        }
    }
}