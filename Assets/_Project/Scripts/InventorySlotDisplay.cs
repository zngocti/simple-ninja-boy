using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TMP_Text _amountText;
    private int _index;

    public event Action<int> OnPointerEnterEvent;
    public event Action<int> OnPointerExitEvent;
    public event Action<int> OnBeginDragEvent;
    public event Action<int> OnEndDragEvent;
    public event Action<Vector2, int> OnDragEvent;

    Transform _parent;
    Vector2 _position;

    public int Index { get => _index; }

    void Start()
    {
        _parent = transform.parent;
        _position = transform.position;
    }

    public void RestoreOriginalState()
    {
        transform.SetParent(_parent);
        transform.position = _position;
        _contentImage.raycastTarget = true;
        _backgroundImage.raycastTarget = true;
    }

    public void TurnOnDragMode()
    {
        _contentImage.raycastTarget = false;
        _backgroundImage.raycastTarget = false;
        transform.SetParent(transform.root);       
    }

    public void Initialize(int index)
    {
        _index = index;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke(_index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent?.Invoke(_index);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(_index);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(eventData.position, _index);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(_index);
    }

    public void ClearView()
    {
        UpdateView(null);
    }

    public void UpdateView(ItemSO item)
    {
        if (item == null)
        {
            _contentImage.gameObject.SetActive(false);
        } 
        else
        {
            _contentImage.gameObject.SetActive(true);
            _contentImage.sprite = item.Icon;
        }        
    }

    public void UpdateAmount(int amount) 
    {
        _amountText.text = amount > 1 ? amount.ToString() : string.Empty;
    }

    public void ColorBackgroundNormal()
    {
        _backgroundImage.color = new Color(0,0,0,0);
    }

    public void ColorBackgroundRed()
    {
        _backgroundImage.color = new Color(0.94f,0.22f,0,0.4f);       
    }

    public void ColorBackgroundGreen()
    {
        _backgroundImage.color = new Color(0.39f,0.56f,0.40f,0.4f);
    }
}
