using EquestriEngine.Data.Scenes;

namespace EquestriEngine.Objects.Graphics
{
    public static class TextureObjectFactory
    {
        public static void GenerateTextureObjectFromMethod(out TextureObject output,string name, int width,int height, System.Action<Equestribatch> method, Color? color = null,EffectObject effect = null)
        {
            TargetObject _temp;
            _temp = new TargetObject("{temp}", width, height);
            _temp.RunTarget(method,color,effect);
            
            output = new TextureObject(name, _temp.Texture);
            _temp.UnloadAsset();
        }
    }
}
