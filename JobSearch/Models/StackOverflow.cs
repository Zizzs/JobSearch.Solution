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
    public class JobSearchClass
    {
        // Initialize the Chrome Driver
        public static void RunSearch()
        {

            ChromeDriver driver = new ChromeDriver("/Users/Guest/Desktop/JobSearch.Solution/JobSearch/wwwroot/drivers");

            // Go to the home page
            driver.Navigate().GoToUrl("https://stackoverflow.com/jobs");

            // Get the page elements
            var searchForm = driver.FindElementById("q");
            // Get the location
            var searchFormLocation = driver.FindElementById("l");

            // Type user name and password
            searchForm.SendKeys("Junior Web Developer");
            searchFormLocation.SendKeys("Seattle WA, USA");

            // and click the login button
            searchForm.Submit();

            // Extract the text and save it into result.txt
            var result = driver.FindElementById("h2");
            var resultText = result.Text;
            File.WriteAllText("result.txt", resultText);

            // ("//*[@id='mainbar']/div[2]/div/div[3]/div[3]/div[1]/h2/a").Text;
            // foreach (var result in results)
            // {

            //     string resultText = result.Text;
            //     File.WriteAllText("result.txt", resultText);
            // }

        }
    }
}