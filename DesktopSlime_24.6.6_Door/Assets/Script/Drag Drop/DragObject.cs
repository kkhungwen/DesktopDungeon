using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class DragObject : MonoBehaviour
{
    public event Action OnDropObject;

    [SerializeField] private LayerMask dragOnLayerMask;
    [SerializeField] private Collider2D selfDropOnCollider;

    public void DragOnObject(Vector2 position, object dragObject)
    {
        Collider2D[] dropOnColliderArray = Physics2D.OverlapPointAll(transform.position, dragOnLayerMask);

        foreach (Collider2D dropOnCollider in dropOnColliderArray)
        {
            if (dropOnCollider == null)
                continue;

            if (dropOnCollider == selfDropOnCollider)
                continue;

            if (!dropOnCollider.transform.TryGetComponent(out IDropOnObject dropOnOnject))
            {
                Debug.Log(dropOnCollider.transform.name + " Drop on collider should contain DropOnObject component");
                continue;
            }

            if (!dropOnOnject.DragOn(dragObject))
                continue;

            break;
        }
    }

    public void DropObject(object dropObject)
    {
        Collider2D[] dropOnColliderArray = Physics2D.OverlapPointAll(transform.position, dragOnLayerMask);

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);

        foreach(RaycastResult raycastResult in results)
        {
            if(raycastResult.gameObject.TryGetComponent(out IDropOnObject dropOnObject))
            {
                if (!dropOnObject.DropOn(dropObject))
                    continue;

                OnDropObject?.Invoke();
                return;
            }
        }

        foreach (Collider2D dropOnCollider in dropOnColliderArray)
        {
            if (dropOnCollider == null)
                continue;

            if (dropOnCollider == selfDropOnCollider)
                continue;

            if (!dropOnCollider.transform.TryGetComponent(out IDropOnObject dropOnObject))
            {
                Debug.Log(dropOnCollider.transform.name + " Drop on collider should contain DropOnObject component");
                continue;
            }

            if (!dropOnObject.DropOn(dropObject))
                continue;

            OnDropObject?.Invoke();
            return;
        }
    }
}
