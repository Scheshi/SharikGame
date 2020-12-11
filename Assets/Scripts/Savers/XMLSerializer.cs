using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace SharikGame
{
    public class XMLSerializer : ISaver
    {
        public IData Load(string path = null)
        {
            if (path == null) throw new ArgumentException("Неверные значения пути для загрузки данных в " + this.GetType());
            var type = typeof(IData);
            XmlSerializer serializer = new XmlSerializer(type);
            using (FileStream stream = new FileStream($"{path}/{type}", FileMode.Create))
            {
                var result = serializer.Deserialize(stream);
                return (IData)result;
            }


        }

        public void Save(IData data, string path = null)
        {
            if (path == null || IData.Equals(data, null)) return;
            XmlSerializer serializer = new XmlSerializer(data.GetType());

            using (FileStream stream = new FileStream($"{path}/{data.GetType()}", FileMode.Create))
            {
                serializer.Serialize(stream, data);
                Debug.Log("Объект сериализован в Xml");
            }
        }
    }
}
