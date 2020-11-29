using System;
using UnityEngine;


namespace SharikGame {
    public abstract class Bonus : MonoBehaviour, IDisposable
    {
        protected IView _slider;
        protected IView _text;

        public void Dispose()
        {
            Destroy(gameObject);
        }

        protected abstract void Interaction(PlayerView playerView);

        public void Initialize(IView slider, IView text)
        {
            _slider = slider;
            _text = text;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var playerView = collision.gameObject.GetComponent<PlayerView>();
            if (playerView != null)
            {
                Interaction(playerView);
                Dispose();
            }
        }

    }
}
