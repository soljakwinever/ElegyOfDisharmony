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

        private Data.Collections.ActionList 
            _path1,
            _path2;
        private bool _hasElse;

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

        public Data.Collections.ActionList Path1
        {
            get { return _path1; }
            set { _path1 = value; }
        }

        public Data.Collections.ActionList Path2
        {
            get { return _path2; }
            set 
            { 
                _path2 = value;
                _hasElse = value != null;
            }
        }

        public bool HasElse
        {
            get { return _hasElse; }
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
                        _inputA = Systems.DataManager.GetVariable("{gold}");
                        break;
                    case CompareType.Var_ProfileName:
                        _inputA = Systems.DataManager.GetVariable("{profile_name}");
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
