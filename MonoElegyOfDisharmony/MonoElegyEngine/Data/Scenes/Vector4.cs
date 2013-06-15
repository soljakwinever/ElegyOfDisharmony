using Vector = Microsoft.Xna.Framework.Vector4;
namespace EquestriEngine.Data.Scenes
{
    public struct Vector4
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
            set { _vector.Z = value; }
        }

        public float W
        {
            get { return _vector.W; }
            set { _vector.W = value; }
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

        public Vector4(float x, float y, float z,float w)
        {
            _vector = new Vector(x, y, z, w);
        }

        private Vector4(Vector v)
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

        public override bool Equals(object obj)
        {
 	        return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _vector.GetHashCode();
        }

        #endregion

        #region Static Methods

        public static Vector4 Transform(Vector4 val, Matrix transform)
        {
            return new Vector4(Vector.Transform(val,transform));
        }

        public static Vector4 Lerp(Vector4 v1, Vector4 v2, float amount)
        {
            return new Vector4(Vector.Lerp(v1, v2, amount));
        }

        public static Vector4 Slerp(Vector4 v1, Vector4 v2, float amount)
        {
            return new Vector4(Vector.SmoothStep(v1, v2, amount));
        }

        public static Vector4 Hermite(Vector4 v1,Vector4 t1, Vector4 v2, Vector4 t2, float amount)
        {
            return new Vector4(Vector.Hermite(v1,t1, v2,t2, amount));
        }

        public static Vector4 CatmullRom(Vector4 v1, Vector4 v2, Vector4 v3, Vector4 v4, float amount)
        {
            return new Vector4(Vector.CatmullRom(v1, v2, v3, v4, amount));
        }

        public static Vector4 Barycentric(Vector4 v1, Vector4 v2, Vector4 v3, float a1,float a2)
        {
            return new Vector4(Vector.Barycentric(v1, v2, v3, a1,a2));
        }

        public static float Distance(Vector4 v1, Vector4 v2)
        {
            return Vector4.Distance(v1, v2);
        }

        public static float DistanceSquared(Vector4 v1, Vector4 v2)
        {
            return Vector4.DistanceSquared(v1, v2);
        }

        public static Vector4 Clamp(Vector4 val, Vector4 min,Vector4 max)
        {
            return new Vector4(Vector.Clamp(val, min, max));
        }

        public static Vector4 Normalize(Vector4 v)
        {
            return new Vector4(Vector.Normalize(v));
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

        public static Vector4 operator +(Vector4 v1, Vector4 v2)
        {
            v1.Vector += v2.Vector;
            return v1;
        }

        public static Vector4 operator -(Vector4 v1, Vector4 v2)
        {
            v1.Vector -= v2.Vector;
            return v1;
        }

        public static Vector4 operator *(Vector4 v1, Vector4 v2)
        {
            v1.Vector *= v2.Vector;
            return v1;
        }

        public static Vector4 operator *(Vector4 v1, float f)
        {
            v1.Vector *= f;
            return v1;
        }

        public static Vector4 operator /(Vector4 v1, Vector4 v2)
        {
            v1.Vector /= v2.Vector;
            return v1;
        }

        public static Vector4 operator /(Vector4 v1, float f)
        {
            v1.Vector /= f;
            return v1;
        }

        public static implicit operator Vector(Vector4 vector)
        {
            return vector.Vector;
        }

        public static implicit operator Vector4(Vector vector)
        {
            return new Vector4(vector);
        }

        public static bool operator ==(Vector4 v1, Vector4 v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z && v1.W == v2.W;
        }

        public static bool operator !=(Vector4 v1, Vector4 v2)
        {
            return !(v1 == v2);
        }


        #endregion
    }
}
