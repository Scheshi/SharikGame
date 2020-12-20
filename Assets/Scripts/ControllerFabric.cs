using System;
using UnityEngine;


namespace SharikGame
{
    public abstract class ControllerFabric : IFixedUpdatable
    {
        #region Fields

        private IModel _model;
        private Rigidbody _rigidbody;
        private Vector3 _movementVector;

        #endregion


        #region Contructors

        public ControllerFabric(IModel model, GameObject go)
        {
            _model = model;
            if (!go.TryGetComponent(out _rigidbody))
            {
                throw new ArgumentException("Невозможно получить RigidBody в " + model.GetType());
            }
            ControllersUpdater.AddUpdate(this);
        }

        #endregion


        #region Methods

        public void FixedUpdateTick()
        {
            _movementVector *= _model.Speed;
            _movementVector.y = 0.0f;
            _rigidbody.velocity = _movementVector;
        }

        public void Move(Vector3 vector)
        {
            _movementVector = vector;
        }

        #endregion
    }
}
