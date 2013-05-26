using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquestriEngine.Data.Inputs
{
    public struct AudioInput : Interfaces.IEventInput
    {
        private string _audioTarget;
        private float
            _volume,
            _pitch;

        public string Target
        {
            get { return _audioTarget; }
            set { _audioTarget = value; }
        }

        public float Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        public float Pitch
        {
            get { return _pitch; }
            set { _pitch = value; }
        }
    }
}
