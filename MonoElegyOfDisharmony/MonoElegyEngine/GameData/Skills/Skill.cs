using EquestriEngine.GameData.Battle;

namespace EquestriEngine.GameData.Skills
{
    public class Skill
    {
        private int _tpCost;
        private string _name;

        public string Name
        {
            get { return _name; }
        }

        public Skill()
        {
            _tpCost = 0;
            _name = "Unnamed";
        }

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
