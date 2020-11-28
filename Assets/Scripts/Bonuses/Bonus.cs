using System;
using UnityEngine;


namespace SharikGame {
    public abstract class Bonus : MonoBehaviour, IDisposable
    {
        [SerializeField]private float _radius = 5.0f;
        protected IView _slider;
        protected IView _text;

        public void Dispose()
        {
            Destroy(gameObject);
        }

        protected abstract void Interaction();

        public void Initialize(IView slider, IView text)
        {
            _slider = slider;
            _text = text;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.GetComponent<PlayerView>() != null)
            {
                Interaction();
                Dispose();
            }
        }

    }
}
