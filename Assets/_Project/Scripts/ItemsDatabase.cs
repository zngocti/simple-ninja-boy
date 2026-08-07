using System;
using UnityEngine;

public class ItemsDatabase : MonoBehaviour
{
    static private ItemsDatabase _instance;
    static public ItemsDatabase Instance { get => _instance; }

    [SerializeField] ItemSO[] _items = new ItemSO[0];

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
    }

    public void GenerateItem(int itemID, int amount, InventorySlotController slot)
    {
        slot.ClearItem();

        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].ItemID == itemID)
            {
                slot.TryAddOrStackItem(_items[i], amount);
                return;
            }
        }
    }
}
