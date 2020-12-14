using System;
using UnityEngine;


namespace SharikGame
{
    internal class PlayerController : IFixedUpdatable
    {
        private PlayerModel _playerModel;
        private Rigidbody _rigidbody;
        private Vector3 _movementVector;

        public PlayerController(PlayerModel model, GameObject go)
        {
            _playerModel = model;
            if(!go.TryGetComponent(out _rigidbody))
            {
                throw new ArgumentException("Невозможно получить объект игрока в PlayerController");
            }
            ControllersUpdater.AddUpdate(this);
        }

        public void FixedUpdateTick()
        {
            _movementVector *= _playerModel.Speed;
            _movementVector.y = _rigidbody.velocity.y;
            _rigidbody.velocity = _movementVector;
        }

        public void Move(Vector3 vector)
        {
            _movementVector = vector;
        }
    }
}
