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
    public class MonsterClass
    {
        private string _title;
        private string _url;
        private string _company;

        private string _location;

        MonsterClass(string title, string url, string company, string location)
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
        public static List<MonsterClass> RunSearch(string jobName, string jobLocation)
        {

            ChromeDriver driver = new ChromeDriver("/Users/Guest/Desktop/JobSearch.Solution/JobSearch/wwwroot/drivers");

            // Go to the home page
            driver.Navigate().GoToUrl("https://www.monster.com/");

            Thread.Sleep(2000);
            // Get the page elements
            var searchForm = driver.FindElementById("q2");
            var locationForm = driver.FindElementById("where2");

            Thread.Sleep(250);
            // Type user name and password
            searchForm.SendKeys(jobName);
            locationForm.SendKeys("");
            for (int i = 0; i < 20; i++)
            {
                locationForm.SendKeys(Keys.Backspace);
            }
            Thread.Sleep(250);
            locationForm.SendKeys(jobLocation);

            Thread.Sleep(250);
            // and click the login button
            searchForm.Submit();

            List<MonsterClass> monsterJobs = new List<MonsterClass> { };
            string tempTitle = "";
            string tempLink = "";
            string tempCompany = "";
            string tempLocation = "";
            Thread.Sleep(750);
            // IReadOnlyCollection<IWebElement> anchors = driver.FindElements(By.ClassName("card-content "));
            IWebElement number = driver.FindElement(By.XPath("//*[@id='ResultsScrollable']/div"));
            int count = int.Parse(number.GetAttribute("data-results-total"));
            if (count > 25)
            {
                count = 25;
            }
            Console.WriteLine(count);
            for (int i = 1; i < count; i++)
            {
                if (i == 3)
                {
                    i = 4;
                }
                else
                {

                    IWebElement single = driver.FindElement(By.XPath("//*/section[" + i + "]/div/div[2]/header/h2/a"));


                    tempTitle = single.Text;
                    tempLink = single.GetAttribute("href");
                    IWebElement company = driver.FindElement(By.XPath("//*/section[" + i + "]/div/div[2]/div[1]/a"));
                    tempCompany = company.Text;
                    IWebElement location = driver.FindElement(By.XPath("//*/section[" + i + "]/div/div[2]/div[2]/a"));
                    if (location.Text == "" || (!string.IsNullOrEmpty(location.Text)))
                    {
                        tempLocation = "No location";
                    }
                    else
                    {
                        tempLocation = location.Text;
                    }
                    MonsterClass tempjob = new MonsterClass(tempTitle, tempLink, tempCompany, tempLocation);
                    monsterJobs.Add(tempjob);
                }
            }
            return monsterJobs;
        }
    }
}