using Vector = Microsoft.Xna.Framework.Vector3;
namespace EquestriEngine.Data.Scenes
{
    public struct Vector3
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

        public float Z
        {
            get { return _vector.Z; }
            set { _vector.X = value; }
        }

        public Vector Vector
        {
            get { return _vector; }
            set { _vector = value; }
        }

        #region Static Members

        public static Vector3 One
        {
            get { return new Vector3(1, 1, 1); }
        }

        public static Vector3 Zero
        {
            get { return new Vector3(0, 0, 0); }
        }

        public static Vector3 UnitX
        {
            get { return new Vector3(1, 0, 0); }
        }

        public static Vector3 UnitY
        {
            get { return new Vector3(0, 1, 0); }
        }

        public static Vector3 UnitZ
        {
            get { return new Vector3(0, 0, 1); }
        }

        public  static Vector3 Up
        {
            get { return new Vector3(0, 1, 0); }
        }

        public static Vector3 Down
        {
            get { return new Vector3(0, -1, 0); }
        }

        public static Vector3 Left
        {
            get { return new Vector3(-1, 0, 0); }
        }

        public static Vector3 Right
        {
            get { return new Vector3(1, 0, 0); }
        }

        public static Vector3 Forward
        {
            get { return new Vector3(0, 0, -1); }
        }

        public static Vector3 Backward
        {
            get { return new Vector3(0, 0, 1); }
        }

        #endregion

        #region Constructors

        public Vector3(float x, float y, float z)
        {
            _vector = new Vector(x, y, z);
        }

        private Vector3(Vector v)
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

        public static Vector3 Lerp(Vector3 v1, Vector3 v2, float amount)
        {
            return new Vector3(Vector.Lerp(v1, v2, amount));
        }

        public static Vector3 Slerp(Vector3 v1, Vector3 v2, float amount)
        {
            return new Vector3(Vector.SmoothStep(v1, v2, amount));
        }

        public static Vector3 Hermite(Vector3 v1,Vector3 t1, Vector3 v2, Vector3 t2, float amount)
        {
            return new Vector3(Vector.Hermite(v1,t1, v2,t2, amount));
        }

        public static Vector3 CatmullRom(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, float amount)
        {
            return new Vector3(Vector.CatmullRom(v1, v2, v3, v4, amount));
        }

        public static Vector3 Barycentric(Vector3 v1, Vector3 v2, Vector3 v3, float a1,float a2)
        {
            return new Vector3(Vector.Barycentric(v1, v2, v3, a1,a2));
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            return new Vector3(Vector.Cross(v1, v2));
        }

        public static Vector3 Clamp(Vector3 val, Vector3 min,Vector3 max)
        {
            return new Vector3(Vector.Clamp(val, min, max));
        }

        public static Vector3 Reflect(Vector3 v, Vector3 n)
        {
            return Vector.Reflect(v, n);
        }

        public static float Distance(Vector3 v1, Vector3 v2)
        {
            return Vector.Distance(v1, v2);
        }

        public static float DistanceSquared(Vector3 v1, Vector3 v2)
        {
            return Vector.DistanceSquared(v1, v2);
        }

        /*public static Vector3 2DTo3D(Vector2 point2D, int width, int height, Matrix view, Matrix projection)
        {

            double x = 2.0 * 
            /*function Point3D get3dPoint(Point2D point2D, int width,
        int height, Matrix viewMatrix, Matrix projectionMatrix) {
 
        double x = 2.0 * winX / clientWidth - 1;
        double y = - 2.0 * winY / clientHeight + 1;
        Matrix4 viewProjectionInverse = inverse(projectionMatrix *
             viewMatrix);

        Point3D point3D = new Point3D(x, y, 0); 
        return viewProjectionInverse.multiply(point3D);
}*/

        #endregion

        #region Operators

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            v1.Vector += v2.Vector;
            return v1;
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            v1.Vector -= v2.Vector;
            return v1;
        }

        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            v1.Vector *= v2.Vector;
            return v1;
        }

        public static Vector3 operator *(Vector3 v1, float f)
        {
            v1.Vector *= f;
            return v1;
        }

        public static Vector3 operator /(Vector3 v1, Vector3 v2)
        {
            v1.Vector /= v2.Vector;
            return v1;
        }

        public static Vector3 operator /(Vector3 v1, float f)
        {
            v1.Vector /= f;
            return v1;
        }

        public static implicit operator Vector(Vector3 vector)
        {
            return vector.Vector;
        }

        public static implicit operator Vector3(Vector vector)
        {
            return new Vector3(vector);
        }

        #endregion
    }
}
