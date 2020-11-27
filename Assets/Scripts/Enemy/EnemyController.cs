using UnityEngine;


namespace SharikGame
{
    public class EnemyController : IUpdatable, IFixedUpdatable
    {
        #region Fields

        private Transform _enemy;
        private Rigidbody _rigidbody;
        private Vector3 _target;
        private EnemyModel _model;

        #endregion


        #region Contructors
        public EnemyController(GameObject enemy)
        {
            _model = new EnemyModel
            {
                Damage = 5.0f,
                Speed = 1.5f
            };
            _enemy = enemy.transform;
            _rigidbody = enemy.GetComponent<Rigidbody>();

        }


        public EnemyController(EnemyModel model, GameObject enemy)
        {
            _enemy = enemy.transform;
            _rigidbody = enemy.GetComponent<Rigidbody>();
            _model = model;
        }

        #endregion


        #region Methods

        public void FixedTick()
        {
            var vectorMovement = (_target - _enemy.position).normalized;
            _rigidbody.velocity = vectorMovement * _model.Speed;
            Debug.Log(vectorMovement);
        }

        public void Tick()
        {
            Collider[] hits = Physics.OverlapSphere(_enemy.position, 15.0f);
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