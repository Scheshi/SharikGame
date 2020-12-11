using System;
using UnityEngine;

namespace SharikGame {
    [Serializable]
    public class PlayerSaveData : IData
    {
        public Vector3Serializable _position;
        public QuaternionSerializable _rotation;
        public PlayerStruct _str;

        public PlayerSaveData(PlayerStruct str, Vector3Serializable position, QuaternionSerializable rotation)
        {
            _position = position;
            _rotation = rotation;
            _str = str;
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
