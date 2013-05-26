using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Collections
{
    public class MethodParamCollection : List<Inputs.MethodParamPair>
    {
        //Requires the players input to continue
        private bool requiresInput;

        public bool RequireInput
        {
            get { return requiresInput; }
            set { requiresInput = value; }
        }
    }
}
