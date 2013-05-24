using EquestriEngine.Data.Collections;
using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Scenes;

namespace EquestriEngine.Objects.GameObjects
{
    public class NPC : Scenes.Node
    {
        private TriggerNode _trigger;
        private MoveToData[] _moveRoute;

        public NPC(string name,Vector3 position, bool _2D)
            :base(name,position,_2D)
        {
            _trigger = null;
        }
    }
}
