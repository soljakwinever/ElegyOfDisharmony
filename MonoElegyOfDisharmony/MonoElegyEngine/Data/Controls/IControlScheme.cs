using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Controls
{
    public enum ControlTypes
    {
        None,
        Interaction,
        Jump,
        Menu,
        Left,
        Right,
        Up,
        Down
#if DEBUG
        ,CONSOLE
        ,CONOLE_SWITCH
#endif
    }

    public interface IControlScheme
    {
        //These two will be used as a velocity type thing. 
        //Will be 1 or -1 for Keyboards
        //Will be between 1 and -1 for controllers
        float X
        {
            get;
        }

        float Y
        {
            get;
        }

        InputControl this[ControlTypes type]
        {
            get;
        }
#if DEBUG
        bool ConsoleButton();
        bool ConsoleSwitchButton();
#endif

        void Update();
    }
}
