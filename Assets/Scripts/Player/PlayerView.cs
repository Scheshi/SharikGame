using UnityEngine;
using System;

public class PlayerView : MonoBehaviour, IUpdatable, IFixedUpdatable
{
    [SerializeField] private PlayerModel _model;
    private PlayerController _controller;
    private Animator _animator;


    private void Awake()
    {
        if(_model.HealthPoints > 0)
        {
            _controller = new PlayerController(_model, gameObject);
        }
        else
        {
            _controller = new PlayerController();
        }
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
