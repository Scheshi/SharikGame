using UnityEngine;
using UnityEditor;


namespace SharikGame
{
    public class SettingSaver : EditorWindow
    {
        private void OnGUI()
        {

            GUILayout.Label("Настройки сохранения сцены", EditorStyles.boldLabel);
            GUILayout.Label("Путь до префабов сцены");
            GUILayout.Label("(Отностительно папки Asset/Resources/)");
            SceneSaver.PrefabsPath = EditorGUILayout.TextField(SceneSaver.PrefabsPath);
            
        }
    }
}
