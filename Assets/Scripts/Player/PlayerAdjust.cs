using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SharikGame {
    public static class PlayerAdjust
    {
        private static PlayerController _playerController;
        public static void Initialize(PlayerController controller)
        {
            _playerController = controller;
        }

        public static void Adjust(PlayerModel model)
        {
            _playerController._model.HealthPoints += model.HealthPoints;
            _playerController._model.Speed += model.Speed;
        }
    }
}
