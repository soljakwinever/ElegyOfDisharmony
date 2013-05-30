using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Inputs
{
    public struct ControlInput : Interfaces.IEventInput
    {
        private Controls.InputControl _control;

        public Controls.InputControl Control
        {
            get { return _control; }
            set { _control = value; }
        }
    }
}
