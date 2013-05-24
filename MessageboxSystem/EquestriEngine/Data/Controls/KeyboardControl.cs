using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EquestriEngine.Data.Controls
{
    public class KeyboardControl : IControlScheme
    {
        private KeyboardState ks, pks;
        private Keys[] _allocatedKeys;
        private float _x, _y;

        //Controls
        Dictionary<string, Keys[]> _keyAllocations;
        private const string
            INTERACTION_KEY = "{interaction}",
            JUMP_KEY = "{jump}",
            MENU_KEY = "{menu}",
            LEFT_KEY = "{left}",
            RIGHT_KEY = "{right}",
            UP_KEY = "{up}",
            DOWN_KEY = "{down}";
#if DEBUG
        private const string
            CONSOLE_KEY = "{console}",
            CONSOLE_SWITCH_KEY = "{cswitch}",
            VARPAGE_SWITCH_KEY = "{vswitch",
            SWTPAGE_SWITCH_KEY = "{sswitch}";
#endif

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
            _keyAllocations = new Dictionary<string, Keys[]>();
            _keyAllocations[INTERACTION_KEY] = new Keys[]
            {
                Keys.E
            };
        }

        public static KeyboardControl DefaultControls
        {
            get
            {
                KeyboardControl controls = new KeyboardControl();
                controls._keyAllocations[INTERACTION_KEY] = new Keys[]
                {
                    Keys.E
                };
                controls._keyAllocations[JUMP_KEY] = new Keys[]
                {
                    Keys.Q,
                    Keys.Space
                };
                controls._keyAllocations[MENU_KEY] = new Keys[]
                {
                    Keys.Escape
                };
                controls._keyAllocations[MENU_KEY] = new Keys[]
                {
                    Keys.Escape
                };
                controls._keyAllocations[MENU_KEY] = new Keys[]
                {
                    Keys.Escape
                };

                return null;
            }
        }

        public void SaveControls(System.IO.BinaryWriter bw)
        {
            bw.Write(true);
            foreach (var kvp in _keyAllocations)
            {
                bw.Write(kvp.Key);
                bw.Write(kvp.Value.Length);
                for (int i = 0; i < kvp.Value.Length; i++)
                    bw.Write((long)kvp.Value[i]);
            }
            bw.Write(false);
        }

        public static void LoadControls(out KeyboardControl k, System.IO.BinaryReader br)
        {
            k = new KeyboardControl();
            //bool working;
        }

        public bool Input1()
        {
            bool val = false;
            foreach (Keys k in _keyAllocations[INTERACTION_KEY])
            {
                if (ks.IsKeyDown(k)
                    && pks.IsKeyUp(k))
                    val = true;
            }
            return val;
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

        /*public bool SwitchSwitchPageButton()
        {

        }*/

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
            if (ks.IsKeyDown(Keys.Right))
                _x = 1;
            //Right Key
            else if (ks.IsKeyDown(Keys.Left))
                _x = -1;
            else _x = 0;
        }

    }
}
