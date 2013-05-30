using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElegyGame.GameData.Battle.Player
{
    public class CharacterData : BattleData
    {
        private float _harmonyBurst;

        /// <summary>
        /// A value of 0-1 determining the progress to a harmony burst
        /// </summary>
        public float HarmonyBurst
        {
            get { return _harmonyBurst; }
        }

        public CharacterData()
            : base()
        {
            _harmonyBurst = 0;
            _inventory = new Dictionary<Item.ItemData, int>();
        }

        public override string ToString()
        {
            return Name;
        }

        public void AddItem(

        public static CharacterData Fast_Character
        {
            get
            {
                CharacterData _data = new CharacterData();
                _data.Defense = 1;
                _data.Health = 10;
                _data.Name = "FAST PONY";
                _data.Speed = 4;
                return _data;
            }
        }

        public static CharacterData Slow_Character
        {
            get
            {
                CharacterData _data = new CharacterData();
                _data.Defense = 1;
                _data.Health = 10;
                _data.Name = "SLOW PONY";
                _data.Speed = 2;
                return _data;
            }
        }
    }
}
