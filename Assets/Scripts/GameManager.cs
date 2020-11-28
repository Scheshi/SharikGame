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
        [SerializeField] private List<Transform> _enemySpawnPoints = new List<Transform>();
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
            GameOver(false);
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
            _updatables.Add(new CameraController(_player, _cameraLocalPositionFromPlayer));
        }


        private void Update()
        {
            CheckDistanceEnemyAndPlayer();

            foreach (var updatable in _updatables)
            {
                updatable.Tick();
            }
            if (_pointsText.GetPoints >= _maxPoints)
            {
                GameOver(true);
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

        private void CheckDistanceEnemyAndPlayer()
        {
            if (_enemySpawnPoints.Count > 0)
            {
                foreach (var point in _enemySpawnPoints)
                {
                    if ((_player.position - point.position).sqrMagnitude <=
                        _distancePlayerForSpawnEnemy * _distancePlayerForSpawnEnemy)
                    {
                        var enemyView = Instantiate(_enemyPrefab, point.position, Quaternion.identity).GetComponent<EnemyView>();
                        _enemySpawnPoints.Remove(point);
                        _fixedUpdatables.Add(enemyView);
                        _updatables.Add(enemyView);
                        break;

                    }
                }
            }
        }

        private void GameOver(bool isActive)
        {

            Time.timeScale = isActive ? 0.0f : 1.0f;
            _gameOverUI.SetActive(isActive);
        }

        #endregion
    }
}
