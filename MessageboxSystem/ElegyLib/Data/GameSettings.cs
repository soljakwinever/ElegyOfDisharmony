using System.IO;
using EquestriEngine.Data.Scenes;

namespace EquestriEngine.Data
{
    public class GameSettings
    {
        private int
            _windowWidth,
            _windowHeight;
        private bool
            _fullScreen,
            _preferMultiSampling,
            need_save;
        private Color _skinColor;

        public int WindowWidth
        {
            get { return _windowWidth; }
            set 
            { 
                _windowWidth = value;
                need_save = true;
            }
        }

        public int WindowHeight
        {
            get { return _windowHeight; }
            set 
            { 
                _windowHeight = value;
                need_save = true;
            }
        }

        public bool FullScreen
        {
            get { return _fullScreen; }
            set
            {
                _fullScreen = value;
                need_save = true;
            }
        }

        public Color SkinColor
        {
            get { return _skinColor; }
            set
            {
                _skinColor = value;
                need_save = true;
            }
        }

        public bool PreferMultiSampling
        {
            get { return _preferMultiSampling; }
            set
            {
                _preferMultiSampling = value;
                need_save = true;
            }
        }

        ~GameSettings()
        {
            if (need_save)
                SaveData();
        }

        public void InitGraphicsDevice(
            Microsoft.Xna.Framework.GraphicsDeviceManager manager, 
            Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
        {
            manager.PreferredBackBufferWidth = _windowWidth;
            manager.PreferredBackBufferHeight = _windowHeight;
            manager.IsFullScreen = _fullScreen;
            manager.PreferMultiSampling = _preferMultiSampling;

            manager.ApplyChanges();
        }

        public void SaveData()
        {
            const string FileName = @"Settings\config.eec";

            if (!Directory.Exists("Settings"))
            {
                Directory.CreateDirectory("Settings");
            }

            FileStream fs;

            if (!File.Exists(FileName))
            {
                fs = File.Create(FileName);
            }
            else
            {
                fs = File.Open(FileName, FileMode.Truncate);
            }

            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                bw.Write(_windowWidth);
                bw.Write(_windowHeight);
                bw.Write(_fullScreen);
                bw.Write(_preferMultiSampling);
                bw.Write(_skinColor.R);
                bw.Write(_skinColor.G);
                bw.Write(_skinColor.B);
            }

            fs.Close();
        }

        public static GameSettings DefaultSettings()
        {
            GameSettings settings = new GameSettings();

            settings._fullScreen = false;
            settings._windowWidth = 800;
            settings._windowHeight = 480;
            settings._preferMultiSampling = false;
            settings._skinColor = Color.SkyBlue;

            return settings;
        }

        public static GameSettings LoadData()
        {
            const string FileName = @"Settings\config.eec";
            GameSettings settings;

            if (!File.Exists(FileName))
            {
                settings = DefaultSettings();
                settings.SaveData();
                return settings;
            }
            else
            {
                FileStream fs = File.OpenRead(FileName);

                using (BinaryReader br = new BinaryReader(fs))
                {
                    settings = new GameSettings();
                    settings._windowWidth = br.ReadInt32();
                    settings._windowHeight = br.ReadInt32();
                    settings._fullScreen = br.ReadBoolean();
                    settings._preferMultiSampling = br.ReadBoolean();
                    byte r, g, b;
                    r = br.ReadByte();
                    g = br.ReadByte();
                    b = br.ReadByte();
                    settings._skinColor = new Color(r, g, b);
                }

                fs.Close();
                return settings;
            }
        }
    }
}
