using UnityEngine;
using System;

public class PlayerView : MonoBehaviour, IUpdatable, IFixedUpdatable
{
    [SerializeField] private float _speed = 0.3f;
    [SerializeField] private float _health = 100.0f;
    [SerializeField] private float _forceJump = 2.0f;
    private PlayerController _controller;
    private Animator _animator;
    private bool _isGround;


    private void Awake()
    {
        _controller = new PlayerController(_speed, _health, _forceJump, gameObject);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controller.Movement += AnimationMove;
    }

    private void AnimationMove(Vector3 vector)
    {
        _animator.SetFloat("Forward", vector.z);
        _animator.SetFloat("Right", vector.x);
    }

    public void Tick()
    {
        _controller.Tick();
    }

    public void FixedTick()
    {
        _controller.FixedTick();
    }
}
