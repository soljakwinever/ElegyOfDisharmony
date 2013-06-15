using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquestriEngine.Data.Scenes;

namespace EquestriEngine.MenuData.Inputs
{
    public class Button : SelectableObject, Interfaces.IControlSelectable
#if WINDOWS
        ,Interfaces.IMouseSelectable
#endif
    {
        private int _tabIndex;

        public int TabIndex
        {
            get { return _tabIndex; }
            set { }
        }

#if WINDOWS

        public Rectangle MouseRect
        {
            get { return new Rectangle(0, 0, 32, 32); }
        }

#endif

        public override void Init()
        {
            base.Init();
            Enabled = true;
        }

        public override void Update(float dt)
        {
            if (!Enabled)
                return;
        }

        public override void Draw(float dt)
        {
            throw new NotImplementedException();
        }
    }
}
