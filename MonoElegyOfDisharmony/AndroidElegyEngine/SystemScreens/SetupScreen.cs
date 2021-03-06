﻿using EquestriEngine.Data.UI;
using EquestriEngine.Objects.Graphics;

namespace EquestriEngine.SystemScreens
{
    public class SetupScreen : GameScreen
    {
        private GameScreen nextScreen;

        private TextureObject 
            _bubbles,
            _pixel;

        private TargetObject backgroundImage;

        public SetupScreen()
            : base(true, true)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            _bubbles = new TextureObject("{bubz}", @"Graphics\UI\menu_bubbles");
            _pixel = new TextureObject("{bubz}", @"Graphics\UI\menu_bubbles");
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            this._stateManager.AddScreen(nextScreen);
        }

        public override void Update(float dt)
        {
            throw new System.NotImplementedException();
        }

        public override void HandleInput(float dt)
        {
            base.HandleInput(dt);
        }

        public override void Draw(float dt)
        {
            throw new System.NotImplementedException();
        }
    }
}
