using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

namespace SharikGame
{
    public class RadarController : IFrameUpdatable
    {
        #region Fields

        private List<RadarObject> _object = new List<RadarObject>();
        private readonly float _mapScale = 2.0f;
        private Transform _mapTransform;
        private Transform _main;

        #endregion


        #region Contructors

        public RadarController(Transform mapTransform)
        {
            _main = Camera.main.transform;
            _mapTransform = mapTransform;
        }
        #endregion


        #region Methods

        public void AddingObject(GameObject obj, GameObject image)
        {
            var trueImage = GameObject.Instantiate(image);


            _object.Add(new RadarObject { GameObject = obj, Image = trueImage });
        }

        public void RemoveObject(GameObject obj)
        {
            RadarObject removingObject = _object.FirstOrDefault(x => x.GameObject == obj);
            _object.Remove(removingObject);
            GameObject.Destroy(removingObject.Image);
        }

        public void UpdateObjects()
        {
            //Гнустно краду код, ибо в математике 0 без палочки
            for(int i = 0; i<_object.Count; i++)
            {
                Vector3 radarPos = (_object[i].GameObject.transform.position -
                    _main.position);
                float distToObject = Vector3.Distance(_main.position,
                    _object[i].GameObject.transform.position) * _mapScale;
                float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg -
                    270 - _main.eulerAngles.y;
                radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
                radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
                _object[i].Image.transform.parent = _mapTransform;
                _object[i].Image.transform.position = new Vector3(radarPos.x,
                    radarPos.z, 0) + _mapTransform.position;
                
            }
        }

        public void UpdateTick()
        {
            UpdateObjects();
        }

        #endregion
    }
}
