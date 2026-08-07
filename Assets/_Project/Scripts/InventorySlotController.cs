using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InventorySlotController : MonoBehaviour
{
    [Tooltip("If you set this to false you will have to place items manually in this slot")]
    [SerializeField] bool _autoAcceptItems = true;
    [SerializeField] InventorySlotDisplay _display;
    [Tooltip("Leave it blank to allow every item type")]
    [SerializeField] ItemTypeSO[] _compatibleItemTypes = new ItemTypeSO[0];
    [Space(10)]
    [Tooltip("Triggers when the item is added from 0, it won't trigger if stacked")]
	[SerializeField] UnityEvent<ItemSO> _onItemAdded = new UnityEvent<ItemSO>();
    [Tooltip("Triggers when the item is totally removed")]
    [SerializeField] UnityEvent _onItemRemoved = new UnityEvent();
    [Tooltip("Triggers when the item amount changed")]
    [SerializeField] UnityEvent<int> _onAmountChanged = new UnityEvent<int>();
    ItemSO _currentItem;
    int _amount = 0;

    public InventorySlotDisplay Display { get => _display; }
    public ItemSO Item { get => _currentItem; }
    public int Amount { get => _amount; }
    public bool AutoAcceptItems { get => _autoAcceptItems; }

    public bool CheckCompatibility(ItemSO item)
    {   
        if (!item)
        {
            return true;
        }

        if (_compatibleItemTypes.Length > 0)
        {
            bool compatibleType = false;
            for (int i = 0; i < _compatibleItemTypes.Length; i++)
            {
                if(_compatibleItemTypes[i] == item.ItemType)
                {
                    compatibleType = true;
                    break;
                }
            }

            if (!compatibleType)
            {
                return false;
            }
        }

        return true;
    }

    public bool TryStackItem(ItemSO item, int amount)
    {
        if(!item || amount < 1 || !item.CanBeStacked)
        {
            return false;
        }

        if(item == _currentItem)
        {
            _amount += amount;
            _onAmountChanged?.Invoke(_amount);
            return true;
        }

        return false;
    }

    public bool TryAddOrStackItem(ItemSO item, int amount)
    {
        if (TryStackItem(item, amount))
        {
            return true;
        }

        if (!_currentItem && CheckCompatibility(item))
        {
            _currentItem = item;
            _amount = amount;
            _onItemAdded?.Invoke(item);
            _onAmountChanged?.Invoke(_amount);
            return true;
        }

        return false;
    }

    public void RemoveItem(int amount = 0)
    {
        if (amount >= _amount || amount == 0)
        {
            _onItemRemoved?.Invoke();
            _onAmountChanged?.Invoke(amount);
            _amount = 0;
            _currentItem = null;
            return;
        }

        _amount -= amount;
        _onAmountChanged?.Invoke(amount);
    }
}
