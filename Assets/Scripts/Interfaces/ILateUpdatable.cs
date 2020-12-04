namespace SharikGame
{
    public interface ILateUpdatable : IUpdatable
    {
        void LateUpdateTick();
    }
}
