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

        IndeedClass(string title, string url)
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
            for(int i =0; i <20; i++)
            {
               locationForm.SendKeys(Keys.Backspace);
            }
            locationForm.SendKeys(jobLocation);

            // and click the login button
            searchForm.Submit();
            
            List<IndeedClass> indeedJobs = new List<IndeedClass>{};
            string tempTitle ="";
            string tempLink = "";
            
            for(int i =1; i < 8; i++)
            {
                var tempListing = driver.FindElementById("sja" +i); 
                tempTitle = tempListing.Text;
                tempLink = tempListing.GetAttribute("href");
                IndeedClass tempJob = new IndeedClass(tempTitle, tempLink);
                indeedJobs.Add(tempJob);
           }

            var nextLink = driver.FindElementByXPath("//*[@id='resultsCol']/div[28]/a[1]/span");
                        nextLink.Click();

           while(indeedJobs.Count <10)
           {
               try
               {
                    for(int j= 7; j<8; j++)   
                   {
                        var tempListing2 = driver.FindElementById("sja" +j); 
                        tempTitle = tempListing2.Text;
                        tempLink = tempListing2.GetAttribute("href");
                        IndeedClass tempJob = new IndeedClass(tempTitle, tempLink);
                        indeedJobs.Add(tempJob);
                   }  
               }
               catch
               {
                   for(int j= 7; j<8; j++)  
                   {
                        var thirdLink = driver.FindElementByXPath("//*[@id='resultsCol']/div[28]/a[3]");
                        var tempListing3 = driver.FindElementById("sja" +j); 
                        tempTitle = tempListing3.Text;
                        tempLink = tempListing3.GetAttribute("href");
                        IndeedClass tempJob = new IndeedClass(tempTitle, tempLink);
                        indeedJobs.Add(tempJob);
                   }
                

               }
           }

   
       
        // IList<IWebElement> listOflinks = new List<IWebElement>();

        // for(int i = 0 ; i < links.Count ; i++)
        // {
        //     tempTitle = links[i].Text;
        //     tempLink = links[i].GetAttribute("href");
        //     IndeedClass tempJob = new IndeedClass(tempTitle, tempLink);
        //     indeedJobs.Add(tempJob);
        // }       

        
  

        // WebBrowser wb = new WebBrowser();
        // List<HtmlElement> links = wb.Document.getElementsByTagAndClassName("div", "error");
        // foreach(HtmlElement link in links)
        // {
        //     tempTitle = tempListing.Text;
        //     tempLink = tempListing.GetAttribute("href");
        //     IndeedClass tempJob = new IndeedClass(tempTitle, tempLink);
        //     indeedJobs.Add(tempJob);
        // }       


            return indeedJobs;
   
            // // Extract the text and save it into result.txt
            // var result = driver.FindElementByXPath("//div[@id='case_login']/h3").Text;
            // File.WriteAllText("result.txt", result);
        }
    }
}
