using EquestriEngine.Data.Scenes;
using EquestriEngine.Data.UI;
using EquestriEngine.Objects.Graphics;
using EquestriEngine.GameData.Battle;

namespace EquestriEngine.SystemScreens
{
    public class BattleScreen : GameScreen
    {
        TargetObject pony1;
        BattleController _controller;

        TextureObject _battleStage;

        public BattleScreen()
            : base(false, true)
        {
            _controller = new BattleController();
            _controller.OnActionPerform += _controller_OnActionPerform;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        private void _controller_OnActionPerform(BattleData data, ActionArgs args)
        {
            var damageWidget = new SystemWidgets.PopTextWidget(SystemWidgets.PopType.HealthPop)
            {
                Number = 40,
                Position = new Vector2(512, 512)
            };
            EngineGlobals.GameReference.WidgetDrawer.AddWidget(damageWidget);
            _controller.Paused = true;
        }

        public override void LoadContent()
        {
            pony1 = EquestriEngine.AssetManager.CapturedFrame;
            Objects.Graphics.Misc.TextureLoadList list;
            Objects.Graphics.Misc.TextureLoadList.LoadList(out list, "debug_BattleScreen");
            EquestriEngine.AssetManager.LoadFromLoadList(list);

#if DEBUG
            _battleStage = EquestriEngine.AssetManager.GetTexture("{debug_stage}");
#endif

            base.LoadContent();
            _controller.Init();
        }

        bool woosh;

        public override void Update(float dt)
        {
            _controller.Update(dt);
            if (woosh)
            {
                scale += dt * 1.2f;
                if (scale > 2)
                {
                    scale = 1;
                    woosh = false;
                }
            }
        }

        public override void HandleInput(float dt)
        {
            if (ControlReference.Input1())
            {
                if (_controller.SelectAction)
                {
                    _controller.SelectAction = false;
                }
            }
            if (ControlReference.Input2())
            {
                woosh = true;
            }
            base.HandleInput(dt);
        }

        Vector2 pos = new Vector2(1024 / 2, 768 / 2);
        float scale = 1.0f;
        public override void Draw(float dt)
        {

            SpriteBatch.Begin();

            SpriteBatch.Draw(_battleStage.Texture, EquestriEngine.ViewPort.Bounds, Color.White);
            if (woosh)
                for (int i = 0; i < 6; i++)
                    SpriteBatch.Draw(pony1.Texture, new Vector2(1024 / 2, 768 / 2), null, Color.Multiply(Color.White, ((scale - 1) / 1) * 0.5f), 0.0f, new Vector2(1024 / 2, 768 / 2), scale, 0, 0.0f);

            SpriteBatch.End();
        }
    }
}
