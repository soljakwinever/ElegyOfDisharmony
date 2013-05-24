using EquestriEngine.Data.Scenes;
using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Objects.Scenes
{
    public enum NodeFlags
    {
        Player,
        NPC,
        Trigger
    }

    public class Node
    {
        string _name;

        Matrix world;

        Vector3 _position, _scale = Vector3.One;
        Quaterion _rotation;

        Node _parent;
        NodeList _nodes;

        protected bool _2D;

        public Node Parent
        {
            get { return _parent; }
            protected set
            {
                _parent = value;
            }
        }

        public Matrix Transform
        {
            get
            {
                if (_name != "{root}")
                {
                    return Matrix.CreateScale(Scale) * Matrix.CreateFromQuaterion(_rotation) * 
                        Matrix.CreateTranslation(_position) *Parent.Transform;
                }
                else
                    return Matrix.Identity;
            }
        }

        public Vector3 Position
        {
            get { return _position + (_parent != null ? _parent.Position : Vector3.Zero); }
            set
            {
                _position = value;
            }
        }

        public Vector3 Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
            }
        }

        public Quaterion Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
            }
        }

        public Node(string name, Vector3 position, bool _2D = true)
        {
            this._2D = _2D;
            this._name = name;
            if (_name == "{root}")
                world = Matrix.Identity;

            _nodes = new NodeList();

            _position = position;

        }

        public virtual void Draw()
        {
            _nodes.ForEach(
                delegate(Node n)
                {
                    n.Draw();
                });
        }

        public override string ToString()
        {
            string returnString = "";
            returnString += this._name;

            return returnString;
        }

        public string PrintSceneNode(int iterationLevel = 0)
        {
            string output = "";
            if (this._parent != null)
                output += "--";
            output += _name + "\n";
            foreach (Node n in _nodes)
            {
                for (int i = 0; i < iterationLevel; i++)
                    output += "--";
                output += n.PrintSceneNode(iterationLevel + 1);
            }
            return output;
        }

        public Node Search(string name)
        {
            Node result = null;
            if (this._name == name)
                result = this;
            else
                _nodes.ForEach(delegate(Node n)
                    {
                        if (result == null)
                            result = n.Search(name);
                    });
            return result;
        }

        public void AddNode(Node node)
        {
            node.Parent = this;
            _nodes.Add(node);
        }

        public void RemoveNode(Node node)
        {
            _nodes.Remove(node);
        }
    }
}
