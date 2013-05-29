using EquestriEngine.Data.UI;
using System.Collections.Generic;
using EquestriEngine.MenuData;

namespace EquestriEngine.SystemScreens
{
    public class MenuScreen : DrawableGameScreen
    {
        List<MenuObject> _controls;

        List<MenuData.Inputs.SelectableObject> _selectableObjects;

        public MenuScreen()
            :base(true)
        {
            _controls = new List<MenuObject>();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {

        }

        public override void UnloadContent()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(float dt)
        {
            foreach (var c in _controls)
            {
                c.Update(dt);
            }
        }

        public override void HandleInput(float dt)
        {
            base.HandleInput(dt);
        }

        public override void Draw(float dt)
        {
            foreach (var c in _controls)
            {
                c.Update(dt);
            }
        }

        protected void AddControl(MenuObject control)
        {
            if (control is MenuData.Inputs.SelectableObject)
            {
                _selectableObjects.Add((MenuData.Inputs.SelectableObject)control);
            }
            if (!control.Ready)
            {
                control.Init();
                control.LoadContent();
            }
            _controls.Add(control);
        }
    }
}
