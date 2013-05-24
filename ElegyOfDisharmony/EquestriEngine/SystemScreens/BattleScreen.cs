using EquestriEngine.Data.Scenes;
using EquestriEngine.Data.UI;
using EquestriEngine.Objects.Graphics;
using EquestriEngine.GameData.Battle;

namespace EquestriEngine.SystemScreens
{
    public class BattleScreen : GameScreen
    {
        TextureObject pony1;
        BattleController _controller;
        
        TextureObject _battleStage;

        public BattleScreen()
            : base(false,true)
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
                Position = new Vector2(512,512)
            };
            EngineGlobals.GameReference.WidgetDrawer.AddWidget(damageWidget);
            _controller.Paused = true;
        }

        public override void LoadContent()
        {
            Objects.Graphics.Misc.TextureLoadList list;
            Objects.Graphics.Misc.TextureLoadList.LoadList(out list, "debug_BattleScreen");
            EquestriEngine.AssetManager.LoadFromLoadList(list);

#if DEBUG
            _battleStage = EquestriEngine.AssetManager.GetTexture("{debug_stage}");
#endif

            base.LoadContent();
            _controller.Init();
        }

        public override void Update(float dt)
        {
            _controller.Update(dt);
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
            base.HandleInput(dt);
        }

        Vector2 pos = new Vector2(1024 / 2, 768 / 2);
        public override void Draw(float dt)
        {

            SpriteBatch.Begin();

            SpriteBatch.Draw(_battleStage.Texture, EquestriEngine.ViewPort.Bounds, Color.White);

            SpriteBatch.End();
        }
    }
}
