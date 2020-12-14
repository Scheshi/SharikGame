using UnityEngine;


namespace SharikGame
{
    public class PlayerInizializator
    {

        private PlayerStruct _struct;
        private GameObject _gameObject;
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        public PlayerInizializator(PersonData playerData, Transform startPointTransform)
        {
            var resource = Resources.Load("Prefabs/Player");
            _gameObject = (GameObject)resource;
            _struct = playerData.PlayerStruct;
            _startPosition = startPointTransform.position;
            _startRotation = Quaternion.identity;
            Initialize();
        }

        public PlayerInizializator(PlayerStruct str, Vector3 position, Quaternion rotation)
        {
            var resource = Resources.Load<GameObject>("Prefabs/Player");
            _gameObject = resource;
            _struct = str;
            _startPosition = position;
            _startRotation = rotation;
            Initialize();
        }


        public void Initialize()
        {
            if (ServiceLocator.IsHas<GameObject>())
            {
                ServiceLocator.RemoveDependency<PlayerModel>();
                ServiceLocator.RemoveDependency<GameObject>();
            }
            var player = GameObject.Instantiate(_gameObject, _startPosition, _startRotation);
            var playerModel = new PlayerModel(_struct);
            var playerController = new PlayerController(playerModel, player);
            var playerView = new PlayerView(playerController, player);
            ServiceLocator.GetDependency<Repository>().
                AddDataToList(new PlayerSaveData(playerModel, player.transform));
            //Repository.AddDataToList(new PlayerSaveData(playerModel, player.transform));
            ServiceLocator.SetDependency(player);
            ServiceLocator.SetDependency(playerModel);
            if (!ServiceLocator.IsHas<PlayerView>())
            {
                ServiceLocator.SetDependency(playerView);
                ControllersUpdater.AddUpdate(playerView);
            }
            if (ServiceLocator.IsHas<CameraController>())
            {
                var oldCamera = ServiceLocator.GetDependency<CameraController>();
                ControllersUpdater.RemoveUpdate(oldCamera);
                ServiceLocator.RemoveDependency<CameraController>();
                
            }
            var camera = new CameraController(ServiceLocator.GetDependency<GameObject>().transform);
            ControllersUpdater.AddUpdate(camera);
            ServiceLocator.SetDependency(camera);
        }
    }
}
