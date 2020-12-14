namespace SharikGame {
    public interface ISaver
    {
        void Save(IData data, string path = null);
        void Load(ref IData data, string path = null);
    }
}
