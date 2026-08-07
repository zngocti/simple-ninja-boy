using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] RectTransform _panel;
    [SerializeField] TMP_Text _title;
    [SerializeField] TMP_Text _itemType;
    [SerializeField] TMP_Text _stats;

    bool _followingMouse = false;
    float _halfScreenWidth;
    float _halfScreenHeight;
    float _halfPanelWidth;
    float _halfPanelHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InventorySlotController[] slots = InventoryController.Instance.InventorySlots;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Display.OnPointerEnterEvent += OnPointerEnterOnSlot;
            slots[i].Display.OnPointerExitEvent += OnPointerExitOnSlot;
        }

        _halfScreenWidth = Screen.width * 0.5f;
        _halfScreenHeight = Screen.height * 0.5f;
        _halfPanelWidth = _panel.rect.width * 0.5f * _canvas.scaleFactor;
        _halfPanelHeight = _panel.rect. height * 0.5f * _canvas.scaleFactor;
    }

    void Update()
    {
        if (_followingMouse)
        {
            UpdatePosition();
        }
    }

    void OnPointerEnterOnSlot(int index)
    {
        if (InventoryController.Instance.CurrentDraggedSlotIndex >= 0)
        {
            return;
        }

        ItemSO item = InventoryController.Instance.InventorySlots[index].Item;

        if (!item)
        {
            return;
        }

        _panel.gameObject.SetActive(true);
        UpdatePanel(item);
        _followingMouse = true;
    }

    void OnPointerExitOnSlot(int index)
    {
        if (_followingMouse)
        {
            TurnOff();   
        }
    }

    void UpdatePanel(ItemSO item)
    {
        _title.text = item.ItemName;
        _itemType.text = item.ItemType.ItemTypeName;
        _stats.text = item.StatsText();
    }

    void OnDisable()
    {
        TurnOff();
    }

    void TurnOff()
    {
        _panel.gameObject.SetActive(false);
        _followingMouse = false;
    }

    void UpdatePosition()
    {
        Vector2 pos = Mouse.current.position.ReadValue();

        if(pos.x < _halfScreenWidth && pos.y < _halfScreenHeight)
        {
            transform.position = pos + new Vector2(_halfPanelWidth, _halfPanelHeight);
        }
        else if (pos.x > _halfScreenWidth && pos.y < _halfScreenHeight)
        {
            transform.position = pos + new Vector2(-_halfPanelWidth, _halfPanelHeight);
        }
        else if (pos.x < _halfScreenWidth && pos.y > _halfScreenHeight)
        {
            transform.position = pos + new Vector2(_halfPanelWidth, -_halfPanelHeight);
        }
        else
        {
            transform.position = pos + new Vector2(-_halfPanelWidth, -_halfPanelHeight);
        }
    }
}
