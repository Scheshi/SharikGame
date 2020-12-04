using UnityEngine;


namespace SharikGame
{

    [CreateAssetMenu(fileName = "New Person Data", menuName = "Data/Person")]
    public class PersonData : ScriptableObject
    {
        [SerializeField] public PlayerStruct PlayerStruct;
        [SerializeField] public GameObject gameObject;

    }
}
