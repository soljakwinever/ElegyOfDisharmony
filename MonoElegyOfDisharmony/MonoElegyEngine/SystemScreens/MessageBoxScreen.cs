using EquestriEngine.Data.UI;
using EquestriEngine.Data.Collections;
using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Graphics;

namespace EquestriEngine.SystemScreens
{
    public enum MessageBoxSpeed
    {
        Slow = 125,
        Medium = 75,
        Fast = 30
    }

    public class MessageBoxScreen : DrawableGameScreen, Data.UI.Interfaces.IInputReciever
    {
        TextureObject
            _messageBoxWindowTexture,
            _nameBoxWindowTexture,
            _continueArrowTexture;
        TargetObject _nameBoxWindow;
        FontObject _messageBoxFont;

        Vector2
            boxPosition,
            boxOrigin;

        string message;
        string name;

        private Data.Inputs.MethodResult _result;

        public Data.Inputs.MethodResult Result
        {
            get { return _result; }
        }

        public bool HasFocus
        {
            get;
            set;
        }

        Data.Inputs.MethodParamPair method;

        private const int MAX_LINES = 4;

        private const float
            FADE_IN_TIME = 0.65f,
            FADE_OUT_TIME = 0.25f;

        bool
            fadingIn,
            fadingOut,
            messageDisplayed;

        private float alphaModifer,
            _deltaTime;

        private int _charactersShown = 0;

        public MessageBoxScreen(string _message)
            : base(false)
        {
            Systems.InputManager.RegisterScreen(this);
            endPos = new Vector2(EngineGlobals.Settings.WindowWidth / 2, EngineGlobals.Settings.WindowHeight / 1.25f);
            startPos = new Vector2(EngineGlobals.Settings.WindowWidth / 2, EngineGlobals.Settings.WindowHeight + 200);
            //_data = new TextLineData[_windowLines.Length / 4][];
            var rough = _message.Split(new string[] { "/n[", "]" }, System.StringSplitOptions.RemoveEmptyEntries);
            if (rough.Length > 1)
            {
                name = rough[0];
                message = rough[1];
            }
            else
                message = rough[0];

            method = new Data.Inputs.MethodParamPair(EngineGlobals.WaitForInput, new Data.Inputs.ControlInput() {  Control = this.ControlReference[Data.Controls.ControlTypes.Interaction] }, 0);

        }

        public override void Initialize()
        {
            fadingIn = true;
        }

        public override void LoadContent()
        {

            //_messageBoxWindow = Systems.AssetManager.GetTexture("message_box");
            _messageBoxWindowTexture = EngineGlobals.GameReference.AssetManager.CreateTextureObjectFromFile("message_box", @"Graphics\UI\MessageBox");
            _nameBoxWindowTexture = EngineGlobals.GameReference.AssetManager.CreateTextureObjectFromFile("name_box", @"Graphics\UI\namebox");
            _continueArrowTexture = EngineGlobals.GameReference.AssetManager.CreateTextureObjectFromFile("continue_arrow", @"Graphics\UI\textbox_arrow");

            _messageBoxFont = EngineGlobals.GameReference.AssetManager.GetFont("{largefont}");
            name = "Fluttershy";
        }

        public override void UnloadContent()
        {
            if (_nameBoxWindow != null)
                _nameBoxWindow.UnloadAsset();
        }

        Vector2 _arrowRise;
        bool methodExecuted = false;

        Vector2 startPos,endPos;

        public override void Update(float dt)
        {
            if (fadingIn)
            {
                _deltaTime += dt;
                alphaModifer = MathHelper.Slerp(0, 1, _deltaTime / FADE_IN_TIME);
                boxPosition = Vector2.Slerp(startPos, endPos, _deltaTime / FADE_IN_TIME);
                if (alphaModifer == 1)
                {
                    _deltaTime = 0;
                    fadingIn = false;
                }
            }
            else if (fadingOut)
            {
                _deltaTime += dt;
                alphaModifer = MathHelper.Slerp(1, 0, _deltaTime / FADE_OUT_TIME);
                boxPosition = Vector2.Slerp(endPos, startPos, _deltaTime / FADE_OUT_TIME);
                if (alphaModifer == 0)
                {
                    _deltaTime = 0;
                    fadingOut = false;
                    StateManager.RemoveScreen(this);
                }
            }
            else
            {
                _deltaTime += dt;
                _arrowRise.Y = (float)System.Math.Sin(_deltaTime) * 16;
                if (!methodExecuted)
                {
                    method.ExecuteMethod(null);
                    methodExecuted = true;
                }
                if (!messageDisplayed)
                {
                    _charactersShown++;
                }
                messageDisplayed = _charactersShown == message.Length;
            }
        }

        public override void HandleInput(float dt)
        {
            if (method.Result == Data.Inputs.MethodResult.Success && !fadingOut)
            {
                _deltaTime = 0;
                fadingOut = true;
                _result = Data.Inputs.MethodResult.Yes;
            }

            base.HandleInput(dt);
        }

        private static Vector2 Arrow_Pos = new Vector2(290, 64);

        public override void Draw(float dt)
        {
            SpriteBatch.Begin();

            //if (name != null)
            //    SpriteBatch.Draw(_nameBoxWindow.Texture, boxPosition - new Vector2(320, 200), Color.Multiply(Color.White, alphaModifer));
            boxOrigin = new Vector2(_messageBoxWindowTexture.Width / 2, _messageBoxWindowTexture.Height / 2);
            SpriteBatch.Draw(_messageBoxWindowTexture.Texture, boxPosition, null,
                Color.Multiply(EngineGlobals.Settings.SkinColor, alphaModifer), 0.0f, boxOrigin, 1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0.0f);

            SpriteBatch.DrawString(_messageBoxFont, message.Substring(0, _charactersShown), boxPosition - new Vector2(319, 71), Color.Multiply(Color.Black, alphaModifer));
            SpriteBatch.DrawString(_messageBoxFont, message.Substring(0, _charactersShown), boxPosition - new Vector2(320, 72), Color.Multiply(Color.White, alphaModifer));

            if (messageDisplayed)
                SpriteBatch.Draw(_continueArrowTexture.Texture,
                    boxPosition + Arrow_Pos - _arrowRise,
                    Color.Multiply(Color.White, alphaModifer));
            SpriteBatch.End();
        }

        private void DrawNameBox(Equestribatch sb)
        {
            int width = (int)_messageBoxFont.Measure(name).X;
            sb.Draw(_nameBoxWindowTexture.Texture, Vector2.Zero,
                new Rectangle(0, 0, 25, _nameBoxWindowTexture.Height),
                EngineGlobals.Settings.SkinColor);
            sb.Draw(_nameBoxWindowTexture.Texture,
                new Rectangle(25, 0, width, _nameBoxWindowTexture.Height),
                new Rectangle(26, 0, 1, _nameBoxWindowTexture.Height),
                EngineGlobals.Settings.SkinColor);
            sb.Draw(_nameBoxWindowTexture.Texture, new Vector2(25 + width, 0),
                new Rectangle(_nameBoxWindowTexture.Width - 25, 0, 25, _nameBoxWindowTexture.Height),
                EngineGlobals.Settings.SkinColor);
            sb.DrawString(_messageBoxFont, name, new Vector2(26, 13), Color.Black);
            sb.DrawString(_messageBoxFont, name, new Vector2(25, 12), Color.White);
        }
    }
}
