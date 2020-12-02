using System.Collections;
using UnityEngine;


namespace SharikGame
{
    public class CameraController : IUpdatable
    {
        #region Fields

        private Camera _main;
        private Transform _player;
        private Vector3 _position;
        private Quaternion _originalRotation;
        private float _shakePower = 2.0f;

        #endregion


        #region Contructors

        public CameraController(Transform playerTransform, Vector3 cameraLocalPosition)
        {
            _player = playerTransform;
            _position = cameraLocalPosition;
            _main = Camera.main;
            _originalRotation = _main.transform.rotation;
        }

        #endregion


        #region Methods

        public void Tick()
        {
            _main.transform.position = _player.position + _position;
            _main.transform.LookAt(_player);
        }

        #endregion
    }
}
