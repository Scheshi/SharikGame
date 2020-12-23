using System;
using UnityEngine;

namespace SharikGame {
    [Serializable]
    public class PlayerSaveData : IData
    {
        public string UserName;
        public Vector3Serializable Position;
        public QuaternionSerializable Rotation;
        public PlayerStruct PlayerStruct;

        [NonSerialized]private Transform _playerTransform;
        [NonSerialized]private PlayerModel _model;

        public PlayerSaveData()
        {
            return;
        }

        public PlayerSaveData(string name, PlayerModel model, Transform transform)
        {
            UserName = name;
            _playerTransform = transform;
            _model = model;
            PlayerStruct = new PlayerStruct()
            {
                LifeCount = _model.LifeCount,
                Speed = _model.Speed
            };
            Position = transform.position;
            Rotation = transform.rotation;

        }

        public PlayerSaveData(PlayerSaveData data)
        {
            Position = data.Position;
            Rotation = data.Rotation;
            PlayerStruct = data.PlayerStruct;
        }


        public void FromSave()
        {
            PlayerStruct = new PlayerStruct()
            {
                LifeCount = _model.LifeCount,
                Speed = _model.Speed
            };
            Position = _playerTransform.position;
            Rotation = _playerTransform.rotation;
        }

        public void FromLoad()
        {
            _model = new PlayerModel(PlayerStruct);
            _playerTransform.position = Position;
            _playerTransform.rotation = Rotation;
        }
    }


    [Serializable]
    public struct Vector3Serializable
    {
        public float x;
        public float y;
        public float z;

        private Vector3Serializable(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public static implicit operator Vector3(Vector3Serializable vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }

        public static implicit operator Vector3Serializable(Vector3 vector)
        {
            return new Vector3Serializable(vector);
        }
    }

    [Serializable]
    public struct QuaternionSerializable
    {
        public float x;
        public float y;
        public float z;
        public float w;

        private QuaternionSerializable(Quaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }

        public static implicit operator Quaternion(QuaternionSerializable quaternion)
        {
            return new Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }

        public static implicit operator QuaternionSerializable(Quaternion quaternion)
        {
            return new QuaternionSerializable(quaternion);
        }
    }
}
