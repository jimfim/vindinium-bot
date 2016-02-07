using System;
using System.Linq;

using vindinium.Infrastructure.Behaviors.Map;
using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.Behaviors.Movement;
using vindinium.Infrastructure.Behaviors.Tactics;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Bots
{
    public class Robot : IBot
    {
        private readonly Server _server;
        private ITactic _tactic;
        private IMovement _movement;

        public Robot(Server server)
        {
            this._server = server; 
        }

        public string BotName => "Robot";

        //starts everything
        public void Run()
        {
            while (this._server.Finished == false && this._server.Errored == false)
            {
                _tactic = new SurvivalGoldRush(_server);
                _movement = new DefaultMovement(_server);

                var destination = _tactic.NextDestination();

                var route = _movement.GetShortestCompleteRouteToLocation(destination.Location);

                var direction = this._server.GetDirection(_server.MyHero.Location, route.Any() ? route.First().Location : null);

                this._server.MoveHero(direction);

                Console.Out.WriteLine("=========================================");
                Console.Out.WriteLine("Target Location : {0},{1}", destination.Location.X, destination.Location.Y);
                Console.Out.WriteLine("Target Cost \t: {0}", destination.MovementCost);
                Console.Out.WriteLine("Target Type \t: {0}", destination.Type);
                Console.Out.WriteLine("=========================================");
                Console.Out.WriteLine("Hero Location \t: {0},{1}", _server.MyHero.Location.X,_server.MyHero.Location.Y);
                Console.Out.WriteLine("Hero Life \t: {0}", (_server.MyHero as HeroNode).Life);
                Console.Out.WriteLine("Hero Gold \t: {0}", (_server.MyHero as HeroNode).Gold);
                Console.Out.WriteLine("Hero Mines \t: {0}", (_server.MyHero as HeroNode).MineCount);
                Console.Out.WriteLine("Hero Moving \t: {0}", direction);
                Console.Out.WriteLine("=========================================");
                Console.Out.WriteLine("Completed Turn " + this._server.CurrentTurn);
            }

            if (this._server.Errored)
            {
                Console.Out.WriteLine("error: " + this._server.ErrorText);
            }
            Console.Out.WriteLine("{0} Finished", BotName);
        }
    }
}