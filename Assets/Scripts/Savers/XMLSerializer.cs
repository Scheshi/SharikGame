using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace SharikGame
{
    public class XMLSerializer : ISaver
    {
        public void Load(ref IData data, string path = null)
        { 
            
            if (path == null) throw new ArgumentException("Неверные значения пути для загрузки данных в " + this.GetType());
            var type = data.GetType();
            Debug.Log(type.Name);
            XmlSerializer serializer = new XmlSerializer(type);
            using (FileStream stream = new FileStream($"{path}/{type.Name}.save", FileMode.Open))
            {
                data = (IData)serializer.Deserialize(stream);
                stream.Close();
            }


        }

        public void Save(IData data, string path = null)
        {
            if (path == null || IData.Equals(data, null)) return;
            Type type = data.GetType();
            XmlSerializer serializer = new XmlSerializer(type);

            using (FileStream stream = new FileStream($"{path}/{type.Name}.save", FileMode.Create))
            {
                serializer.Serialize(stream, data);
                stream.Close();
                //Debug.Log($"Объект сериализован в тип {this.GetType()} по пути {path}");
            }
        }
    }
}
