using UnityEngine;
using System;


namespace SharikGame
{
    public class PlayerController : IFixedUpdatable
    {
        #region Fields

        public PlayerModel _model;
        public event Action Death;
        private Rigidbody _rigidbody;
        private Vector3 _movement;

        #endregion


        #region Constructors

        public PlayerController(GameObject player)
        {
            _model = new PlayerModel {
                HealthPoints = 100.0f,
                Speed = 2.0f
            };
            _rigidbody = player.GetComponent<Rigidbody>();
            PlayerAdjust.Initialize(this);
        }

        public PlayerController(float speed, float health, float forceJump, GameObject player)
        {
            _model = new PlayerModel {
                HealthPoints = health,
                Speed = speed,
                ForceJump = forceJump
            };
            _rigidbody = player.GetComponent<Rigidbody>();
        }

        public PlayerController(PlayerModel model, GameObject player)
        {
            _model = model;
            _rigidbody = player.GetComponent<Rigidbody>();
        }

        #endregion


        #region Methods

        public void Adjust(PlayerModel model)
        {
            _model.HealthPoints += model.HealthPoints;
            _model.Speed += model.Speed;

            if(_model.HealthPoints <= 0)
            {
                Death?.Invoke();
            }
        }

        public void CheckMove(Vector3 vector)
        {
            _movement = vector;
        }

        public void FixedTick()
        {
            _movement.y = _rigidbody.velocity.y;
            _rigidbody.velocity = _movement * _model.Speed;
        }

        #endregion
    }
}
