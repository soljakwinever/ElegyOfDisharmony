using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.UI.Interfaces
{
    public interface IDrawable
    {
        bool CoversOthers
        {
            get;
        }

        bool OnTop
        {
            get;
            set;
        }

        bool IsCovered
        {
            get;
            set;
        }

        void LoadContent();
        void UnloadContent();
        void Draw(float dt);

        event GenericEvent
            OnWindowShow,
            OnWindowHide;
    }
}
