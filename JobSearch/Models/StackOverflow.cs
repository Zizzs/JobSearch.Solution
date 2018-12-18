using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using JobSearch;
using System.IO;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;

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
            //             IWebElement myDynamicElement =
            // (new WebDriverWait(driver, 10)).until(ExpectedConditions.presenceOfElementLocated(By.id("usrUTils")));


            // driver.Url("https://stackoverflow.com/jobs");
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
            bool existsElement(int i)
            {
                try
                {
                    driver.FindElementByXPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[1]/h2/a");
                }
                catch
                {
                    return false;
                }
                return true;
            }
            try
            {
                for (int i = 1; i < 20; i++)
                {
                    if (existsElement(i))
                    {
                        Console.WriteLine("first try started");
                        var tempListing = driver.FindElementByXPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[1]/h2/a");
                        // var tempListing = driver.FindElementByPartialLinkText("developer");
                        tempTitle = tempListing.Text;
                        tempLink = tempListing.GetAttribute("href");
                        StackOverflow tempJob = new StackOverflow(tempTitle, tempLink);
                        stackOverflowJobs.Add(tempJob);
                        Console.WriteLine("finished first try");
                    }
                }
            }
            catch
            {
                for (int i = 1; i < 20; i++)
                {
                    Console.WriteLine("catch triggered");
                    if (existsElement(i))
                    {
                        var tempListing = driver.FindElementByXPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[1]/h2/a");
                        // var tempListing = driver.FindElementByPartialLinkText("developer");
                        tempTitle = tempListing.Text;
                        tempLink = tempListing.GetAttribute("href");
                        StackOverflow tempJob = new StackOverflow(tempTitle, tempLink);
                        stackOverflowJobs.Add(tempJob);
                        Console.WriteLine("finished second try");
                    }
                }
            }

            return stackOverflowJobs;
        }
    }
}
