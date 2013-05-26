using System.Collections.Generic;
using EquestriEngine.Data.Scenes;
using System.IO;

namespace EquestriEngine.Objects.Graphics
{
    /// <summary>
    /// A texture that has areas you can use to pick areas from
    /// </summary>
    public class TextureAtlas : TextureObject
    {
        private Dictionary<string, Rectangle> _areas;

        public Rectangle this[string area]
        {
            get 
            {
                if (_ready)
                    return _areas[area];
                else
                    return new Rectangle();
            }
        }

        public string[] Areas
        {
            get
            {
                string[] temp = new string[_areas.Keys.Count];
                int i = 0;
                foreach( string key in _areas.Keys)
                {
                    temp[i] = key;
                    i++;
                }
                return temp;
            }
        }

        public TextureAtlas(string name, string file, object areas)
            : base(name, file)
        {
            _areas = areas as Dictionary<string, Rectangle>;
        }
    }
}
