using EquestriEngine.Data.Collections;
using EquestriEngine.Data.UI.Interfaces;

namespace EquestriEngine.Systems
{
    public class WidgetDrawer : Bases.BaseDrawableSystem
    {
        private static WidgetList _widgets;

        private float dt;

        public WidgetDrawer(object game)
            :base(game)
        {
            _widgets = new WidgetList();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
              _widgets.ForEach(delegate(IWidget w)
            {
                w.Update(dt);
                if (!w.Shown)
                {
                    w.Unload();
                    _widgets.Remove(w);
                }
            });
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_widgets.Count > 0)
                _widgets.ForEach(delegate(IWidget w)
                {
                    if(w.Shown)
                        w.Draw(SpriteBatch);
                });
            base.Draw(gameTime);
        }

        public void AddWidget(IWidget widget)
        {
            _widgets.Add(widget);
            widget.Init();
        }
    }
}
