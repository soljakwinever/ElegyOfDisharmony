namespace EquestriEngine.Data.UI
{
    public abstract class GameScreen : Interfaces.IGameScreen
    {
        private bool 
            _coversOthers,
            _isCovered,
            _hasFocus,
            _getsInput;

        Objects.Graphics.TextureCollection _screenAssets;

        protected Systems.StateManager _stateManager;

        private Controls.IControlScheme _controlReference;

        public Equestribatch SpriteBatch
        {
            get { return _stateManager.SpriteBatch; }
        }

        public Controls.IControlScheme ControlReference
        {
            get { return _controlReference; }
            set 
            { 
                _controlReference = value; 
            }
        }

        public bool CoversOthers
        {
            get { return _coversOthers; }
        }

        public bool IsCovered
        {
            get { return _isCovered; }
            set
            {
                _isCovered = value;
            }
        }

        public bool GetsInput
        {
            get { return _getsInput; }
        }

        public bool HasFocus
        {
            get { return _hasFocus; }
            set
            {
                _hasFocus = value;
            }
        }

        public Systems.StateManager StateManager
        {
            get { return _stateManager; }
            set { _stateManager = value; }
        }

        public event GenericEvent
            OnWindowShow,
            OnWindowHide,
            OnWindowDestroy;

        public GameScreen(bool coversOthers, bool getsInput)
        {
            _getsInput = getsInput;
            _coversOthers = coversOthers;
            _screenAssets = new Objects.Graphics.TextureCollection();
        }
        ~GameScreen()
        {
            OnWindowShow = null;
            OnWindowHide = null;
            OnWindowDestroy = null;
        }

        public virtual void Initialize()
        {
            if (_stateManager == null)
                throw new Data.Exceptions.EngineException("Screen does not have a valid State Manager",true);
        }
        
        public virtual void LoadContent()
        {

        }

        public abstract void Update(float dt);

        public virtual void HandleInput(float dt)
        {

        }

        public virtual void UnloadContent()
        {
            
        }

        public abstract void Draw(float dt);
    }
}
