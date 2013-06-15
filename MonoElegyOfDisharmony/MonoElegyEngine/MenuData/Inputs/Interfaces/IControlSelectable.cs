using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.MenuData.Inputs.Interfaces
{
    public interface IControlSelectable
    {
        int TabIndex
        {
            get;
            set;
        }
    }
}
