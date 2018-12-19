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
    public class GlassdoorClass
    {
        private string _title;
        private string _url;
        private string _company;
        GlassdoorClass(string title, string url, string company)
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
        public static List<GlassdoorClass> RunSearch(string jobName, string jobLocation)
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
            driver.Navigate().GoToUrl("https://www.glassdoor.co.in/Jobs/Glassdoor-Jobs-E100431.htm");
            driver.Manage().Cookies.DeleteAllCookies();
            // Get the page elements
            var searchForm = driver.FindElementById("sc.keyword");
            var locationForm = driver.FindElementById("sc.location");


            searchForm.SendKeys("");
            for (int i = 0; i < 20; i++)
            {
                searchForm.SendKeys(Keys.Backspace);
            }
            searchForm.SendKeys(jobName);
            locationForm.SendKeys("");
            for (int i = 0; i < 20; i++)
            {
                locationForm.SendKeys(Keys.Backspace);
            }
            locationForm.SendKeys(jobLocation);

            // and click the login button
            searchForm.Submit();

            List<GlassdoorClass> glassdoorJobs = new List<GlassdoorClass> { };
            string tempTitle = "";
            string tempLink = "";
            string tempCompany = "";

            IList<IWebElement> anchors = driver.FindElements(By.ClassName("jobLink"));
            for (int i = 1; i < 30; i++)
            {
                IWebElement title = driver.FindElement(By.XPath("//*[@id='MainCol']/div/ul/li[" + i + "]/div[2]/div[1]/div[1]/a"));
                tempTitle = title.Text;
                tempLink = title.GetAttribute("href");

                IWebElement company = driver.FindElement(By.XPath("//*[@id='MainCol']/div/ul/li[" + i + "]/div[2]/div[2]/div"));
                tempCompany = company.Text;

                GlassdoorClass tempjob = new GlassdoorClass(tempTitle, tempLink, tempCompany);
                glassdoorJobs.Add(tempjob);
            }
            driver.Close();
            return glassdoorJobs;
        }
    }
}