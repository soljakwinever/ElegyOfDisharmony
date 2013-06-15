using EquestriEngine.Objects.Graphics;
using EquestriEngine.Objects;
using Microsoft.Xna.Framework;
using Device = Microsoft.Xna.Framework.Graphics.GraphicsDevice;
using Microsoft.Xna.Framework.Content;

namespace EquestriEngine.Systems
{
    public enum AssetQuality
    {
        High,
        Medium,
        Low
    }

    public class AssetManager : Bases.BaseDrawableSystem
    {
        private bool _systemReady = true;

        public const string MISSING_TEXTURE = "{notex}";

        private TextureCollection _textures = null;
        private FontCollection _fonts = null;
        private EffectCollection _effects = null;
        private SkeletonCollection _skeletons = null;

        private TargetObject _frameCapture;
        private TextureObject _empty;
        private PixelObject _singleWhite;
        private bool _frameCaptured;

        public TextureObject ErrorTexture
        {
            get { return _textures[MISSING_TEXTURE]; }
        }

        public AssetQuality Quality = AssetQuality.High;

        private ElegyEngine.Content.ExclusiveContentManager _content;

        private bool 
            _textureLoad,
            _skeletonLoad,
            _effectLoad,
            _fontLoad;

        public int ItemsLoaded
        {
            get { return _textures.Count + _fonts.Count; }
        }

        public bool FrameCapture
        {
            get { return _frameCaptured; }
            set { _frameCaptured = value; }
        }

        public TargetObject CapturedFrame
        {
            get { return _frameCapture; }
        }

        public TextureObject Empty
        {
            get { return _empty; }
        }

        public PixelObject SingleWhite
        {
            get { return _singleWhite; }
        }

        public AssetManager(Game game)
            : base(game)
        {
            _textures = new TextureCollection();
            _fonts = new FontCollection();
            _effects = new EffectCollection();
            _skeletons = new SkeletonCollection();

            _content = new ElegyEngine.Content.ExclusiveContentManager(game.Services);

            _textureLoad = false;
            _effectLoad = false;
            _fontLoad = false;
            _skeletonLoad = false;

            _frameCaptured = false;
        }
        ~AssetManager()
        {
            _textures.Clear();
            _fonts.Clear();
            _effects.Clear();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _systemReady = true;

            try
            {
                FromInternal("{error}", "Error.derpyeyes");
                FromInternal("{notex}", "Error.notexture");
                FromInternal("{achievement}", "Data.achievements");
                FromInternal("{bits}", "UI.bitcollection");
                FromInternal("{load}", "Loading.loadingscreen_filler");

                var Effect = new Objects.Graphics.BasicEffectObject("{basic_effect}");

                var smallFont = new FontObject("{smallfont}", @"fonts\celestia_redux");
                _fonts["{smallfont}"] = smallFont;
                var largeFont = new FontObject("{largefont}", @"fonts\celestia_redux_large");
                _fonts["{largefont}"] = largeFont;

                _frameCapture = new TargetObject("{screen}", GraphicsDevice, EngineGlobals.Settings.WindowWidth, EngineGlobals.Settings.WindowHeight);
                _empty = CreatePixelTexture("{empty}", Color.Transparent);
                _singleWhite = CreatePixelTexture("{single}");
            }
            catch (System.Exception ex)
            {
                EquestriEngine.ErrorMessage = ex.Message;
            }

            LoadTextures();
            LoadFonts();
            LoadEffects();
            LoadSkeletons();
        }

        protected override void UnloadContent()
        {
            _frameCapture.UnloadAsset();
            base.UnloadContent();
        }

        private void LoadTextures()
        {
            bool result = false;
            foreach (var kvp in _textures)
            {
                if (!kvp.Value.Ready)
                {
                    if(kvp.Value is TextureObject || kvp.Value is TextureAtlas)
                        result = kvp.Value.Load(_content);
                    if (!result)
                        EquestriEngine.ErrorMessage = "Error loading texture";
                }
                else
                    result = true;
                if (!result)
                    Systems.ConsoleWindow.WriteLine("Error loading texture " + kvp.Value);
            }
            _textureLoad = false;
        }

        private void LoadFonts()
        {
            bool result = false;
            foreach (var kvp in _fonts)
            {
                if (!kvp.Value.Ready)
                {
                    result = kvp.Value.LoadFont(_content);

                    if (!result)
                        EquestriEngine.ErrorMessage = "Error loading font";
                }
                if (!result)
                    Systems.ConsoleWindow.WriteLine("Error loading font " + kvp.Value);
            }
            _fontLoad = false;
        }

        private void LoadEffects()
        {
            bool result = false;
            foreach (var kvp in _effects)
            {
                if (!kvp.Value.Ready)
                {
                    if (kvp.Value is Objects.Graphics.Interfaces.IGraphicsLoadableEffect)
                        result = (kvp.Value as Objects.Graphics.Interfaces.IGraphicsLoadableEffect).InitEffect(GraphicsDevice);
                    else if (kvp.Value is Objects.Graphics.Interfaces.IContentLoadableEffect)
                        result = (kvp.Value as Objects.Graphics.Interfaces.IContentLoadableEffect).InitEffect(_content);
                    else
                        throw new EngineException("Effect not loadable", true);
                }
                if (!result)
                    Systems.ConsoleWindow.WriteLine("Error loading effect " + kvp.Value);
            }
            _effectLoad = false;
        }

        private void LoadSkeletons()
        {
            bool result = false;
            foreach (var kvp in _skeletons)
            {
                if (!kvp.Value.Ready)
                {
                    try
                    {
                        if (kvp.Value is Objects.DrawableSkeleton)
                            result = (kvp.Value as Objects.DrawableSkeleton).Load(GraphicsDevice);
                    }
                    catch(System.Exception ex)
                    {
                        EquestriEngine.ErrorMessage = "Content Load Error: " + ex.Message;
                    }
                }
                if (!result)
                    Systems.ConsoleWindow.WriteLine("Error loading font " + kvp.Value);
            }
            _skeletonLoad = false;
        }

        private void FromInternal(string texName, string imageName)
        {
            using (System.IO.Stream stream =
    System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("EquestriEngine.Resources." + imageName + ".png"))
            {
                var temp = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(GraphicsDevice, stream);
                var newTex = new TextureObject(texName, temp);
                _textures[texName] = newTex;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_textureLoad)
            {
                LoadTextures();
            }
            if (_fontLoad)
            {
                LoadFonts();
            }
            if (_effectLoad)
            {
                LoadEffects();
            }
            if (_skeletonLoad)
            {
                LoadSkeletons();
            }

            base.Update(gameTime);
        }

        #region Unloading


        public void UnloadTexture(string name, bool disposed)
        {
            if (!disposed)
                _content.Unload(_textures[name].Texture);
            _textures.Remove(name);
        }

        public void UnloadTexture(TextureObject texture, bool disposed)
        {
            if (disposed)
                _content.Unload(texture.Texture);
            _textures.Remove(texture.Name);
        }

        public void UnloadTexture(TextureCollection collection)
        {
            foreach (var kvp in collection.Values)
            {
                _content.Unload(kvp.Texture);
            }
        }

        #endregion

        #region Propagation Methods

        public TextureCollection LoadFromLoadList(Objects.Graphics.Misc.TextureLoadList list)
        {
            if (!_systemReady)
                throw new EngineException("System not ready for creation", true);
            TextureCollection collection = new TextureCollection();
            foreach (var kvp in list.Entries)
            {
                switch(kvp.Value[1].ToString())
                {
                    case"Atlas":
                        var atlas = new TextureAtlas(kvp.Key, kvp.Value[0].ToString(),kvp.Value[2]);
                        _textures[atlas.Name] = atlas;
                        collection.Add(atlas.Name, atlas);
                        break;
                    case"Texture":
                        var texture = new TextureObject(kvp.Key, kvp.Value[0].ToString());
                        _textures[texture.Name] = texture;
                        collection.Add(texture.Name, texture);
                        break;
                    case "Target":
                        var target = CreateTargetObject(kvp.Key, (int)((int[])kvp.Value[2])[0], (int)((int[])kvp.Value[2])[1]);
                        _textures[target.Name] = target;
                        collection.Add(target.Name, target);
                        break;
                }
            }
            LoadTextures();
            return collection;
        }

        public FontObject CreateFontFromFile(string name, string fileName)
        {
            if (!_systemReady)
                throw new EngineException("System not ready for creation", true);
            if (_fonts.ContainsKey(name))
                return _fonts[name];
            else
            {
                FontObject newFont = new FontObject(name, fileName);
                _fonts[name] = newFont;
                return newFont;
            }
        }

        public PixelObject CreatePixelTexture(string name)
        {
            PixelObject newPixel = new PixelObject(name, GraphicsDevice, Color.White);
            _textures[name] = newPixel;
            return newPixel;
        }

        public PixelObject CreatePixelTexture(string name,Color color)
        {
            PixelObject newPixel = new PixelObject(name, GraphicsDevice, color);
            _textures[name] = newPixel;
            return newPixel;
        }

        public TextureObject CreateTextureObjectFromFile(string name, string file)
        {
            if (!_systemReady)
                throw new EngineException("System not ready for creation", true);
            if (!_textures.ContainsKey(name))
            {
                TextureObject newTexture = new TextureObject(name, file);
                _textures[name] = newTexture;
                _textureLoad = true;
                return newTexture;
            }
            else
            {
                return _textures[name];
            }
        }

        public TextureObject CreateTextureObjectFromTarget(string name, Microsoft.Xna.Framework.Graphics.RenderTarget2D target)
        {
            if (!_systemReady)
                throw new EngineException("System not ready for creation", true);
            if (!_textures.ContainsKey(name))
            {
                TextureObject newTexture = new TextureObject(name, target,true);
                _textures[name] = newTexture;
                return newTexture;
            }
            else
            {
                return _textures[name];
            }
        }

        public TextureObject CreateTextureObjectFromTarget(string name, TargetObject target)
        {
            if (!_systemReady)
                throw new EngineException("System not ready for creation", true);
            if (!_textures.ContainsKey(name))
            {
                TextureObject newTexture = new TextureObject(name, target.Texture);
                _textures[name] = newTexture;
                return newTexture;
            }
            else
            {
                return _textures[name];
            }
        }

        public TargetObject CreateTargetObject(string name, int width, int height)
        {
            if (!_systemReady)
                throw new EngineException("System not ready for creation", true);
            if (!_textures.ContainsKey(name))
            {
                TargetObject newTarget = new TargetObject(name, GraphicsDevice, width, height);
                _textures[name] = newTarget;
                return newTarget;
            }
            else
            {
                return _textures[name] as TargetObject;
            }
        }

        #endregion

        public TextureObject GetTexture(string texureObject)
        {
            try
            {
                return _textures[texureObject];
            }
            catch
            {
                return _textures[MISSING_TEXTURE];
            }
        }

        public EffectObject GetEffect(string effectObject)
        {
            EffectObject temp = null;
            try
            {
                temp = _effects[effectObject];
            }
            catch
            {
                EquestriEngine.ErrorMessage = "No such Effect exists";
            }
            return temp;
        }

        /// <summary>
        /// Get a Font Object from the AssetManager
        /// </summary>
        /// <param name="fontObject"></param>
        /// <returns></returns>
        public FontObject GetFont(string fontObject)
        {
            FontObject temp = null;
            try
            {
                temp = _fonts[fontObject];
            }
            catch
            {
                throw new EngineException("No such font exists " + fontObject, true);
            }
            return temp;
        }
    }
}
