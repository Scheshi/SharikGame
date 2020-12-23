using System;
using UnityEngine;


namespace SharikGame
{
    public class PlayerController : ControllerFabric
    {
        private PlayerModel _playerModel;
        private Rigidbody _rigidbody;
        private Vector3 _movementVector;

        public PlayerController(PlayerModel model, GameObject go) : base(model, go)
        {
            /*_playerModel = model;
            if (!go.TryGetComponent(out _rigidbody))
            {
                throw new ArgumentException("Невозможно получить объект игрока в PlayerController");
            }
            ControllersUpdater.AddUpdate(this);*/
        }
    }
}
