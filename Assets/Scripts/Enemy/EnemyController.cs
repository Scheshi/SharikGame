﻿using UnityEngine;


namespace SharikGame
{
    public class EnemyController : ControllerFabric, IFrameUpdatable
    {
        private Transform _transform;
        private EnemyModel _model;
        private float _radiusForCheck = 5.0f;

        public EnemyController(IModel model, GameObject go) : base(model, go)
        {
            ControllersUpdater.AddUpdate(this);
            _transform = go.transform;
            _model = model as EnemyModel;
        }

        public void UpdateTick()
        {
            Collider[] hits = Physics.OverlapSphere(_transform.position, _radiusForCheck);
            if(hits.Length > 0)
            {
                foreach(var hit in hits)
                {
                    if(hit.gameObject == ServiceLocator.GetDepencity<GameObject>())
                    {
                        if ((hit.transform.position - _transform.position).sqrMagnitude <= 1.5f) 
                            ServiceLocator.GetDepencity<PlayerModel>().Adjust(_model.Damage);

                        Move((hit.transform.position - _transform.position).normalized);
                    }
                }
            }
        }
    }
}
