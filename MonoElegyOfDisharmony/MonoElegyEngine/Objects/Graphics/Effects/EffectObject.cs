using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EquestriEngine.Objects.Graphics
{
    public abstract class EffectObject
    {
        private GraphicsDevice _device;
        protected Effect _effect;
        protected SamplerState _samplerstate;

        private readonly string _TechniqueName;

        protected bool _ready;

        public bool Ready
        {
            get { return _ready; }
        }

        public SamplerState SamplerState
        {
            get { return _samplerstate; }
            set { _samplerstate = value; }
        }

        protected EffectObject(string technique)
        {
            _TechniqueName = technique;
        }


        public virtual void Load(GraphicsDevice device)
        {
            _device = device;
            _ready = true;
        }

        public void ApplyEffect()
        {

        }

        public static implicit operator Effect(EffectObject o)
        {
            if (o == null)
                return null;
            if (!(o._effect is BasicEffect))
            {
                o._effect.CurrentTechnique = o._effect.Techniques[o._TechniqueName];
                return o._effect;
            }
            else if (o._ready)
                return o._effect;

            return null;
        }
    }
}
