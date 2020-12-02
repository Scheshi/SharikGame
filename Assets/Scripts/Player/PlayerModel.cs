using System;
using UnityEngine;

namespace SharikGame {
    public class PlayerModel
    {
        private PlayerStruct _struct;

        public PlayerModel(PlayerStruct str)
        {
            if (str.LifeCount <= 0 || str.Speed <= 0 || str.ForceJump < 0)
            {
                throw new ArgumentException("Неверные значения в данных игрока");
            }
            else _struct = str;
        }

        public int LifeCount
        {
            get
            {
                return _struct.LifeCount;
            }
        }

        public float Speed
        {
            get
            {
                return _struct.Speed;
            }
        }

        public float ForceJump
        {
            get
            {
                return _struct.ForceJump;
            }
        }

        /// <summary>
        /// Прибавляет переданную структуру к имеющейся
        /// </summary>
        /// <param name="plusingStruct">Структура для добавления к имеющейся</param>
        public void Adjust(PlayerStruct plusingStruct)
        {
            _struct = new PlayerStruct
            {
                LifeCount = _struct.LifeCount + plusingStruct.LifeCount,
                Speed = _struct.Speed + plusingStruct.Speed,
                ForceJump = _struct.ForceJump + plusingStruct.ForceJump
            };
            Debug.Log(LifeCount);
            if(LifeCount <= 0)
            {
                GameOverChecker.GameOver(true);
            }
        }
    }
}
