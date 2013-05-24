using EquestriEngine.Data.Scenes;
using Microsoft.Xna.Framework.Graphics;

namespace EquestriEngine.Objects.Graphics
{
    public static class TextureObjectFactory
    {
        public static GraphicsDevice Device_Ref = null;

        public static void GenerateTextureObjectFromMethod(TargetObject input, out TextureObject output,string name, System.Action<Equestribatch> method, Color? color = null,EffectObject effect = null)
        {

            input.RunTarget(method,color,effect);
            
            output = new TextureObject(name, input.Texture,true);
        }

        public static void GenerateTextureObjectFromPopText(out TextureObject output,int num, Color? color = null)
        {
            TextureAtlas atlas = EquestriEngine.AssetManager.GetTexture("{pop_text}") as TextureAtlas;

            string temp = "" + num;
            int width = 0, height = 0;

            for (int i = 0; i < temp.Length; i++)
            {
                var rect = atlas["num_" + temp[i]];
                width += rect.Width;
                if (rect.Height > height)
                    height = rect.Height;
            }
            
            RenderTarget2D _textTarget = new RenderTarget2D(Device_Ref,width,height);
            Device_Ref.SetRenderTarget(_textTarget);
            Device_Ref.Clear(color == null ? Color.Black : color.Value);

            Equestribatch batch = new Equestribatch(Device_Ref);
            batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            int lastWidth = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                batch.Draw(atlas.Texture, new Vector2(lastWidth, 0), atlas["num_" + temp[i]], Color.White);
                lastWidth += atlas["num_" + temp[i]].Width;
            }

            batch.End();

            Device_Ref.SetRenderTarget(null);

            output = EquestriEngine.AssetManager.CreateTextureObjectFromTarget("{"+temp+"}", _textTarget);

            _textTarget.Dispose();
        }
    }
}
