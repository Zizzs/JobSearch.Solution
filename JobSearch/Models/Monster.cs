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
        private string _date;



        MonsterClass(string title, string url, string company, string location, string date)
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
            string tempLocation = "No Location Specified";
            string tempDate = "";

            Thread.Sleep(750);
            // IReadOnlyCollection<IWebElement> anchors = driver.FindElements(By.ClassName("card-content "));
            IWebElement number = driver.FindElement(By.XPath("//*[@id='ResultsScrollable']/div"));
            int count = int.Parse(number.GetAttribute("data-results-total"));
            if (count > 20)
            {
                count = 20;
            }
            Console.WriteLine(count);
            for (int i = 1; i < count; i++)
            {
                if (i == 3)
                {
                    i = 4;
                }
                if (i == 10)
                {
                    i = 11;
                }
                else
                {

                    IWebElement single = driver.FindElement(By.XPath("//*/section[" + i + "]/div/div[2]/header/h2/a"));


                    tempTitle = single.Text;
                    tempLink = single.GetAttribute("href");

                    IWebElement company = driver.FindElement(By.XPath("//*/section[" + i + "]/div/div[2]/div[1]/a"));
                    tempCompany = company.Text;

                    List<IWebElement> location = new List<IWebElement>();
                    location.AddRange(driver.FindElements(By.XPath("//*/section[" + i + "]/div/div[2]/div[2]/a")));


                    if (location.Count >= 1)
                    {
                        if (!string.IsNullOrEmpty(location[0].Text))
                        {
                            tempLocation = location[0].Text;
                        }
                        else if (string.IsNullOrEmpty(location[0].Text))
                        {
                            tempLocation = "Location not listed";
                        }

                    }
                    IWebElement date = driver.FindElement(By.XPath("//*/section[" + i + "]/div/div[3]/time"));
                    tempDate = date.Text;
                    MonsterClass tempjob = new MonsterClass(tempTitle, tempLink, tempCompany, tempLocation, tempDate);
                    monsterJobs.Add(tempjob);
                }
            }
            return monsterJobs;
        }
    }
}