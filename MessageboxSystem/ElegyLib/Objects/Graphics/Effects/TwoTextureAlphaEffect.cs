using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Objects.Graphics
{
    public class TwoTextureAlphaEffect : EffectObject, Interfaces.IContentLoadableEffect
    {
        public Texture2D TextureA
        {
            set { _effect.Parameters["image"].SetValue(value); }
        }

        public Texture2D TextureB
        {
            set { _effect.Parameters["textureB"].SetValue(value); }
        }

        public Texture2D AlphaMap
        {
            set { _effect.Parameters["alpha"].SetValue(value); }
        }

        public TwoTextureAlphaEffect(string name)
            :base("TwoTextureAlpha")
        {
            Systems.AssetManager.AddEffect(name, this);
        }

        public bool InitEffect(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            try
            {
                _effect = content.Load<Effect>(@"Graphics\Effects\AlphaMix");
                _ready = true;
            }
            catch
            {
                Systems.ConsoleWindow.WriteLine("Error loading TwoTextureAlphaEffect");
                return false;
            }
            return true;
        }
    }
}
