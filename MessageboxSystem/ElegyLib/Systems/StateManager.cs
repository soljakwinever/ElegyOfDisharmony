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

        public static void AddScreen(Data.UI.Interfaces.IGameScreen screen)
        {
            if (_ready)
            {
                screen.Initialize();
                screen.LoadContent();
            }
            if (screen.CoversOthers)
            {
                screen.HasFocus = true;
                if (_gameScreens.Count > 0)
                {
                    var oldScreen = _gameScreens.First;
                    do
                    {
                        oldScreen.Value.HasFocus = false;
                        oldScreen.Value.IsCovered = true;
                        oldScreen = oldScreen.Next;
                    } while (oldScreen != null);
                }
                _gameScreens.AddFirst(screen);
            }
        }

        public static void RemoveScreen(Data.UI.Interfaces.IGameScreen screen)
        {
            screen.UnloadContent();
            if (screen.CoversOthers && screen.HasFocus)
            {
                _gameScreens.Remove(screen);
                if (_gameScreens.Count > 0)
                {
                    var oldScreen = _gameScreens.First;
                    oldScreen.Value.HasFocus = true;
                    oldScreen.Value.IsCovered = false;
                }
            }
        }
    }
}
