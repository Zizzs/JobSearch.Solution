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
        private string _company;
        public StackOverflow(string title, string url, string company)
        {
            _title = title;
            _url = url;
            _company = company;
        }
        public string GetTitle()
        {
            return _title;
        }

        public string GetUrl()
        {
            return _url;
        }
        public string GetCompany()
        {
            return _company;
        }

        public static List<StackOverflow> RunSearch(string jobName, string jobLocation)
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
            string tempCompany = "";
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
                        var tempListing = driver.FindElementByXPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[1]/h2/a");
                        var tempList = driver.FindElementByXPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[2]/span[1]");
                        tempTitle = tempListing.Text;
                        tempLink = tempListing.GetAttribute("href");
                        tempCompany = tempList.Text;
                        StackOverflow tempJob = new StackOverflow(tempTitle, tempLink, tempCompany);
                        stackOverflowJobs.Add(tempJob);
                    }
                }
            }
            catch
            {
                for (int i = 1; i < 20; i++)
                {
                    if (existsElement(i))
                    {
                        var tempListing = driver.FindElementByXPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[1]/h2/a");
                        var tempList = driver.FindElementByXPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[2]/span[1]");
                        tempTitle = tempListing.Text;
                        tempLink = tempListing.GetAttribute("href");
                        tempCompany = tempList.Text;
                        StackOverflow tempJob = new StackOverflow(tempTitle, tempLink, tempCompany);
                        stackOverflowJobs.Add(tempJob);
                    }
                }
            }
            return stackOverflowJobs;
        }
    }
}
