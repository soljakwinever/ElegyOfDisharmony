using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data
{
    public enum CheckValue
    {
        ValueON = 0x01,    //SWITCH Exclusive
        ValueOFF = 0x02,   //SWITCH Exclusive
        ValueEqual = 0x04, //Used for Ints and strings
        ValueGreater = 0x10, //Used for ints
        ValueLess = 0x11, //Used for ints
    }

    public class Achievement
    {
        Interfaces.IDataEntry _data;
        CheckValue _checkValue;
        object _value;

        private bool _unlocked;

        private int 
            _iconX, 
            _iconY;

        private string 
            _name, 
            _desc,
            _dataName;

        Inputs.MethodParamPair _rewardMethod;

        public event GenericEvent OnAchievementUnlocked;

        private const string ACHIEVEMENT_SHEET = "{achievement}";

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public string DataName
        {
            get { return _dataName; }
            set { _dataName = value; }
        }

        public string Reward
        {
            get { return ""; }
            set 
            {
                _rewardMethod = EngineGlobals.GenerateMethodFromString(value);
            }
        }

        public bool Unlocked
        {
            get { return _unlocked; }
        }

        public CheckValue Value
        {
            get { return _checkValue; }
            set { _checkValue = value; }
        }

        public string CheckFor
        {
            get { return (string)_value; }
            set 
            {
                _value = value;
            }
        }

        public int IconX
        {
            get { return _iconX; }
            set
            {
                if (value > 5)
                    throw new Exception("Out of sheet bounds");
                _iconX = value;
            }
        }

        public int IconY
        {
            get { return _iconY; }
            set
            {
                if (value > 0)
                    throw new Exception("Out of sheet bounds");
                _iconY = value;
            }
        }

        public Achievement()
        {

        }
        ~Achievement()
        {
            OnAchievementUnlocked = null;
        }

        public Achievement(Interfaces.IDataEntry data, CheckValue cval = CheckValue.ValueEqual, object val2Check = null)
        {
            if (val2Check != null && !(val2Check is string ^ val2Check is int))
                throw new Exception("Invalid Data type");

            if (data is Switch)
            {
                switch (cval)
                {
                    case CheckValue.ValueEqual:
                        _checkValue = CheckValue.ValueON;
                        break;
                    case CheckValue.ValueGreater:
                    case CheckValue.ValueLess:
                        throw new Exception("Invalid Check Type - Numeral Booleans cannot be used on Switches");
                    default :
                        _checkValue = cval;
                        break;
                }
            }
            if (data is Variable)
            {
                switch (cval)
                {
                    case CheckValue.ValueOFF:
                    case CheckValue.ValueON:
                        throw new Exception("Invalid Check Type - Switch Checks cannot be used on Variables");
                    case CheckValue.ValueGreater:
                    case CheckValue.ValueLess:
                        if (val2Check is string)
                            throw new Exception("Invalid Check Value - Cannot test Numeral Booleans on String Variables");
                        break;
                    default:
                        _checkValue = cval;
                        break;
                }
            }
            
            _value = val2Check;
            _unlocked = false;
            _data = data;
            data.OnValueChange += ValueCheck;
        }

        public void RegisterData(Interfaces.IDataEntry data)
        {
            _data = data;
            data.OnValueChange += ValueCheck;
        }

        public override string ToString()
        {
            return string.Format("-{0}-\n{1}", _name, _desc);
        }

        public void ValueCheck(object sender, Inputs.Interfaces.IEventInput input)
        { 
            //First test will use Switches only
            if (!_unlocked && sender == _data)
            {
                if (sender is Switch)
                {
                    switch (_checkValue)
                    {
                        case CheckValue.ValueON:
                            if ((sender as Switch).Value)
                                GiveReward();
                            break;
                        case CheckValue.ValueOFF:
                            if (!(sender as Switch).Value)
                                GiveReward();
                            break;
                    }

                }
                if (sender is Variable)
                {
                    switch (_checkValue)
                    {
                        case CheckValue.ValueEqual:
                            if ((sender as Variable).EqualTo(_value))
                                GiveReward();
                            break;
                        case CheckValue.ValueLess:
                            if ((sender as Variable).LessThan(_value))
                                GiveReward();
                            break;
                        case CheckValue.ValueGreater:
                            if ((sender as Variable).GreaterThan(_value))
                                GiveReward();
                            break;
                    }
                }
            }
        }


        public void GiveReward()
        {
            Systems.ConsoleWindow.WriteLine("{0}\n{1}\nAchievement Get!",_name,_desc);
            //var temp = new SystemWidgets.AchievementDisplay(8.0f,this);
            //EngineGlobals.GameReference.WidgetDrawer.AddWidget(temp);

            if (OnAchievementUnlocked != null)
            {
                OnAchievementUnlocked(this, null);
            }
            if (_rewardMethod != null)
                _rewardMethod.ExecuteMethod(this);
            _unlocked = true;
        }
    }
}
