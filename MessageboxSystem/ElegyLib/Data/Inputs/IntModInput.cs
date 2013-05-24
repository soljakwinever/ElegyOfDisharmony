using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Inputs
{
    public enum AdjustType
    {
        Set,
        SetToGold,
        SetToPlayerX,
        SetToPlayerY,
        Addition,
        Subtract,
        Divide,
        Multiply,
        Modulus
    }

    public struct IntModInput : Interfaces.IEventInput
    {
        private Variable varInput;
        private int intInput;
        private AdjustType adjustType;

        public Variable VarInput
        {
            get { return varInput; }
            set { varInput = value; }
        }

        public int IntInput
        {
            get { return intInput; }
            set { intInput = value; }
        }

        public AdjustType AdjustmentType
        {
            get { return adjustType; }
            set { adjustType = value; }
        }
    }
}
