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
            _model = model;
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

        public void Damage(GameObject obj)
        {
            PlayerView playerView;
            if(obj.TryGetComponent(out playerView))
            {
                playerView.Model.Adjust(
                    new PlayerStruct
                    {
                        LifeCount = -_model.Damage
                    });
            }
        }
        #endregion
    }
}