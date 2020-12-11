namespace SharikGame {
    public interface ISaver
    {
        void Save(IData data, string path = null);
        IData Load(string path = null);
    }
}
