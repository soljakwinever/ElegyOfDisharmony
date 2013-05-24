using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Inputs
{
    public enum CompareType
    {
        Switch_Val,
        Var_Var,
        Var_Int,
        Var_String,
        Var_Gold,
        Var_PlayerX,
        Var_PlayerY,
        Var_ProfileName,
        Achievement_Unlocked
    }

    public struct ConditionInput : Interfaces.IEventInput
    {
        private Data.Interfaces.IDataEntry _inputA;
        private Data.Interfaces.IDataEntry _inputB;

        private CompareType _comparison;
        private CheckValue checkVal;

        public Data.Interfaces.IDataEntry InputA
        {
            get { return _inputA; }
            set { _inputA = value; }
        }

        public Data.Interfaces.IDataEntry InputB
        {
            get { return _inputB; }
            set { _inputB = value; }
        }

        public CompareType Comparison
        {
            get { return _comparison; }
            set 
            { 
                _comparison = value;
                switch (_comparison)
                {
                    case CompareType.Var_Gold:
                        _inputB = Systems.DataManager.GetVariable("{gold}");
                        break;
                    case CompareType.Var_ProfileName:
                        _inputB = Systems.DataManager.GetVariable("{profile_name}");
                        break;
                }
            }
        }

        public CheckValue CheckValue
        {
            get { return checkVal; }
            set { checkVal = value; }
        }
    }
}
