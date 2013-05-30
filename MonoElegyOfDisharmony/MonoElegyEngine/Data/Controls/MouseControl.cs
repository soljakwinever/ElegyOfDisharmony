using Microsoft.Xna.Framework.Input;

namespace EquestriEngine.Data.Controls
{
    public class MouseControl : IControlScheme
    {
        MouseState ms, pms;

        public float X
        {
            get { return 0; }
        }

        public float Y
        {
            get { return 0; }
        }

        public InputControl this[ControlTypes i]
        {
            get { return null; }
        }

        public bool Input1()
        {
            return ms.LeftButton == ButtonState.Pressed 
                && pms.LeftButton == ButtonState.Released;
        }

        public bool Input2()
        {
            return ms.RightButton == ButtonState.Pressed
                && pms.RightButton == ButtonState.Released;
        }

        public bool Input3()
        {
            return false;
        }

        public bool Input4()
        {
            return false;
        }

        public bool Input5()
        {
            return false;
        }

        public bool Input6()
        {
            return false;
        }

        public bool ConsoleButton()
        {
            return false;
        }

        public bool ConsoleSwitchButton()
        {
            return false;
        }

        public void Update()
        {
            pms = ms;
            ms = Mouse.GetState();
        }
    }
}
