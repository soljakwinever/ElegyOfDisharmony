using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Objects.Graphics
{
    public class TextureAlphaEffect : EffectObject, Interfaces.IContentLoadableEffect
    {
        public Texture2D TextureA
        {
            set { _effect.Parameters["image"].SetValue(value); }
        }

        public Texture2D AlphaMap
        {
            set { _effect.Parameters["alpha"].SetValue(value); }
        }

        public TextureAlphaEffect(string name)
            :base("TextureAlpha")
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
                Systems.ConsoleWindow.WriteLine("Error loading TextureAlphaEffect");
                return false;
            }
            return true;
        }
    }
}
