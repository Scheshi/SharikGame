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

        void Start()
        {
            var gameOverChecker = new GameOverChecker(_uiGameOver);
            new ButtonReloaderView(_uiGameOver.GetComponentInChildren<Button>());
            ServiceLocator.SetDepencity(gameOverChecker);
            gameOverChecker.GameEnd(false, false);
            new PlayerInizializator(_playerData, _startPoint);
            var camera = new CameraController(ServiceLocator.GetDepencity<GameObject>().transform);
            ControllersUpdater.AddUpdate(camera);
            _sliderUI.maxValue = _interactiveObjects.Length;
            var slider = new SliderController(_sliderUI);
            ServiceLocator.SetDepencity(slider);
            

            foreach(var obj in _interactiveObjects)
            {
                new PointBonus(obj);
            }
            new EnemySpawner(_pointsForEnemySpawn, _enemyData);


        }
    }
}