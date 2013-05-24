using EquestriEngine.Data.UI;
using EquestriEngine.Objects.Graphics;
using EquestriEngine.Data.Scenes;

namespace EquestriEngine.SystemScreens
{
    public class GameplayScreen : GameScreen
    {
        BasicEffectObject eo;

        public GameplayScreen()
            : base(true,true)
        {

        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            base.LoadContent();
            //eo = EquestrEngine.AssetManager.GetEffect("{basic_effect}") as BasicEffectObject;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
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
