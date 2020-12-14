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
            _gameObject = Resources.Load<GameObject>("Prefabs/Player");
            _struct = playerData.PlayerStruct;
            _startPosition = startPointTransform.position;
            _startRotation = Quaternion.identity;
            Initialize();
        }

        public PlayerInizializator(PlayerSaveData data)
        {
            if (ServiceLocator.IsHas<PlayerSaveData>())
            {
                ServiceLocator.RemoveDependency<PlayerSaveData>();
                ServiceLocator.SetDependency(data);
            }
                _gameObject = Resources.Load<GameObject>("Prefabs/Player");
            _struct = data.PlayerStruct;
            _startPosition = data.Position;
            _startRotation = data.Rotation;
            Initialize();
        }


        public void Initialize()
        {
            if (ServiceLocator.IsHas<CameraController>())
            {
                var oldCamera = ServiceLocator.GetDependency<CameraController>();
                ControllersUpdater.RemoveUpdate(oldCamera);
                ServiceLocator.RemoveDependency<CameraController>();

            }
            if (ServiceLocator.IsHas<GameObject>())
            {
                var oldPlayer = ServiceLocator.GetDependency<GameObject>();
                ServiceLocator.RemoveDependency<PlayerModel>();
                ServiceLocator.RemoveDependency<GameObject>();
                ServiceLocator.RemoveDependency<PlayerView>();
                GameObject.Destroy(oldPlayer);
            }
            var player = GameObject.Instantiate(_gameObject, _startPosition, _startRotation);
            var playerModel = new PlayerModel(_struct);
            var playerController = new PlayerController(playerModel, player);
            var playerView = new PlayerView(playerController, player);
            if (!ServiceLocator.IsHas<PlayerSaveData>())
            {
                var saveData = new PlayerSaveData(playerModel, player.transform);
                ServiceLocator.SetDependency(saveData);
                ServiceLocator.GetDependency<Repository>().
                AddDataToList(saveData);
            }
            //Repository.AddDataToList(new PlayerSaveData(playerModel, player.transform));
            ServiceLocator.SetDependency(player);
            ServiceLocator.SetDependency(playerModel);
            if (!ServiceLocator.IsHas<PlayerView>())
            {
                ServiceLocator.SetDependency(playerView);
                ControllersUpdater.AddUpdate(playerView);
            }
            var camera = new CameraController(ServiceLocator.GetDependency<GameObject>().transform);
            ControllersUpdater.AddUpdate(camera);
            ServiceLocator.SetDependency(camera);
        }
    }
}
