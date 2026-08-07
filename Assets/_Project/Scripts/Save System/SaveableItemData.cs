using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveableItemData
{
    [SerializeField] List<string> _id = new List<string>();
    [SerializeField] List<int> _items = new List<int>();
    [SerializeField] List<int> _amount = new List<int>();

    public void SaveData(InventorySlotController data)
    {
        int index = _id.IndexOf(data.SaveID);


        if (index > -1)
        {
            if (!data.Item)
            {
                _items[index] = -1;    
                _amount[index] = 0;
            }
            else
            {
                _items[index] = data.Item.ItemID;
                _amount[index] = data.Amount;
            }
        }
        else
        {
            _id.Add(data.SaveID);

            if (!data.Item)
            {
                _items.Add(-1);
                _amount.Add(0);                
            }
            else
            {
                _items.Add(data.Item.ItemID);
                _amount.Add(data.Amount);
            }
        }
    }

    public void LoadData(InventorySlotController data)
    {
        int index = _id.IndexOf(data.SaveID);        

        if (index < 0)
        {
            data.ClearItem();
            return;
        }

        ItemsDatabase.Instance?.GenerateItem(_items[index], _amount[index], data);
    } 
}
