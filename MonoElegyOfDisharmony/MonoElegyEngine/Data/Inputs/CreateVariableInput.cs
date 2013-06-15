using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Inputs
{
    public struct CreateVariableInput : Interfaces.IEventInput
    {
        private string _name;
        private object _value;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
