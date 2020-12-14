using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SharikGame {
    public class EnemySpawner : IFrameUpdatable
    {
        private List<Transform> _pointsForSpawn = new List<Transform>();
        private EnemyData _data;
        private float _radius = 5.0f;

        public EnemySpawner(Transform[] transforms, EnemyData data)
        {
            _pointsForSpawn.AddRange(transforms);
            _data = data;
            ControllersUpdater.AddUpdate(this);
        }

        public void UpdateTick()
        {
            for (int i = 0; i < _pointsForSpawn.Count; i++)
            {
                Collider[] hits = Physics.OverlapSphere(_pointsForSpawn[i].position, _radius);
                if(hits.Length > 0)
                {
                    foreach (var hit in hits)
                    {
                        if (hit.gameObject == ServiceLocator.GetDependency<GameObject>())
                        {
                            var enemy = GameObject.Instantiate(_data.gameObject, _pointsForSpawn[i].position, Quaternion.identity);
                            var model = new EnemyModel(_data.EnemyStruct);
                            var controller = new EnemyController(model, enemy);
                            ServiceLocator.GetDependency<Repository>().AddDataToList(new EnemySaveData(model, enemy.transform));
                            var point = _pointsForSpawn[i];
                            _pointsForSpawn.Remove(point);
                            GameObject.Destroy(point.gameObject);
                        }
                    }
                }

            }
        }
    }
}
