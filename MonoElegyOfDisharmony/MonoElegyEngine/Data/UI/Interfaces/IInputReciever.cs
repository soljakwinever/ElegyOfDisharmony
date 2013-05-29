using EquestriEngine.Data.Controls;

namespace EquestriEngine.Data.UI.Interfaces
{
    public interface IInputReciever
    {
        bool HasFocus
        {
            get;
            set;
        }

        IControlScheme ControlReference
        {
            get;
            set;
        }

        void HandleInput(float dt);
    }
}
