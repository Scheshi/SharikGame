using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraLocalPositionFromPlayer;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _startPoint;
    private List<IUpdatable> _updatables = new List<IUpdatable>();
    private List<IFixedUpdatable> _fixedUpdatables = new List<IFixedUpdatable>();
    private Transform _player;
    

    private void Start()
    {
        var player = Instantiate(_playerPrefab, _startPoint.position, Quaternion.identity);
        var playerView = player.GetComponent<PlayerView>();
        _player = player.transform;
        _updatables.Add(playerView);
        _fixedUpdatables.Add(playerView);
        _updatables.Add(new CameraController(_player, _cameraLocalPositionFromPlayer));
    }


    private void Update()
    {
        foreach(var updatable in _updatables)
        {
            updatable.Tick();
        }
    }

    private void FixedUpdate()
    {
        foreach(var fixedUpdateble in _fixedUpdatables)
        {
            fixedUpdateble.FixedTick();
        }
    }
}
