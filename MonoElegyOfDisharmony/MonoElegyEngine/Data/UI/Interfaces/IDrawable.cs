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

        bool Visible
        {
            get;
        }

        void LoadContent();
        void UnloadContent();
        void Draw(float dt);

        void Show();
        void Hide();

        event GenericEvent
            OnWindowShow,
            OnWindowHide;
    }
}
