using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action onPointerClick;
    public event Action onBeginDrag;
    public event Action<Vector2> onDrag;
    public event Action onEndDrag;
    public event Action onPointerEnter;
    public event Action onPointerExit;

    public void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(HelperUtils.GetMouseWorldPosition());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onEndDrag?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onPointerClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke();
    }
}
