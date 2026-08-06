using UnityEngine;
using UnityEngine.Events;

public class FadeComponent : MonoBehaviour
{
    [SerializeField] Color _startingColor = new Color(0,0,0,0);
    [SerializeField] Color _endingColor = Color.black;
    [SerializeField] float _timeToFade = 1f;
    [Tooltip("If you want to do a complete cycle: starting color to ending color, wait and the ending color to starting color")]
    [SerializeField] bool _fullFade = false;
    [Tooltip("The time to wait if you are using the full fade. This is the time between the fades, not the time for the fade to hapen")]
    [SerializeField] float _timeToWait = 2f;
    [Space(10)]
    [SerializeField] UnityEvent _onEndFade = new UnityEvent();

    public void StartFade()
    {
        if (_fullFade)
        {
            FadeDisplay.Instance?.PlayFullCycle(_startingColor,_endingColor, _timeToFade, _timeToWait);
        }
        else
        {
            FadeDisplay.Instance?.PlaySimpleFade(_startingColor,_endingColor, _timeToFade);
        }

        FadeDisplay.Instance.onFadeCompleted += OnEndFade;
    }

    void OnEndFade()
    {
        FadeDisplay.Instance.onFadeCompleted -= OnEndFade;
        _onEndFade?.Invoke();
    }
}
