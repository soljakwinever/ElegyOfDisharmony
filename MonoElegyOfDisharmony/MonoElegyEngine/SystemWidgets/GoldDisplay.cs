using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Graphics;

namespace EquestriEngine.SystemWidgets
{
    public class GoldDisplay : Data.UI.Widget
    {
        private int startGold = 0;
        private int goldDisplayed;
        private int goldRecieved = 0;

        private float timer;

        private const int 
            WIDGET_HEIGHT = 128,
            WIDGET_WIDTH = 256;

        private const float 
            ENTRYTIME = 1.15f,
            DISPLAYTIME = 2.25f,
            DELAYTIME = 2.0f,
            EXITTIME = 1.15f;

        TextureObject _bits;
        FontObject _fontDisplay;

        private Color _displayColor;

        Vector2 _displayPos;

        private bool entryComplete, displayComplete, delayComplete, exitComplete;

        public GoldDisplay(int _startGold, int _goldRecieved)
        {                
            startGold = goldDisplayed = _startGold;
            goldRecieved = _goldRecieved;

            _displayPos = new Vector2(EngineGlobals.Settings.WindowWidth - 80, EngineGlobals.Settings.WindowHeight - 46);
        }

        public override void Init()
        {
            entryComplete = 
                displayComplete = 
                delayComplete = 
                exitComplete = false;
            _bits = EngineGlobals.GameReference.AssetManager.GetTexture("{bits}");
            _fontDisplay = EngineGlobals.GameReference.AssetManager.GetFont("{largefont}");
            Show();
        }

        public override void Unload()
        {
            
        }

        /// <summary>
        /// Update the Widget
        /// </summary>
        /// <param name="dt"></param>
        public override void Update(float dt)
        {
            timer += dt;
            _displayPos.Y = MathHelper.Slerp(_displayPos.Y, EngineGlobals.Settings.WindowHeight - System.Math.Max(AchievementDisplay.Height_Displace + 46, 128), 0.1f);
            if (!entryComplete && timer > ENTRYTIME)
            {
                timer = 0;
                entryComplete = true;
            }
            else if (
                (entryComplete && !displayComplete))
            {
                goldDisplayed = (int)MathHelper.Slerp(startGold, startGold + goldRecieved, timer / DELAYTIME);
                if (timer >= DISPLAYTIME)
                {
                    displayComplete = true;
                    timer = 0;
                }
            }
            else if(
                (displayComplete && !delayComplete) && timer > DELAYTIME)
            {
                timer = 0;
                delayComplete = true;
            }
            else if (
                (delayComplete && !exitComplete))
            {
                if (timer > EXITTIME)
                {
                    Hide();
                }
            }
        }

        public override void Draw(Equestribatch sb)
        {
            if (!entryComplete)
                _displayColor = Color.Multiply(Color.White, timer / ENTRYTIME);
            if (delayComplete && !exitComplete)
                _displayColor = Color.Multiply(Color.White, 1 - (timer / EXITTIME));
            sb.Begin();

            if(entryComplete && !displayComplete)
                sb.DrawString(_fontDisplay,(goldRecieved >= 0 ? "+" : "") + goldRecieved,
                    Vector2.Lerp(_displayPos,_displayPos - new Vector2(0,128),timer / DISPLAYTIME),
                    Color.Multiply(goldRecieved >= 0 ? Color.Yellow : Color.Red,1 - (timer/DISPLAYTIME)));

            //sb.DrawString(_fontDisplay, "" + goldDisplayed, _displayPos + Vector2.One, );
            sb.DrawString(_fontDisplay, "" + goldDisplayed, _displayPos, _displayColor);
            sb.Draw(_bits.Texture, _displayPos - new Vector2(38, -8), _displayColor);

            sb.End();

            base.Draw(sb);
        }
    }
}
