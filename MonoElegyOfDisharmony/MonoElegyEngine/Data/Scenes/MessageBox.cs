using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Color = Microsoft.Xna.Framework.Color;
using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Data.Scenes
{
    public class MessageBox
    {
        private bool finished;

        public bool Finished
        {
            get { return finished; }
            set
            {
                finished = value;
            }
        }

        public MessageBox(string message)
        {
        }

        public void Update(float dt)
        {
        }

        public void Draw(SpriteBatch sb, SpriteFont font, Vector2 Position)
        {
        }

        private static void ParseMessagebox(MessageBox box, string message)
        {
        }
    }
}
