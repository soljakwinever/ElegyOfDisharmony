using Microsoft.Xna.Framework.Input;

namespace EquestriEngine.Data.Controls
{
    public class KeyboardControl : IControlScheme
    {
        private KeyboardState ks, pks;
        private Keys[] _allocatedKeys;
        private float _x, _y;

        public float X
        {
            get { return _x; }
        }
        public float Y
        {
            get { return _y; }
        }

        public KeyboardControl()
        {
            _allocatedKeys = new Keys[]
            {
                Keys.E,
                Keys.Q,
                Keys.D1,
                Keys.D3,
                Keys.Z,
                Keys.C,
                Keys.Up,
                Keys.Down,
                Keys.Left,
                Keys.Right
            };
        }

        public bool Input1()
        {
            return ks.IsKeyDown(_allocatedKeys[0]) 
                && pks.IsKeyUp(_allocatedKeys[0]);
        }

        public bool Input2()
        {
            return ks.IsKeyDown(_allocatedKeys[1])
                && pks.IsKeyUp(_allocatedKeys[1]);
        }

        public bool Input3()
        {
            return ks.IsKeyDown(_allocatedKeys[2])
                && pks.IsKeyUp(_allocatedKeys[2]);
        }

        public bool Input4()
        {
            return ks.IsKeyDown(_allocatedKeys[3])
                && pks.IsKeyUp(_allocatedKeys[3]);
        }

        public bool Input5()
        {
            return ks.IsKeyDown(_allocatedKeys[4])
                && pks.IsKeyUp(_allocatedKeys[4]);
        }

        public bool Input6()
        {
            return ks.IsKeyDown(_allocatedKeys[5])
                && pks.IsKeyUp(_allocatedKeys[5]);
        }
#if DEBUG

        public bool ConsoleButton()
        {
            return ks.IsKeyDown(Keys.OemTilde)
                && pks.IsKeyUp(Keys.OemTilde);
        }

        public bool ConsoleSwitchButton()
        {
            return ks.IsKeyDown(Keys.F1)
                && pks.IsKeyUp(Keys.F1);
        }

#endif

        public void Update()
        {
            pks = ks;
            ks = Keyboard.GetState();
            //Up Key
            if (ks.IsKeyDown(_allocatedKeys[6]))
                _y = 1;
            //Down Key
            else if (ks.IsKeyDown(_allocatedKeys[7]))
                _y = -1;
            else _y = 0;
            //Left Key
            if (ks.IsKeyDown(_allocatedKeys[8]))
                _x = 1;
            //Right Key
            else if (ks.IsKeyDown(_allocatedKeys[9]))
                _x = -1;
            else _x = 0;
        }

    }
}
