using EquestriEngine.Data.Controls;

namespace EquestriEngine.Data.UI.Interfaces
{
    public interface IGameScreen
    {
        bool HasFocus
        {
            get;
            set;
        }

        bool CoversOthers
        {
            get;
        }

        bool IsCovered
        {
            get;
            set;
        }

        IControlScheme ControlReference
        {
            get;
            set;
        }

        void Initialize();
        void LoadContent();
        void UnloadContent();
        void Update(float dt);
        void HandleInput(float dt);
        void Draw(float dt);

        event GenericEvent
            OnWindowShow,
            OnWindowHide,
            OnWindowDestroy;
    }
}
