using UnityEngine;
using System;


namespace SharikGame
{
    public abstract class InteractableObjectsController : IFrameUpdatable, IDisposable, IInteractable, IData
    {
        private int _objectID;
        private GameObject _gameObject;
        private float _radius = 1.2f;
        public bool IsActive;

        public string ObjectID
        {
            get
            {
                return _objectID.ToString();
            }
            set
            {
                return;
            }
        }

        public InteractableObjectsController()
        {
            return;
        }

        public InteractableObjectsController(GameObject gameObject, int objectID)
        {
            _objectID = objectID;
            _gameObject = gameObject;
            ControllersUpdater.AddUpdate(this);
            IsActive = true;
        }

        public void Dispose()
        {
            ControllersUpdater.RemoveUpdate(this);
            _gameObject.SetActive(false);
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
                        IsActive = false;
                        break;
                    }
                }
            }
        }

        public void FromSave()
        {
            
        }

        public void FromLoad()
        {
            Debug.Log(ObjectID + IsActive);
            if (IsActive)
            {
                _gameObject.SetActive(true);
                ControllersUpdater.AddUpdate(this);
            }
        }
    }
}
