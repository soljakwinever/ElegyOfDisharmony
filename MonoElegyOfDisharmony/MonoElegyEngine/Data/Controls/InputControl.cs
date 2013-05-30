using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Controls
{
    public class InputControl
    {
        bool _value;

        public bool Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
