using Quat = Microsoft.Xna.Framework.Quaternion;

namespace EquestriEngine.Data.Scenes
{
    public struct Quaterion
    {
        Quat _quaterion;

        public float W
        {
            get { return _quaterion.W; }
            set { _quaterion.W = value; }
        }

        public float X
        {
            get { return _quaterion.X; }
            set { _quaterion.X = value; }
        }

        public float Y
        {
            get { return _quaterion.Y; }
            set { _quaterion.Y = value; }
        }

        public float Z
        {
            get { return _quaterion.Z; }
            set { _quaterion.Z = value; }
        }

        public Quaterion(float x, float y, float z, float w)
        {
            _quaterion = new Quat(x, y, z, w);
        }

        private Quaterion(Quat q)
        {
            _quaterion = q;
            q.Length();
            q.LengthSquared();
            q.Normalize();

        }

        #region Methods

        public void Conjugate()
        {
            _quaterion.Conjugate();
        }

        public void Normalize()
        {
            _quaterion.Normalize();
        }

        public float Length()
        {
            return _quaterion.Length();
        }

        public float LengthSquared()
        {
            return _quaterion.LengthSquared();
        }

        public override string ToString()
        {
            return _quaterion.ToString();
        }

        #endregion

        #region Static Methods

        public static Quaterion FromAxisAngle(Vector3 axis, float angle)
        {
            return Quat.CreateFromAxisAngle(axis,angle);
        }

        public static Quaterion FromRotationMatrix(Matrix matrix)
        {
            return Quat.CreateFromRotationMatrix(matrix);
        }

        public static Quaterion FromYawPitchRoll(float yaw, float pitch,float roll)
        {
            return Quat.CreateFromYawPitchRoll(yaw, pitch, roll);
        }

        #endregion

        #region Operators

        public static implicit operator Quat(Quaterion q)
        {
            return q._quaterion;
        }

        public static implicit operator Quaterion(Quat q)
        {
            return new Quaterion(q);
        }

        #endregion
    }
}
