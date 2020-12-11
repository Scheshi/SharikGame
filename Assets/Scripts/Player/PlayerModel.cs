using System;


namespace SharikGame
{
    internal class PlayerModel : IModel
    {
        private PlayerStruct _playerStruct;

        public PlayerModel(PlayerStruct @struct)
        {
            if (@struct.Speed <= 0 || @struct.LifeCount <= 0) 
                throw new ArgumentException("Неверные значения в структуре игрока");

            _playerStruct = @struct;
        }

        public float Speed => _playerStruct.Speed;

        public int LifeCount => _playerStruct.LifeCount;

        public PlayerStruct Struct => _playerStruct;

        /// <summary>
        /// Урон игрока
        /// </summary>
        /// <param name="count">Сколько снять жизней</param>
        public void PlayerDamage(int count)
        {
            _playerStruct.LifeCount -= count;

            if(_playerStruct.LifeCount <= 0)
            {
                ServiceLocator.GetDependency<GameOverChecker>().GameEnd(true, false);
            }
        }


        /// <summary>
        /// Изменение скорости игрока
        /// </summary>
        /// <param name="count">Сколько добавить к скорости</param>
        public void PlusingSpeed(float count)
        {
            _playerStruct.Speed += count;
        }
    }
}
