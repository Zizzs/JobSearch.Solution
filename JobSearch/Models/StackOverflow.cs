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
        private string _company;
        private string _location;
        private string _date;
        public StackOverflow(string title, string url, string company, string location, string date)
        {
            _title = title;
            _url = url;
            _company = company;
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
        public string GetCompany()
        {
            return _company;
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
                driverLocation = "../JobSearch/wwwroot/drivers";
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
            string tempDate = "";
            string tempLocation = "";
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
                        IWebElement location = driver.FindElement(By.XPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[2]/span[2]"));
                        tempLocation = location.Text;
                        IWebElement date = driver.FindElement(By.XPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[1]/span[2]"));
                        tempDate = date.Text;

                        StackOverflow tempJob = new StackOverflow(tempTitle, tempLink, tempCompany, tempLocation, tempDate);
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
                        IWebElement location = driver.FindElement(By.XPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[2]/span[2]"));
                        tempLocation = location.Text;
                        IWebElement date = driver.FindElement(By.XPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[1]/span[2]"));
                        tempDate = date.Text;

                        StackOverflow tempJob = new StackOverflow(tempTitle, tempLink, tempCompany, tempLocation, tempDate);
                        stackOverflowJobs.Add(tempJob);
                    }
                }
            }
            driver.Close();
            return stackOverflowJobs;
        }
    }
}
