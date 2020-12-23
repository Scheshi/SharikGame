using UnityEngine;


namespace SharikGame
{
    public class PlayerInizializator
    {
        #region Fields

        private PlayerStruct _struct;
        private GameObject _gameObject;
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private string _playerName;

        #endregion


        #region Contructors

        public PlayerInizializator(PersonData playerData, Transform startPointTransform, string playerName)
        {
            _playerName = playerName;
            _gameObject = Resources.Load<GameObject>("Prefabs/Player");
            _struct = playerData.PlayerStruct;
            _startPosition = startPointTransform.position;
            _startRotation = Quaternion.identity;
            Initialize();
        }

        #endregion


        #region Methods

        public void Initialize()
        {
            var player = GameObject.Instantiate(_gameObject, _startPosition, _startRotation);
            var playerModel = new PlayerModel(_struct);
            var playerController = new PlayerController(playerModel, player);
            var playerView = new PlayerView(playerController, player);
            ServiceLocator.SetDependency(player);
            ServiceLocator.SetDependency(playerModel);
            ControllersUpdater.AddUpdate(playerView);
            var camera = new CameraController(ServiceLocator.GetDependency<GameObject>().transform);
            ControllersUpdater.AddUpdate(camera);
            ServiceLocator.SetDependency(camera);
            ServiceLocator.GetDependency<Repository>().AddDataToList(new PlayerSaveData(_playerName ,playerModel, player.transform));
            var playerSprite = Resources.Load<GameObject>("Textures/PlayerRadar");
            ServiceLocator.GetDependency<RadarController>().AddingObject(player, playerSprite);
        }

        #endregion
    }
}
