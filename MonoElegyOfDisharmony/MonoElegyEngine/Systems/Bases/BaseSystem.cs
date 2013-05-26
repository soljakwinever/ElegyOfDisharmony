using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Systems.Bases
{
    public class BaseSystem : GameComponent
    {
        public BaseSystem(object game)
            : base(game as Game)
        {

        }
    }
}
