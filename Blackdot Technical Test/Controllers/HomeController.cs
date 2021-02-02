using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Blackdot_Technical_Test.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace Blackdot_Technical_Test.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string searchString)
        {

            ViewBag.searchString = searchString;

            List<SearchResult> searchResults = new List<SearchResult>();

            using (WebClient client = new WebClient())
            {

                

                List<String> searchURLs = new List<string>()
                {
                    "https://www.bing.com/search?count=30&q=" + searchString,
                    "https://uk.search.yahoo.com/search?b=10&p=" + searchString
                };

                foreach (string URL in searchURLs)
                {
                    string HtmlStr = client.DownloadString(URL);
                    var HtmlDoc = new HtmlDocument();
                    HtmlDoc.LoadHtml(HtmlStr);
                    var results = HtmlDoc.DocumentNode.SelectNodes("//li");
                    foreach (var result in results)
                    {
                        if (result.OuterHtml.Contains("b_algo") || result.InnerHtml.Contains("algo-sr"))
                        {
                            SearchResult sr = new SearchResult();
                            sr.Title = result.SelectSingleNode(".//a").InnerText;
                            sr.Link = result.SelectSingleNode(".//a").Attributes["href"].Value;
                            sr.Description = result.SelectSingleNode(".//p").InnerText;
                            sr.SearchEngine = searchURLs.IndexOf(URL);
                            searchResults.Add(sr);
                        }
                    }
                }

                ////YAHOO
                //string yahooStr = "https://uk.search.yahoo.com/search?n=30&p=" + searchString;
                //string yHtmlStr = client.DownloadString(yahooStr);
                //var yHtmlDoc = new HtmlDocument();
                //yHtmlDoc.LoadHtml(yHtmlStr);
                //var yResults = yHtmlDoc.DocumentNode.SelectNodes("//li");

                //foreach (var result in yResults)
                //{
                //    if (result.InnerHtml.Contains("algo-sr"))
                //    {
                //        SearchResult sr = new SearchResult();
                //        sr.Title = result.SelectSingleNode(".//a").InnerText;
                //        sr.Link = result.SelectSingleNode(".//a").Attributes["href"].Value;
                //        sr.Description = result.SelectSingleNode(".//p").InnerText;
                //        searchResults.Add(sr);
                //    }
                //}

                //Bing
                //string bingStr = "https://www.bing.com/search?q=" + searchString;
                //string bHtmlStr = client.DownloadString(bingStr);
                //var bHtmlDoc = new HtmlDocument();
                //bHtmlDoc.LoadHtml(bHtmlStr);
                //var bResults = bHtmlDoc.DocumentNode.SelectNodes("//li");
                //foreach (var result in bResults)
                //{
                //    if (result.OuterHtml.Contains("b_algo"))
                //    {
                //        SearchResult sr = new SearchResult();
                //        sr.Title = result.SelectSingleNode(".//a").InnerText;
                //        sr.Link = result.SelectSingleNode(".//a").Attributes["href"].Value;
                //        sr.Description = result.SelectSingleNode(".//p").InnerText;
                //        searchResults.Add(sr);
                //    }
                //}


            }



            return View(searchResults);
        }
    }
}
