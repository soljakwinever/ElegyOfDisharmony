using Exception = System.Exception;

namespace EquestriEngine.Data.Exceptions
{
    public class EngineException : Exception
    {
        private bool _serious;

        /// <summary>
        /// Is this an error the engine should stop for?
        /// </summary>
        public bool Serious
        {
            get { return _serious; }
        }

        public EngineException(string message, bool serious)
            : base(message)
        {
            _serious = serious;
            if (serious)
                EquestriEngine.ErrorMessage = message;
        }
    }
}
