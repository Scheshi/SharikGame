using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace SharikGame
{
    public class EnemyWather : IUpdatable
    {
        private List<Transform> _enemySpawnPoints;
        private Transform _playerTransform;
        private GameObject _enemyObject;
        private GameManager _manager;
        private float _distance;


        public EnemyWather(Transform[] spawnsEnemy, Transform playerTransform, float distance, GameObject enemyObject, GameManager manager)
        {
            _enemySpawnPoints = new List<Transform>(spawnsEnemy);
            _playerTransform = playerTransform;
            _distance = distance;
            _enemyObject = enemyObject;
            _manager = manager;
        }

        public void Tick()
        {
            if (_enemySpawnPoints.Count > 0 || _playerTransform != null)
            {
                foreach (var point in _enemySpawnPoints)
                {
                    if ((_playerTransform.position - point.position).sqrMagnitude <=
                        _distance * _distance)
                    {
                        EnemyView enemyView = null;
                        GameObject enemy = null;
                        try
                        {
                            GameObject.Instantiate
                                (_enemyObject, point.position, Quaternion.identity)
                                .TryGetComponent(out enemyView);
                        }
                        catch (UnassignedReferenceException)
                        {
                            throw new UnassignedReferenceException("Отсутствует префаб врага в GameManager");
                        }
                        _manager.AddingUpdatable(enemyView);
                        _manager.AddingFixedUpdatable(enemyView);
                        _enemySpawnPoints.Remove(point);
                        break;
                    }
                }
            }
        }
    }
}
