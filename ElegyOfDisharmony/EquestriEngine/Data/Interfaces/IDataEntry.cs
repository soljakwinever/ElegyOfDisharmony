using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Interfaces
{
    public interface IDataEntry
    {
        string Name
        {
            get;
        }

        event GenericEvent OnValueChange;
    }
}
