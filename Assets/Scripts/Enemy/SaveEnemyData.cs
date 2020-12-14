using System;
using UnityEngine;


namespace SharikGame
{
    [Serializable]
    public class EnemySaveData : IData
    {
        public EnemyStruct EnemyStruct;
        public Vector3Serializable Position;
        public QuaternionSerializable Rotation;

        private Transform _enemyTransform;
        private EnemyModel _model;

        public EnemySaveData()
        {
            return;
        }

        public EnemySaveData(EnemyModel model, Transform transform)
        {
            _enemyTransform = transform;
            _model = model;
            EnemyStruct = new EnemyStruct()
            {
                Damage = _model.Damage,
                Speed = _model.Speed
            };
            Position = transform.position;
            Rotation = transform.rotation;

        }

        public EnemySaveData(EnemySaveData data)
        {
            Position = data.Position;
            Rotation = data.Rotation;
            EnemyStruct = data.EnemyStruct;
        }

        public void FromSave()
        {
            EnemyStruct = new EnemyStruct()
            {
                Damage = _model.Damage,
                Speed = _model.Speed
            };
            Position = _enemyTransform.position;
            Rotation = _enemyTransform.rotation;
        }

        public void FromLoad()
        {
            _model = new EnemyModel(EnemyStruct);
            _enemyTransform.position = Position;
            _enemyTransform.rotation = Rotation;
        }
    }
}
