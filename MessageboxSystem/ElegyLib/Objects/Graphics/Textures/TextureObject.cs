using ContentManager = Microsoft.Xna.Framework.Content.ContentManager;
using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Objects.Graphics
{
    public class TextureObject
    {
        protected Texture2D _texture;
        protected bool _ready;

        private string _fileName, _name;

        public int Width
        {
            get {
                if(_texture == null)
                    return -1;
                return _texture.Width; 
            }
        }

        public int Height
        {
            get
            {
                if (_texture == null)
                    return -1;
                return _texture.Height; 
            }
        }

        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
        }

        public bool Ready
        {
            get { return _ready; }
        }

        public TextureObject(string name, string fileName)
        {
            _ready = false;
            _fileName = fileName;
            Systems.AssetManager.AddTexture(name, this);
            _name = name;
        }
        public TextureObject(string name,Texture2D texture)
        {

            _ready = true;
            if (texture != null)
            {
                Texture2D temp = new Texture2D(texture.GraphicsDevice, texture.Width, texture.Height);
                Data.Scenes.Color[] rawData = new Data.Scenes.Color[texture.Height * texture.Width];
                texture.GetData(rawData);
                temp.SetData(rawData);
                _texture = temp;
            }
            else
                _texture = texture;
            Systems.AssetManager.AddTexture(name, this);
            _name = name;
        }
        protected TextureObject(string name)
        {
            _name = name;
        }
        ~TextureObject()
        {
            if (_texture != null && !_texture.IsDisposed)
                _texture.Dispose();
        }

        public bool LoadTexture(ContentManager content)
        {
            if (_ready)
                return true;
            try
            {
                _texture = content.Load<Texture2D>(_fileName);
                _ready = true;
            }
            catch (System.Exception ex)
            {
                Systems.ConsoleWindow.WriteLine("Warning: {0}", ex.Message);
                return false;
            }
            return true;
        }

        public void UnloadAsset()
        {
            _texture.Dispose();
            Systems.AssetManager.UnloadTexture(_name);
        }

        
        public static implicit operator Texture2D(TextureObject t)
        {
            return t.Texture;
        }
    }
}
