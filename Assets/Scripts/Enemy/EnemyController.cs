using UnityEngine;


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
            Collider[] hits = null;
            Physics.OverlapSphereNonAlloc(_enemyTransform.position, 15.0f, hits);
            if (hits.Length > 0)
            {
                foreach (var obj in hits)
                {
                    if (obj.GetComponent<PlayerView>() != null)
                    {
                        _target = obj.transform.position;
                    }
                }

            }
        }

        #endregion
    }
}