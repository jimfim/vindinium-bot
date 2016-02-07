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
              //  _mapBuilder = new DefaultMapBuilder(_server);
                _tactic = new SurvivalGoldRush(_server);
                _movement = new DefaultMovement(_server);

                var destination = _tactic.NextDestination();
                var route = _movement.GetShortestCompleteRouteToLocation(destination.Location);
                var direction = this._server.GetDirection(_server.MyHero.Location, route.Any() ? route.First().Location : null);
                this._server.MoveHero(direction);
                Console.Out.WriteLine("Target {0}", destination.Type);
                Console.Out.WriteLine("Moving {0}", direction);
                Console.Out.WriteLine("completed turn " + this._server.CurrentTurn);
            }

            if (this._server.Errored)
            {
                Console.Out.WriteLine("error: " + this._server.ErrorText);
            }
            Console.Out.WriteLine("{0} Finished", BotName);
        }
    }
}