using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.UI
{
    public abstract class DrawableGameScreen : GameScreen, Interfaces.IDrawable
    {
        private bool
            _coversOthers,
            _isCovered,
            _onTop;

        public bool OnTop
        {
            get { return _onTop; }
            set { _onTop = value; }
        }

        public bool IsCovered
        {
            get { return _isCovered; }
            set { _isCovered = value; }
        }

        public bool CoversOthers
        {
            get { return _coversOthers; }
        }

        public DrawableGameScreen(bool coversOthers)
            : base()
        {
            _coversOthers = coversOthers;
        }

        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Draw(float dt);
    }
}
