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
        Dictionary<ControlTypes, Keys[]> _keyAllocations;
#if DEBUG
        private const string
            CONSOLE_KEY = "{console}",
            CONSOLE_SWITCH_KEY = "{cswitch}",
            VARPAGE_SWITCH_KEY = "{vswitch",
            SWTPAGE_SWITCH_KEY = "{sswitch}";
#endif


        Dictionary<ControlTypes, InputControl> _inputs;

        public float X
        {
            get { return _x; }
        }
        public float Y
        {
            get { return _y; }
        }

        public InputControl this[ControlTypes control]
        {
            get { return _inputs[control]; }
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
            _inputs = new Dictionary<ControlTypes,InputControl>();
            _inputs[ControlTypes.Interaction] = new InputControl();
            _keyAllocations = new Dictionary<ControlTypes, Keys[]>();
            InitDefault();
        }

        private void InitDefault()
        {
            _keyAllocations[ControlTypes.Interaction] = new Keys[]
                {
                    Keys.E
                };

            _keyAllocations[ControlTypes.Jump] = new Keys[]
                {
                    Keys.Q,
                    Keys.Space
                };
            _keyAllocations[ControlTypes.Menu] = new Keys[]
                {
                    Keys.Escape
                };
            _keyAllocations[ControlTypes.CONSOLE] = new Keys[]
                {
                    Keys.OemTilde
                };

            foreach (ControlTypes t in _keyAllocations.Keys)
            {
                _inputs[t] = new InputControl();
            }
        }

        public static KeyboardControl DefaultControls
        {
            get
            {
                KeyboardControl controls = new KeyboardControl();
                controls._keyAllocations[ControlTypes.Interaction] = new Keys[]
                {
                    Keys.E
                };
                
                controls._keyAllocations[ControlTypes.Jump] = new Keys[]
                {
                    Keys.Q,
                    Keys.Space
                };
                controls._keyAllocations[ControlTypes.Menu] = new Keys[]
                {
                    Keys.Escape
                };
                controls._keyAllocations[ControlTypes.CONSOLE] = new Keys[]
                {
                    Keys.OemTilde
                };

                foreach (ControlTypes t in controls._keyAllocations.Keys)
                {
                    controls._inputs[t] = new InputControl();
                }

                return null;
            }
        }

        public void SaveControls(System.IO.BinaryWriter bw)
        {
            //bw.Write(true);
            //foreach (var kvp in _keyAllocations)
            //{
            //    bw.Write(kvp.Key[0]);
            //    bw.Write(kvp.Value.Length);
            //    for (int i = 0; i < kvp.Value.Length; i++)
            //        bw.Write((long)kvp.Value[i]);
            //}
            //bw.Write(false);
        }

        public static void LoadControls(out KeyboardControl k, System.IO.BinaryReader br)
        {
            k = new KeyboardControl();
            //bool working;
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

            foreach (var kvp in _inputs)
            {
                kvp.Value.Value = ks.IsKeyDown(_keyAllocations[kvp.Key][0]) && pks.IsKeyUp(_keyAllocations[kvp.Key][0]);
            }
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
