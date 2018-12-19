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


        IndeedClass(string title, string url, string company, string location)
        {
            _title = title;
            _url = url;
            _company = company;
            _location = location;
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
            for (int i = 0; i < 20; i++)
            {
                locationForm.SendKeys(Keys.Backspace);
            }
            locationForm.SendKeys(jobLocation);

            // and click the login button
            searchForm.Submit();

            List<IndeedClass> indeedJobs = new List<IndeedClass> { };
            string tempTitle = "";
            string tempLink = "";
            string tempCompany = "";
            string tempLocation = "";


            IList<IWebElement> links = driver.FindElements(By.ClassName("turnstileLink"));

            Console.WriteLine(links.Count);

            for (int i = 0; i < links.Count; i++)
            {
                links = driver.FindElements(By.ClassName("turnstileLink"));


                if (!string.IsNullOrEmpty(links[i].Text))
                {
                    if (links[i].GetAttribute("data-tn-element") == "jobTitle")
                    {
                        tempTitle = links[i].Text;
                        tempLink = links[i].GetAttribute("href");
                        links[i].Click();
                        int timeout = 0;
                        while (driver.FindElements(By.Id("vjs-cn")).Count == 0 && timeout < 500)
                        {
                            Thread.Sleep(200);
                            timeout++;
                        }
                        IWebElement company = driver.FindElement(By.Id("vjs-cn"));
                        while (driver.FindElements(By.Id("vjs-loc")).Count == 0 && timeout < 500)
                        {
                            Thread.Sleep(200);
                            timeout++;
                        }
                        IWebElement location = driver.FindElement(By.Id("vjs-loc"));

                        tempCompany = company.Text;
                        tempLocation = location.Text;
                    }
                }

                IndeedClass tempJob = new IndeedClass(tempTitle, tempLink, tempCompany, tempLocation);
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
            return result;
        }
    }

}