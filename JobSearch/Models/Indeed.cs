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
        private string _company;


        IndeedClass(string title, string url, string company)
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


            IList<IWebElement> links = driver.FindElements(By.ClassName("turnstileLink"));
            IList<IWebElement> companies = driver.FindElements(By.ClassName("turnstileLink"));


            for (int i = 0; i < links.Count; i++)
            {
                links = driver.FindElements(By.ClassName("turnstileLink"));
                companies = driver.FindElements(By.ClassName("turnstileLink"));


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
                            timeout++;
                        }
                        IWebElement company = driver.FindElement(By.Id("vjs-cn"));

                        tempCompany = company.Text;
                    }
                }

                // if (!string.IsNullOrEmpty(companies[i].Text))
                // {
                //     if (companies[i].GetAttribute("tag") == "span" && companies[i].GetAttribute("class") == "company")
                //     {
                //         tempCompany = companies[i].Text;
                //     }
                // }


                IndeedClass tempJob = new IndeedClass(tempTitle, tempLink, tempCompany);
                indeedJobs.Add(tempJob);



            }
            return indeedJobs;
        }
    }
}
