using EquestriEngine.Systems;
using System.Collections.Generic;
using System.IO;
namespace EquestriEngine.Objects.Graphics.Misc
{
    public class TextureLoadList : Interfaces.ILoadable
    {
        private bool _ready;
        Dictionary<string, object[]> _entries;

        public bool Ready
        {
            get { return _ready; }
        }

        public bool DeviceLoad
        {
            get { return false; }
        }

        public Dictionary<string, object[]> Entries
        {
            get { return _entries; }
        }

        private TextureLoadList()
        {
            _entries = new Dictionary<string, object[]>();
        }

        private const string LOAD_EXTENSION = ".ell";

        private static string RemoveTab(string input)
        {
            return input.Split(new char[] { '\t' }, System.StringSplitOptions.RemoveEmptyEntries)[0];
        }

        public static void LoadList(out TextureLoadList list, string file)
        {
            list = new TextureLoadList();
            try
            {
                using (Utilities.FormattedFile formatFile = new Utilities.FormattedFile())
                {
                    formatFile.ReadBegin(@"Data\LoadLists\" + file + LOAD_EXTENSION);
                    if (formatFile.ReadBlock() != "LoadList")
                        throw new Data.Exceptions.EngineException("Missing correct block header, should be \"LoadList\"", true);
                    int entries = int.Parse(formatFile.ReadLine());
                    for (int i = 0; i < entries; i++)
                    {
                        string type = formatFile.ReadBlock();
                        string name = formatFile.ReadLine();
                        string path = "";
                        object extra = null;
                        switch (type)
                        {
                            #region Atlas Loading
                            case "Atlas":
                                {
                                    int x, y, w, h;
                                    string atlasName;
                                    path = formatFile.ReadBlock();
                                    int atlastEntries = int.Parse(formatFile.ReadLine());
                                    Dictionary<string, Data.Scenes.Rectangle> _areas = new Dictionary<string, Data.Scenes.Rectangle>();

                                    for (int j = 0; j < atlastEntries; j++)
                                    {
                                        Data.Scenes.Rectangle rect;
                                        atlasName = formatFile.ReadBlock();
                                        string raw = formatFile.ReadLine();
                                        formatFile.ReadEndBlock();
                                        var temp = raw.Split(',');
                                        if (temp.Length != 4)
                                            throw new Data.Exceptions.EngineException("Invalid atlas entry", true);
                                        x = int.Parse(temp[0]);
                                        y = int.Parse(temp[1]);
                                        w = int.Parse(temp[2]);
                                        h = int.Parse(temp[3]);
                                        rect = new Data.Scenes.Rectangle(x, y, w, h);
                                        _areas[atlasName] = rect;
                                    }
                                    extra = _areas;
                                    formatFile.ReadEndBlock();
                                }
                                break;
                            case "Target":
                                {
                                    int w, h;
                                    string raw = formatFile.ReadLine();
                                    var temp = raw.Split(',');
                                    w = int.Parse(temp[0]);
                                    h = int.Parse(temp[1]);
                                    extra = new int[] { w, h };
                                }
                                break;
                            case "Texture":
                                path = formatFile.ReadLine();
                                break;
                            #endregion
                        }

                        formatFile.ReadEndBlock();
                        if (type == "Target" || type == "Atlas")
                            list._entries[name] = new object[] { path, type, extra };
                        else
                            list._entries[name] = new object[] { path, type };

                    }
                    formatFile.ReadEndBlock();
                    formatFile.ReadEnd();
                }
                list._ready = true;
            }
            catch
            {
                list._ready = false;
            }
        }
    }
}
