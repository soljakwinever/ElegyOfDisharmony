using EquestriEngine.Data.UI;
using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Graphics;

namespace EquestriEngine.SystemWidgets
{
    /*struct Particle2D
    {
        Vector2 
            position,
            velocity;
        float life;

        public void Update(float dt)
        {
            life -= dt;
        }
    }*/

    public enum PopType
    {
        PDamagePop,
        EDamagePop,
        HealthPop,
        MagicPop,
        DebuffPop,
        BuffPops
    }

    public class PopTextWidget : Widget
    {
        private TextureAtlas _popText;
        public TextureObject _number;
        private int _num;

        PopType _type;

        private float _life, start_life,
            _starRotation;

        private const string STAR_NAME = "star";

        System.Random random = new System.Random();

        private Vector2 _velocity;

        public int Number
        {
            get { return _num; }
            set
            {
                _num = value;
            }
        }

        public PopTextWidget(PopType type)
        {
            _life = start_life = random.Next(1, 3);

            _velocity = new Vector2((float)((random.NextDouble() * 5) - 2.5f), -5);
            Position = new Vector2(1024 / 2, 768 / 2);

            _type = type;
        }

        public override void Init()
        {
            _popText = EngineGlobals.AssetManager.CreateTextureObjectFromFile("{pop_text}",@"Graphics\UI\pop_text") as TextureAtlas;
            TextureObjectFactory.GenerateTextureObjectFromPopText(out _number, _num, Color.Transparent);
            Show();
        }

        public override void Unload()
        {
            _popText.UnloadAsset();
            _number.UnloadAsset();
        }

        public override void Update(float dt)
        {
            _life -= dt;
            if (_life <= 0)
            {
                Hide();
            }
            _starRotation += dt * 5;
            _velocity.Y += 0.25f;
            Position += _velocity;
        }

        public override void Draw(Equestribatch sb)
        {
            sb.Begin();

            sb.Draw(_popText.Texture, Position, _popText[STAR_NAME], Color.Yellow,
                _starRotation, _popText[STAR_NAME].Center, 1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0.0f);

            Color textColor;

            switch (_type)
            {
                case PopType.HealthPop:
                    textColor = Color.Green;
                    break;
                case PopType.PDamagePop:
                    textColor = Color.Red;
                    break;
                case PopType.EDamagePop:
                    textColor = Color.White;
                    break;
                default:
                    textColor = Color.Violet;
                    break;
            }

            sb.Draw(_number.Texture, Position, null,textColor,
             0.0f, new Vector2(_number.Width / 2, _number.Height / 2), 1, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0.0f);

            sb.End();

            //base.Draw(sb);
        }

        #region PopType Specifics

        private void UpdatePDamagePop(float dt)
        {

        }

        private void UpdateEDamagePop(float dt)
        {

        }

        #endregion
    }
}
