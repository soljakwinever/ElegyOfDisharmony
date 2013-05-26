using Microsoft.Xna.Framework.Content;

namespace ElegyEngine.Content
{
    public class ExclusiveContentManager : ContentManager
    {
        private bool _ready;

        public bool Ready
        {
            get { return _ready; }
        }

        public ExclusiveContentManager(System.IServiceProvider services)
            : base(services)
        {
            this.RootDirectory = "ElegyContent";
        }

        public void Init()
        {

            _ready = true;
        }

        public T LoadExclusiveContent<T>(string fileName)
        {
            return ReadAsset<T>(fileName, null);
        }

        public void Unload(System.IDisposable ContentItem)
        {
            ContentItem.Dispose();
        }
    }
}
