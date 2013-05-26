using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Inputs
{
    public struct DataInput : Interfaces.IEventInput
    {
        private Data.Interfaces.IDataEntry input;

        public Data.Interfaces.IDataEntry Input
        {
            get { return input; }
            set { input = value; }
        }
    }
}
