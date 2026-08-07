using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputFilter : MonoBehaviour
{
	[SerializeField] UnityEvent _onInputStarted = new UnityEvent();

    public void OnInputStarted(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        _onInputStarted?.Invoke();
    }
}
