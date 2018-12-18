using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using JobSearch;
using System.IO;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
// using OpenQA.Selenium.Support.UI;

namespace JobSearch.Models
{
    public class StackOverflow
    {
        private string _title;
        private string _url;
        public StackOverflow(string title, string url)
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
        // Initialize the Chrome Driver
        public static List<StackOverflow> RunSearch(string jobName, string jobLocation)
        {

            ChromeDriver driver = new ChromeDriver("/Users/Guest/Desktop/JobSearch.Solution/JobSearch/wwwroot/drivers");

            // Go to the home page
            //             IWebElement myDynamicElement =
            // (new WebDriverWait(driver, 10)).until(ExpectedConditions.presenceOfElementLocated(By.id("usrUTils")));


            // driver.Url("https://stackoverflow.com/jobs");
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
            int timeout = 0;
            // while (driver.FindElements(By.XPath("//*[@id='mainbar']/div[2]/div/div[3]")).Count == 0 && timeout < 500)
            // {
            //     Console.WriteLine("timing out");
            //     Thread.Sleep(2000);
            //     timeout++;
            // }
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
                        Console.WriteLine("first try started");
                        var tempListing = driver.FindElementByXPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[1]/h2/a");
                        // var tempListing = driver.FindElementByPartialLinkText("developer");
                        tempTitle = tempListing.Text;
                        tempLink = tempListing.GetAttribute("href");
                        StackOverflow tempJob = new StackOverflow(tempTitle, tempLink);
                        stackOverflowJobs.Add(tempJob);
                        Console.WriteLine("finished first try");
                    }
                }
            }
            catch
            {
                for (int i = 1; i < 20; i++)
                {
                    Console.WriteLine("catch triggered");
                    if (existsElement(i))
                    {
                        var tempListing = driver.FindElementByXPath("//*[@id='mainbar']/div[2]/div/div[" + i + "]/div[3]/div[1]/h2/a");
                        // var tempListing = driver.FindElementByPartialLinkText("developer");
                        tempTitle = tempListing.Text;
                        tempLink = tempListing.GetAttribute("href");
                        StackOverflow tempJob = new StackOverflow(tempTitle, tempLink);
                        stackOverflowJobs.Add(tempJob);
                        Console.WriteLine("finished second try");
                    }
                }
            }
            //     IList<IWebElement> links = driver.FindElements(By.ClassName("s-link"));
            //     IList<IWebElement> listOflinks = new List<IWebElement>();

            //     for(int i = 0 ; i < links.Count ; i++)
            //     {
            //     links = driver.FindElements(By.ClassName("s-link"));

            //     if (!string.IsNullOrEmpty(links[i].Text) && links[i].GetAttribute("data-tn-element") == "jobTitle")
            //     {
            //         tempTitle = links[i].Text;
            //         tempLink = links[i].GetAttribute("href");
            //         StackOverflow tempJob = new StackOverflow(tempTitle, tempLink);
            //         stackOverflowJobs.Add(tempJob);
            //     }


            //   }

            // return stackOverflowJobs;
            // Console.WriteLine(stackOverflowJobs[0]);
            // // Extract the text and save it into result.txt
            // // File.WriteAllText("result.txt", resultText);
            // //*[@id="mainbar"]/div[2]/div/div[8]/div[3]/div[1]/h2/a
            // //*[@id="mainbar"]/div[2]/div/div[7]/div[3]/div[1]/h2/a
            // //*[@id="mainbar"]/div[2]/div/div[6]/div[3]/div[1]/h2/a
            // //*[@id="mainbar"]/div[2]/div/div[3]/div[3]/div[1]/h2/a
            // //*[@id="mainbar"]/div[2]/div/div[1]/div[3]/div[1]/h2/a
            // //*[@id="mainbar"]/div[2]/div/div[1]/div[3]/div[1]/h2/a
            // //*[@id="mainbar"]/div[2]/div/div[3]/div[3]/div[1]/h2/a

            return stackOverflowJobs;
        }
    }
}
