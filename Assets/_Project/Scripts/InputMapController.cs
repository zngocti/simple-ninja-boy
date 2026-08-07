using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMapController : MonoBehaviour
{
    static private InputMapController _instance;
    static public InputMapController Instance { get => _instance; }

    [SerializeField] PlayerInput _playerInput;
    string _lastActionMap;

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (!_playerInput && TryGetComponent<PlayerInput>(out _playerInput))
        {
            Debug.LogError("Player Input not found");
        }
    }

    public void SwitchToMap(string mapName)
    {
        _lastActionMap = _playerInput.currentActionMap.name;
        _playerInput.SwitchCurrentActionMap(mapName);
    }

    public void SwitchToPreviousMap()
    {
        _playerInput.SwitchCurrentActionMap(_lastActionMap);
    }
}
