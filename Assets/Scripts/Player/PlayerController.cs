using UnityEngine;
using System;


namespace SharikGame
{
    public class PlayerController : IFixedUpdatable
    {
        #region Fields

        public PlayerModel Model;
        private Rigidbody _rigidbody;
        private Vector3 _movement;

        #endregion


        #region Constructors

        public PlayerController(PlayerModel model, GameObject player)
        {
            Model = model;
            _rigidbody = player.GetComponent<Rigidbody>();
        }

        #endregion


        #region Methods

        public void CheckMove(Vector3 vector)
        {
            _movement = vector;
        }

        public void FixedTick()
        {
            _movement *= Model.Speed;
            _movement.y = _rigidbody.velocity.y;
            _rigidbody.velocity = _movement;
            
        }

        #endregion
    }
}
