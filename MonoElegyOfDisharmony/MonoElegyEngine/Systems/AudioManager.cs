using Microsoft.Xna.Framework.Audio;

namespace EquestriEngine.Systems
{
    public class AudioManager : Bases.BaseSystem
    {
        //AudioEngine _engine;
        //WaveBank environmentBank;
        //SoundBank sound_bank;

        public AudioManager(object game)
            :base(game)
        {

        }

        public override void Initialize()
        {
            //_engine = new AudioEngine(@"Data\Audio\ElegySound.xgs");
            //environmentBank = new WaveBank(_engine, @"Data\Audio\Environment_Stream.xwb", 0, 4);
            //sound_bank = new SoundBank(_engine, @"Data\Audio\Environment.xsb");
            
            //base.Initialize();
            //var cue = sound_bank.GetCue("scary-night-at-forest");
            //cue.Play();
            int breakpoint = 0;
        }

        protected override void Dispose(bool disposing)
        {
            //environmentBank.Dispose();
            //_engine.Dispose();
            base.Dispose(disposing);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            //_engine.Update();
            base.Update(gameTime);
        }
    }
}
