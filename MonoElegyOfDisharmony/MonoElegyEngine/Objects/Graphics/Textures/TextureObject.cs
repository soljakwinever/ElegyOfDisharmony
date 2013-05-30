using ContentManager = Microsoft.Xna.Framework.Content.ContentManager;
using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Objects.Graphics
{
    public class TextureObject : Interfaces.ILoadable
    {
        protected Texture2D _texture;
        protected bool _ready;

        protected string _fileName, _name;

        public int Width
        {
            get {
                if(!_ready)
                    return -1;
                return _texture.Width; 
            }
        }

        public int Height
        {
            get
            {
                if (!_ready)
                    return -1;
                return _texture.Height; 
            }
        }

        public string Name
        {
            get { return _name; }
        }

        public bool Ready
        {
            get { return _ready; }
        }

        public Texture2D Texture
        {
            get 
            {
                if (_ready)
                {
                    return _texture;
                }
                else
                    return EngineGlobals.GameReference.AssetManager.ErrorTexture.Texture;
            }
        }

        public TextureObject(string name, string fileName)
        {
            _ready = false;
            _fileName = fileName;
            _name = name;
        }
        public TextureObject(string name,Texture2D texture, bool copyData = false)
        {
            _ready = true;
            if (copyData)
            {
                _texture = new Texture2D(texture.GraphicsDevice, texture.Width, texture.Height);
                Data.Scenes.Color[] rawData = new Data.Scenes.Color[texture.Height * texture.Width];
                texture.GetData(rawData);
                _texture.SetData(rawData);
                rawData = null;
            }
            else
                _texture = texture;
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

        public bool Load(ContentManager content)
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

        public virtual void UnloadAsset()
        {
            EngineGlobals.GameReference.AssetManager.UnloadTexture(_name, false);
        }

        public override string ToString()
        {
            if(_texture != null)
                return string.Format("{0} - {1}", _name, _texture.IsDisposed);
            else
                return string.Format("{0} - Null", _name);
        }

        public override int GetHashCode()
        {
            return _texture.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            try
            {
                var tex = obj as TextureObject;
                return this._fileName == tex._fileName || this._name == tex._name;
            }
            catch
            {
                return false;
            }
        }
    }
}
