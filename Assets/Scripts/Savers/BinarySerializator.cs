using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace SharikGame
{
    public class BinarySerializator : ISaver
    {
        public void Load(ref IData data, string path = null)
        {
            var type = data.GetType();
            var fullPath = $"{path}/{type.Name}.save";
            if (path == null) throw new ArgumentException("Неверные значения пути для загрузки данных в " + this.GetType());

            Debug.Log(type.Name);
            BinaryFormatter serializer = new BinaryFormatter();
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (!File.Exists(fullPath))
            {
                Debug.Log($"Нет сохранений для типa {type.Name}");
                return;
            }
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                data = (IData)serializer.Deserialize(stream);
                stream.Close();
            }
        }

        public void Save(IData data, string path = null)
        {
            if (path == null || Equals(data, null)) return;
            Type type = data.GetType();
            BinaryFormatter serializer = new BinaryFormatter();

            using (FileStream stream = new FileStream($"{path}/{type.Name}.save", FileMode.Create))
            {
                serializer.Serialize(stream, data);
                stream.Close();
                //Debug.Log($"Объект сериализован в тип {this.GetType()} по пути {path}");
            }
        }
    }
}
