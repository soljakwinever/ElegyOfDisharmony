using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace EquestriEngine.Audio
{
    public class AudioDevice : System.IDisposable
    {
        private IWavePlayer _player;
        private WaveMixerStream32 _mixer;

        public WaveMixerStream32 Mixer
        {
            get { return _mixer; }
        }

        public AudioDevice()
        {
            
        }

        public void Initialize()
        {
            _player = new DirectSoundOut(DirectSoundOut.DSDEVID_DefaultPlayback, 40);
            _mixer = new WaveMixerStream32();
            _mixer.AutoStop = false;

            _player.Init(_mixer);
        }

        public void Dispose()
        {
            _mixer.Dispose();
            _player.Dispose();
        }
    }
}
