using EquestriEngine.Data.UI;
using EquestriEngine.Objects.Graphics;
using EquestriEngine.Data.Scenes;

namespace EquestriEngine.SystemScreens
{
    public class GameplayScreen : DrawableGameScreen
    {
        BasicEffectObject eo;

        public GameplayScreen()
            : base(false)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            //eo = EquestrEngine.AssetManager.GetEffect("{basic_effect}") as BasicEffectObject;
        }

        public override void UnloadContent()
        {

        }

        public override void Update(float dt)
        {
            //Systems.ConsoleWindow.WriteLine("Updating Gameplay Screen");
        }

        public override void HandleInput(float dt)
        {
            base.HandleInput(dt);
        }

        public override void Draw(float dt)
        {
        }
    }
}
