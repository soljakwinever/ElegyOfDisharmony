using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Inputs
{
    public struct StringInput : Interfaces.IEventInput
    {
        private string input;

        public string Input
        {
            get { return input; }
            set { input = value; }
        }
    }
}
