using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1.Classes
{
    public class AtlasRect
    {
        public static Texture2D AreaTexture;

        public static void Draw(SpriteBatch sb,AtlasArea area)
        {
            for (int y = 0; y < area.Area.Height; y++)
            {
                for (int x = 0; x < area.Area.Width; x++)
                {
                    if(x == 0 || x == area.Area.Width - 1 || y == 0 || y == area.Area.Height - 1)
                    sb.Draw(AreaTexture, new Vector2(area.Area.X + x, area.Area.Y + y), Color.Teal);
                }
            }
        }
    }
}
