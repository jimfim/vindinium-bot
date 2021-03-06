﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using vindinium.Infrastructure.Behaviors.Extensions;
using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Tactics
{
    /// <summary>
    /// Charges for closest un-owned goldmine
    /// </summary>
    public class DumbGoldRush : ITactic
    {
        private readonly IMapBuilder _game;

        public DumbGoldRush(IMapBuilder game)
        {
            this._game = game;
        }

        public Node NextDestination()
        {
            return _game.GetClosestChest();
        }
    }
}
