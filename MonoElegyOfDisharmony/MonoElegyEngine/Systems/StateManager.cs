using EquestriEngine.Data.Collections;
using EquestriEngine.Data.UI.Interfaces;
using System.Linq;

namespace EquestriEngine.Systems
{
    public class StateManager : Bases.BaseDrawableSystem
    {
        private static GameScreenCollection _gameScreens;
        System.Collections.Generic.List<IGameScreen> updateThisFrame;

        public StateManager(object game)
            : base(game)
        {
            _gameScreens = new GameScreenCollection();
            updateThisFrame = new System.Collections.Generic.List<IGameScreen>();
        }
        ~StateManager()
        {
            foreach (IDrawable screen in _gameScreens)
            {
                screen.UnloadContent();
            }
            _gameScreens.Clear();
        }

        public override void Initialize()
        {
            foreach (var screen in _gameScreens)
            {
                screen.Initialize();
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (IDrawable screen in _gameScreens)
            {
                screen.LoadContent();
            }
            base.LoadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_gameScreens.Count > 0)
            {
                var array = _gameScreens.ToArray();
                foreach (var screen in array)
                {
                    screen.Update(dt);
                    if (screen is IInputReciever)
                    {
                        var iScreen = screen as IInputReciever;
                        if (iScreen.HasFocus)
                            iScreen.HandleInput(dt);
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var array = _gameScreens.ToArray();
            foreach (IDrawable screen in array)
            {
                if (!screen.IsCovered)
                    screen.Draw(dt);
            }


            base.Draw(gameTime);
        }

        public void AddScreenLoad(Data.UI.DrawableGameScreen screen)
        {
            var loadScreen = new SystemScreens.LoadingScreen(screen.LoadContent, screen);
            loadScreen.LoadContent();
            AddScreen(loadScreen, true);
        }

        public void AddScreen(Data.UI.Interfaces.IGameScreen screen, bool loaded = false)
        {
            screen.StateManager = this;
            if (screen is IDrawable)
            {
                var dScreen = screen as IDrawable;
                if (!loaded)
                    dScreen.LoadContent();
                if (dScreen.CoversOthers)
                {
                    foreach (IDrawable oScreen in _gameScreens)
                    {
                        oScreen.IsCovered = true;
                        oScreen.OnTop = false;
                    }
                    dScreen.OnTop = true;
                    dScreen.IsCovered = false;
                }
            }
            if (screen is IInputReciever)
            {
                var iScreen = screen as IInputReciever;
                var inputScreens = from s in _gameScreens where s is IInputReciever select s;
                foreach (IInputReciever oScreen in inputScreens)
                {
                    oScreen.HasFocus = false;
                }
                InputManager.RegisterScreen(iScreen);
                iScreen.HasFocus = true;
            }

            screen.Initialize();
            if (_gameScreens.Count > 0 && _gameScreens.Last.Value is SystemScreens.LoadingScreen)
                _gameScreens.AddBefore(_gameScreens.Last, screen);
            else
                _gameScreens.AddLast(screen);
        }

        public void RemoveScreen(Data.UI.Interfaces.IGameScreen screen)
        {
            _gameScreens.Remove(screen);
            if (screen is IDrawable)
            {
                var dScreen = screen as IDrawable;
                dScreen.UnloadContent();
                if (dScreen.CoversOthers)
                {
                    foreach (IDrawable oScreen in _gameScreens)
                    {
                        oScreen.IsCovered = false;
                        oScreen.OnTop = false;
                    }
                    if (_gameScreens.Last.Value is IDrawable)
                    {
                        (_gameScreens.Last.Value as IDrawable).OnTop = true;
                    }
                }
            }
            if (screen is IInputReciever)
            {
                if (_gameScreens.Count > 0)
                {
                    if (_gameScreens.Last.Value is IInputReciever)
                    {
                        (_gameScreens.Last.Value as IInputReciever).HasFocus = true;
                    }
                }
            }
        }
    }
}
