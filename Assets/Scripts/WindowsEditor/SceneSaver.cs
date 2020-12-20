using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace SharikGame
{
    public class SceneSaver
    {
        private static string _dataPath = Application.dataPath + "/Resources/";
        private static string _prefabsPath = "Prefabs/Level/";

        public static string PrefabsPath
        {
            get
            {
                return _prefabsPath;
            }
            set
            {
                if(value != String.Empty)
                {
                    _prefabsPath = value;
                }
            }
        }

        [MenuItem("Сцена/Сохранить сцену")]
        private static void SaveScene()
        {
            var fullPath = _dataPath + _prefabsPath;
            Debug.Log("Начинаю сохранение сцены");
            if (Directory.Exists(fullPath)) 
            {
                var files = Directory.GetFiles(fullPath);
                foreach(var file in files)
                {
                    File.Delete(file);
                }
                Directory.Delete(fullPath); 
            }
            
            if(!Directory.Exists(fullPath))Directory.CreateDirectory(fullPath);
            var objects = GameObject.FindObjectsOfType<Transform>();
            for(var i = 0; i<objects.Length; i++)
            {
                if (objects[i].transform.parent != null)
                {
                    continue;
                }
                bool success;
                PrefabUtility.SaveAsPrefabAsset(objects[i].gameObject, fullPath + objects[i].gameObject.name + ".prefab", out success);
                if (!success)
                    throw new ArgumentException("Проблема с сохранением объекта в префаб");
                GameObject.DestroyImmediate(objects[i].gameObject);
                Debug.Log($"Сцена сохранена на {((float)i /objects.Length) * 100}%");
            }

            Debug.Log("Сцена успешно сохранена");
        }

        [MenuItem("Сцена/Загрузить сцену")]
        public static void LoadScene()
        {
            Debug.Log("Очистка сцены");
            foreach (var obj in GameObject.FindObjectsOfType<Transform>()) 
            {
                if (obj.parent != null) continue;
#if UNITY_EDITOR
                GameObject.DestroyImmediate(obj.gameObject);
#else
                GameObject.Destroy(obj.gameObject);
#endif
            }
            Debug.Log("Начинаю загрузку сцены");
            var objects = Resources.LoadAll(_prefabsPath, typeof(GameObject)).Cast<GameObject>().ToArray();
            if (objects.Length <= 0) throw new NullReferenceException("Нет префабов для загрузки. Пожалуйста, сначала сохраните карту.");
            foreach (var obj in objects)
            {
                var go = GameObject.Instantiate(obj);
                go.name = obj.name;
            }
            Debug.Log("Сцена загружена");
        }

        [MenuItem("Сцена/Открыть окно настройки сохранения сцены")]
        private static void MenuOption()
        {
            EditorWindow.GetWindow(typeof(SettingSaver), false, "Настройки сохранения сцены");
        }
    }
}
