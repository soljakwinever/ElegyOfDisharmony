using EquestriEngine.Data.UI;
using EquestriEngine.Data.Collections;
using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Graphics;

namespace EquestriEngine.SystemScreens
{
    struct TextLineData
    {
        string _text;
    }

    public enum MessageBoxSpeed
    {
        Slow = 125,
        Medium = 75,
        Fast = 30
    }

    public class MessageBoxScreen : GameScreen
    {
        static TextureObject 
            _messageBoxWindowTexture, 
            _nameBoxWindowTexture, 
            _continueArrowTexture;
        TargetObject _nameBoxWindow;
        FontObject _messageBoxFont;

        Vector2 
            boxPosition,
            boxOrigin;

        ActionList _actions;

        TextLineData[][] _data;
        private int _currentWindow;
        string message;
        string name;

        private const int MAX_LINES = 4;

        private const float 
            FADE_IN_TIME = 0.65f,
            FADE_OUT_TIME = 0.25f;

        bool 
            fadingIn,
            fadingOut,
            messageDisplayed;

        private float alphaModifer,
            _deltaTime,
            DEBUG_MESSAGE_TIME = 0.75f;

        private int _charactersShown = 0;

        public MessageBoxScreen(string _message, ActionList actions)
            :base(false,true)
        {
            Systems.InputManager.RegisterScreen(this);
            boxPosition = new Vector2(EquestriEngine.Settings.WindowWidth / 2, EquestriEngine.Settings.WindowHeight / 1.25f);
            _actions = actions;
            //_data = new TextLineData[_windowLines.Length / 4][];
            var rough = _message.Split(new string[] { "/n[", "]" },System.StringSplitOptions.RemoveEmptyEntries);
            if (rough.Length > 1)
            {
                name = rough[0];
                message = rough[1];
            }
            else
                message = rough[0];
            
        }

        public override void Initialize()
        {
            fadingIn = true;
        }

        public override void LoadContent()
        {
            
            //_messageBoxWindow = Systems.AssetManager.GetTexture("message_box");
            if (_messageBoxWindowTexture == null)
            {
                _messageBoxWindowTexture = new TextureObject("message_box", @"Graphics\UI\MessageBox");
                _nameBoxWindowTexture = new TextureObject("name_box", @"Graphics\UI\namebox");
                _continueArrowTexture = new TextureObject("continue_arrow", @"Graphics\UI\textbox_arrow");
            }

            _messageBoxFont = EquestriEngine.AssetManager.GetFont("{largefont}");
            boxOrigin = new Vector2(_messageBoxWindowTexture.Width / 2, _messageBoxWindowTexture.Height / 2);
            name = "Fluttershy";
            if (name != null)
            {
                _nameBoxWindow = EquestriEngine.AssetManager.CreateTargetObject("{namebox_texture}", (int)_messageBoxFont.Measure(name).X + 50, 74);
                _nameBoxWindow.RunTarget(DrawNameBox, Color.Transparent);
            }
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            if (_nameBoxWindow != null)
                _nameBoxWindow.UnloadAsset();
            base.UnloadContent();
        }

        Vector2 _arrowRise;

        public override void Update(float dt)
        {
            if (fadingIn)
            {
                _deltaTime += dt;
                alphaModifer = MathHelper.Slerp(0, 1, _deltaTime / FADE_IN_TIME);
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
                if (alphaModifer == 0)
                {
                    _deltaTime = 0;
                    fadingOut = false;
                    endMessageBox();
                }
            }
            else
            {
                _deltaTime += dt;
                _arrowRise.Y = (float)System.Math.Sin(_deltaTime) * 16;
                if (!messageDisplayed)
                {
                    _charactersShown++;
                }
                messageDisplayed = _charactersShown == message.Length;
            }
        }

        public override void HandleInput(float dt)
        {

                if (ControlReference.Input1())
                {
                    if (!messageDisplayed)
                    {
                        messageDisplayed = true;
                        _charactersShown = message.Length;
                    }
                    else
                    {
                        fadingOut = true;
                        _deltaTime = 0;
                    }
                }
            
            base.HandleInput(dt);
        }

        private void endMessageBox()
        {
            _stateManager.RemoveScreen(this);
            if(_actions != null && !_actions.Finished)
                _actions.ExecuteCurrent();
        }

        private static Vector2 Arrow_Pos = new Vector2(290, 64);

        public override void Draw(float dt)
        {
            SpriteBatch.Begin();

            if (name != null)
                SpriteBatch.Draw(_nameBoxWindow.Texture, boxPosition - new Vector2(320, 200), Color.Multiply(Color.White, alphaModifer));

            SpriteBatch.Draw(_messageBoxWindowTexture.Texture, boxPosition, null,
                Color.Multiply(EquestriEngine.Settings.SkinColor,alphaModifer), 0.0f, boxOrigin, 1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0.0f);

            SpriteBatch.DrawString(_messageBoxFont, message.Substring(0,_charactersShown), boxPosition - new Vector2(319, 71), Color.Multiply(Color.Black, alphaModifer));
            SpriteBatch.DrawString(_messageBoxFont, message.Substring(0,_charactersShown), boxPosition - new Vector2(320,72), Color.Multiply(Color.White,alphaModifer));

            if (messageDisplayed)
                SpriteBatch.Draw(_continueArrowTexture.Texture, 
                    boxPosition + Arrow_Pos - _arrowRise, 
                    Color.Multiply(Color.White,alphaModifer));
            SpriteBatch.End();
        }

        private void DrawNameBox(Equestribatch sb)
        {
            int width = (int)_messageBoxFont.Measure(name).X;
            sb.Draw(_nameBoxWindowTexture.Texture, Vector2.Zero, 
                new Rectangle(0, 0, 25, _nameBoxWindowTexture.Height), 
                EquestriEngine.Settings.SkinColor);
            sb.Draw(_nameBoxWindowTexture.Texture, 
                new Rectangle(25, 0, width, _nameBoxWindowTexture.Height), 
                new Rectangle(26, 0, 1, _nameBoxWindowTexture.Height), 
                EquestriEngine.Settings.SkinColor);
            sb.Draw(_nameBoxWindowTexture.Texture, new Vector2(25 + width, 0), 
                new Rectangle(_nameBoxWindowTexture.Width - 25, 0, 25, _nameBoxWindowTexture.Height), 
                EquestriEngine.Settings.SkinColor);
            sb.DrawString(_messageBoxFont, name, new Vector2(26,13), Color.Black);
            sb.DrawString(_messageBoxFont, name, new Vector2(25,12), Color.White);
        }
    }
}
