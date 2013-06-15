using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElegyGame.GameData.Map.Character.Enemy
{
    /// <summary>
    /// Represents an enemy walking around on the map, will actively seek the player
    /// when the player is in range and in sight, and it will usually trigger a battle
    /// on contact with the enemy
    /// </summary>
    public class Enemy : Character
    {
        public override void Update(float dt)
        {

        }
    }
}
