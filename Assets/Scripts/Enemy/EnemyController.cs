﻿using UnityEngine;


namespace SharikGame
{
    public class EnemyController : IUpdatable, IFixedUpdatable
    {
        #region Fields

        private Transform _enemyTransform;
        private Rigidbody _enemyRigidbody;
        private Vector3 _target;
        private EnemyModel _model;

        #endregion


        #region Contructors
        public EnemyController(EnemyModel model, GameObject enemy)
        {
            _enemyTransform = enemy.transform;

            enemy.TryGetComponent(out _enemyRigidbody);
            if (model.Speed <= 0)
            {
                model = new EnemyModel
                {
                    Damage = 0.5f,
                    Speed = 1.5f
                };
            }
            else
            {
                _model = model;
            }
        }

        #endregion


        #region Methods

        public void FixedTick()
        {
                var vectorMovement = (_target - _enemyTransform.position).normalized;
                _enemyRigidbody.velocity = vectorMovement * _model.Speed;
        }

        public void Tick()
        {
            //Не знаю, нафига. Ну да ладно.
            Collider[] hits = Physics.OverlapSphere(_enemyTransform.position, 5.0f);
                foreach (var obj in hits)
                {
                    PlayerView playerView;
                    obj.TryGetComponent(out playerView);
                    if (playerView != null)
                    {
                        _target = obj.transform.position;
                        break;
                    }
                }
        }

        #endregion
    }
}