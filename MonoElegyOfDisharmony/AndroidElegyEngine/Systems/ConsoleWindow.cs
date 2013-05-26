using EquestriEngine.Objects.Graphics;
using EquestriEngine.Data.Scenes;
using Variable = EquestriEngine.Data.Variable;

namespace EquestriEngine.Systems
{
    enum ConsoleMode
    {
        Console = 0,
        SceneView = 1,
        SwitchView = 2,
        VariableView = 3,
        Stats = 4
    }

    public class ConsoleWindow : Bases.BaseDrawableSystem
    {
        private PixelObject _windowTexture;
        private readonly FontObject _font;
        private TargetObject _renderedText;
        private readonly Color _consoleColor;
        private static string _entries, _consoleEntries = "";

        private static ConsoleMode _currentMode;

        private static Variable _consoleInput;

        private const int
            CONSOLE_LINES = 10,
            CONSOLE_HEIGHT = 256;

        private bool _consoleClosed;

        private float showAmount = 0.0f;
        private int windowShown;

        private static bool Console_Open = false, Refresh_Required = true;

        private Vector2 _consoleTextPos;

        System.Diagnostics.Process process;

        private static int LineCount
        {
            get { return _consoleEntries.Split('\n').Length; }
        }

        public ConsoleWindow(object game)
            : base(game)
        {
            _font = EquestriEngine.AssetManager.CreateFontFromFile("{console}", @"fonts\celestia_redux");
            _consoleColor = Color.Multiply(Color.White, 0.5f);
            _currentMode = ConsoleMode.Console;
            _consoleInput = new Variable("");
            _entries = "";
            process = System.Diagnostics.Process.GetCurrentProcess();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _windowTexture = EquestriEngine.AssetManager.CreatePixelTexture("{console}");
            _renderedText = EquestriEngine.AssetManager.CreateTargetObject("{rendred_text}", EquestriEngine.Settings.WindowWidth, 256);
            base.LoadContent();
        }

        public static void FlushConsole()
        {
            _entries = "";
        }

        public static void TrimConsole()
        {
            do
            {
                string[] tempArray = _consoleEntries.Split(new[] { '\n' });
                string tempString = "";
                for (int i = 1; i < tempArray.Length; i++)
                {
                    tempString += tempArray[i] + (i != tempArray.Length - 1 ? "\n" : "");
                }
                _consoleEntries = tempString;
            } while (LineCount > 14);
        }

        float refresh = 0.0f;

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (Console_Open)
            {
                if (_currentMode == ConsoleMode.Stats)
                {
                    refresh += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (refresh > 1.0)
                    {

                        refresh = 0;
                        _entries = string.Format(
                            "--Engine Stats--\n" +
                            "FPS: {0}\n" +
                            "Memory Used: {1} MB\n" +
                            "Objects Loaded: {2}",
                            new object[]
                        {
                          frames,
                          ((int)System.Diagnostics.Process.GetCurrentProcess().PagedMemorySize64 / 1048576),
                          EquestriEngine.AssetManager.ItemsLoaded
                        });
                        Refresh_Required = true;
                        frames = 0;
                    }
                }

                showAmount += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (showAmount > 1)
                    showAmount = 1;
            }
            else
            {
                showAmount -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (showAmount < 0)
                    showAmount = 0;
            }
            _consoleClosed = showAmount == 0.0f;
            if ((!Console_Open && !_consoleClosed) ^ (Console_Open && windowShown != CONSOLE_HEIGHT))
            {
                windowShown = (int)MathHelper.Slerp(0, CONSOLE_HEIGHT, showAmount);
                _consoleTextPos = 
                    Vector2.Slerp(
                    new Vector2(0, EquestriEngine.Settings.WindowHeight), 
                    new Vector2(0, EquestriEngine.Settings.WindowHeight - CONSOLE_HEIGHT), showAmount);
            }
        }

        int frames = 0;

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (!_consoleClosed && !EquestriEngine.AssetManager.FrameCapture)
            {
                if (Refresh_Required)
                {
                    _renderedText.RunTarget(RenderText, Color.Transparent);
                    Refresh_Required = false;
                }
                if (_currentMode == ConsoleMode.Stats)
                    frames++;

                SpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend,
                    null, EquestriEngine.DepthState, null);
                if(_windowTexture.Ready)
                SpriteBatch.Draw(_windowTexture.Texture,
                    new Rectangle(0, EquestriEngine.Settings.WindowHeight - windowShown, EquestriEngine.Settings.WindowWidth, windowShown), Color.Multiply(_consoleColor, showAmount));

                SpriteBatch.Draw(_renderedText.Texture, _consoleTextPos, Color.Multiply(Color.White, showAmount));

                if (windowShown >= 128)
                {
                    SpriteBatch.Draw(_windowTexture.Texture,
                        new Rectangle(0, EquestriEngine.Settings.WindowHeight - 32, EquestriEngine.Settings.WindowWidth, 32), 
                        showAmount > 0.75f ? Color.Multiply(_consoleColor, (showAmount - 0.75f) / 0.25f) : Color.Transparent);
                    SpriteBatch.DrawString(_font, _consoleInput.AsString + "|", new Vector2(4, EquestriEngine.Settings.WindowHeight - 26) + Vector2.One, Color.Black);
                    SpriteBatch.DrawString(_font, _consoleInput.AsString + "|", new Vector2(4, EquestriEngine.Settings.WindowHeight - 26), Color.Blue);
                }

                SpriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private void RenderText(Equestribatch sb)
        {
            sb.DrawString(_font, _entries, new Vector2(5, 5), /*Color.Multiply(Color.White, showAmount)*/ Color.Black);
            sb.DrawString(_font, _entries, new Vector2(4, 4), /*Color.Multiply(Color.White, showAmount)*/ Color.White);
        }

        public static void RefreshEntries()
        {
            switch (_currentMode)
            {
                case ConsoleMode.Console:
                    if (LineCount > 14)
                        TrimConsole();
                    _entries = _consoleEntries;
                    break;
                case ConsoleMode.SceneView:
                    //_entries = SceneManager.DisplayScene();
                    break;
                case ConsoleMode.SwitchView:
                    _entries = DataManager.PrintSwitches(0);
                    break;
                case ConsoleMode.VariableView:
                    _entries = DataManager.PrintVariables(0);
                    break;
                case ConsoleMode.Stats:
                    _entries = "Getting Engine Stats...";
                    break;
            }
            Refresh_Required = true;
        }

        public static void ChangeMode()
        {
            if (_currentMode == ConsoleMode.Stats)
            {
                _currentMode = ConsoleMode.Console;
            }
            else
                _currentMode++;
            RefreshEntries();
        }

        public static void OpenConsole()
        {
            Console_Open = true;
            _currentMode = ConsoleMode.Console;
            RefreshEntries();
        }

        public static void ToggleConsole()
        {
            Console_Open = !Console_Open;
            if (Console_Open)
                InputManager.RegisterTextInput(_consoleInput);
            else
            {
                InputManager.UnregisterTextInput();
                _consoleInput.AsString = "";
            }
            switch (_currentMode)
            {
                case ConsoleMode.Console:
                    _entries = _consoleEntries;
                    break;
                case ConsoleMode.SceneView:
                    //_entries = SceneManager.DisplayScene();
                    break;
            }
        }

        public static void WriteLine(string input)
        {
            _consoleEntries += input + "\n";
            RefreshEntries();
        }

        public static void WriteLine(string format, object obj1)
        {
            _consoleEntries += string.Format(format, obj1) + "\n";
            RefreshEntries();
        }

        public static void WriteLine(string format, object obj1, object obj2)
        {
            _consoleEntries += string.Format(format, obj1, obj2) + "\n";
            RefreshEntries();
        }

        public static void WriteLine(string format, object[] array)
        {
            _consoleEntries += string.Format(format, array) + "\n";
            RefreshEntries();
        }

        public static void Write(string input)
        {
            _consoleEntries += input;
            RefreshEntries();
        }
    }
}
