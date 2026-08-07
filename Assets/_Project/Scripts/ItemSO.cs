using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] string _itemName;
    [SerializeField] Sprite _icon;
    [SerializeField] ItemTypeSO _itemType;
    [SerializeField] bool _canBeStacked = false;

    public string ItemName { get => _itemName; }
    public Sprite Icon { get => _icon; }
    public ItemTypeSO ItemType { get => _itemType; }
    public bool CanBeStacked { get => _canBeStacked; }
}
