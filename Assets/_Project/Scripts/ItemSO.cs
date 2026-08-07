using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] string _itemName;
    [SerializeField] Sprite _icon;
    [SerializeField] ItemTypeSO _itemType;
    [SerializeField] bool _canBeStacked = false;
    [SerializeField] int _healthRestored;
    [SerializeField] int _attack;
    [SerializeField] int _magic;

    public string ItemName { get => _itemName; }
    public Sprite Icon { get => _icon; }
    public ItemTypeSO ItemType { get => _itemType; }
    public bool CanBeStacked { get => _canBeStacked; }
    public int HealthRestored { get => _healthRestored; }
    public int Attack { get => _attack; }
    public int Magic { get => _magic; }
    
    public string StatsText()
    {
        string text = string.Empty;

        if(_healthRestored > 0)
        {
            text += $"Restores {_healthRestored} health\n";
        }
        else if(_healthRestored < 0)
        {
            text += $"Deals {_healthRestored} damage on consumption\n";
        }

        if(_attack > 0)
        {
            text += $"+{_attack} damage\n";
        }

        if(_magic > 0)
        {
            text += $"+{_magic} magic damage";
        }

        return text;
    }
}
