using UnityEngine;
using System;


namespace SharikGame
{
    public class PlayerController : IFixedUpdatable
    {
        #region Fields

        public PlayerModel _model;
        private Rigidbody _rigidbody;
        private Vector3 _movement;

        #endregion


        #region Constructors

        public PlayerController(PlayerModel model, GameObject player)
        {
            if(model.HealthPoints <= 0 || model.Speed <= 0)
            model = new PlayerModel
            {
                HealthPoints = 100.0f,
                Speed = 2.0f
            };
            _model = model;
            _rigidbody = player.GetComponent<Rigidbody>();
            PlayerAdjust.Initialize(this);
        }

        #endregion


        #region Methods

        public void CheckMove(Vector3 vector)
        {
            _movement = vector;
        }

        public void FixedTick()
        {
            _movement *= _model.Speed;
            _movement.y = _rigidbody.velocity.y;
            _rigidbody.velocity = _movement;
            
        }

        #endregion
    }
}
