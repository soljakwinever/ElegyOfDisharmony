using EquestriEngine.Objects.Graphics;
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
        private static bool _systemReady = true;

        private static TextureCollection _textures = null;
        private static FontCollection _fonts = null;
        private static EffectCollection _effects = null;

        public static AssetQuality Quality = AssetQuality.High;

        private static ContentManager _content;
        private static Device _graphics;

        public static int ItemsLoaded
        {
            get { return _textures.Count + _fonts.Count; }
        }

        public AssetManager(Game game)
            : base(game)
        {
            _textures = new TextureCollection();
            _fonts = new FontCollection();
            _effects = new EffectCollection();
            _content = game.Content;
            _graphics = game.GraphicsDevice;
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
            _graphics = Game.GraphicsDevice;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _systemReady = true;
            bool result;

            try
            {
                using (System.IO.Stream stream =
                    System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("EquestriEngine.Resources.Error.derpyeyes.png"))
                {
                    var temp = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(GraphicsDevice, stream);
                    var errorTexture = new TextureObject("{error}", temp);
                }

                using (System.IO.Stream stream =
        System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("EquestriEngine.Resources.Data.achievements.png"))
                {
                    var temp = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(GraphicsDevice, stream);
                    var achievementTexture = new TextureObject("{achievement}", temp);
                }

                using (System.IO.Stream stream =
System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("EquestriEngine.Resources.UI.bitcollection.png"))
                {
                    var temp = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(GraphicsDevice, stream);
                    var achievementTexture = new TextureObject("{bits}", temp);
                }

                var Effect = new Objects.Graphics.BasicEffectObject("{basic_effect}");

                var smallFont = new FontObject("{smallfont}", @"fonts\celestia_redux");
                var largeFont = new FontObject("{largefont}", @"fonts\celestia_redux_large");
            }
            catch (System.Exception ex)
            {
                EquestriEngine.ErrorMessage = ex.Message;
            }

            foreach (TextureObject obj in _textures.Values)
            {
                if (obj is SingleColorObject)
                    result = (obj as SingleColorObject).LoadTexture(GraphicsDevice);
                else
                    result = obj.LoadTexture(_content);
                if (!result)
                    EquestriEngine.ErrorMessage = "Error loading texture";

            }
            foreach (BasicEffectObject obj in _effects.Values)
            {
                obj.InitEffect(GraphicsDevice);
            }
            foreach (FontObject obj in _fonts.Values)
            {
                result = obj.LoadFont(_content);
                if (!result)
                    EquestriEngine.ErrorMessage = "Error loading font";

            }
        }

        public static void AddTexture(string name, TextureObject obj)
        {
            bool result = false;
            if (_textures.ContainsKey(name))
            {
                ConsoleWindow.WriteLine("Warning: Texture {0} already exists", name);
                obj = _textures[name];
                return;
            }

            if (_systemReady)
            {
                if (obj is SingleColorObject)
                    result = _systemReady && (obj as SingleColorObject).LoadTexture(_graphics);
                else if (obj is TargetObject)
                    result = _systemReady && (obj as TargetObject).LoadTexture(_graphics);
                else
                    result = _systemReady && obj.LoadTexture(_content);

            }
            if (!result)
                EquestriEngine.ErrorMessage = "Error loading Texture Asset " + name;
            _textures.Add(name, obj);
        }

        public static void AddEffect(string name, EffectObject obj)
        {
            if (_effects.ContainsKey(name))
            {
                ConsoleWindow.WriteLine("Warning: Effect {0} already exists", name);
                return;
            }
            if (_systemReady)
            {
                if (obj is Objects.Graphics.Interfaces.IGraphicsLoadableEffect)
                    (obj as Objects.Graphics.Interfaces.IGraphicsLoadableEffect).InitEffect(_graphics);
                else if (obj is Objects.Graphics.Interfaces.IContentLoadableEffect)
                    (obj as Objects.Graphics.Interfaces.IContentLoadableEffect).InitEffect(_content);
                else
                    throw new Data.Exceptions.EngineException("Effect not loadable", true);
            }
            _effects.Add(name, obj);
        }

        public static void AddFont(string name, FontObject obj)
        {
            if (_fonts.ContainsKey(name))
            {
                ConsoleWindow.WriteLine("Warning: Font {0} already exists", name);
                return;
            }
            if (_systemReady)
                obj.LoadFont(_content);
            _fonts.Add(name, obj);
        }


        public static void UnloadTexture(string name)
        {
            _textures.Remove(name);
            System.GC.Collect();
        }

        /// <summary>
        /// Get a Texture Object from the AssetManager
        /// </summary>
        /// <param name="texureObject"></param>
        /// <returns></returns>
        public static TextureObject GetTexture(string texureObject)
        {
            TextureObject temp = null;
            try
            {
                temp = _textures[texureObject];
            }
            catch
            {
                EquestriEngine.ErrorMessage = "No such Texture exists";
            }
            return temp;
        }

        public static EffectObject GetEffect(string effectObject)
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
        public static FontObject GetFont(string fontObject)
        {
            FontObject temp = null;
            try
            {
                temp = _fonts[fontObject];
            }
            catch
            {
                EquestriEngine.ErrorMessage = "No such Font exists";
            }
            return temp;
        }
    }
}
