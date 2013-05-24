using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Objects.Scenes
{
    public class SceneObjectNode : Node
    {
        private SceneObject sceneObject;

        public SceneObjectNode(string name,string textureName,Vector3 position, bool _2D = true)
            : base(name,position,_2D)
        {
            sceneObject = new SceneObject(textureName);
            sceneObject.Hidden = false;
        }

        public SceneObjectNode(string name, string textureName, Vector3 position,float uvX, float uvY, bool _2D = true)
            : base(name, position, _2D)
        {
            sceneObject = new SceneObject(textureName,uvX, uvY);
            sceneObject.Hidden = false;
        }

        public override void Draw()
        {
            Matrix transform = Transform;
            if(_2D)
                transform = Matrix.CreateConstrainedBillboard(Position, Systems.SceneManager.CurrentCamera.Position, Vector3.UnitY, null, null);
            sceneObject.Draw(transform);
            base.Draw();
        }
    }
}
