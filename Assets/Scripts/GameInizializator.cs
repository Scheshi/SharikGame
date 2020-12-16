using System;
using UnityEngine;
using UnityEngine.UI;


namespace SharikGame
{
    public class GameInizializator : MonoBehaviour
    {
        
        [SerializeField] private PersonData _playerData;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Slider _sliderUI;
        [SerializeField] private GameObject[] _interactiveObjects;
        [SerializeField] private Transform[] _pointsForEnemySpawn;
        [SerializeField] private EnemyData _enemyData;
        [SerializeField] private GameObject _uiGameOver;
        [SerializeField] private SerializerEnum _serializer;

        void Start()
        {
            var repository = new Repository(_serializer);
            ServiceLocator.SetDependency(repository);
            GameObject updaterGO = new GameObject("Updater");
            updaterGO.AddComponent<ControllersUpdater>();

            RadarController radar = new RadarController(FindObjectOfType<Image>().transform);
            ServiceLocator.SetDependency(radar);
            ControllersUpdater.AddUpdate(radar);

            var gameOverChecker = new GameOverChecker(_uiGameOver);
            new ButtonReloaderView(_uiGameOver.GetComponentInChildren<Button>());
            ServiceLocator.SetDependency(gameOverChecker);
            gameOverChecker.GameEnd(false, false);
            new PlayerInizializator(_playerData, _startPoint);
            _sliderUI.maxValue = _interactiveObjects.Length;
            var slider = new SliderController(_sliderUI);
            ServiceLocator.SetDependency(slider);

            //ServiceLocator.GetDependency<Repository>().AddDataToList(slider);
            repository.AddDataToList(slider);


            for(int i = 0; i < _interactiveObjects.Length; i++)
            {
                var bonus = new PointBonus(_interactiveObjects[i], i);
                repository.AddDataToList(bonus);
                    var sprite = Resources.Load<GameObject>("Textures/PickupRadar");
                    radar.AddingObject(_interactiveObjects[i], sprite);

            }
            new EnemySpawner(_pointsForEnemySpawn, _enemyData);

            Destroy(gameObject);
        }
    }
}