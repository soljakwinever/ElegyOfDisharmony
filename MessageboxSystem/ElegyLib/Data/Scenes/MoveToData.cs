using EquestriEngine.Objects.Scenes;

namespace EquestriEngine.Data.Scenes
{
    public enum MovementType
    {
        Linear,
        SmoothStep,
        Hermite,
        Cat_Mull,
        Barycentric
    }

    public class MoveToData
    {
        public Vector3 NewPos;
        public Vector3 NewScale;
        public Quaterion NewRot;
        public float ttt,
            originalTTT;
        public bool instant, flip, finished;

        MovementType _moveType;

        Node _affectedNode;

        bool gotStats;
        Vector3 
            oldPos,
            oldScale;
        Quaterion 
            oldRot;

        public bool Finished
        {
            get { return finished; }
        }

        public MoveToData
            (   Node affectedNode,
                Vector3 pos,
                Vector3 scale,
                Quaterion rot,
                bool flip,
                bool instant,
                float ttt = 0.0f,
                bool smooth = false)
        {
            NewPos = pos;
            NewScale = scale;
            NewRot = rot;
            this.flip = flip;
            this.instant = instant;
            this.ttt = ttt;
            this.originalTTT = ttt;
            _affectedNode = affectedNode;
        }

        public void Update(float dt)
        {
            finished = ttt <= 0;

            if (!gotStats)
            {
                oldPos = _affectedNode.Position;
                oldRot = _affectedNode.Rotation;
                oldScale = _affectedNode.Scale;
                gotStats = true;
            }

            _affectedNode.Position = Vector3.Lerp(oldPos, NewPos, 1 - (ttt / originalTTT));
            //_affectedNode.Rotation = Vector3.Lerp(oldRot, NewRot, 1 - (ttt / originalTTT));
            _affectedNode.Scale = Vector3.Lerp(oldScale, NewScale, 1 - (ttt / originalTTT));
            ttt -= dt;
        }
    }
}
