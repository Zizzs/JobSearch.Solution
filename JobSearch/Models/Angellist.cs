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
    public class AngelListClass
    {
        private string _title;
        private string _url;

        AngelListClass(string title, string url)
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
        public static List<AngelListClass> RunSearch(string jobName, string jobLocation)
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
            driver.Navigate().GoToUrl("https://angel.co/login");
            Thread.Sleep(100);
            var emailForm = driver.FindElementById("user_email");
            var passwordForm = driver.FindElementById("user_password");
            Thread.Sleep(100);
            emailForm.SendKeys("kairon.rahm@plutofox.com");
            passwordForm.SendKeys("angellist55");
            passwordForm.Submit();
            Thread.Sleep(100);
            driver.Navigate().GoToUrl("https://angel.co/jobs#find");
            Thread.Sleep(300);
            var delete = driver.FindElementByClassName("remove-filter");
            delete.Click();
            var click = driver.FindElementByClassName("currently-showing");
            click.Click();
            var searchForm = driver.FindElementByClassName("keyword-input");
            Thread.Sleep(100);
            searchForm.SendKeys(jobName);
            Thread.Sleep(100);
            searchForm.SendKeys(Keys.Enter);
            Thread.Sleep(500);
            searchForm.SendKeys(jobLocation);
            Thread.Sleep(100);
            searchForm.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            List<AngelListClass> angelListClass = new List<AngelListClass> { };
            string tempTitle = "";
            string tempLink = "";


            IReadOnlyCollection<IWebElement> anchors = driver.FindElements(By.ClassName("startup-link"));
            foreach (IWebElement link in anchors)
            {
                tempTitle = link.Text;
                tempLink = link.GetAttribute("href");
                AngelListClass tempjob = new AngelListClass(tempTitle, tempLink);
                angelListClass.Add(tempjob);
            }

            return angelListClass;
        }
    }
}