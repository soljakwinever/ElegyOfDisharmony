using Rect = Microsoft.Xna.Framework.Rectangle;
namespace EquestriEngine.Data.Scenes
{
    public struct Rectangle
    {
        Rect _rectangle;

        public int X
        {
            get { return _rectangle.X; }
            set { _rectangle.X = value; }
        }

        public int Y
        {
            get { return _rectangle.Y; }
            set { _rectangle.Y = value; }
        }

        public int Width
        {
            get { return _rectangle.Width; }
            set { _rectangle.Width = value; }
        }

        public int Height
        {
            get { return _rectangle.Height; }
            set { _rectangle.Height = value; }
        }

        public int Left
        {
            get { return _rectangle.Left; }
        }

        public int Right
        {
            get { return _rectangle.Right; }
        }

        public int Top
        {
            get { return _rectangle.Top; }
        }

        public int Bottom
        {
            get { return _rectangle.Bottom; }
        }

        public Vector2 Center
        {
            get { return new Vector2(_rectangle.Width / 2, _rectangle.Height / 2); }
        }

        public Rectangle(int x, int y, int width, int height)
        {
            _rectangle = new Rect(x, y, width, height);
        }

        private Rectangle(Rect r)
        {
            _rectangle = r;
        }

        public bool Intersects(Rectangle r)
        {
            return this._rectangle.Intersects(r);
        }

        public static implicit operator Rectangle(Rect r)
        {
            return new Rectangle(r);
        }

        public static implicit operator Rect(Rectangle r)
        {
            return r._rectangle;
        }
    }
}
