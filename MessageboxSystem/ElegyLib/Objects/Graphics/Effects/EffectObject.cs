using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EquestriEngine.Objects.Graphics
{
    public abstract class EffectObject
    {
        protected Effect _effect;

        private readonly string _TechniqueName;

        protected bool _ready;

        public bool Ready
        {
            get { return _ready; }
        }

        protected EffectObject(string technique)
        {
            _TechniqueName = technique;
        }

        public static implicit operator Effect(EffectObject o)
        {
            if (o == null)
                return null;
            if (!(o._effect is BasicEffect))
                o._effect.CurrentTechnique = o._effect.Techniques[o._TechniqueName];
            if (o._ready)
                return o._effect;
            else
                throw new Data.Exceptions.EngineException("Effect not ready for use", false);
        }
    }
}
