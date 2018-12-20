#### Job Hunter 12/20/18

#### By **Leilani Leach, Manasa Vesala, James Cho, and Alex Williams**

## Description

Allows users to input a single search term and view results from several job sites on a single page.

### Specs

* User inputs a search term (job title and location)
* Selenium opens another Chrome instance and enters the search term on the selected job site(s)
* If a single job site has been selected, the search results from that site will be displayed to the user on the same page
* If all the jobs sites have been selected, the search results from each site will be displayed together on the same page
* Each search result will display the title of the job posting and company, plus the city and date posted if applicable.
* The user can click any of the search results to open that job posting URL

## Setup/Installation Requirements

1. Clone this repository.
2. From the command line, navigate to JobSearch.Solution/JobSearch
3. From the command line, enter _dotnet restore_ to install necessary packages
4. From the command line, enter _dotnet build_
5. From the command line, enter _dotnet run_
6. In the Chrome browser, navigate to localhost:5000
7. When done using the program, enter _Ctrl + C_ in the command line to exit 

## Known Bugs
* Searching Craigslist with a state instead of a city will cause a DNS error.

## Technologies Used
* HTML
* CSS
* Bootstrap
* C#
* Selenium WebDriver
* ASP.Net Core MVC

## Support and Contact Details

_Email leilanileach@yahoo.com, vesalamanasa@gmail.com_

### License

*MIT License*

Copyright (c) 2018 **_Leilani Leach, Manasa Vesala, James Cho, and Alex Williams_**

