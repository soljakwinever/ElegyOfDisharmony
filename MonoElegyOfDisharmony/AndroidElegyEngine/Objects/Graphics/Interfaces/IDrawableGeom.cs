using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Objects.Graphics.Interfaces
{
    public interface IDrawableGeom
    {
        VertexPositionNormalTexture[] Vertices
        {
            get;
        }

        int[] Indices
        {
            get;
        }

        bool Ready
        {
            get;
        }
    }
}
