using EquestriEngine.Data;
using EquestriEngine;
using System.Linq;

namespace ElegyGame.GameData.Battle
{
    public delegate void OnAction(BattleData sender, ActionArgs args);

    public class BattleController
    {
        public const int 
            PLAYERS_IN_PARTY = 3,
            MOBS_IN_TROOP = 6;

        private Variable _lastBattleDamage;

        float delay;

        private BattleData[] _characters = null;

        public BattleData Current_Actor = null;

        int character;

        public event OnAction OnActionPerform;

        private bool _selectAction, _pause;

        public bool SelectAction
        {
            get { return _selectAction; }
            set
            {
                if (_selectAction)
                {
                    character++;
                    if (character == _characters.Length)
                    {
                        character = 0;
                    }

                    _lastBattleDamage.AsInt = 4;
                    if (OnActionPerform != null)
                        OnActionPerform(Current_Actor, null);
                    Current_Actor = _characters[character];

                }
                _selectAction = value;
            }
        }

        public bool Paused
        {
            get { return _pause; }
            set
            {
                _pause = value;
            }
        }

        public int LastBattleDamage
        {
            get { return _lastBattleDamage.AsInt; }
        }

        public BattleController()
        {
            _characters = new BattleData[2];
            _characters[0] = Player.CharacterData.Slow_Character;
            _characters[1] = Player.CharacterData.Fast_Character;
            _lastBattleDamage = new Variable(0);
            EngineGlobals.DataManager.SetVariable("{lastdamage}", _lastBattleDamage);
            _selectAction = false;
        }

        public void Init()
        {
            var list = _characters.ToList();
            list.Sort(
                delegate(BattleData lhs, BattleData rhs)
                {
                    return rhs.Speed.CompareTo(lhs.Speed);
                });
            _characters = list.ToArray();
            character = 0;
            Current_Actor = _characters[0];
        }

        public void Update(float dt)
        {
            if (_pause)
                return;
            if (_selectAction)
            {
            }
            else
            {
                delay += dt;
                if (delay > 1)
                {
                    _selectAction = true;
                    //ElegyOfDisharmonyGlobals.GameReference.ConsoleWindow.WriteLine(string.Format("{0} is now battling", Current_Actor.Name));
                    delay = 0;
                }
            }
        }
    }
}
