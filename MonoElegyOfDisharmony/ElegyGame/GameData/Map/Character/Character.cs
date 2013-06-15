using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Drawing;

namespace ElegyGame.GameData.Map.Character
{
    public delegate void CollideAction(Character sender, Character collided, EquestriEngine.Data.Inputs.Interfaces.IEventInput input);
    public abstract class Character
    {
        public virtual Vector2 Postion
        {
            get { return Vector2.Zero; }
            set { }
        }

        public event CollideAction OnCollision;

        public abstract void Update(float dt);
    }
}
