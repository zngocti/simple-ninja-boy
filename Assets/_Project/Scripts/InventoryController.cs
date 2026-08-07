using UnityEngine;

public class InventoryController : MonoBehaviour
{
    static private InventoryController _instance;
    static public InventoryController Instance { get => _instance; }

    [SerializeField] InventorySlotController[] _inventorySlots = new InventorySlotController[0]; 

    int _currentDraggedSlot = -1;
    int _currentPointedSlot = -1;

    public InventorySlotController[] InventorySlots { get => _inventorySlots; }
    public int CurrentDraggedSlotIndex { get => _currentDraggedSlot; }
    public int CurrentPointedSlotIndex { get => _currentPointedSlot; }

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

    void Start()
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            _inventorySlots[i].Display.Initialize(i);
            _inventorySlots[i].Display.OnPointerEnterEvent += OnPointerEnterOnSlot;
            _inventorySlots[i].Display.OnPointerExitEvent += OnPointerExitOnSlot;
            _inventorySlots[i].Display.OnBeginDragEvent += OnBeginDragOnSlot;
            _inventorySlots[i].Display.OnDragEvent += OnDragOnSlot;
            _inventorySlots[i].Display.OnEndDragEvent += OnEndDragOnSlot;
        }
    }

    void OnPointerEnterOnSlot(int index)
    {
        if (_currentDraggedSlot < 0)
        {
            return;
        }

        if (_currentDraggedSlot == index)
        {
            return;
        }

        _currentPointedSlot = index;

        if (_inventorySlots[_currentPointedSlot].CheckCompatibility(_inventorySlots[_currentDraggedSlot].Item) &&
            _inventorySlots[_currentDraggedSlot].CheckCompatibility(_inventorySlots[_currentPointedSlot].Item))
        {
            _inventorySlots[_currentPointedSlot].Display.ColorBackgroundGreen();
        }
        else
        {
            _inventorySlots[_currentPointedSlot].Display.ColorBackgroundRed();
        }
    }

    void OnPointerExitOnSlot(int index)
    {
        if (_currentDraggedSlot < 0)
        {
            return;
        }

        if (_currentDraggedSlot == index)
        {
            return;
        }

        if (_currentPointedSlot != index)
        {
            return;
        }

        _inventorySlots[_currentPointedSlot].Display.ColorBackgroundNormal();
        _currentPointedSlot = -1;
    }

    void OnBeginDragOnSlot(int index)
    {
        if (_inventorySlots[index].Item == null)
        {
            return;
        }

        _currentDraggedSlot = index;
        _inventorySlots[_currentDraggedSlot].Display.TurnOnDragMode();
    }

    void OnDragOnSlot(Vector2 position, int index)
    {
        if (_currentDraggedSlot != index)
        {
            return;
        }

        _inventorySlots[_currentDraggedSlot].transform.position = position;
    }

    void OnEndDragOnSlot(int index)
    {
        if (_currentDraggedSlot != index)
        {
            return;
        }

        _inventorySlots[_currentDraggedSlot].Display.RestoreOriginalState();

        if (_currentPointedSlot >= 0)
        {
            _inventorySlots[_currentPointedSlot].Display.ColorBackgroundNormal();            
        }

        if (_currentPointedSlot < 0 || _currentDraggedSlot == _currentPointedSlot)
        {
            _currentDraggedSlot = -1;
            return;
        }

        if(_inventorySlots[_currentPointedSlot].TryAddOrStackItem(_inventorySlots[_currentDraggedSlot].Item, _inventorySlots[_currentDraggedSlot].Amount))
        {
            _inventorySlots[_currentDraggedSlot].RemoveItem();
        }        
        else if(_inventorySlots[_currentPointedSlot].CheckCompatibility(_inventorySlots[_currentDraggedSlot].Item) &&
                _inventorySlots[_currentDraggedSlot].CheckCompatibility(_inventorySlots[_currentPointedSlot].Item))
        {
            ItemSO itemTemp = _inventorySlots[_currentPointedSlot].Item;
            int amountTemp = _inventorySlots[_currentPointedSlot].Amount;
            _inventorySlots[_currentPointedSlot].RemoveItem();
            _inventorySlots[_currentPointedSlot].TryAddOrStackItem(_inventorySlots[_currentDraggedSlot].Item, _inventorySlots[_currentDraggedSlot].Amount);
            _inventorySlots[_currentDraggedSlot].RemoveItem();
            _inventorySlots[_currentDraggedSlot].TryAddOrStackItem(itemTemp, amountTemp);             
        }

        _currentDraggedSlot = -1;
    }

    public bool TryPickupItem(ItemSO item, int amount = 1)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (!_inventorySlots[i].AutoAcceptItems)
            {
                continue;
            }

            if (_inventorySlots[i].TryAddOrStackItem(item, amount))
            {
                return true;
            }
        }

        return false;
    }

    public void ResetSlotInteraction()
    {
        if (_currentDraggedSlot >= 0)
        {
            _inventorySlots[_currentDraggedSlot].Display.RestoreOriginalState();
            _currentDraggedSlot = -1;
        }

        if (_currentPointedSlot >= 0)
        {
            _inventorySlots[_currentPointedSlot].Display.ColorBackgroundNormal();
            _currentPointedSlot = -1;
        }
    }
}
