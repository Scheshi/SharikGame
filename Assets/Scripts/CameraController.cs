using UnityEngine;


namespace SharikGame
{
    public class CameraController : ILateUpdatable, IData
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

        public CameraController(Transform playerTransform, Vector3 vector)
        {
            _playerTransform = playerTransform;
            _main = Camera.main;
            _offset = vector;
        }

        public void FromLoad()
        {
            ControllersUpdater.RemoveUpdate(this);
        }

        public void FromSave()
        {
            throw new System.NotImplementedException();
        }

        public void LateUpdateTick()
        {
            _main.transform.position = _playerTransform.position + _offset;
            _main.transform.LookAt(_playerTransform.position);
        }
    }
}
