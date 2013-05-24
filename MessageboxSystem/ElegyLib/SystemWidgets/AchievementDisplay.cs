using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Graphics;
using Achievement = EquestriEngine.Data.Achievement;

namespace EquestriEngine.SystemWidgets
{
    public class AchievementDisplay : Data.UI.Widget
    {
        private float _life, _startLife;
        private int instanceNumber;

        public float FirstQuarter
        {
            get { return (_startLife / 4); }
        }

        public float LastQuarter
        {
            get { return (_startLife / 4) * 3; }
        }

        private Rectangle AchievementSourceRect
        {
            get { return new Rectangle(_achievement.IconX * 96, _achievement.IconY * 96, 96, 96); }
        }

        private static Color
            Border_Color = Color.Magenta,
            Mid_Color = Color.Blue;

        Vector2 initPosition,finalPosition;

        Achievement _achievement;

        FontObject _displayFont;
        TextureObject _windowTexture, _achievementTexture;
        TextureObject _windowDisplay;

        public static int Height_Displace = 0,
            _instances = 0;

        private const int 
            WIDGET_HEIGHT = 128,
            WIDGET_WIDTH = 256;

        public AchievementDisplay(float life,Achievement achievement)
        {
            _startLife = _life = life;
            _achievement = achievement;
            initPosition = new Vector2(EquestriEngine.Settings.WindowWidth - WIDGET_WIDTH - 4, EquestriEngine.Settings.WindowHeight);
            finalPosition = new Vector2(EquestriEngine.Settings.WindowWidth - WIDGET_WIDTH - 4, EquestriEngine.Settings.WindowHeight - WIDGET_HEIGHT - Height_Displace);
            Height_Displace += WIDGET_HEIGHT;
            Position = new Vector2(0, -WIDGET_HEIGHT);
            _displayFont = Systems.AssetManager.GetFont("{smallfont}");
            _windowTexture = Systems.AssetManager.GetTexture("{console}");
            _achievementTexture = Systems.AssetManager.GetTexture("{achievement}");
            instanceNumber = _instances;
            TextureObjectFactory.GenerateTextureObjectFromMethod(out _windowDisplay, "{achievement-" + instanceNumber + "}", WIDGET_WIDTH, WIDGET_HEIGHT, GenerateWidget);
            _instances++;
        }

        public override void Init()
        {
            Show();
        }

        public override void Unload()
        {
            _windowDisplay.UnloadAsset();
        }

        public override void Update(float dt)
        {
            if (_life > _startLife - FirstQuarter)
            {
                float pos = _startLife - _life;
                Position = Vector2.Slerp(initPosition, finalPosition, pos / FirstQuarter);
            }
            if (_life < _startLife - LastQuarter && _life >= 0)
            {
                float pos = _startLife - _life;
                Position = Vector2.Slerp(finalPosition, initPosition, (pos - LastQuarter) / (_startLife - LastQuarter));
            }
            if (_life <= 0)
            {
                if (Height_Displace > 0)
                    Height_Displace -= WIDGET_HEIGHT;
                Hide();
            }
            _life -= dt;
        }

        public override void Draw(Equestribatch sb)
        {
            sb.Draw(_windowDisplay, new Rectangle((int)Position.X, (int)Position.Y, WIDGET_WIDTH, WIDGET_HEIGHT), Color.White);
            base.Draw(sb);
        }

        private void GenerateWidget(Equestribatch sb)
        {
            sb.Draw(_windowTexture.Texture, new Rectangle((int)0, (int)0, WIDGET_WIDTH, WIDGET_HEIGHT), Border_Color);
            sb.Draw(_windowTexture.Texture, new Rectangle((int)1, (int)1, WIDGET_WIDTH - 2, WIDGET_HEIGHT - 2), Mid_Color);
            sb.Draw(_achievementTexture.Texture, new Rectangle((int)4, (int)4, 96, 96), AchievementSourceRect, Color.White);
            sb.DrawString(_displayFont, _achievement.ToString(), new Vector2(98, 4), Color.Magenta); 
        }
    }
}
