using UnityEngine;


namespace SharikGame
{
    public class EnemyController : ControllerFabric, IFrameUpdatable
    {
        private Transform _transform;
        private EnemyModel _model;
        private float _radiusForCheck = 5.0f;
        private bool _isActive;

        public EnemyController(EnemyModel model, GameObject go) : base(model, go)
        {
            ControllersUpdater.AddUpdate(this);
            _transform = go.transform;
            _model = model;
        }

        public void UpdateTick()
        {
            Collider[] hits = Physics.OverlapSphere(_transform.position, _radiusForCheck);
            if(hits.Length > 0)
            {
                foreach(var hit in hits)
                {
                    if(hit.gameObject == ServiceLocator.GetDependency<GameObject>())
                    {
                        if ((hit.transform.position - _transform.position).sqrMagnitude <= 1.5f) 
                            ServiceLocator.GetDependency<PlayerModel>().PlayerDamage(_model.Damage);

                        Move((hit.transform.position - _transform.position).normalized);
                    }
                }
            }
        }
    }
}
