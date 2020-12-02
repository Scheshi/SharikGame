using UnityEngine;


namespace SharikGame
{
    public class EnemyView : MonoBehaviour, IUpdatable, IFixedUpdatable
    {
        #region Fields

        [SerializeField] private EnemyStruct _enemyStruct;
        private EnemyController _controller;

        

        #endregion


        #region UnityMethods

        private void Awake()
        {
            var enemyModel = new EnemyModel(_enemyStruct);
           _controller = new EnemyController(enemyModel, gameObject);
        }

        #endregion


        #region Methods

        public void FixedTick()
        {
            _controller.FixedTick();
        }

        public void Tick()
        {
            _controller.Tick();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _controller.Damage(collision.gameObject);
        }
        #endregion
    }
}
