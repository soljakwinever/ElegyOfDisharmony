using EquestriEngine.Objects.Graphics;
using Microsoft.Xna.Framework;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using SpriteEffects = Microsoft.Xna.Framework.Graphics.SpriteEffects;

namespace EquestriEngine.Objects.Scenes
{
    /// <summary>
    /// Used as something for drawing an image on the screen
    /// </summary>
    public class SceneObject
    {
        BasicEffectObject _effect;
        TextureObject _texture;
        DrawableQuad drawableQuad;

        bool hidden;

        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        public TextureObject Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public SceneObject(string texture, float uvX = 1.0f,float uvY = 1.0f)
        {
            this._texture =Systems.AssetManager.GetTexture(texture);
            this._effect = Systems.AssetManager.GetEffect("{basic_effect}") as BasicEffectObject;
            float widthRatio = (float)_texture.Height / _texture.Width;
            float heightRatio = (float)_texture.Width / _texture.Height; 
            this.drawableQuad = new DrawableQuad(Vector3.Zero, 1, 1, Vector3.Forward, Vector3.Up,uvX,uvY);
            hidden = true;
        }

        public void Draw(Matrix transform)
        {
            if (!hidden)
            {
                _effect.World = transform;
                _effect.Texture = Texture.Texture;
                _effect.DrawWithEffect(drawableQuad);
            }
        }
    }
}
