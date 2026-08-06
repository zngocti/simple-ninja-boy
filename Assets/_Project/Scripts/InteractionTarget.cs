using UnityEngine;
using UnityEngine.Events;

public class InteractionTarget : MonoBehaviour
{
    bool _isInteracting = false;

    [SerializeField] UnityEvent _onInteractionStart = new UnityEvent();
    [SerializeField] UnityEvent _onInteractionEnd = new UnityEvent();

    public void StartInteraction()
    {
        if (_isInteracting)
        {
            return;
        }

        _isInteracting = true;

        _onInteractionStart?.Invoke();
    }

    public void EndInteraction()
    {
        if (!_isInteracting)
        {
            return;
        }

        _isInteracting = false;

        _onInteractionEnd?.Invoke();
    }
}
