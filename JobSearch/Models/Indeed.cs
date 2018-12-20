using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using JobSearch;
using System.IO;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;


namespace JobSearch.Models
{
    public class IndeedClass
    {
        private string _title;
        private string _url;
        private string _company;
        private string _location;

        private string _date;
        private string _description;

        IndeedClass(string title, string url, string company, string location, string date, string description)
        {
            _title = title;
            _url = url;
            _company = company;
            _location = location;
            _date = date;
            _description = description;
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

        public string GetDescription()
        {
            return _description;
        }
        // Initialize the Chrome Driver
        public static List<IndeedClass> RunSearch(string jobName, string jobLocation)
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
            driver.Navigate().GoToUrl("http://www.indeed.com");

            // Get the page elements
            var searchForm = driver.FindElementByName("q");
            var locationForm = driver.FindElementByName("l");


            // Type job title and location
            searchForm.SendKeys(jobName);
            locationForm.SendKeys("");
            for (int i = 0; i < 20; i++)
            {
                locationForm.SendKeys(Keys.Backspace);
            }
            locationForm.SendKeys(jobLocation);

            // and click the submit button
            searchForm.Submit();

            List<IndeedClass> indeedJobs = new List<IndeedClass> { };
            string tempTitle = "";
            string tempLink = "";
            string tempCompany = "";
            string tempLocation = "";
            string tempDate = "";
            string tempDescription = "";


            IList<IWebElement> links = driver.FindElements(By.ClassName("turnstileLink"));



            for (int i = 0; i < links.Count - 1; i++)
            {
                links = driver.FindElements(By.ClassName("turnstileLink"));
                Thread.Sleep(500);
                if (string.IsNullOrEmpty(links[i].Text))
                {
                    continue;
                }

                else if (!string.IsNullOrEmpty(links[i].Text))
                {
                    if (links[i].GetAttribute("data-tn-element") == "jobTitle")
                    {
                        tempTitle = links[i].Text;
                        tempLink = links[i].GetAttribute("href");
                        links[i].Click();
                        int timeout = 0;
                        while (driver.FindElements(By.Id("vjs-cn")).Count == 0 && timeout < 200)
                        {
                            Thread.Sleep(25);
                            timeout++;
                        }
                        IWebElement company = driver.FindElement(By.Id("vjs-cn"));

                        while (driver.FindElements(By.Id("vjs-loc")).Count == 0 && timeout < 200)
                        {
                            Thread.Sleep(25);
                            timeout++;
                        }
                        IWebElement location = driver.FindElement(By.Id("vjs-loc"));
                        while (driver.FindElements(By.Id("vjs-loc")).Count == 0 && timeout < 200)
                        {
                            timeout++;
                        }

                        IWebElement description = driver.FindElement(By.Id("vjs-desc"));
                        while (driver.FindElements(By.Id("vjs-desc")).Count == 0 && timeout < 200)
                        {
                            timeout++;
                        }
                        IWebElement date = driver.FindElement(By.CssSelector("#vjs-footer > div > div > span.date"));

                        tempCompany = company.Text;
                        tempLocation = location.Text;
                        tempDate = date.Text;
                        tempDescription = description.Text;
                    }
                }
                // Create an instance ob the object and push to the list
                IndeedClass tempJob = new IndeedClass(tempTitle, tempLink, tempCompany, tempLocation, tempDate, tempDescription);
                indeedJobs.Add(tempJob);

            }

            // Filter out duplicates
            List<IndeedClass> result = new List<IndeedClass> { };

            for (int i = 0; i < indeedJobs.Count; i++)
            {
                bool exists = false;

                foreach (IndeedClass job in result)
                {
                    if (indeedJobs[i].GetUrl() == job.GetUrl())
                    {
                        exists = true;
                    }
                }

                if (!exists)
                {
                    result.Add(indeedJobs[i]);
                }
            }
            if (indeedJobs.Count == 0)
            {
                IndeedClass tempJob = new IndeedClass("Sorry, there are no results. Please try a different search.", tempLink, tempCompany, tempLocation, tempDate, tempDescription);
                indeedJobs.Add(tempJob);
                driver.Close();
                return indeedJobs;
            }
            driver.Close();
            return result;
        }
    }

}