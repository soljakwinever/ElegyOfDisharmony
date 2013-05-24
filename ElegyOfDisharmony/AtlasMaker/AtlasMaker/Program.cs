using System;
using System.IO;

namespace WindowsGame1
{
    public class AtlasArea
    {
        private string _name;
        private Microsoft.Xna.Framework.Rectangle _area;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Microsoft.Xna.Framework.Rectangle Area
        {
            get { return _area; }
            set { _area = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} - ({1},{2},{3},{4})", _name, _area.X, _area.Y, _area.Width, _area.Height);
        }
    }

#if WINDOWS || XBOX
    
    static class Program
    {
        private static AtlasEditor editor;
        private static Game1 game;
        private static System.Collections.Generic.List<AtlasArea> _areas;

        public static bool EditorFocus
        {
            get { return editor.Focused; }
        }

        public static AtlasEditor Editor
        {
            get { return editor; }
        }

        public static Game1 Game
        {
            get { return game; }
        }


        public static System.Collections.Generic.List<AtlasArea> Areas
        {
            get { return _areas; }
        }

        public static void Close()
        {
            editor.Close();
            game.Exit();
        }

        public static void SaveFile()
        {
            const string Atlas_Path = @"\Data\Atlases\";
            string filename = Path.GetFileNameWithoutExtension(Game1.FilePath);
            string directory = Path.GetDirectoryName(Game1.FilePath);
            using(var writer = 
                new StreamWriter(
                    File.OpenWrite(Editor.GameDirectory + Atlas_Path + filename + ".atlas")
                    ))
            {
                foreach (var atlas in _areas)
                {
                    writer.WriteLine(atlas.Name);
                    writer.WriteLine("{");
                    writer.WriteLine(string.Format("\t{0},{1},{2},{3}",
                        atlas.Area.X,atlas.Area.Y,
                        atlas.Area.Width,atlas.Area.Height));
                    writer.WriteLine("}");
                }
                writer.Close();
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            _areas = new System.Collections.Generic.List<AtlasArea>();
            using (TextureSelection texture = new TextureSelection())
            {
                if (texture.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (editor = new AtlasEditor())
                    {
                        using (game = new Game1())
                        {
                            editor.Show();
                            game.Run();
                        }
                    }
                }
            }
        }
    }
#endif
}

