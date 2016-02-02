using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vindinium.Infrastructure.Behaviors.Extensions;
using vindinium.Infrastructure.Behaviors.Models;

namespace vindinium.Infrastructure.Behaviors.Tactics
{
    public class SurvivalGoldRush : ITactic
    {
        private readonly IMapBuilder _game;

        public SurvivalGoldRush(IMapBuilder game)
        {
            this._game = game;
        }

        public Node NextDestination()
        {
            if (_game.MyHero.life < 30 )
            {
                return _game.GetClosestTavern();
            }
            if (_game.MyHero.life < 90 && _game.GetClosestTavern().MovementCost < 2)
            {
                return _game.GetClosestTavern();
            }
            return _game.GetClosestChest();
        }
    }
}
