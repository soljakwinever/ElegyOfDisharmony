using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace EquestriEngine.Objects.Graphics
{
    /// <summary>
    /// Used to draw to a render target
    /// </summary>
    public class TargetObject : TextureObject
    {
        private GraphicsDevice devRef;
        private static Equestribatch targetBatch;
        private int _width, _height;

        private bool _inUse = false;

        public new int Width
        {
            get {
                return _width; 
            }
        }

        public new int Height
        {
            get
            {
                return _height; 
            }
        }

        public TargetObject(string name, int width, int height)
            :base(name)
        {
            _ready = false;
            
            _width = width;
            _height = height;
            Systems.AssetManager.AddTexture(name, this);
        }

        public bool LoadTexture(GraphicsDevice graphics)
        {
            if (graphics == null)
                return false;
            devRef = graphics;
            if (_ready)
                return true;
            try
            {
                if (targetBatch == null)
                    targetBatch = new Equestribatch(graphics);
                _texture = new RenderTarget2D(graphics, _width, _height,false,SurfaceFormat.Color,DepthFormat.Depth24,4,RenderTargetUsage.PreserveContents);
                _ready = true;
            }
            catch (System.Exception ex)
            {
                Systems.ConsoleWindow.WriteLine("Warning: {0}", ex.Message);
                return false;
            }
            return true;
        }

        public void BeginTarget()
        {
            devRef.SetRenderTarget(_texture as RenderTarget2D);
            _inUse = true;
        }

        public void RunTarget(
            System.Action<Equestribatch> _method,
            Color? clearColor = null,
            EffectObject effect = null,
            SamplerState sampler = null)
        {
            if (_inUse)
            {
                Systems.ConsoleWindow.WriteLine("Tried used Target Asset while it was already in use");
                return;
            }
            if (!_ready)
            {
                Systems.ConsoleWindow.WriteLine("Render Target is not ready for use");
                return;
            }
            BeginTarget();
            devRef.Clear((clearColor == null ? Color.Black : clearColor.Value));
            targetBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,sampler,null,null,effect);

            _method.Invoke(targetBatch);

            targetBatch.End();
            EndTarget();
        }

        public void EndTarget()
        {
            devRef.SetRenderTarget(null);
            _inUse = false;
        }
    }
}
