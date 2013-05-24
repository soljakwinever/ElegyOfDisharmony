using Microsoft.Xna.Framework.Graphics;
using EquestriEngine.Data.Scenes;
using Camera = EquestriEngine.Objects.Scenes.CameraNode;


namespace EquestriEngine.Objects.Graphics
{
    public class BasicEffectObject : EffectObject, Interfaces.IGraphicsLoadableEffect
    {
        private Camera _cNode;

        private GraphicsDevice _deviceReference;

        public Texture2D Texture
        {
            set { BasicEffect.Texture = value; }
        }

        public Camera CameraNode
        {
            get { return _cNode; }
            set { _cNode = value; }
        }

        public bool TextureEnabled
        {
            set { BasicEffect.TextureEnabled = value; }
        }

        public bool PerferPixelLighting
        {
            set { BasicEffect.PreferPerPixelLighting = value; }
        }

        public Matrix Projection
        {
            set { BasicEffect.Projection = value; }
        }

        public Matrix View
        {
            set { BasicEffect.View = value; }
        }

        public Matrix World
        {
            set { BasicEffect.World = value; }
        }

        public BasicEffect BasicEffect
        {
            get { return _effect as BasicEffect; }
        }

        public BasicEffectObject(string name)
            :base("")
        {
            Systems.AssetManager.AddEffect(name, this);
        }

        public bool InitEffect(GraphicsDevice graphics)
        {
            _deviceReference = graphics;
            try
            {
                _effect = new BasicEffect(_deviceReference);
                BasicEffect.FogEnabled = true;
                BasicEffect.FogStart = 12f;
                BasicEffect.FogEnd = 30f;
                BasicEffect.FogColor = Color.White.ToVector3();
            }
            catch
            {
                Systems.ConsoleWindow.WriteLine("");
                return false;
            }
            _ready = true;
            return true;
        }

        public void DrawWithEffect(Interfaces.IDrawableGeom geom)
        {
            if (_cNode != null)
            {
                View = _cNode.View;
                Projection = _cNode.Projection;
            }
            if (geom.Ready)
            {
                _deviceReference.BlendState = BlendState.AlphaBlend;
                _deviceReference.DepthStencilState = EquestriEngine.DepthState;

                foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    _deviceReference.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(
                        PrimitiveType.TriangleList, geom.Vertices, 0, geom.Vertices.Length,
                        geom.Indices, 0, 2);
                }
            }
            else
                Systems.ConsoleWindow.WriteLine("Failed to draw geometry");
        }
    }
}
