using Vector2 = EquestriEngine.Data.Scenes.Vector2;
namespace EquestriEngine.Data.UI
{
    public abstract class Widget : Interfaces.IWidget
    {
        private Vector2 _position;
        private bool _shown;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public bool Shown
        {
            get { return _shown; }
        }

        public Widget()
        {
            Systems.WidgetDrawer.AddWidget(this);
        }

        public abstract void Init();
        public abstract void Update(float dt);
        public abstract void Unload();

        public void Show()
        {
            _shown = true;
        }

        public void Hide()
        {
            _shown = false;
        }

        public virtual void Draw(Equestribatch sb)
        {

        }
    }
}
