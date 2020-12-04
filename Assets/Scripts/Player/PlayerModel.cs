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

        public float Speed
        {
            get
            {
                return _playerStruct.Speed;
            }
        }

        public int LifeCount
        {
            get
            {
                return _playerStruct.LifeCount;
            }
        }


        /// <summary>
        /// Урон игрока
        /// </summary>
        /// <param name="count">Сколько снять жизней</param>
        public void Adjust(int count)
        {
            _playerStruct.LifeCount -= count;

            if(_playerStruct.LifeCount <= 0)
            {
                ServiceLocator.GetDepencity<GameOverChecker>().GameEnd(true, false);
            }
        }


        /// <summary>
        /// Изменение скорости игрока
        /// </summary>
        /// <param name="count">Сколько добавить к скорости</param>
        public void ChangeSpeed(float count)
        {
            _playerStruct.Speed += count;
        }
    }
}
