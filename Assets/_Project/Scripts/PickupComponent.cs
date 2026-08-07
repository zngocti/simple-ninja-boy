using UnityEngine;
using UnityEngine.Events;

public class PickupComponent : MonoBehaviour
{
    [SerializeField] ItemSO _item;
    [SerializeField] int _amount = 1;

    [Space(10)]
    [SerializeField] UnityEvent _onPickupDone = new UnityEvent();
    [SerializeField] UnityEvent _onPickupFailed = new UnityEvent();

    public void TryToPickup()
    {
        if (InventoryController.Instance.TryPickupItem(_item, _amount))
        {
            _onPickupDone?.Invoke();
        }
        else
        {
            _onPickupFailed?.Invoke();
        }
    }
}
