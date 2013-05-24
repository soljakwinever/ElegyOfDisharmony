using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Graphics;
using Variable = EquestriEngine.Data.Variable;

namespace EquestriEngine.SystemWidgets
{
    public class NameInputWidget : Data.UI.Widget
    {
        private const int 
            WIDGET_HEIGHT = 96,
            WIDGET_WIDTH = 512;

        Variable _nameInput;

        public override void Init()
        {
            Show();
        }

        public override void Unload()
        {
           
        }

        public override void Update(float dt)
        {

        }

        public override void Draw(Equestribatch sb)
        {

        }
    }
}
