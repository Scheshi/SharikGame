using System.Collections.Generic;
using UnityEngine;


namespace SharikGame
{
    public class GameManager : MonoBehaviour
    {
        #region Fields

        [Header("Player")]
        [SerializeField] private Vector3 _cameraLocalPositionFromPlayer;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _startPoint;

        [Header("Enemy")]
        [SerializeField] private Transform[] _enemySpawnPoints;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _distancePlayerForSpawnEnemy;

        [Header("UI")]
        [SerializeField] private GameObject _gameOverUI;

        private List<IUpdatable> _updatables = new List<IUpdatable>();
        private List<IFixedUpdatable> _fixedUpdatables = new List<IFixedUpdatable>();
        private Transform _player;
        private TextPoints _pointsText;
        private int _maxPoints;

        #endregion


        #region UnityMethods

        private void Start()
        {
            GameOverChecker.Initialize(_gameOverUI);
            GameOverChecker.GameOver(false);

            var objects = FindObjectsOfType<PointBonus>();
            _maxPoints = objects.Length;
            Debug.Log(_maxPoints);
            var slider = new SliderPoints(objects.Length);
            _pointsText = new TextPoints();

            foreach (var obj in objects)
            {
                obj.Initialize(slider, _pointsText);
            }
            var player = Instantiate(_playerPrefab, _startPoint.position, Quaternion.identity);
            var playerView = player.GetComponent<PlayerView>();
            _player = player.transform;
            _updatables.Add(playerView);
            _fixedUpdatables.Add(playerView);
            _updatables.Add
                (new CameraController(_player, _cameraLocalPositionFromPlayer));
            _updatables.Add
                (new EnemyWather(_enemySpawnPoints, _player,
                _distancePlayerForSpawnEnemy, _enemyPrefab, this));
        }


        private void Update()
        {
            foreach (var updatable in _updatables)
            {
                updatable.Tick();
            }
            if (_pointsText.GetPoints >= _maxPoints)
            {
                GameOverChecker.GameOver(true);
            } 
        }

        private void FixedUpdate()
        {
            foreach (var fixedUpdateble in _fixedUpdatables)
            {
                fixedUpdateble.FixedTick();
            }
        }

        #endregion


        #region Methods

        public void AddingUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void AddingFixedUpdatable(IFixedUpdatable updatable)
        {
            _fixedUpdatables.Add(updatable);
        }

        #endregion
    }
}
