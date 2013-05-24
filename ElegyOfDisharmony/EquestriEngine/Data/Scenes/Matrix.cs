using Matrix4x4 = Microsoft.Xna.Framework.Matrix;

namespace EquestriEngine.Data.Scenes
{
    public struct Matrix
    {
        private Matrix4x4 _matrix;

        private Matrix(Matrix4x4 matrix)
        {
            _matrix = matrix;
        }

        public static Matrix Identity
        {
            get { return Matrix4x4.Identity; }
        }

        #region Position Fields

        public Vector3 Forward
        {
            get { return _matrix.Forward; }
        }

        public Vector3 Backward
        {
            get { return _matrix.Backward; }
        }

        public Vector3 Up
        {
            get { return _matrix.Up; }
        }

        public Vector3 Down
        {
            get { return _matrix.Down; }
        }

        public Vector3 Left
        {
            get { return _matrix.Left; }
        }

        public Vector3 Right
        {
            get { return _matrix.Right; }
        }

        public Vector3 Translation
        {
            get { return _matrix.Translation; }
        }

        #endregion

        #region Static Methods


        public static Matrix Invert(Matrix input)
        {
            return new Matrix(Matrix4x4.Invert(input));
        }


        public static Matrix CreatePerspective(float fov, float aspect, float near, float far)
        {
            return new Matrix(Matrix4x4.CreatePerspectiveFieldOfView(fov, aspect, near, far));
        }

        public static Matrix CreateLookAt(Vector3 pos, Vector3 target, Vector3 up)
        {
            return new Matrix(Matrix4x4.CreateLookAt(pos, target, up));
        }

        public static Matrix CreateTranslation(Vector3 pos)
        {
            return new Matrix(Matrix4x4.CreateTranslation(pos));
        }

        public static Matrix CreateWorld(Vector3 pos, Vector3 forward, Vector3 up)
        {
            return new Matrix(Matrix4x4.CreateWorld(pos, forward, up));
        }

        public static Matrix CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            return new Matrix(Matrix4x4.CreateFromYawPitchRoll(yaw, pitch, roll));
        }

        public static Matrix CreateConstrainedBillboard(
            Vector3 objectPosition,
            Vector3 cameraPosition,
            Vector3 rotateAxis,
            Vector3? cameraForward,
            Vector3? objectForward)
        {
            return new Matrix(Matrix4x4.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, cameraForward, objectForward));
        }

        public static Matrix CreateBillboard(
            Vector3 objectPosition,
            Vector3 cameraPosition,
            Vector3 cameraUp,
            Vector3? cameraForward)
        {
            return new Matrix(Matrix4x4.CreateBillboard(objectPosition, cameraPosition, cameraUp, cameraForward));
        }

        public static Matrix CreateScale(Vector3 scale)
        {
            return new Matrix(Matrix4x4.CreateScale(scale));
        }

        public static Matrix CreateFromQuaterion(Quaterion q)
        {
            return new Matrix(Matrix4x4.CreateFromQuaternion(q));
        }

        public static Matrix CreateRotationX(float rotX)
        {
            return new Matrix(Matrix4x4.CreateRotationX(rotX));
        }

        public static Matrix CreateRotationY(float rotY)
        {
            return new Matrix(Matrix4x4.CreateRotationY(rotY));
        }

        public static Matrix CreateRotationZ(float rotZ)
        {
            return new Matrix(Matrix4x4.CreateRotationZ(rotZ));
        }

        #endregion

        #region Operators

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            m1._matrix *= m2._matrix;
            return m1;
        }

        public static implicit operator Matrix4x4(Matrix m)
        {
            return m._matrix;
        }

        public static implicit operator Matrix(Matrix4x4 m)
        {
            return new Matrix(m);
        }

        #endregion
    }
}
