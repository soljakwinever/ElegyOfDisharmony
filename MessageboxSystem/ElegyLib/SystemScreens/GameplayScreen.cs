using EquestriEngine.Data.UI;
using EquestriEngine.Objects.GameObjects;
using EquestriEngine.Objects.Graphics;
using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Scenes;

namespace EquestriEngine.SystemScreens
{
    public class GameplayScreen : GameScreen
    {
        Player _player;

        BasicEffectObject eo;

        SceneObjectNode
            floor,
            wall_n,
            wall_e,
            wall_s,
            wall_w;

        public GameplayScreen(Systems.StateManager manager)
            : base(manager, true)
        {
            _player = new Player();
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            var fluttershy = new TextureObject("fluttershy_body", @"Graphics\Character\Fluttershy\fluttershy_body");
            var debug_walls = new TextureObject("debug_room_walls", @"Graphics\Debug\debug_room_wall");
            var debug_floor = new TextureObject("debug_room_floor", @"Graphics\Debug\debug_room_floorboards");

            _player = new Objects.GameObjects.Player();
            Systems.InputManager.RegisterPlayerNode(_player);
            _player.Position = Vector3.One;

            _player.LoadContent();

            var wGroup = Systems.SceneManager.SearchNodes("World");

            eo = Systems.AssetManager.GetEffect("{basic_effect}") as BasicEffectObject;

            eo.Projection = Matrix.CreatePerspective(MathHelper.PiOver4, 1024f / 768, 0.1f, 100);
            eo.View = Matrix.CreateLookAt(new Vector3(0, 4, -15), Vector3.Zero, Vector3.Up);

            eo.TextureEnabled = true;
            eo.CameraNode = Systems.SceneManager.CurrentCamera;

            floor = new SceneObjectNode("debug_room_floor", "debug_room_floor", new Vector3(0, -0.5f, 0), 4.0f, 4.0f, false);
            floor.Rotation = Quaterion.FromAxisAngle(Vector3.UnitX, MathHelper.ToRadians(90));
            floor.Scale = new Vector3(10, 10, 10);

            wall_n = new SceneObjectNode("debug_room_wall_n", "debug_room_walls", new Vector3(0, 1.5f, 5), false);
            wall_n.Scale = new Vector3(10, 4, 0);
            wGroup.AddNode(wall_n);

            wall_w = new SceneObjectNode("debug_room_wall_e", "debug_room_walls", new Vector3(5, 1.5f, 0), false);
            wall_w.Scale = new Vector3(10, 4, 10);
            wall_w.Rotation = Quaterion.FromAxisAngle(Vector3.UnitY, MathHelper.ToRadians(90));
            wGroup.AddNode(wall_w);

            wall_s = new SceneObjectNode("debug_room_wall_e", "debug_room_walls", new Vector3(0, 1.5f, -5), false);
            wall_s.Scale = new Vector3(10, 4, 10);
            wall_s.Rotation = Quaterion.FromAxisAngle(Vector3.UnitY, MathHelper.ToRadians(180));
            wGroup.AddNode(wall_s);

            wall_e = new SceneObjectNode("debug_room_wall_e", "debug_room_walls", new Vector3(-5, 1.5f, 0), false);
            wall_e.Scale = new Vector3(10, 4, 10);
            wall_e.Rotation = Quaterion.FromAxisAngle(Vector3.UnitY, MathHelper.ToRadians(270));
            wGroup.AddNode(wall_e);

            Systems.SceneManager.CurrentCamera.Target = _player;
            Systems.SceneManager.CurrentCamera.Position = new Vector3(0, 1, 0);

            wGroup.AddNode(floor);
            wGroup.AddNode(_player);

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(float dt)
        {
            _player.Update(dt);
            //Systems.ConsoleWindow.WriteLine("Updating Gameplay Screen");
        }

        public override void HandleInput(float dt)
        {
            base.HandleInput(dt);
        }

        public override void Draw(float dt)
        {
            _stateManager.GraphicsDevice.SamplerStates[0] = Microsoft.Xna.Framework.Graphics.SamplerState.LinearWrap;
        }
    }
}
