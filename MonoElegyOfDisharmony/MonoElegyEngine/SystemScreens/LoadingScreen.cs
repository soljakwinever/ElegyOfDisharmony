using EquestriEngine.Data.UI;
//using EquestriEngine.Objects.GameObjects;
using EquestriEngine.Objects.Graphics;
using EquestriEngine.Data.Scenes;
//using EquestriEngine.Objects.Nodes;

using Action = System.Action;
using TimedAction = System.Action<float>;

namespace EquestriEngine.SystemScreens
{
    public class LoadingScreen : DrawableGameScreen
    {
        private bool multiThreaded;
        static TextureObject _background;
        private Action 
            _loadMethod;
        private TimedAction
            _drawMethod,
            _updateMathod;

        bool fadeBlack;

        float fadeAmount;
        bool finishedLoading;

        Data.UI.Interfaces.IGameScreen nextScreen;

        public LoadingScreen(Action loadMethod, 
            Data.UI.Interfaces.IGameScreen next)
            :base(true)
        {
            multiThreaded = false;
            fadeBlack = false;
            _loadMethod = loadMethod;
            nextScreen = next;
        }
        
        public LoadingScreen(Action loadMethod,
            Data.UI.Interfaces.IGameScreen next,
            TimedAction updateMethod, 
            TimedAction drawMethod)
            :base(true)
        {
            multiThreaded = true;
            nextScreen = next;
            _loadMethod = loadMethod;
            _drawMethod = drawMethod;
            _updateMathod = updateMethod;
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            if (fadeBlack)
                _background = EquestriEngine.AssetManager.CreatePixelTexture("{background}", Color.Black);
            else
                _background = EquestriEngine.AssetManager.GetTexture("{load}");
            _loadMethod.BeginInvoke(new System.AsyncCallback(FinishedLoading), null);
        }

        private void FinishedLoading(System.IAsyncResult result)
        {
            _loadMethod.EndInvoke(result);
            finishedLoading = true;
            _stateManager.AddScreen(nextScreen, true);
        }

        public override void UnloadContent()
        {
            _background.UnloadAsset();
        }

        public override void Update(float dt)
        {
            if (multiThreaded)
                _updateMathod.Invoke(dt);
            else
            {
                if (fadeAmount < 1 && !finishedLoading)
                    fadeAmount += dt;
                else if(finishedLoading)
                {
                    if (fadeAmount > 0)
                        fadeAmount -= dt;
                    else
                    {
                        _stateManager.RemoveScreen(this);
                    }
                }
            }
        }

        public override void Draw(float dt)
        {
            if (multiThreaded)
                _drawMethod.Invoke(dt);
            else
            {
                if (!_background.Ready)
                    return;
                SpriteBatch.Begin();

                SpriteBatch.Draw(_background.Texture,
                    new Rectangle(0,0,EquestriEngine.Settings.WindowWidth,EquestriEngine.Settings.WindowHeight),
                    Color.Multiply(Color.White,fadeAmount));

                SpriteBatch.End();
            }
        }
    }
}
