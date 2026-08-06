using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private const string HORIZONTAL_ANIMATION_NAME = "Horizontal";
    private const string VERTICAL_ANIMATION_NAME = "Vertical"; 
    private const string LAST_HORIZONTAL_ANIMATION_NAME = "LastHorizontal";
    private const string LAST_VERTICAL_ANIMATION_NAME = "LastVertical"; 

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;

    [Header("Parameters")]
    [SerializeField] private float _moveSpeed = 5f;

    private Vector2 _moveInput;

    void Awake()
    {
        if (!_rigidbody && !TryGetComponent(out _rigidbody))
        {
            Debug.LogError("Missing Rigidbody on " + name);            
        }

        if (!_animator && !TryGetComponent(out _animator))
        {
            Debug.LogError("Missing Animator on " + name);
        }
    }

    private void Update()
    {
        DoAnimations();
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_moveInput.x * _moveSpeed, _moveInput.y * _moveSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void DoAnimations()
    {
        _animator.SetFloat(HORIZONTAL_ANIMATION_NAME, _moveInput.x);
        _animator.SetFloat(VERTICAL_ANIMATION_NAME, _moveInput.y);

        if (_moveInput != Vector2.zero)
        {
            _animator.SetFloat(LAST_HORIZONTAL_ANIMATION_NAME, _moveInput.x);
            _animator.SetFloat(LAST_VERTICAL_ANIMATION_NAME, _moveInput.y);
        }
    }
}
