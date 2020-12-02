using System;
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
        private List<IUpdatable> _newUpdatables = new List<IUpdatable>();
        private List<IFixedUpdatable> _newFixedUpdatables = new List<IFixedUpdatable>();


        private Transform _playerTrasform;
        private TextPoints _pointsText;
        private int _maxPoints;

        #endregion


        #region UnityMethods

        private void Start()
        {
            var player = Instantiate(_playerPrefab, _startPoint.position, Quaternion.identity);
            var playerView = player.GetComponent<PlayerView>();
            _playerTrasform = player.transform;
            _updatables.Add(playerView);
            _fixedUpdatables.Add(playerView);

            _updatables.Add
                (new EnemyWather(_enemySpawnPoints, _playerTrasform,
                _distancePlayerForSpawnEnemy, _enemyPrefab, this));

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

            _updatables.Add
                (new CameraController(_playerTrasform, _cameraLocalPositionFromPlayer));
        }


        private void Update()
        {
            foreach (var updatable in _updatables)
            {
                updatable.Tick();
            }
            if(_newUpdatables.Count > 0)
            {
                _updatables.AddRange(_newUpdatables);
                _newUpdatables.Clear();
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
            if(_newFixedUpdatables.Count > 0)
            {
                _fixedUpdatables.AddRange(_newFixedUpdatables);
                _newFixedUpdatables.Clear();
            }
        }

        #endregion


        #region Methods

        public void AddingUpdatable(IUpdatable updatable)
        {
            _newUpdatables.Add(updatable);
        }

        public void AddingFixedUpdatable(IFixedUpdatable updatable)
        {
            _newFixedUpdatables.Add(updatable);
        }

        #endregion
    }
}
