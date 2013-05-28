using EquestriEngine.Data.Scenes;

namespace EquestriEngine.MenuData
{
    public abstract class MenuObject
    {
        private Vector2 _position;
        private bool _ready;

        public bool Ready
        {
            get { return _ready; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public virtual void Init()
        {
            _ready = true;
        }

        public virtual bool LoadContent()
        {
            return true;
        }

        public abstract void Update(float dt);
        public abstract void Draw(float dt);
    }
}
