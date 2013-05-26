using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Inputs
{
    public enum SAdjustType
    {
        Set,
        SetToProfileName,
        SetToPartyMemberName,
        SetToKeyboardInput,
        SetToMapName,
        SetToItemName,
        SetToAchievement
    }

    public struct StringModInput : Interfaces.IEventInput
    {
        private Variable varInput;
        private int stringInput;
        private SAdjustType adjustType;

        public Variable VarInput
        {
            get { return varInput; }
            set { varInput = value; }
        }

        public int StringInput
        {
            get { return stringInput; }
            set { stringInput = value; }
        }

        public SAdjustType AdjustmentType
        {
            get { return adjustType; }
            set { adjustType = value; }
        }
    }
}
