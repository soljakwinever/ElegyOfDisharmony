using Vector = Microsoft.Xna.Framework.Vector2;
namespace EquestriEngine.Data.Scenes
{
    public struct Vector2
    {
        Vector _vector;

        public float X
        {
            get { return _vector.X; }
            set { _vector.X = value; }
        }

        public float Y
        {
            get { return _vector.Y; }
            set { _vector.Y = value; }
        }


        public Vector Vector
        {
            get { return _vector;}
            set { _vector = value; }
        }

        #region Static Members

        public static Vector2 One
        {
            get { return new Vector2(1, 1); }
        }

        public static Vector2 Zero
        {
            get { return new Vector2(0, 0); }
        }

        #endregion

        #region Constructors

        public Vector2(float x, float y)
        {
            _vector = new Vector(x, y);
        }

        private Vector2(Vector v)
        {
            _vector = v;
        }

        #endregion

        #region Methods

        public void Normalize()
        {
            _vector.Normalize();
        }

        public float Length()
        {
            return _vector.Length();
        }

        public float LengthSquared()
        {
            return _vector.LengthSquared();
        }

        public override string ToString()
        {
            return _vector.ToString();
        }

        #endregion

        #region Static Methods

        public static Vector2 Lerp(Vector2 v1, Vector2 v2, float amount)
        {
            return new Vector2(Vector.Lerp(v1, v2, amount));
        }

        public static Vector2 Slerp(Vector2 v1, Vector2 v2, float amount)
        {
            return new Vector2(Vector.SmoothStep(v1, v2, amount));
        }

        public static Vector2 Hermite(Vector2 v1,Vector2 t1, Vector2 v2, Vector2 t2, float amount)
        {
            return new Vector2(Vector.Hermite(v1,t1, v2,t2, amount));
        }

        public static Vector2 CatmullRom(Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v4, float amount)
        {
            return new Vector2(Vector.CatmullRom(v1, v2, v3, v4, amount));
        }

        public static Vector2 Barycentric(Vector2 v1, Vector2 v2, Vector2 v3, float a1,float a2)
        {
            return new Vector2(Vector.Barycentric(v1, v2, v3, a1,a2));
        }

        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return Vector.Dot(v1, v2);
        }

        public static Vector2 Clamp(Vector2 val, Vector2 min,Vector2 max)
        {
            return new Vector2(Vector.Clamp(val, min, max));
        }

        public static Vector2 Reflect(Vector2 v, Vector2 n)
        {
            return Vector.Reflect(v, n);
        }

        public static float Distance(Vector2 v1, Vector2 v2)
        {
            return Vector.Distance(v1, v2);
        }

        public static float DistanceSquared(Vector2 v1, Vector2 v2)
        {
            return Vector.DistanceSquared(v1, v2);
        }

        #endregion

        #region Operators

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            v1.Vector += v2.Vector;
            return v1;
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            v1.Vector -= v2.Vector;
            return v1;
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            v1.Vector *= v2.Vector;
            return v1;
        }

        public static Vector2 operator *(Vector2 v1, float f)
        {
            v1.Vector *= f;
            return v1;
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            v1.Vector /= v2.Vector;
            return v1;
        }

        public static Vector2 operator /(Vector2 v1, float f)
        {
            v1.Vector /= f;
            return v1;
        }

        public static implicit operator Vector(Vector2 vector)
        {
            return vector.Vector;
        }

        public static implicit operator Vector2(Vector vector)
        {
            return new Vector2(vector);
        }

        #endregion
    }
}
