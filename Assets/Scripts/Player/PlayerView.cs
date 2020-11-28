using UnityEngine;
using System;


namespace SharikGame
{
    public class PlayerView : MonoBehaviour, IUpdatable, IFixedUpdatable, IDisposable
    {
        #region Fields

        [SerializeField] private PlayerModel _model;
        private PlayerController _controller;
        private Animator _animator;

        #endregion


        public PlayerController GetController 
        { 
            get
            {
                return _controller;
            } 
        }


        #region UnityMethods

        private void Awake()
        {
            if (_model.HealthPoints > 0)
            {
                _controller = new PlayerController(_model, gameObject);
            }
            else
            {
                _controller = new PlayerController(gameObject);
            }
        }

        private void OnEnable()
        {
            _controller.Movement += AnimationMove;
            _controller.Death += Dispose;
        }

        private void OnDisable()
        {
            _controller.Movement -= AnimationMove;
            _controller.Death -= Dispose;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        #endregion


        #region Methods

        private void AnimationMove(Vector3 vector)
        {
            _animator.SetFloat("Forward", vector.z);
            _animator.SetFloat("Right", vector.x);
        }

        public void Tick()
        {
            _controller.Tick();
        }

        public void FixedTick()
        {
            _controller.FixedTick();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
