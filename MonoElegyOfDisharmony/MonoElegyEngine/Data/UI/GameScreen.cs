namespace EquestriEngine.Data.UI
{
    public abstract class GameScreen : Interfaces.IGameScreen
    {
        private bool 
            _enabled;

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



        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
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

        public GameScreen()
        {
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

        public abstract void Update(float dt);

        public virtual void HandleInput(float dt)
        {

        }
    }
}
