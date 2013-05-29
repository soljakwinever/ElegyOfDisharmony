#region Using Statements
using System;
using System.Collections.Generic;
using EquestriEngine;
#endregion

namespace MonoElegyOfDisharmony
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ElegyGame : EquestriEngine.EquestriEngine
    {
        //Screens.ParticleTestImageScreen _Screen;
        EquestriEngine.SystemScreens.BattleScreen _Screen;

        public ElegyGame()
            :base("Elegy Of Disharmony")
        {
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //_Screen = new Screens.ParticleTestImageScreen(_stateManager);
            _Screen = new EquestriEngine.SystemScreens.BattleScreen();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new Equestribatch(GraphicsDevice);

            //scene.Messages.AddMessageBox(new MessageBox("/name[Trixie]Where the fuck is my money Fluttershy?", scene));
            //scene.Messages.AddMessageBox(new MessageBox("/name[Fluttershy]O-oh!/d[2]/d[] Uhm...I-i'll go get it.../move[(1000|260),0,(0.7|0.7),1,1,5]", scene));
            //scene.Messages.AddMessageBox(new MessageBox("/name[Trixie]/move[(1000|260),0,(0.7|0.7),1,1,5]/d[2]/d[]Bitch better have my fucking money :v", scene));

            // TODO: use this.Content to load your game content here
            //EquestriEngine.Objects.DrawableSkeleton skeleton = new EquestriEngine.Objects.DrawableSkeleton("chicken", "skeleton","Default");

#if !DEBUG
            var logo = new EquestriEngine.Objects.Graphics.TextureObject("{logo}", @"Graphics\UI\logo");
            _stateManager.AddScreen(new EquestriEngine.SystemScreens.LogoScreen(logo, _Screen));
#else

            _stateManager.AddScreenLoad(_Screen);
#endif
            //EquestriEngine.Systems.StateManager.AddScreen(new EquestriEngine.SystemScreens.GameplayScreen(_stateManager));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearColor);
            base.Draw(gameTime);
        }
    }
}
