using UnityEngine;
using System;


namespace SharikGame
{
    public abstract class InteractableObjectsController : IFrameUpdatable, IDisposable, IInteractable
    {
        private GameObject _gameObject;
        private float _radius = 1.2f;

        public InteractableObjectsController(GameObject gameObject)
        {
            _gameObject = gameObject;
            ControllersUpdater.AddUpdate(this);
        }

        public void Dispose()
        {
            ControllersUpdater.RemoveUpdate(this);
            GameObject.Destroy(_gameObject);
        }

        public abstract void Interact();

        public void UpdateTick()
        {
            TriggerCheck();
        }

        public void TriggerCheck()
        {
            Collider[] hits = Physics.OverlapSphere(_gameObject.transform.position, _radius);
            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.gameObject == ServiceLocator.GetDependency<GameObject>())
                    {
                        
                        Interact();
                        Dispose();
                        break;
                    }
                }
            }
        }

    }
}
