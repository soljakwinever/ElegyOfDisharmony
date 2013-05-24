using PlayerIndex = Microsoft.Xna.Framework.PlayerIndex;
using Microsoft.Xna.Framework.Input;

namespace EquestriEngine.Data.Controls
{
    /*
    public class GamepadControl : IControlScheme
    {
        private GamePadState gs, pgs;
        private Buttons[] _allocatedButtons;
        private float _x, _y;
        private PlayerIndex _index;

        public float X
        {
            get { return _x; }
        }
        public float Y
        {
            get { return _y; }
        }

        public GamepadControl(PlayerIndex index)
        {
            _allocatedButtons = new Buttons[]
            {
                Buttons.X,
                Buttons.A,
                Buttons.RightShoulder,
                Buttons.LeftShoulder,
                Buttons.Y,
                Buttons.B
            };

            _index = index;
        }

        public bool Input1()
        {
            return ButtonPressed(_allocatedButtons[0]);
        }

        public bool Input2()
        {
            return ButtonPressed(_allocatedButtons[1]);
        }

        public bool Input3()
        {
            return ButtonPressed(_allocatedButtons[2]);
        }

        public bool Input4()
        {
            return ButtonPressed(_allocatedButtons[3]);
        }

        public bool Input5()
        {
            return ButtonPressed(_allocatedButtons[4]);
        }

        public bool Input6()
        {
            return ButtonPressed(_allocatedButtons[5]);
        }

#if DEBUG

        public bool ConsoleButton()
        {
            return ButtonPressed(Buttons.Back);
        }

        public bool ConsoleSwitchButton()
        {
            return ButtonPressed(Buttons.Start);
        }

#endif

        public void Update()
        {
            pgs = gs;
            gs = GamePad.GetState(_index);
            
            _x = -gs.ThumbSticks.Left.X;
            _y = gs.ThumbSticks.Left.Y;
        }

        private bool ButtonPressed(Buttons button)
        {
            return gs.IsButtonDown(button) && pgs.IsButtonUp(button);
        }

        private bool ButtonDown(Buttons button)
        {
            return gs.IsButtonDown(button);
        }
    }
     * */
}
