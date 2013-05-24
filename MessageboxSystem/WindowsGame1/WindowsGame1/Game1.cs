using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{

    struct MouseAreaData
    {
        Point tl, br;
        bool ready;

        public Point TopLeft
        {
            get { return tl; }
            set { tl = value; }
        }

        public Point BottomRight
        {
            get { return br; }
            set { br = value; }
        }

        public bool Ready
        {
            get { return ready; }
            set { ready = value; }
        }

        public Rectangle ToRect()
        {
            Rectangle rect = new Rectangle();
            bool
                reverseX = tl.X > br.X,
                reverseY = tl.Y > br.Y;

            rect.X = reverseX ? br.X : tl.X;
            rect.Width = reverseX ? tl.X - br.X : br.X - tl.X;

            rect.Y = reverseY ? br.Y : tl.Y;
            rect.Height = reverseY ? tl.Y - br.Y : br.Y - tl.Y;

            return rect;
        }
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<AtlasArea> _atlases;

        public static string FilePath;

        Texture2D texture;

        SpriteFont font;

        public List<AtlasArea> Atlases
        {
            get { return _atlases; }
            set { _atlases = value; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Texture Atlas Editor";

            this.IsMouseVisible = true;
            _atlases = new List<AtlasArea>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Texture2D.FromStream(GraphicsDevice, System.IO.File.Open(FilePath, System.IO.FileMode.Open));
            this.graphics.PreferredBackBufferWidth = texture.Width;
            this.graphics.PreferredBackBufferHeight = texture.Height;
            graphics.ApplyChanges();

            font = Content.Load<SpriteFont>("Arial");

            Classes.AtlasRect.AreaTexture = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[] { Color.White };
            Classes.AtlasRect.AreaTexture.SetData(data);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            texture.Dispose();
            Classes.AtlasRect.AreaTexture.Dispose();
        }

        MouseState ms, pms;

        MouseAreaData area = new MouseAreaData(), oldArea;
        bool clicked, locked;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!Program.EditorFocus)
            {
                pms = ms;
                ms = Mouse.GetState();
                if (Program.Editor.Selected_Area != null)
                {
                    if (ms.RightButton == ButtonState.Pressed && pms.RightButton == ButtonState.Released)
                    {
                        if (!clicked && !locked)
                        {
                            oldArea = area;
                            area.Ready = false;
                            area.TopLeft = new Point(ms.X, ms.Y);
                            clicked = true;
                        }
                    }
                    else if (ms.LeftButton == ButtonState.Pressed && pms.LeftButton == ButtonState.Released)
                    {
                        area.Ready = true;
                        clicked = false;
                        Program.Editor.UpdateListbox();
                    }
                    if (clicked)
                    {
                        area.BottomRight = new Point(ms.X, ms.Y);
                        Program.Editor.Selected_Area.Area = area.ToRect();
                    }
                }
            }
            else
            {
                clicked = false;
                System.Threading.Thread.Sleep(1);
                return;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(texture, Vector2.Zero, Color.White);

            if (Program.Editor.Selected_Area != null)
                Classes.AtlasRect.Draw(spriteBatch, Program.Editor.Selected_Area);

            spriteBatch.End();

            if (Program.Editor.Selected_Area != null)
            {
                spriteBatch.Begin();

                spriteBatch.DrawString(font, Program.Editor.Selected_Area.ToString(), Vector2.One * 7, Color.Black);
                spriteBatch.DrawString(font, Program.Editor.Selected_Area.ToString(), Vector2.One * 6, Color.White);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
