using ContentManager = Microsoft.Xna.Framework.Content.ContentManager;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace EquestriEngine.Objects.Graphics
{
    public class PixelObject : TextureObject
    {
        private Color _color;

        public new int Width
        {
            get
            {
                return 1;
            }
        }

        public new int Height
        {
            get
            {
                return 1;
            }
        }

        public PixelObject(string name,GraphicsDevice device, Color color)
            :base(name)
        {
            _ready = true;
            _color = color;
            _texture = new Texture2D(device, 1, 1);
            Color[] data = new Color[]
            {
                color
            };
            _texture.SetData(data);
        }

        ~PixelObject()
        {
            if (_texture != null && !_texture.IsDisposed)
                _texture.Dispose();
        }

        public override void UnloadAsset()
        {
            Texture.Dispose();
            EquestriEngine.AssetManager.UnloadTexture(this, true);
        }
    }
}
