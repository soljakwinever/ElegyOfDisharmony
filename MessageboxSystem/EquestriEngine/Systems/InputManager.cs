using Microsoft.Xna.Framework;
using EquestriEngine.Data.Controls;
using KeyGrabber = EquestriEngine.Helpers.KeyGrabber;

namespace EquestriEngine.Systems
{
    public class InputManager : Bases.BaseSystem
    {
        private static IControlScheme _controls;
        private static bool _recievingRawInput;
        private static Data.Variable _variable;

        public static bool RecievingRawInput
        {
            get { return _recievingRawInput; }
        }

        public InputManager(object game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            _controls = new KeyboardControl();
            base.Initialize();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
#if DEBUG
            if (_controls.ConsoleButton())
            {
                ConsoleWindow.ToggleConsole();
            }
            if (_controls.ConsoleSwitchButton())
            {
                ConsoleWindow.ChangeMode();
            }
#endif
            _controls.Update();
            base.Update(gameTime);
        }

        public static void RegisterTextInput(Data.Variable variable)
        {
            _variable = variable;
            KeyGrabber.InboundCharEvent += ProcessInput;
            KeyGrabber.RegisterMessageFilter();
            _recievingRawInput = true;
        }

        public static void UnregisterTextInput()
        {
            _variable = null;
            KeyGrabber.InboundCharEvent -= ProcessInput;
            KeyGrabber.UnregisterMessageFilter();
            _recievingRawInput = false;
        }

        /*public static void RegisterPlayerNode(Objects.GameObjects.Player player)
        {
            player.RegisterControlReference(_controls);
        }*/

        public static void RegisterScreen(Data.UI.Interfaces.IGameScreen screen)
        {
            screen.ControlReference = _controls;
        }

        private static void ProcessInput(char input)
        {
            //Only append characters that exist in the spritefont.
            if (input == 13)
            {
                try
                {
                    var method = 
                        EngineGlobals.GenerateMethodFromString(
                        _variable.AsString.Replace(' ',';'));
                    method.ExecuteMethod(null);
                    ConsoleWindow.WriteLine("Executing...");
                }
                catch(Data.Exceptions.EngineException ex)
                {
                    ConsoleWindow.WriteLine("Warning: {0}", ex.Message);
                }
                _variable.AsString = "";
                return;
            }

            if (input < 32 && input > 9)
                return;

            if (input > 126)
                return;

            if (input == 8)
            {
                if (_variable.AsString.Length > 0)
                    _variable.AsString = _variable.AsString.Substring(0, _variable.AsString.Length - 1);
            }
            else
                _variable += input;
        }
    }
}
