using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Controls
{
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

        //May be used for interaction
        bool Input1();
        //May be used for character selection access
        bool Input2();
        //Accessing Character menu perhaps?
        bool Input3();
        //Fuck I dunno about these next three
        bool Input4();
        //Undecided
        bool Input5();
        //Undecided
        bool Input6();
#if DEBUG
        bool ConsoleButton();
        bool ConsoleSwitchButton();
#endif

        void Update();
    }
}
