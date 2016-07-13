using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;
using RestSharp;
using vindinium.common.Dtos;
using vindinium.scraper.Infrastructure;

namespace vindinium.scraper
{
    public class Program
    {
        static readonly RestClient Client = new RestClient("http://vindinium.org");
        private static VindiniumWebClient _vwc;
        public static void Main()
        {
            _vwc = new VindiniumWebClient();
            var gameids = _vwc.GetLinks();
            foreach (var gameid in gameids.Take(1))
            {
                ScrapeGameData(gameid);
            }

            Console.WriteLine("test");
            Console.ReadLine();
        }
       

        public static void ScrapeGameData(string gameId)
        {
            var gamedata = _vwc.GetEvent(gameId);

            var gameTurns = gamedata.Split(new string[] { "data: " }, StringSplitOptions.None).ToList();
            gameTurns.RemoveAt(0);
            Game game = new Game();

            foreach (var turn in gameTurns)
            {
                game.Rounds.Add(JsonConvert.DeserializeObject<Round>(turn));
            }
            game.Id = game.Rounds[1].Id;
        }
    }
}