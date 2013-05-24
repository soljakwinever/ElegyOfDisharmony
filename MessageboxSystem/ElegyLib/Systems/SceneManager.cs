using GameTime = Microsoft.Xna.Framework.GameTime;
using EquestriEngine.Objects.Scenes;
using EquestriEngine.Data.Scenes;

namespace EquestriEngine.Systems
{
    public class SceneManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private static Node RootNode;
        private static MoveToList _movements;
        private static CameraNode _currentCamera;

        public static CameraNode CurrentCamera
        {
            get { return _currentCamera; }
        }

        public SceneManager(Microsoft.Xna.Framework.Game game)
            : base(game)
        {

            _movements = new MoveToList();
            //Create a Nodegraph to start with
            RootNode = new Node("{root}", Vector3.Zero);
            Node TriggerGroup = new Node("Triggers",Vector3.Zero);
            Node WorldGroup = new Node("World", Vector3.Zero);
            Node CameraGroup = new Node("Cameras", Vector3.Zero);

            RootNode.AddNode(WorldGroup);
            RootNode.AddNode(TriggerGroup);
            RootNode.AddNode(CameraGroup);

            _currentCamera = new CameraNode("{default_camera}",new Vector3(0,0,0));
            CameraGroup.AddNode(_currentCamera);
        }

        public void RunNodeTests()
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_movements.Count > 0)
                _movements.ForEach(delegate(MoveToData mtd)
                {
                    if (mtd.Finished)
                        _movements.Remove(mtd);
                    mtd.Update(dt);
                });
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            RootNode.Draw();
            base.Draw(gameTime);
        }

        public static string DisplayScene()
        {
            string temp = "--Scene Display--\n";
            temp += RootNode.PrintSceneNode();
            return temp;
        }

        public static Node SearchNodes(string nodeName = "")
        {
            var output = RootNode.Search(nodeName);
            return output;
        }

        public static void AddNodeTo(Node node, string nodeName = "")
        {
            if (nodeName == "")
                RootNode.AddNode(node);
            else
            {
                Node tempNode = RootNode.Search(nodeName);
                tempNode.AddNode(node);
            }
        }
    }
}
