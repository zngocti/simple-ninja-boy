using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueComponent : MonoBehaviour
{
    [SerializeField] string[] _dialogueText = new string[0];
    [Space(10)]
    [SerializeField] UnityEvent _onDialogueStart = new UnityEvent();
    [SerializeField] UnityEvent _onDialogueEnd = new UnityEvent();
    
    public void StartDialogue()
    {
        DialogueDisplay.Instance.SetTextToWrite(_dialogueText);
        DialogueDisplay.Instance.StartTypewriter();
        DialogueDisplay.Instance.onWritterCompleted += EndDialogue;
        _onDialogueStart?.Invoke();
    }

    public void EndDialogue()
    {
        DialogueDisplay.Instance.onWritterCompleted -= EndDialogue;
        _onDialogueEnd?.Invoke();
    }

    public void ForceSkip()
    {
        DialogueDisplay.Instance.SkipOrContinue();
    }

    public void OnSkipByInput(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        ForceSkip();
    }
}
