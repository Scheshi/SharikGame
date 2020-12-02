using System;


namespace SharikGame
{
    public class EnemyModel
    {
        private EnemyStruct _struct;
        public EnemyModel(EnemyStruct str)
        {
            if(str.Damage < 0 || str.Speed <= 0)
            {
                throw new ArgumentException("Неверные значения в данных противника");
            }
            else
            {
                _struct = str;
            }
        }

        public int Damage
        {
            get
            {
                return _struct.Damage;
            }
        }

        public float Speed
        {
            get
            {
                return _struct.Speed;
            }
        }

    }
}
