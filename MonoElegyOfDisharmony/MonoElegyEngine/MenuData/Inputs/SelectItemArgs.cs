using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquestriEngine.MenuData.Inputs
{
    public struct SelectItemArgs
    {
        private bool _selected;
        private bool _hovering;

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public bool Hovering
        {
            get { return _hovering; }
            set { _hovering = value; }
        }
    }
}
