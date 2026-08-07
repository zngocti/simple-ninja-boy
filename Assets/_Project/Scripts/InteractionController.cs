using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _interactionDistance = 1f;
    [SerializeField] private LayerMask _layerToIgnore;

    Vector2 _pointingDirection = Vector2.down;
    Vector2 _raycastDirection;

    public void OnChangeDirection(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>() == Vector2.zero)
        {
            if (_pointingDirection == Vector2.zero)
            {
                return;
            }

            // When the character stops moving we use the last pointing direction to define the raycast target 
            DefineRaycastDirection();
        }

        _pointingDirection = context.ReadValue<Vector2>();    
    }

    void DefineRaycastDirection()
    {
        if (Mathf.Abs(_pointingDirection.x) > Mathf.Abs(_pointingDirection.y))
        {
            if (_pointingDirection.x > 0)
            {
                _raycastDirection = Vector2.right;
            }
            else
            {
                _raycastDirection = Vector2.left;
            }
        }
        else
        {
            if (_pointingDirection.y > 0)
            {
                _raycastDirection = Vector2.up;;
            }
            else
            {
                _raycastDirection = Vector2.down;
            }
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (!context.started || _raycastDirection == Vector2.zero)
        {
            return;
        }

        int layersToDetect = ~_layerToIgnore.value;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _raycastDirection, _interactionDistance, layersToDetect); 

        if (hit.collider != null)
        {
            InteractionTarget target;

            if (hit.collider.TryGetComponent<InteractionTarget>(out target))
            {
                target.StartInteraction();
            }            
        }
    }
}
