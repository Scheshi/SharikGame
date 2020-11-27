using UnityEngine;

public class CameraController : IUpdatable
{

    private Camera _main;
    private Transform _player;
    private Vector3 _position;


    public CameraController(Transform playerTransform, Vector3 cameraLocalPosition)
    {
        _player = playerTransform;
        _position = cameraLocalPosition;
        _main = Camera.main;
    }

    public void Tick()
    {
        _main.transform.position = _player.position + _position;
        _main.transform.LookAt(_player);
    }
}
