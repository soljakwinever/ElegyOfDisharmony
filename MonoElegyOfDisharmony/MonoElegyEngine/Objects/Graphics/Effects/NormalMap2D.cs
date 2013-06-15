using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Objects.Graphics.Effects
{
    public class NormalMap2D : EffectObject
    {
        public NormalMap2D()
            : base("")
        {

        }

        public void Load(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
        {
            //_effect = content.Load<Microsoft.Xna.Framework.Graphics.Effect>(@"Effects\");
            base.Load(device);
        }
    }
}
