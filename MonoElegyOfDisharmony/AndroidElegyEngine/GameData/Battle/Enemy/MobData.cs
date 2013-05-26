using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.GameData.Battle.Enemy
{
    public class MobData : BattleData
    {

        public MobData(
            string name,
            int health, int techniquePoints,
            int defense, int strength)
            : base(name, health,
            techniquePoints, defense, strength)
        {

        }
    }
}
