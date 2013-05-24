using EquestriEngine.Data.UI;
using EquestriEngine.Data.Collections;
using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Graphics;

namespace EquestriEngine.SystemScreens
{
    public class MessageBoxScreen : GameScreen
    {
        TextureObject _messageBoxWindow;

        Vector2 
            boxPosition,
            boxOrigin;

        MethodParamCollection _methods;

        public MessageBoxScreen(Systems.StateManager manager)
            :base(manager,true)
        {
            Systems.InputManager.RegisterScreen(this);
            boxPosition = new Vector2(EquestriEngine.Settings.WindowWidth / 2, EquestriEngine.Settings.WindowHeight / 1.25f);
            _methods = new MethodParamCollection();
        }

        public override void Initialize()
        {
            
        }

        public override void LoadContent()
        {
            _messageBoxWindow = new TextureObject("message_box", @"Graphics\UI\MessageBox");
            boxOrigin = new Vector2(_messageBoxWindow.Width / 2, _messageBoxWindow.Height / 2);
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            _messageBoxWindow.UnloadAsset();
        }

        public override void Update(float dt)
        {

        }

        public override void HandleInput(float dt)
        {
            if (ControlReference.Input1())
            {
                Systems.ConsoleWindow.WriteLine("Hi");
            }
            base.HandleInput(dt);
        }

        public override void Draw(float dt)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(_messageBoxWindow, boxPosition, null,
                EquestriEngine.Settings.SkinColor, 0.0f, boxOrigin, 1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0.0f);

            SpriteBatch.End();
        }
    }
}
