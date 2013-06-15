using ElegyGame.GameData.Skills;
using System.Collections.Generic;

namespace ElegyGame.GameData.Battle
{
    public abstract class BattleData
    {
        private string
            _name;
        private int
            _health,
            _techniquePoints,
            _speed,
            _defense,
            _strength;

        private byte
            _attackOrder;
        //private Node 
        //   _characterNode;

        private Skill[] _skills;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public int TechniquePoints
        {
            get { return _techniquePoints; }
            set { _techniquePoints = value; }
        }

        public int Defense
        {
            get { return _defense; }
            set { _defense = value; }
        }

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public int Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        public Skill[] Skills
        {
            get { return _skills; }
            set { _skills = value; }
        }

        /// <summary>
        /// Set each time a battle starts
        /// </summary>
        public byte AttackOrder
        {
            get { return _attackOrder; }
            set { _attackOrder = value; }
        }

        public BattleData(string name, int health, int tp,
            int defense, int strength)
        {

        }

        protected BattleData()
        {

        }
    }
}
