using EquestriEngine.Data.Scenes;
namespace EquestriEngine.Data.UI.Interfaces
{
    public interface IWidget
    {
        Vector2 Position
        {
            get;
            set;
        }
        bool Shown
        {
            get;
        }
        void Init();
        void Update(float dt);
        void Unload();
        void Draw(Equestribatch sb);
    }
}
