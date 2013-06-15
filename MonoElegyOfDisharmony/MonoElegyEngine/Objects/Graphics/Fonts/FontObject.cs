using Vector2 = Microsoft.Xna.Framework.Vector2;
using SpriteFont = Microsoft.Xna.Framework.Graphics.SpriteFont;
using ContentManager = Microsoft.Xna.Framework.Content.ContentManager;
using System.Text;

namespace EquestriEngine.Objects.Graphics
{
    public class FontObject
    {
        private string _fileName;
        private SpriteFont _font;
        private bool _ready;

        public bool Ready
        {
            get { return _ready; }
        }

        public SpriteFont Font
        {
            get {
                if (_ready)
                    return _font;
                else
                    throw new EngineException("Font has not been loaded", false);
            }
        }

        public FontObject(string name,string fileName)
        {
            _fileName = fileName;
            _ready = false;
        }
        ~FontObject()
        {
            
        }

        public bool LoadFont(ContentManager content)
        {
            try
            {
                _font = content.Load<SpriteFont>(_fileName);
                _ready = true;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Vector2 Measure(string input)
        {
            return _font.MeasureString(input);
        }

        public Vector2 CenterAlign(string input)
        {
            Vector2 temp = _font.MeasureString(input) / 2;
            return temp;
        }

        public Vector2 CenterAlignX(string input)
        {
            Vector2 temp = _font.MeasureString(input);
            temp.X /= 2;
            return temp;
        }

        public Vector2 CenterAlignY(string input)
        {
            Vector2 temp = _font.MeasureString(input);
            temp.Y /= 2;
            return temp;
        }

        public string WrapText(string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');

            StringBuilder sb = new StringBuilder();

            float lineWidth = 0f;

            float spaceWidth = _font.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = _font.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }

        public static implicit operator SpriteFont(FontObject f)
        {
            return f.Font;
        }
    }
}
