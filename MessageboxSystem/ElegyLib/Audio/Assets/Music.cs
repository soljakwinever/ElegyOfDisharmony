using NAudio.Wave;

namespace EquestriEngine.Audio.Assets
{
    public class Music : System.IDisposable
    {
        private WaveStream _stream;
        private bool _dispose;
        private string _name,_fileName;

        private float _volume,_pitch;

        AudioDevice _deviceReference;

        public bool IsDisposed
        {
            get { return _dispose; }
        }

        public Music(string name, string fileName)
        {
            _name = name;
            _fileName = fileName;
        }

        public bool LoadMusic()
        {
            try
            {
                if (_fileName.EndsWith(".mp3"))
                {
                    _stream = new Mp3FileReader(_fileName);
                    var stream = _stream as Mp3FileReader;
                    
                }
                else if (_fileName.EndsWith(".wav"))
                {

                }
                else
                    return false;
            }
            catch
            {
                Systems.ConsoleWindow.WriteLine("Error loading music {0}", _name);
            }

            return true;
        }

        public void Play()
        {
            //_deviceReference.Mixer.
        }

        public void Dispose()
        {
            _stream.Close();
            _stream.Dispose();
            _dispose = true;
        }
    }
}
