using EquestriEngine.Data.UI;
using EquestriEngine.Objects.Graphics;
using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Scenes;
using EquestriEngine;

namespace ElegyGame_Beta.Screens
{
    public class ParticleTestImageScreen : GameScreen
    {

        TextureObject _cloud;
        TextureObject _swirl;
        TextureObject _alpha;

        TextureObject _screenshot;

        TextureObject _screenTexture;

        BasicEffectObject eo;

        TwoTextureAlphaEffect _twoTextureEffect;

        TextureAlphaEffect _textureEffect;

        TargetObject _swirlTarget;

        SceneObjectNode _floor;

        float rotationA, rotationB;

        public ParticleTestImageScreen(EquestriEngine.Systems.StateManager manager)
            : base(manager, true)
        {

        }

        public override void Initialize()
        {

        }
        public override void LoadContent()
        {
            _cloud = new TextureObject("{cloud}", @"Graphics\Particles\cloud_puff");
            _swirl = new TextureObject("{cloud_swirl}", @"Graphics\Particles\cloud_swirl");
            _alpha = new TextureObject("{cloud_alpha}", @"Graphics\Particles\cloud_puff_a");

            _swirlTarget = new TargetObject("swirl_target", 96, 96);
            _twoTextureEffect = new TwoTextureAlphaEffect("{two_tex_alpha}");
            _twoTextureEffect.TextureB = _swirlTarget;
            _twoTextureEffect.AlphaMap = _alpha;

            _textureEffect = new TextureAlphaEffect("{tex_alpha}");
            _screenshot = new TextureObject("screen_shot", @"Graphics\Debug\screenshot_a");
            var _screenshotAlpha = new TextureObject("screen_shot_alpha", @"Graphics\Debug\screenshot_a_alpha");
            _textureEffect.TextureA = _screenshot;
            _textureEffect.AlphaMap = _screenshotAlpha;
            TextureObjectFactory.GenerateTextureObjectFromMethod(out _screenTexture,"{screen_texture}",638,360,CreateScreen,Color.Transparent,_textureEffect);
            _screenshot.UnloadAsset();
            _screenshotAlpha.UnloadAsset();

            var wGroup = EquestriEngine.Systems.SceneManager.SearchNodes("World");
            float y1 = 0, y2 = 3.35f;
            for (int j = 5; j >= -5; j -= 10)
            {
                for (int i = 0; i < 5; i++)
                {
                    var temp = new SceneObjectNode("screen_" + i, "{screen_texture}", new Vector3(j, i % 2 == 0 ? y1 : y2, -3 + (i * 6f)), false);
                    temp.Scale = new Vector3(6, 2.75f, 1);
                    temp.Rotation = Quaterion.FromAxisAngle(Vector3.UnitY, MathHelper.ToRadians(j == 5 ? 90 : -90));
                    wGroup.AddNode(temp);
                }
                y1 = 3.35f;
                y2 = 0;
            }

            eo = EquestriEngine.Systems.AssetManager.GetEffect("{basic_effect}") as BasicEffectObject;

            eo.TextureEnabled = true;
            eo.CameraNode = EquestriEngine.Systems.SceneManager.CurrentCamera;

            var camera = EquestriEngine.Systems.SceneManager.CurrentCamera;

            camera.Position -= new Vector3(0, 4,0);

            var floorTexture = new TextureObject("{floor_texture}", @"Graphics\Debug\gradient_path");

            _floor = new SceneObjectNode("floor", "{floor_texture}", Vector3.Zero, false);
            _floor.Rotation = Quaterion.FromAxisAngle(Vector3.UnitX, MathHelper.ToRadians(90));
            _floor.Scale = new Vector3(3, 30, 1);
            wGroup.AddNode(_floor);

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(float dt)
        {
            rotationB += dt * 4;
        }

        public override void HandleInput(float dt)
        {
            base.HandleInput(dt);
        }

        public override void Draw(float dt)
        {
            _swirlTarget.RunTarget(DrawSwirl,Color.Transparent);

            _stateManager.SpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred,
                Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend,null,null,null,_twoTextureEffect);

            _stateManager.SpriteBatch.Draw(_cloud,new Rectangle(0,0,96,96),Color.White);

            _stateManager.SpriteBatch.End();
        }

        private void CreateScreen(EquestriEngine.Equestribatch sb)
        {
            sb.Draw(_screenshot, new Vector2(0, 0), null, Color.White);
        }

        private void DrawSwirl(EquestriEngine.Equestribatch sb)
        {
            sb.Draw(_swirl, new Vector2(35, 20), null, Color.White, rotationB, new Vector2(48, 48), 0.75f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0.0f);
        }
    }
}
