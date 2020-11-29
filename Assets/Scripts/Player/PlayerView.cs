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

        private int _forwardAnimationHash;
        private int _rightAnimationHash;

        #endregion


        #region UnityMethods

        private void Awake()
        {
                _controller = new PlayerController(_model, gameObject);
        }

        private void OnEnable()
        {
            PlayerAdjust.Death += Dispose;
        }

        private void OnDisable()
        {
            PlayerAdjust.Death -= Dispose;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _forwardAnimationHash = Animator.StringToHash("Forward");
            _rightAnimationHash = Animator.StringToHash("Right");
        }

        #endregion


        #region Methods

        private void AnimationMove()
        {
            var vector = 
                new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _controller.CheckMove(vector);
            _animator.SetFloat(_forwardAnimationHash, vector.z);
            _animator.SetFloat(_rightAnimationHash, vector.x);
        }

        public void Tick()
        {
            AnimationMove();
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
