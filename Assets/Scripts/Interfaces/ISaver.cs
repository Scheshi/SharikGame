namespace SharikGame {
    public interface ISaver<T>
    {
        void Save(IData data, string path = null);
        T Load<T>(string path = null);
    }
}
