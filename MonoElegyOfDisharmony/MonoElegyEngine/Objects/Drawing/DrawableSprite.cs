using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Objects.Drawing
{
    public class DrawableSprite
    {
        private TextureObject _texture;

        private Vector2 
            _position, _origin;
        private float _rotation, _scale, _depth;

        private bool _flipped;
        private Color _color;

        private Rectangle? _source;

        #region Properties

        public TextureObject Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public float Depth
        {
            get { return _depth; }
            set { _depth = value; }
        }

        public bool Flipped
        {
            get { return _flipped; }
            set { _flipped = value; }
        }

        public Rectangle? Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        #endregion

        #region Static Members

        private static Equestribatch _eqBatch;

        static DrawableSprite()
        {
            _eqBatch = EngineGlobals.GameReference.SpriteBatch;
        }

        #endregion

        #region Methods

        public void Draw()
        {
            _eqBatch.Draw(_texture.Texture,new Rectangle(
                (int)_position.X,
                (int)_position.Y,(int)(_texture.Width * _scale),(int)(_texture.Height * _scale)),
                _source,_color,_rotation,_origin,_flipped ? SpriteEffects.FlipHorizontally : 0,_depth);
        }

        #endregion

        #region Static Methods

        public static void BeginDrawSortBTF(Matrix? m)
        {
            _eqBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, 
                null, null, null, 
                null, m.HasValue ? m.Value : Matrix.Identity);
        }

        public static void EndDrawSort()
        {
            _eqBatch.End();

            _eqBatch.GraphicsDevice.DepthStencilState = EquestriEngine.DepthState;
        }

        #endregion
    }
}
