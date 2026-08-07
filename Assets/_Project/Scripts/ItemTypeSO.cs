using UnityEngine;

[CreateAssetMenu(fileName = "New Item Type SO", menuName = "Scriptable Objects/ItemTypeSO")]
public class ItemTypeSO : ScriptableObject
{
    [SerializeField] string _itemTypeName;
    public string ItemTypeName { get => _itemTypeName; }
}
