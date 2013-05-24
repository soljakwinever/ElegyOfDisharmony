using EquestriEngine.Data.Scenes;

namespace EquestriEngine.Objects.Scenes
{
    /// <summary>
    /// Used to Represent Spacial area within the game
    /// </summary>
    public class CameraNode : Node
    {
        private Node _target;
        private Matrix projection;

        public Node Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public Matrix View
        {
            get
            {
                Vector3 target;
                if (_target == null)
                    target = new Vector3(this.Position.X, 0, this.Position.Z + 15);
                else
                    target = _target.Position;
                return Matrix.CreateLookAt(Position, target, Vector3.Up);
            }
        }

        public Matrix Projection
        {
            get { return projection; }
        }

        public CameraNode(string name, Vector3 position)
            : base(name, position, false)
        {
            Position = new Vector3(0, 5, -15);
            InitProjection(0.1f, 100f);
        }

        public void InitProjection(float near, float far)
        {
            projection = Matrix.CreatePerspective(MathHelper.PiOver4, (float)EquestriEngine.Settings.WindowWidth / EquestriEngine.Settings.WindowHeight, near, far);
        }
    }
}
