using System;


namespace SharikGame {
    public static class PlayerAdjust
    {
        public static event Action Death;
        private static PlayerController _playerController;
        public static void Initialize(PlayerController controller)
        {
            _playerController = controller;
        }

        public static void Adjust(PlayerModel model)
        {
            _playerController._model.HealthPoints += model.HealthPoints;
            _playerController._model.Speed += model.Speed;

            if(_playerController._model.HealthPoints <= 0)
            {
                GameOverChecker.GameOver(true);
            }
        }
    }
}
