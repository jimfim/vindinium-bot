using vindinium.Infrastructure.Behaviors.Extensions;
using vindinium.Infrastructure.Behaviors.Map;

namespace vindinium.Infrastructure.Behaviors.Tactics
{
    public class SurvivalGoldRush : ITactic
    {
        private readonly Server _game;

        public SurvivalGoldRush(Server game)
        {
            this._game = game;
        }

        public IMapNode NextDestination()
        {
            var hero = _game.MyHero as HeroNode;
            if (hero == null || _game.GetClosestChest() == null)
            {
                return _game.GetClosestTavern();
            }

            if ((hero.Life < 30) || (hero.Life < 90 && this._game.GetClosestTavern().MovementCost < 2))
            {
                return _game.GetClosestTavern();
            }

            return _game.GetClosestChest();
        }
    }
}
