using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] Transform _playerDestination;
    [SerializeField] Transform _cameraDestination;
    [Space(10)]
    [SerializeField] UnityEvent _onTeleport = new UnityEvent();

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = _playerDestination.position;
        Camera.main.transform.position = _cameraDestination.position;
        _onTeleport?.Invoke();
    }
}
