using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using RestSharp;

namespace vindinium.scraper.Infrastructure
{
    internal class VindiniumWebClient
    {
        string base_url = "http://vindinium.org";

        private readonly RestClient _client;
        private readonly RestRequest _request;

        public VindiniumWebClient()
        {
            _client = new RestClient(base_url);
            _request = new RestRequest(Method.GET);
        }

        public List<string> GetLinks()
        {
            string request_url = "";
            _request.Resource = request_url;
            var page = _client.Execute(_request).Content;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            var gameList = ExtractAllGameLinks(doc);
            return gameList;
        }


        public string GetEvent(string eventId)
        {
            string request_url = "events/{eventId}";
            _request.Resource = request_url;
            _request.AddUrlSegment("eventId", eventId);

            return _client.Execute(_request).Content;
        }

        private static List<string> ExtractAllGameLinks(HtmlDocument htmlSnippet)
        {
            return
                htmlSnippet.DocumentNode.SelectNodes("//*[@id=\"recent-games\"]/ul/li//a[@href]")
                    .Where(x => x.InnerText.Contains("Finished"))
                    .Select(x => x.Attributes["href"].Value.TrimStart('/'))
                    .ToList();
        }
    }
}