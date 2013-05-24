using EquestriEngine.Data.Collections;
using EquestriEngine.Data.UI.Interfaces;

namespace EquestriEngine.Systems
{
    public class StateManager : Bases.BaseDrawableSystem
    {
        private static GameScreenCollection _gameScreens;
        System.Collections.Generic.List<IGameScreen> updateThisFrame;
        private static bool _ready;

        public StateManager(object game)
            : base(game)
        {
            _gameScreens = new GameScreenCollection();
            updateThisFrame = new System.Collections.Generic.List<IGameScreen>();
        }
        ~StateManager()
        {
            foreach (var screen in _gameScreens)
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
            foreach (var screen in _gameScreens)
            {
                screen.LoadContent();
            }
            base.LoadContent();
            _ready = true;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_gameScreens.Count > 0)
            {
                var screen = _gameScreens.First;
                do
                {
                    if (!screen.Value.IsCovered)
                        updateThisFrame.Add(screen.Value);
                    screen = screen.Next;
                } while (screen != null);

                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                for (int i = 0; i < updateThisFrame.Count; i++)
                {
                    var temp = updateThisFrame[i];
                    temp.Update(dt);
                    if (temp.HasFocus)
                        temp.HandleInput(dt);
                }
                updateThisFrame.Clear();
            }

            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var screen in _gameScreens)
            {
                if (!screen.IsCovered)
                    screen.Draw(dt);
            }


            base.Draw(gameTime);
        }

        public void AddScreenLoad(Data.UI.Interfaces.IGameScreen screen)
        {
            if (!_ready)
            {
                throw new Data.Exceptions.EngineException("System not ready", true);
            }
            screen.StateManager = this;
            var loadingScreen = new SystemScreens.LoadingScreen(screen.LoadContent, screen);
            AddScreen(loadingScreen, false);
        }

        public void AddScreen(Data.UI.Interfaces.IGameScreen screen, bool loaded = false)
        {
            screen.StateManager = this;
            if (_ready && !loaded)
            {
                screen.Initialize();
                screen.LoadContent();
            }
            if (screen.GetsInput)
            {
                screen.HasFocus = true;
                InputManager.RegisterScreen(screen);
                if (_gameScreens.Count > 0)
                {
                    var oldScreen = _gameScreens.First;
                    do
                    {
                        oldScreen.Value.HasFocus = false;
                        oldScreen = oldScreen.Next;
                    } while (oldScreen != null);
                }
            }
            if (screen.CoversOthers)
            {
                screen.HasFocus = true;
                if (_gameScreens.Count > 0)
                {
                    var oldScreen = _gameScreens.First;
                    do
                    {
                        oldScreen.Value.IsCovered = true;
                        oldScreen = oldScreen.Next;
                    } while (oldScreen != null);
                }
                _gameScreens.AddLast(screen);
            }
            else
                _gameScreens.AddBefore(_gameScreens.Last,screen);

        }

        public void RemoveScreen(Data.UI.Interfaces.IGameScreen screen)
        {
            screen.UnloadContent();
            _gameScreens.Remove(screen);
            if (_gameScreens.Count > 1)
            {
                if (screen.CoversOthers)
                {
                    var oldScreen = _gameScreens.Last;
                    if (_gameScreens.Count > 1)
                    {
                        do
                        {
                            if (oldScreen.Value.CoversOthers)
                            {
                                oldScreen.Value.IsCovered = false;
                                break;
                            }
                            oldScreen = oldScreen.Previous;
                        } while (oldScreen.Previous != null);
                    }

                }
                if (screen.HasFocus)
                {
                    var oldScreen = _gameScreens.Last;
                    do
                    {
                        if (oldScreen.Value.GetsInput)
                        {
                            oldScreen.Value.HasFocus = true;
                            break;

                        }
                        oldScreen = oldScreen.Previous;
                    } while (oldScreen.Previous != null);

                }
            }
            else
            {
                var temp = _gameScreens.First;
                temp.Value.IsCovered = false;
                if (temp.Value.GetsInput)
                    temp.Value.HasFocus = true;
            }

        }
    }
}
