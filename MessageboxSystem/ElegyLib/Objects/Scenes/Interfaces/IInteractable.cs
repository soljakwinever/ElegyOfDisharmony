using EquestriEngine.Data.Scenes;

namespace EquestriEngine.Objects.Scenes.Interfaces
{
    public interface IInteractable
    {
        NodeFlags Flags
        {
            get;
        }

        BoundingSphere Area
        {
            get;
            set;
        }

        void CheckCollision(Node n);
    }
}
