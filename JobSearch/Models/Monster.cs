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
            IList<IWebElement> cards = driver.FindElements(By.ClassName("card-content"));
            string tempTitle = "";
            string tempLink = "";
            string tempCompany = "";
            string tempLocation = "No Location Specified";
            string tempDate = "";

            Thread.Sleep(750);

            bool existsElement(int i)
            {
                try
                {
                    driver.FindElement(By.XPath("//*[@id='SearchResults']/section[" + i + "]/div/div[2]/div[1]/a"));
                }
                catch
                {
                    return false;
                }
                return true;
            }

            try
            {
                for (int i = 1; i < cards.Count - 1; i++)
                {
                    if (existsElement(i))
                    {
                        cards = driver.FindElements(By.ClassName("card-content"));
                        IWebElement single = driver.FindElement(By.XPath("//*[@id='SearchResults']/section[" + i + "]/div/div[2]/header/h2/a"));
                        tempTitle = single.Text;
                        tempLink = single.GetAttribute("href");

                        IWebElement company = driver.FindElement(By.XPath("//*[@id='SearchResults']/section[" + i + "]/div/div[2]/div[1]/a"));
                        tempCompany = company.Text;

                        List<IWebElement> location = new List<IWebElement>();
                        location.AddRange(driver.FindElements(By.XPath("//*[@id='SearchResults']/section[" + i + "]/div/div[2]/div[2]/a")));


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
                        IWebElement date = driver.FindElement(By.XPath("//*[@id='SearchResults']/section[" + i + "]/div/div[3]/time"));
                        tempDate = date.Text;
                        MonsterClass tempjob = new MonsterClass(tempTitle, tempLink, tempCompany, tempLocation, tempDate);
                        monsterJobs.Add(tempjob);
                    }
                }
            }
            catch
            {
                for (int i = 1; i < cards.Count - 1; i++)
                {
                    if (existsElement(i))
                    {
                        cards = driver.FindElements(By.ClassName("card-content"));
                        IWebElement single = driver.FindElement(By.XPath("//*[@id='SearchResults']/section[" + i + "]/div/div[2]/header/h2/a"));
                        tempTitle = single.Text;
                        tempLink = single.GetAttribute("href");

                        IWebElement company = driver.FindElement(By.XPath("//*[@id='SearchResults']/section[" + i + "]/div/div[2]/div[1]/a"));
                        tempCompany = company.Text;

                        List<IWebElement> location = new List<IWebElement>();
                        location.AddRange(driver.FindElements(By.XPath("//*[@id='SearchResults']/section[" + i + "]/div/div[2]/div[2]/a")));


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

            }
            driver.Close();
            return monsterJobs;
        }
    }
}