using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using NHibernate;
using RestSharp;
using vindinium.common.Data;
using vindinium.common.Dtos;
using vindinium.common.Entities;
using vindinium.common.Mappings;
using vindinium.scraper.Infrastructure;

namespace vindinium.scraper
{
    public class Program
    {
        static readonly RestClient Client = new RestClient("http://vindinium.org");
        private static VindiniumWebClient _vwc;
        private static ISessionFactory sessionFactory;
        private static IMapper mapper;
        public static void Main()
        {
            StartUp();

            var gameids = _vwc.GetRecentGameLinks();
            foreach (var gameid in gameids.Distinct())
            {
                var game = ScrapeGameData(gameid);
                foreach (var roundDto in game.Rounds)
                {
                    var round = mapper.Map<Round>(roundDto);
                    using (var session = sessionFactory.OpenSession())
                    {
                        using (var transaction = session.BeginTransaction())
                        {
                            session.SaveOrUpdate(round);
                            transaction.Commit();
                        }
                    }
                }
                Task.Delay(3000);
            }

            Console.WriteLine("test");
            Console.ReadLine();
        }

        private static void StartUp()
        {
            sessionFactory = DbConnect.InitializeDBconnection();
            _vwc = new VindiniumWebClient();


            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<EntityMapper>();
            });
            mapper = config.CreateMapper();
        }


        public static GameDTO ScrapeGameData(string gameId)
        {
            var gamedata = _vwc.GetEvent(gameId);

            var gameTurns = gamedata.Split(new string[] { "data: " }, StringSplitOptions.None).ToList();
            gameTurns.RemoveAt(0);
            GameDTO gameDto = new GameDTO();

            foreach (var turn in gameTurns)
            {
                gameDto.Rounds.Add(JsonConvert.DeserializeObject<RoundDTO>(turn));
            }
            gameDto.Id = gameDto.Rounds[1].Id;

            return gameDto;
        }
    }
}