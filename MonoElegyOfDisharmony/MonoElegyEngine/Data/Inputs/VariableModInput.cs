using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Inputs
{
    public struct VariableModInput : Interfaces.IEventInput
    {
        private Variable input_l, input_r;
        private AdjustType adjustType;

        public Variable InputL
        {
            get { return input_l; }
            set { input_l = value; }
        }

        public Variable InputR
        {
            get { return input_r; }
            set { input_r = value; }
        }

        public AdjustType AdjustmentType
        {
            get { return adjustType; }
            set { adjustType = value; }
        }
    }
}
