using UnityEngine;
using System;

public class PlayerController : IUpdatable, IFixedUpdatable
{
    public event Action<Vector3> Movement;
    private Rigidbody _rigidbody;
    private PlayerModel _model;
    private Vector3 _movement;


    public PlayerController()
    {
        _model = new PlayerModel();
        _model.HealthPoints = 100.0f;
        _model.Speed = 2;
    }

    public PlayerController(float speed, float health, float forceJump, GameObject player)
    {
        _model = new PlayerModel();
        _model.Speed = speed;
        _model.HealthPoints = health;
        _model.ForceJump = forceJump;
        _rigidbody = player.GetComponent<Rigidbody>();
    }

    public PlayerController(PlayerModel model, GameObject player)
    {
        _model = model;
        _rigidbody = player.GetComponent<Rigidbody>();
    }

    public void Tick()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.z = Input.GetAxis("Vertical");
        Movement?.Invoke(_movement);
    }

    public void FixedTick()
    {
        _movement.y = _rigidbody.velocity.y;
        _rigidbody.velocity = _movement * _model.Speed;
    }
}
