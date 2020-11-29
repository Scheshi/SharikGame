using UnityEngine;


namespace SharikGame
{
    public class EnemyView : MonoBehaviour, IUpdatable, IFixedUpdatable
    {
        #region Fields

        [SerializeField] private EnemyModel _model;
        private EnemyController _controller;

        

        #endregion


        #region UnityMethods

        private void Awake()
        {
            if(_model.Damage > 0)
            {
                _controller = new EnemyController(_model, gameObject);
            }
            else
            {
                _controller = new EnemyController(gameObject);
            }
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

        #endregion
    }
}
