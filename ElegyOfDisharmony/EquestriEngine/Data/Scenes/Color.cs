using ColorRGBA = Microsoft.Xna.Framework.Color;
namespace EquestriEngine.Data.Scenes
{
    public struct Color
    {
        private ColorRGBA _color;

        #region Members

        public byte R
        {
            get { return _color.R; }
            set { _color.R = value; }
        }

        public byte G
        {
            get { return _color.G; }
            set { _color.G = value; }
        }

        public byte B
        {
            get { return _color.B; }
            set { _color.B = value; }
        }

        public byte A
        {
            get { return _color.A; }
            set { _color.A = value; }
        }

        public uint PackedValue
        {
            get { return _color.PackedValue; }
        }

        #endregion

        #region Constructors

        public Color(int r, int g, int b)
        {
            _color = new ColorRGBA(r, g, b);
        }

        public Color(float r = 1, float g = 1, float b = 1, float a = 1)
        {
            _color = new ColorRGBA(r, g, b, a);
        }

        private Color(ColorRGBA c)
        {
            _color = c;
        }

        #endregion

        #region Methods

        public Vector3 ToVector3()
        {
            return new Vector3((float)R / 255, (float)G / 255, (float)B / 255);
        }

        #endregion

        #region Presets

        public static Color AliceBlue
        {
            get { return ColorRGBA.AliceBlue; }
        }

        public static Color AntiqueWhite
        {
            get { return ColorRGBA.AntiqueWhite; }
        }

        public static Color White
        {
            get { return ColorRGBA.White; }
        }

        public static Color Black
        {
            get { return ColorRGBA.Black; }
        }

        public static Color Red
        {
            get { return ColorRGBA.Red; }
        }

        public static Color Blue
        {
            get { return ColorRGBA.Blue; }
        }

        public static Color Green
        {
            get { return ColorRGBA.Green; }
        }

        public static Color SkyBlue
        {
            get { return ColorRGBA.SkyBlue; }
        }

        public static Color LightSkyBlue
        {
            get { return ColorRGBA.LightSkyBlue; }
        }

        public static Color DeepSkyBlue
        {
            get { return ColorRGBA.DeepSkyBlue; }
        }

        public static Color Orange
        {
            get { return ColorRGBA.Orange; }
        }

        public static Color Pink
        {
            get { return ColorRGBA.Pink; }
        }

        public static Color HotPink
        {
            get { return ColorRGBA.HotPink; }
        }

        public static Color LightPink
        {
            get { return ColorRGBA.LightPink; }
        }

        public static Color DeepPink
        {
            get { return ColorRGBA.DeepPink; }
        }

        public static Color Purple
        {
            get { return ColorRGBA.Purple; }
        }

        public static Color MediumPurple
        {
            get { return ColorRGBA.MediumPurple; }
        }

        public static Color Magenta
        {
            get { return ColorRGBA.Magenta; }
        }

        public static Color Transparent
        {
            get { return ColorRGBA.Transparent; }
        }

        public static Color Violet
        {
            get { return ColorRGBA.Violet; }
        }

        public static Color LightYellow
        {
            get { return ColorRGBA.LightYellow; }
        }

        public static Color Yellow
        {
            get { return ColorRGBA.Yellow; }
        }

        #endregion

        #region Static Methods

        public static Color Multiply(Color color, float amount)
        {
            return ColorRGBA.Multiply(color, amount);
        }

        public static Color Lerp(Color colorA,Color colorB, float amount)
        {
            return ColorRGBA.Lerp(colorA,colorB,amount);
        }


        #endregion

        #region Operators

        public static implicit operator ColorRGBA(Color c)
        {
            return c._color;
        }

        public static implicit operator Color(ColorRGBA c)
        {
            return new Color(c);
        }

        #endregion
    }
}
