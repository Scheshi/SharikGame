using UnityEngine;


namespace SharikGame
{    
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Data/Enemy")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] public EnemyStruct EnemyStruct;
        [SerializeField] public GameObject gameObject;

    }
}
