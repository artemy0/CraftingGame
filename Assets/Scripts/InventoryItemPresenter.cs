using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemPresenter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action Ejecting;

    [SerializeField] private Text _nameField;
    [SerializeField] private Image _iconFiedl;

    private Transform _draggingParent;
    private Transform _originalParent;
    //private RectTransform _inventoryGridRectTransform;

    public void Init(Transform draggingParent)
    {
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
    }

    public void Render(IItem item)
    {
        _nameField.text = item.Name;
        _iconFiedl.sprite = item.UIIcon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(_draggingParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.pointerCurrentRaycast.screenPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //мы используем RectTransform (который размером с весь инвентарь) родительского элемента Content (который размером с совокупность размеров объектов InventoryItemPresenter)
        RectTransform inventoryGridRectTransform = (RectTransform)_originalParent.parent;
        if (In(inventoryGridRectTransform))
        {
            InsertInInventoryGrid();
        }
        else
        {
            Eject();
        }
    }

    private void Eject()
    {
        Ejecting?.Invoke();
    }

    private void InsertInInventoryGrid()
    {
        int closestIndex = 0;

        for (int i = 0; i < _originalParent.childCount; i++)
        {
            if (Vector3.Distance(transform.position, _originalParent.GetChild(i).position) < Vector3.Distance(transform.position, _originalParent.GetChild(closestIndex).position))
            {
                closestIndex = i;
            }
        }

        transform.SetParent(_originalParent);
        transform.SetSiblingIndex(closestIndex);
    }

    private bool In(RectTransform rectTransform)
    {
        return rectTransform.rect.Contains(transform.position);
    }
}
