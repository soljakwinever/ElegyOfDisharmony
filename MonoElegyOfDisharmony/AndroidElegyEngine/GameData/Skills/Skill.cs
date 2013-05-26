using EquestriEngine.GameData.Battle;

namespace EquestriEngine.GameData.Skills
{
    public class Skill
    {
        private int _tpCost;

        public bool UseSkill(BattleData user)
        {
            if (user.TechniquePoints < _tpCost)
                return false;
            else
            {
                user.TechniquePoints -= _tpCost;
                return true;
            }
        }
    }
}
