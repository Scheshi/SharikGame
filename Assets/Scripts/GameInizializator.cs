using System;
using UnityEngine;
using UnityEngine.UI;


namespace SharikGame
{
    public class GameInizializator : MonoBehaviour
    {
        #region Fields

        [HideInInspector]public string UserName = "Player";
        [SerializeField] private PersonData _playerData;
        [SerializeField] private EnemyData _enemyData;
        [SerializeField] private SerializerEnum _serializer;

        private Transform[] _interactiveObjects;
        private Transform[] _pointsForEnemySpawn;

        private GameObject _uiGameOver;
        private Transform _startPoint;
        private Slider _sliderUI;

        #endregion


        #region UnityMethods

        private void Start()
        {

            if (GameObject.FindObjectsOfType<Transform>().Length <= 4)
            {
                SceneSaver.LoadScene();
            }
            else
            {

                _startPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
                _uiGameOver = GameObject.FindGameObjectWithTag("Finish");
                _sliderUI = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
                var enemyesRespawns = GameObject.FindGameObjectsWithTag("EnemyRespawn");
                _pointsForEnemySpawn = new Transform[enemyesRespawns.Length];

                for (int i = 0; i < enemyesRespawns.Length; i++)
                {
                    _pointsForEnemySpawn[i] = enemyesRespawns[i].transform;
                }

                _interactiveObjects = GameObject.FindGameObjectWithTag("Bonuses").transform.GetComponentsInChildren<Transform>();

                var repository = new Repository(_serializer);
                ServiceLocator.SetDependency(repository);
                GameObject updaterGO = new GameObject("Updater");
                updaterGO.AddComponent<ControllersUpdater>();

                RadarController radar = new RadarController(FindObjectOfType<Image>().transform);
                ServiceLocator.SetDependency(radar);
                ControllersUpdater.AddUpdate(radar);

                _sliderUI.maxValue = _interactiveObjects.Length;
                var slider = new SliderController(_sliderUI);
                //ServiceLocator.SetDependency(slider);

                var gameOverChecker = new GameOverChecker(_uiGameOver, slider);
                new ButtonReloaderView(_uiGameOver.GetComponentInChildren<Button>());
                ServiceLocator.SetDependency(gameOverChecker);
                gameOverChecker.GameEnd(false, false);
                new PlayerInizializator(_playerData, _startPoint, UserName);

                //ServiceLocator.GetDependency<Repository>().AddDataToList(slider);
                repository.AddDataToList(slider);


                for (int i = 0; i < _interactiveObjects.Length; i++)
                {
                    var bonus = new PointBonus(_interactiveObjects[i].gameObject, i, slider);
                    repository.AddDataToList(bonus);
                    var sprite = Resources.Load<GameObject>("Textures/PickupRadar");
                    radar.AddingObject(_interactiveObjects[i].gameObject, sprite);

                }
                new EnemySpawner(_pointsForEnemySpawn, _enemyData);
                Destroy(gameObject);
            }

            #endregion

        }
    }
}