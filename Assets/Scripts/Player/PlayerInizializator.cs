using UnityEngine;


namespace SharikGame
{
    public class PlayerInizializator
    {
        public PlayerInizializator(PersonData playerData, Transform startPointTransform)
        {
            var player = GameObject.Instantiate(playerData.gameObject, startPointTransform.position, Quaternion.identity);
            var playerModel = new PlayerModel(playerData.PlayerStruct);
            var playerController = new PlayerController(playerModel, player);
            var playerView = new PlayerView(playerController, player);
            ServiceLocator.SetDependency(player);
            ServiceLocator.SetDependency(playerModel);
            ControllersUpdater.AddUpdate(playerView);
        }
    }
}
