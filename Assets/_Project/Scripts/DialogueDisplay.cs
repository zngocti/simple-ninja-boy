using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueDisplay : MonoBehaviour
{
    static private DialogueDisplay _instance;
    static public DialogueDisplay Instance { get => _instance; }

    [SerializeField] TMP_Text _tmpText;
    [SerializeField] GameObject _textContainer;
    [Header("Typewritter settings")]
	[SerializeField] float _delayBeforeStart = 0f;
	[SerializeField] float _timeBtwChars = 0.1f;
	[SerializeField] string _leadingChar = string.Empty;
	[SerializeField] bool _leadingCharBeforeDelay = false;
    [SerializeField] bool _autoContinue = false;
    [Space(10)]
	[SerializeField] UnityEvent _onWritterCompleted;
	public event System.Action onWritterCompleted;

	string[] _writer;
    int _currentText = 0;

	WaitForSeconds _waitBeforeStart;
	WaitForSeconds _waitBtwChars;

	bool _writing = false;
	bool _skip = false;
    bool _waitingForContinue = false;

	public bool Writting { get => _writing; }

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

		_waitBeforeStart = new WaitForSeconds(_delayBeforeStart);
		_waitBtwChars = new WaitForSeconds(_timeBtwChars);
    }

	public void SetTextToWrite(string[] text, int currentText = 0)
    {
		_writer = text;
        _currentText = currentText;
    }

	public void SetTargetTMP(TMP_Text target)
    {
		_tmpText = target;
    }

	public void StartTypewriter()
	{
        if (!StopAndEraseText())
        {
			return;
        }

        _textContainer.SetActive(true);
		StartCoroutine(nameof(TypeWriterTMP));
	}

	public bool StopAndEraseText()
    {
        if (_tmpText == null)
        {
            Debug.LogError("No tmp found to use");
			return false;
        }

		StopAllCoroutines();

		_writing = false;
		_tmpText.text = string.Empty;

        if (_writer.Length < 1)
        {
            Debug.LogWarning("No text to write");
            return false;
        }

		return true;
	}

	private void OnDisable()
	{
		StopAllCoroutines();
        RemoveListeners();
		_writing = false;
		_skip = false;
	}

	IEnumerator TypeWriterTMP()
    {
		_writing = true;
        _waitingForContinue = false;

	    _tmpText.text = _leadingCharBeforeDelay ? _leadingChar : string.Empty;

        yield return _waitBeforeStart;

		foreach (char c in _writer[_currentText])
		{
            if (_skip)
            {
				_tmpText.text = _writer[_currentText];
				_tmpText.text += _leadingChar;
				break;
			}

			if (_tmpText.text.Length > 0)
			{
				_tmpText.text = _tmpText.text.Substring(0, _tmpText.text.Length - _leadingChar.Length);
			}
			_tmpText.text += c;
			_tmpText.text += _leadingChar;
			yield return _waitBtwChars;
		}

		if (!string.IsNullOrEmpty(_leadingChar))
		{
			_tmpText.text = _tmpText.text.Substring(0, _tmpText.text.Length - _leadingChar.Length);
		}

		_writing = false;
		_skip = false;
        _waitingForContinue = true;

        if (_autoContinue)
        {
            yield return _waitBeforeStart;
            CompleteWrittingPhase();   
        }
	}

    void CompleteWrittingPhase()
    {
        StopAllCoroutines();
        _currentText++;
        
        if (_currentText >= _writer.Length)
        {
            _textContainer.SetActive(false);
            onWritterCompleted?.Invoke();
		    _onWritterCompleted.Invoke();   
        }
        else
        {
            StartTypewriter();
        }        
    }

	public void RemoveListeners()
    {
        if (onWritterCompleted == null)
        {
			return;
        }

        foreach (var item in onWritterCompleted.GetInvocationList())
        {
			onWritterCompleted -= (item as System.Action);
		}
    }

	public void SkipOrContinue()
    {
        if (_writing)
        {
			_skip = true;
        }
        else if (_waitingForContinue)
        {
            CompleteWrittingPhase();
        }
    }
}
