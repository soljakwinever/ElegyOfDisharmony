using ContentManager = Microsoft.Xna.Framework.Content.ContentManager;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace EquestriEngine.Objects.Graphics
{
    public class SingleColorObject : TextureObject
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

        public SingleColorObject(string name, Color color)
            :base(name)
        {
            _ready = false;
            _color = color;
            Systems.AssetManager.AddTexture(name, this);
        }

        ~SingleColorObject()
        {
            if (_texture != null && !_texture.IsDisposed)
                _texture.Dispose();
        }

        public bool LoadTexture(GraphicsDevice graphics)
        {
            if (graphics == null)
                return false;
            if (_ready)
                return true;
            try
            {
                _texture = new Texture2D(graphics, 1, 1);
                Color[] temp = new Color[1] { _color };
                _texture.SetData(temp);
                _ready = true;
            }
            catch (System.Exception ex)
            {
                Systems.ConsoleWindow.WriteLine("Warning: {0}", ex.Message);
                return false;
            }
            return true;
        }
    }
}
