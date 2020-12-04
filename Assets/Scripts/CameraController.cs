using UnityEngine;


namespace SharikGame
{
    public class CameraController : ILateUpdatable
    {
        private Transform _playerTransform;
        private Vector3 _offset;
        private Camera _main;
        public CameraController(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            _main = Camera.main;
            _offset = _main.transform.position - _playerTransform.position;
        }

        public void LateUpdateTick()
        {
            _main.transform.position = _playerTransform.position + _offset;
            _main.transform.LookAt(_playerTransform.position);
        }
    }
}
