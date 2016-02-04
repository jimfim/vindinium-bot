using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vindinium.Infrastructure.Behaviors.Extensions;
using vindinium.Infrastructure.Behaviors.Map;
using vindinium.Infrastructure.Behaviors.Models;

namespace vindinium.Infrastructure.Behaviors.Tactics
{
    public class SurvivalGoldRush : ITactic
    {
        private readonly IMapBuilder game;

        public SurvivalGoldRush(IMapBuilder game)
        {
            this.game = game;
        }

        public IMapNode NextDestination()
        {
            var hero = game.MyHero as HeroNode;
            if (hero != null && hero.Life < 30 )
            {
                return game.GetClosestTavern();
            }
            if (hero != null && (hero.Life < 90 && this.game.GetClosestTavern().MovementCost < 2))
            {
                return game.GetClosestTavern();
            }
            return game.GetClosestChest();
        }
    }
}
