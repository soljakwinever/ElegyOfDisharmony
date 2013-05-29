﻿using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Graphics;

namespace EquestriEngine.SystemScreens
{
    public class GameOverScreen : Data.UI.DrawableGameScreen
    {
        private const float screen_on_time = 6.0f;

        private float _timeOn;

        private Color _logoColor;

        TextureObject _logo;

        Vector2 logoPos, logoOrigin;

        public GameOverScreen()
            :base(true)
        {
            EquestriEngine.ClearColor = Color.Black;
            _logoColor = Color.Black;
        }

        public override void Initialize()
        {
            logoPos = new Vector2(EquestriEngine.Settings.WindowWidth / 2, EquestriEngine.Settings.WindowHeight / 2);
            logoOrigin = new Vector2(_logo.Width / 2, _logo.Height / 2);
        }

        public override void LoadContent()
        {

        }

        public override void UnloadContent()
        {
            _logo.UnloadAsset();
        }

        public override void Update(float dt)
        {
            if (_timeOn < screen_on_time)
            {
                _timeOn += dt;
            }
            else
            {
                _stateManager.AddScreenLoad(new TitleScreen());
                _stateManager.RemoveScreen(this);
            }
            if (_timeOn < 1)
            {
                _logoColor = Color.Lerp(Color.Black, Color.White, _timeOn / 1);
            }
            else if (_timeOn > screen_on_time - 1)
            {
                _logoColor = Color.Lerp(Color.White, Color.Black, (_timeOn - (screen_on_time - 1)) / 1);
            }
        }

        public override void Draw(float dt)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(_logo.Texture, logoPos, null, _logoColor, 0.0f, logoOrigin,1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0.0f);

            SpriteBatch.End();
        }
    }
}