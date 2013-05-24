using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace EquestriEngine.Objects.Graphics
{
    /// <summary>
    /// Used to draw to a render target
    /// </summary>
    public class TargetObject : TextureObject
    {
        private static GraphicsDevice devRef;
        private static Equestribatch targetBatch;
        private int _width, _height;

        private bool _inUse = false;

        public GraphicsDevice DevRef
        {
            get { return devRef; }
            set { devRef = value; }
        }

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

        public bool InUse
        {
            get { return _inUse; }
        }

        public new Texture2D Texture
        {
            get 
            { 
                if(_inUse)
                    return EquestriEngine.AssetManager.Empty.Texture;
                 else
                    return _texture; 
            }
        }

        public TargetObject(string name,GraphicsDevice device, int width, int height)
            :base(name)
        {
            
            _width = width;
            _height = height;
            _texture = new RenderTarget2D(device, width, height);
            devRef = device;
            _ready = true;
            targetBatch = new Equestribatch(device);
        }

        public override void UnloadAsset()
        {
            Texture.Dispose();
            EquestriEngine.AssetManager.UnloadTexture(this, true);
        }

        public void BeginTarget()
        {
            if (_inUse)
                throw new Data.Exceptions.EngineException("Target already in use! End the target before calling begin",true);
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
            if (!_inUse)
                throw new Data.Exceptions.EngineException("The target has not been started, run Begin in order to use it", true);
            devRef.SetRenderTarget(null);
            _inUse = false;
        }
    }
}
