using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EquestriEngine.Objects.Graphics;
using EquestriEngine.Systems;
using GameSettings = EquestriEngine.Data.GameSettings;

namespace EquestriEngine
{
    public class EquestriEngine : Game
    {
        private static GameSettings _settings;

        TextureObject errorTexture;

        FontObject smallFont;

        private const string VERSION_NUMBER = "1.2.1.4";

        protected AssetManager _textureManager = null;
        protected DataManager _dataManager = null;
        protected InputManager _inputManager = null;
        protected WidgetDrawer _widgetDisplay = null;
        protected SceneManager _sceneManager = null;
        protected StateManager _stateManager = null;

#if PROFILER
        protected Indiefreaks.Xna.Profiler.ProfilerGameComponent profiler;
#endif

#if DEBUG
        ConsoleWindow _debugConsole = null;
#endif

        GraphicsDeviceManager graphics;
        protected Equestribatch spriteBatch;

        public static readonly DepthStencilState
            DepthState,
            NonDepthState;

        static bool _errorOccured;

        public static GameSettings Settings
        {
            get { return _settings; }
        }

        private static string _errorMessage;

        public static string ErrorMessage
        {
            set
            {
                _errorOccured = true;
                ConsoleWindow.OpenConsole();
                ConsoleWindow.WriteLine("Critical Error: {0}", value);
                _errorMessage = value;
            }
        }

        static EquestriEngine()
        {
            DepthState = DepthStencilState.Default;
            NonDepthState = DepthStencilState.None;
        }

        public EquestriEngine(string title = "Elegy Engine")
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreparingDeviceSettings += OnPreparingDeviceSettings;

#if RUN_FAST
            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
#endif
            _settings = GameSettings.LoadData();
            _settings.InitGraphicsDevice(graphics, GraphicsDevice);

            _textureManager = new AssetManager(this);
            _dataManager = new DataManager(this);
            _inputManager = new InputManager(this);
            _sceneManager = new SceneManager(this);
            _stateManager = new StateManager(this);
            _widgetDisplay = new WidgetDrawer(this);

            this.Components.Add(_dataManager);
            this.Components.Add(_textureManager);
            this.Components.Add(_inputManager);
            this.Components.Add(_sceneManager);
            this.Components.Add(_stateManager);
            this.Components.Add(_widgetDisplay);
#if PROFILER
            profiler = new Indiefreaks.Xna.Profiler.ProfilerGameComponent(this, @"Fonts\celestia_redux");
            Indiefreaks.AOP.Profiler.ProfilingManager.Run = true;
            this.Components.Add(profiler);
#endif
#if DEBUG
            _debugConsole = new ConsoleWindow(this);
            this.Components.Add(_debugConsole);
#endif
            Content.RootDirectory = "Content";

            this.Window.Title = string.Format("{0} - ver {1}",title,VERSION_NUMBER);
        }

        private void OnPreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new Equestribatch(GraphicsDevice);
            errorTexture = AssetManager.GetTexture("{error}");
            smallFont = AssetManager.GetFont("{smallfont}");
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
#if DEBUG
            if (_errorOccured)
            {
                _debugConsole.Update(gameTime);
            }
            else
#endif
                base.Update(gameTime);
        }

        protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_errorOccured)
            {
                GraphicsDevice.Clear(Color.Red);
                spriteBatch.Begin();
                Vector2 Center = new Vector2(Settings.WindowWidth / 2, Settings.WindowHeight / 2);
                spriteBatch.Draw(errorTexture.Texture, Center, null, Color.White, 0.0f,
                    new Vector2(errorTexture.Width / 2, errorTexture.Height / 2),
                    1.0f, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(smallFont, "CRITICAL ERROR", Center + new Vector2(0, 128), Color.White, 0.0f,
                    smallFont.CenterAlignX("CRITICAL ERROR"), 1.0f, SpriteEffects.None, 0.0f);
                spriteBatch.End();
#if DEBUG
                _debugConsole.Draw(gameTime);
#endif
            }
            else

                base.Draw(gameTime);
        }
    }
}
