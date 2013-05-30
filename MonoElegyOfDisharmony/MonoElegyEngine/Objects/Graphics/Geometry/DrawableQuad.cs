using EquestriEngine.Data.Scenes;
using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Objects.Graphics
{
    public class DrawableQuad : Interfaces.IDrawableGeom
    {
        private bool _ready;

        int[] indices;
        VertexPositionNormalTexture[] vertices;
        Vector3
            _origin,
            _normal,
            _up,
            _left,
            _upperLeft,
            _upperRight,
            _lowerLeft,
            _lowerRight;

        GraphicsDevice device;

        public VertexPositionNormalTexture[] Vertices
        {
            get { return vertices; }
        }

        public int[] Indices
        {
            get { return indices; }
        }

        public bool Ready
        {
            get { return _ready; }
        }

        static VertexDeclaration _declaration;

        public static VertexDeclaration Declaration
        {
            get { return _declaration; }
        }

        
        static DrawableQuad()
        {
            _declaration = new VertexDeclaration(new VertexElement[]
            {
                new VertexElement(0,VertexElementFormat.Vector3,
                    VertexElementUsage.Position,0),
                new VertexElement(12,VertexElementFormat.Vector3,
                    VertexElementUsage.Normal,0),
                new VertexElement(24,VertexElementFormat.Vector2,
                    VertexElementUsage.TextureCoordinate,0),
            });
        }

        public DrawableQuad(Vector3 origin, float width, float height, Vector3 normal, Vector3 up, float uvX = 1.0f, float uvY = 1.0f)
        {
            _ready = false;
            vertices = new VertexPositionNormalTexture[4];
            indices = new int[6];
            _origin = origin;
            _normal = normal;
            _up = up;

            _left = Vector3.Cross(_normal, _up);
            Vector3 upperCenter = (_up * height / 2) + origin;
            _upperLeft = upperCenter + (_left * width / 2);
            _upperRight = upperCenter - (_left * width / 2);
            _lowerLeft = _upperLeft - (_up * height);
            _lowerRight = _upperRight - (_up * height);

            FillVertices(uvX,uvY);
        }

        private void FillVertices(float uvX, float uvY)
        {
            Vector2 texUpperLeft = new Vector2(0.0f, 0.0f);
            Vector2 texUpperRight = new Vector2(uvX, 0.0f);
            Vector2 texLowerLeft = new Vector2(0.0f, uvY);
            Vector2 texLowerRight = new Vector2(uvX, uvY);

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].Normal = _normal;
            }

            vertices[0].Position = _lowerLeft;
            vertices[0].TextureCoordinate = texLowerLeft;
            vertices[1].Position = _upperLeft;
            vertices[1].TextureCoordinate = texUpperLeft;
            vertices[2].Position = _lowerRight;
            vertices[2].TextureCoordinate = texLowerRight;
            vertices[3].Position = _upperRight;
            vertices[3].TextureCoordinate = texUpperRight;

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 2;
            indices[4] = 1;
            indices[5] = 3;

            _ready = true;
        }
    }
}
