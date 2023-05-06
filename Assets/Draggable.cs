using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 _startPosition;
    private Transform _startParent;
    private bool _isDragged;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _startParent = transform.parent;
        transform.SetParent(transform.root);
        _isDragged = true;
        ShopManager.instance.OnBeginDrag(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

  public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isDragged) return;
       transform.position = _startPosition;
        transform.SetParent(_startParent);
       _isDragged = false;
     ShopManager.instance.OnEndDragShopItem();
}
    
    
}
