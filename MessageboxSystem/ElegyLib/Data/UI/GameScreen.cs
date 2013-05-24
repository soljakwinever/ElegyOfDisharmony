namespace EquestriEngine.Data.UI
{
    public abstract class GameScreen : Interfaces.IGameScreen
    {
        private bool 
            _coversOthers,
            _isCovered,
            _hasFocus;

        public Equestribatch SpriteBatch
        {
            get { return _stateManager.SpriteBatch; }
        }

        private Controls.IControlScheme _controlReference;

        public Controls.IControlScheme ControlReference
        {
            get { return _controlReference; }
            set 
            { 
                _controlReference = value; 
            }
        }

        public event GenericEvent 
            OnWindowShow,
            OnWindowHide,
            OnWindowDestroy;

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

        public bool HasFocus
        {
            get { return _hasFocus; }
            set
            {
                _hasFocus = value;
            }
        }

        protected Systems.StateManager _stateManager;

        public GameScreen(Systems.StateManager manager, bool coversOthers)
        {
            _stateManager = manager;
            _coversOthers = coversOthers;
        }
        ~GameScreen()
        {
            OnWindowShow = null;
            OnWindowHide = null;
            OnWindowDestroy = null;
        }

        public abstract void Initialize();
        
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
