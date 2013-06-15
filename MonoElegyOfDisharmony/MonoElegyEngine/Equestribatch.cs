using EquestriEngine.Objects.Graphics;
using EquestriEngine.Data.Scenes;
using Microsoft.Xna.Framework.Graphics;
namespace EquestriEngine
{
    public class Equestribatch : Microsoft.Xna.Framework.Graphics.SpriteBatch
    {
        private bool _ready;

        public bool Ready
        {
            get { return _ready; }
        }

        public Equestribatch(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
            : base(device)
        {
            _ready = true;
        }

        public void Draw(TextureObject textureObject, Rectangle destRect, Color color)
        {
            base.Draw(textureObject.Texture, destRect, color);
        }

        public void Draw(TextureObject textureObject, Vector2 position, Rectangle? sourceRect, Color color)
        {
            base.Draw(textureObject.Texture, position, sourceRect, color);
        }

        public void Draw(TextureObject textureObject, Rectangle destRect, Rectangle? sourceRect, Color color)
        {
            base.Draw(textureObject.Texture, destRect, sourceRect, color);
        }

        public void Draw(TextureObject textureObject, Vector2 position, Rectangle? sourceRect, 
            Color color,float rotation, Vector2 origin,float scale,int effects,float depth)
        {
            SpriteEffects sfx;
            switch (effects)
            {
                case 1:
                    sfx = SpriteEffects.FlipHorizontally;
                    break;
                case 2:
                    sfx = SpriteEffects.FlipVertically;
                    break;
                case 3:
                    sfx = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                    break;
                default:
                    sfx = SpriteEffects.None;
                    break;
            }

            base.Draw(textureObject.Texture, position, sourceRect, color,rotation,origin,scale,sfx,depth);
        }

        public void Draw(TextureObject textureObject, Vector2 position, Rectangle? sourceRect,
    Color color, float rotation, Vector2 origin, Vector2 scale, int effects, float depth)
        {
            SpriteEffects sfx;
            switch (effects)
            {
                case 1:
                    sfx = SpriteEffects.FlipHorizontally;
                    break;
                case 2:
                    sfx = SpriteEffects.FlipVertically;
                    break;
                case 3:
                    sfx = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                    break;
                default:
                    sfx = SpriteEffects.None;
                    break;
            }

            base.Draw(textureObject.Texture, position, sourceRect, color, rotation, origin, scale, sfx, depth);
        }

        public void Draw(TextureObject textureObject, Rectangle destRect, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, int effects, float depth)
        {
            SpriteEffects sfx;
            switch (effects)
            {
                case 1:
                    sfx = SpriteEffects.FlipHorizontally;
                    break;
                case 2:
                    sfx = SpriteEffects.FlipVertically;
                    break;
                case 3:
                    sfx = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                    break;
                default:
                    sfx = SpriteEffects.None;
                    break;
            }

            base.Draw(textureObject.Texture, destRect, sourceRect, color, rotation, origin, sfx, depth);
        }
    }

}
