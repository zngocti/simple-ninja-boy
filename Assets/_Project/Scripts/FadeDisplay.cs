using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeDisplay : MonoBehaviour
{
    static private FadeDisplay _instance;
    static public FadeDisplay Instance { get => _instance; }
    
    [SerializeField] Image _fadeImage;
	public event System.Action onFadeCompleted;

    private void Awake()
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

        if (!_fadeImage && !TryGetComponent<Image>(out _fadeImage))
        {
            Debug.LogError("There is no image for the Fade Display");
        }
    }

    public void PlayFullCycle(Color startColor, Color endColor, float fadeDuration, float holdDuration)
    {
        StopAllCoroutines();
        StartCoroutine(FullCycleRoutine(startColor, endColor, fadeDuration, holdDuration));
    }

    public void PlaySimpleFade(Color startColor, Color endColor, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SimpleRoutine(startColor, endColor, duration));
    }

    private IEnumerator FullCycleRoutine(Color startColor, Color endColor, float fadeDuration, float holdDuration)
    {
        yield return FadeRoutine(startColor, endColor, fadeDuration);

        if (holdDuration > 0f)
        {
            yield return new WaitForSeconds(holdDuration);
        }

        yield return FadeRoutine(endColor, startColor, fadeDuration);

        onFadeCompleted?.Invoke();
    }

    private IEnumerator SimpleRoutine(Color startColor, Color endColor, float fadeDuration)
    {
        yield return FadeRoutine(startColor, endColor, fadeDuration);
        onFadeCompleted?.Invoke();
    }

    private IEnumerator FadeRoutine(Color fromColor, Color toColor, float duration)
    {
        float elapsedTime = 0f;

        _fadeImage.color = fromColor;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            
            // Interpolación suave del color
            _fadeImage.color = Color.Lerp(fromColor, toColor, t);
            
            yield return null;
        }

        _fadeImage.color = toColor;
    }
}
