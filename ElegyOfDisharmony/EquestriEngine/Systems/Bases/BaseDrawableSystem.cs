using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Systems.Bases
{
    public class BaseDrawableSystem : DrawableGameComponent
    {
        private Equestribatch _spriteBatch;

        public Equestribatch SpriteBatch
        {
            get { return _spriteBatch; }
        }

        public BaseDrawableSystem(object game)
            :base(game as Game)
        {

        }

        protected override void LoadContent()
        {
            _spriteBatch = new Equestribatch(GraphicsDevice);
            base.LoadContent();
        }
    }
}
