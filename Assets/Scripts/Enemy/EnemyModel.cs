using System;


namespace SharikGame {
    public class EnemyModel : IModel
    {
        private EnemyStruct _enemyStruct;

        public EnemyModel(EnemyStruct @struct)
        {
            if (@struct.Speed <= 0 || @struct.Damage <= 0)
                throw new ArgumentException("Неверные значения в структуре игрока");

            _enemyStruct = @struct;
        }

        public int Damage
        {
            get
            {
                return _enemyStruct.Damage;
            }
        }

        public float Speed
        {
            get
            {
                return _enemyStruct.Speed;
            }
        }
    }
}
