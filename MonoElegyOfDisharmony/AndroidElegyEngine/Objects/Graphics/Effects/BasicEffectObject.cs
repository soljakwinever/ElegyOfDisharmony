using Microsoft.Xna.Framework.Graphics;
using EquestriEngine.Data.Scenes;
//using Camera = EquestriEngine.Objects.Nodes.CameraNode;


namespace EquestriEngine.Objects.Graphics
{
    public class BasicEffectObject : EffectObject, Interfaces.IGraphicsLoadableEffect
    {
        private GraphicsDevice _deviceReference;

        public Texture2D Texture
        {
            set { BasicEffect.Texture = value; }
        }

        /*public Camera CameraNode
        {
            get { return _cNode; }
            set { _cNode = value; }
        }*/

        public bool TextureEnabled
        {
            set { BasicEffect.TextureEnabled = value; }
        }

        public bool PerferPixelLighting
        {
            set { BasicEffect.PreferPerPixelLighting = value; }
        }

        public bool FogEnabled
        {
            get { return BasicEffect.FogEnabled; }
            set { BasicEffect.FogEnabled = value; }
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
            //Systems.AssetManager.AddEffect(name, this);
        }

        public bool InitEffect(GraphicsDevice graphics)
        {
            _samplerstate = SamplerState.LinearWrap;
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
            /*if (_cNode != null)
            {
                View = _cNode.View;
                Projection = _cNode.Projection;
            }*/
            if (geom.Ready)
            {
                object oldState = null;
                if (_samplerstate != null)
                {
                    oldState = BasicEffect.GraphicsDevice.SamplerStates[0];
                    BasicEffect.GraphicsDevice.SamplerStates[0] = _samplerstate;
                }

                _deviceReference.BlendState = BlendState.AlphaBlend;
                _deviceReference.DepthStencilState = EquestriEngine.DepthState;

                foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    _deviceReference.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(
                        PrimitiveType.TriangleList, geom.Vertices, 0, geom.Vertices.Length,
                        geom.Indices, 0, 2);
                }
                if (oldState != null)
                    BasicEffect.GraphicsDevice.SamplerStates[0] = oldState as SamplerState;
            }
            else
                Systems.ConsoleWindow.WriteLine("Failed to draw geometry");
        }
    }
}
