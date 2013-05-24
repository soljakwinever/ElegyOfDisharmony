using EquestriEngine.Data.Scenes;

namespace EquestriEngine.Objects.Scenes
{
    /// <summary>
    /// A node that can be placed into the world, it will trigger an event when an area is walked into
    /// </summary>
    public class TriggerNode : Node, Interfaces.IInteractable
    {
        public event TriggerEvent OnBeginTrigger;
        public event TriggerEvent OnTrigger;
        public event TriggerEvent OnEndTrigger;

        private readonly NodeFlags _flags;

        public NodeFlags Flags
        {
            get { return _flags; }
        }

        private BoundingSphere _area;

        public BoundingSphere Area
        {
            get { return Area; }
            set { _area = value; }
        }

        public TriggerNode(string name, Vector3 position)
            : base("trigger_" + name, position)
        {
            _flags = NodeFlags.Trigger;
        }

        public TriggerNode(string name, Vector3 position, NodeFlags flags)
            : base("trigger_" + name, position)
        {
            _flags = flags;
        }

        public void CheckCollision(Node n)
        {
            if (n is Interfaces.IInteractable)
            {
                var interactor = n as Interfaces.IInteractable;
                if (_area.Intersects(interactor.Area))
                    OnTrigger(this, interactor.Flags);

            }
        }
    }
}
