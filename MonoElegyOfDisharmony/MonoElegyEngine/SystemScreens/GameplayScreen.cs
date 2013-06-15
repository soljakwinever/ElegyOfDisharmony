using EquestriEngine.Data.UI;
using EquestriEngine.Objects.Graphics;
using EquestriEngine.Data.Scenes;

namespace EquestriEngine.SystemScreens
{
    public abstract class GameplayScreen : DrawableGameScreen, Data.UI.Interfaces.IInputReciever
    {
        private bool _hasFocus;

        public bool HasFocus
        {
            get { return _hasFocus; }
            set { _hasFocus = value; }
        }

        public GameplayScreen()
            : base(false)
        {

        }

        public abstract void HandleInput(float dt);
    }
}
