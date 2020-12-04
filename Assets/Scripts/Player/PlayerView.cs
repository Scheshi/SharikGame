using UnityEngine;


namespace SharikGame
{
    internal class PlayerView : IFrameUpdatable
    {
        private PlayerController _controller;
        private Animator _animator;
        private int _forwardHash;
        private int _rightHash;

        public PlayerView(PlayerController controller, GameObject player)
        {
            _controller = controller;
            player.TryGetComponent(out _animator);
            _forwardHash = Animator.StringToHash("Forward");
            _rightHash = Animator.StringToHash("Right");
        }


        public void UpdateTick()
        {
            var vector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            _controller.Move(vector);
            _animator.SetFloat(_forwardHash, vector.z);
            _animator.SetFloat(_rightHash, vector.x);
        }

    }
}
