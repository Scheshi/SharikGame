using UnityEngine;
using System;


namespace SharikGame
{
    public class PlayerView : MonoBehaviour, IUpdatable, IFixedUpdatable, IDisposable
    {
        #region Fields

        [SerializeField] private PlayerStruct _playerStruct;
        private PlayerController _controller;
        private Animator _animator;

        private int _forwardAnimationHash;
        private int _rightAnimationHash;

        #endregion


        #region Properties

        public PlayerModel Model
        {
            get
            {
                return _controller.Model;
            }
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            var playerModel = new PlayerModel(_playerStruct);
            _controller = new PlayerController(playerModel, gameObject);
        }

        private void OnEnable()
        {
            GameOverChecker.EndGame += Dispose;
        }

        private void OnDisable()
        {
            GameOverChecker.EndGame -= Dispose;
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
            //_animator.SetFloat(_forwardAnimationHash, vector.z);
            //_animator.SetFloat(_rightAnimationHash, vector.x);
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
