using System;
using UnityEngine;


namespace SharikGame
{
    [Serializable]
    public struct ObjectSerialzable
    {
        public GameObject gameObject
        {
            get
            {
                return _gameObject;
            }
            set
            {
                _gameObject = value;
            }
        }
        public string FileName;
        public string ParentName;
        public Vector3Serializable Position;
        public QuaternionSerializable Rotation;
        public Vector3Serializable Scale;
        private GameObject _gameObject;
    }
}
