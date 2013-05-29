
namespace EquestriEngine.Data.UI.Interfaces
{
    public interface IGameScreen
    {
        Systems.StateManager StateManager
        {
            get;
            set;
        }

        void Initialize();
        void Update(float dt);

        event GenericEvent
            OnWindowDestroy;
    }
}
