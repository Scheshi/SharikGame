using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;


namespace SharikGame
{
    public class SceneSaver
    {
        private static string _path = Application.dataPath + "/Saves";

        [MenuItem("Сцена/Сохранить сцену")]
        private static void SaveScene()
        {
            var fullPath = _path + "Level.save";
            var objects = GameObject.FindObjectsOfType<Transform>();
            ObjectSerialzable[] serialzables = new ObjectSerialzable[objects.Length];
            for(var i = 0; i<serialzables.Length; i++)
            {
                serialzables[i] = new ObjectSerialzable()
                {
                    FileName = objects[i].gameObject.name,
                    Position = objects[i].position,
                    Rotation = objects[i].rotation,
                    Scale = objects[i].localScale
                };
            }
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
            XmlDocument xml = new XmlDocument();
            XmlNode rootNode = xml.CreateElement("Level");
            xml.AppendChild(rootNode);
            
                foreach(var item in serialzables)
                {
                var element = xml.CreateElement("FileName");
                element.SetAttribute("value", item.FileName);
                rootNode.AppendChild(element);

                element = xml.CreateElement("ParentName");
                element.SetAttribute("value", item.ParentName);
                rootNode.AppendChild(element);

                element = xml.CreateElement("Position");
                element.SetAttribute("x", item.Position.x.ToString());
                element.SetAttribute("y", item.Position.y.ToString());
                element.SetAttribute("z", item.Position.z.ToString());
                rootNode.AppendChild(element);


                element = xml.CreateElement("Rotation");
                element.SetAttribute("x", item.Rotation.x.ToString());
                element.SetAttribute("y", item.Rotation.y.ToString());
                element.SetAttribute("z", item.Rotation.z.ToString());
                element.SetAttribute("w", item.Rotation.w.ToString());
                rootNode.AppendChild(element);

                element = xml.CreateElement("Scale");
                element.SetAttribute("x", item.Scale.x.ToString());
                element.SetAttribute("y", item.Scale.y.ToString());
                element.SetAttribute("z", item.Scale.z.ToString());
                rootNode.AppendChild(element);

                

                xml.Save(fullPath);
            }
        }

        [MenuItem("Сцена/Загрузить сцену")]
        private static void LoadScene()
        {
            var fullPath = _path + "Level.save";
            if (!File.Exists(fullPath)) throw new NullReferenceException("Нет файла для загрузки уровня");
            List<ObjectSerialzable> objects = new List<ObjectSerialzable>();
            using (var stream = new XmlTextReader(fullPath))
            {
                while (stream.Read())
                {
                    float x;
                    float y;
                    float z;
                    float w;
                    var obj = new ObjectSerialzable();

                    var key = "FileName";
                    if (stream.IsStartElement(key)) {
                        obj.FileName = stream.GetAttribute("value");
                    }

                    key = "ParentName";
                    if (stream.IsStartElement(key))
                    {
                        obj.ParentName = stream.GetAttribute("value");
                    }

                    key = "Position";
                    if (stream.IsStartElement(key)) {

                        float.TryParse(stream.GetAttribute("x"), out x);
                        float.TryParse(stream.GetAttribute("y"), out y);
                        float.TryParse(stream.GetAttribute("z"), out z);
                        obj.Position = new Vector3(x, y, z);
                            }
                    key = "Rotation";
                    if (stream.IsStartElement(key))
                    {
                        float.TryParse(stream.GetAttribute("x"), out x);
                        float.TryParse(stream.GetAttribute("y"), out y);
                        float.TryParse(stream.GetAttribute("z"), out z);
                        float.TryParse(stream.GetAttribute("z"), out w);
                        obj.Rotation = new Quaternion(x, y, z, w);
                    }
                    key = "Scale";
                    if (stream.IsStartElement(key))
                    {
                        float.TryParse(stream.GetAttribute("x"), out x);
                        float.TryParse(stream.GetAttribute("y"), out y);
                        float.TryParse(stream.GetAttribute("z"), out z);
                        obj.Scale = new Vector3(x, y, z);
                    }

                    objects.Add(obj);
                }
            }
            var gos = new GameObject[objects.Count];
            for (int i = 0; i < gos.Length; i++)
            {
                gos[i] = GameObject.Instantiate
                    (
                        GameObject.CreatePrimitive(PrimitiveType.Cube)
                    );
                gos[i].name = objects[i].FileName;
            }
            for (int i = 0; i < gos.Length; i++)
            {
                gos[i].transform.parent = GameObject.Find(objects[i].ParentName).transform;
                gos[i].transform.position = objects[i].Position;
                gos[i].transform.rotation = objects[i].Rotation;
                gos[i].transform.localScale = objects[i].Scale;
            }
        }
    }
}
