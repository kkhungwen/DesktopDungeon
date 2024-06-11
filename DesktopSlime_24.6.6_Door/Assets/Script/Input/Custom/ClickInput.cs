using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickInput : MonoBehaviour
{
    [SerializeField] private float holdThreshHold;
    [SerializeField] private float dragDistanceThreshHold;

    private IClickable currentOverClickable;
    private IClickable currentDragClickable;
    private Vector2 leftMouseDownPosition;
    private Vector2 targetRelativePosition;
    private Vector2 mouseWorldPosition;
    private bool isStartLeftMouseDrag;
    private float leftMouseHoldTime;
    private Collider2D[] overlapColliderArray = new Collider2D[10];

    private void Awake()
    {
        leftMouseHoldTime = 0;
        currentDragClickable = null;
        isStartLeftMouseDrag = false;
    }

    private void Update()
    {
        mouseWorldPosition = HelperUtils.GetMouseWorldPosition();

        // Try physics overlap collider
        Array.Clear(overlapColliderArray, 0, overlapColliderArray.Length);
        overlapColliderArray = Physics2D.OverlapPointAll(mouseWorldPosition);

        HandleMouseOver();

        if (Input.GetMouseButtonDown(0))
            HandleLeftMouseDown();

        if (Input.GetMouseButton(0))
            HandleLeftMouseHold();

        if (!Input.GetMouseButton(0))
            HandleLeftMouseUp();
    }

    private void HandleMouseOver()
    {
        foreach (Collider2D collider in overlapColliderArray)
        {
            if (!collider.TryGetComponent(out IClickable overClickable))
                continue;

            if(currentOverClickable == null)
            {
                overClickable.Enter();
                currentOverClickable = overClickable;
                return;
            }

            if(currentOverClickable == overClickable)
            {
                currentOverClickable.Over();
                return;
            }

            if(currentOverClickable != overClickable)
            {
                currentOverClickable.Exit();
                overClickable.Enter();
                currentOverClickable = overClickable;
                return;
            }
        }

        if (currentOverClickable != null)
        {
            currentOverClickable.Exit();
            currentOverClickable = null;
            return;
        }
    }

    private void HandleLeftMouseDown()
    {
        // Reset parameters
        currentDragClickable = null;
        isStartLeftMouseDrag = false;
        leftMouseHoldTime = 0;
        leftMouseDownPosition = mouseWorldPosition;

        foreach (Collider2D collider in overlapColliderArray)
        {
            if (!collider.TryGetComponent(out currentDragClickable))
                continue;

            targetRelativePosition = currentDragClickable.GetWorldPosition() - leftMouseDownPosition;
            break;
        }
    }

    private void HandleLeftMouseUp()
    {
        // If there is Clickable being held
        if (currentDragClickable == null)
            return;

        // Call IClickable mouse up function
        if (leftMouseHoldTime < holdThreshHold)
            currentDragClickable.LeftClick(mouseWorldPosition);

        currentDragClickable.LeftMouseUp(mouseWorldPosition, mouseWorldPosition + targetRelativePosition);

        // Reset parameters
        isStartLeftMouseDrag = false;
        leftMouseHoldTime = 0;
        currentDragClickable = null;
    }

    private void HandleLeftMouseHold()
    {
        // If there is Clickable being held
        if (currentDragClickable == null)
            return;

        leftMouseHoldTime += Time.deltaTime;
        float dragDistance = Vector2.Distance(leftMouseDownPosition, mouseWorldPosition);

        // If held longer then hold threshhold     or     mouse move furthur then drag distance thrashhold
        if (leftMouseHoldTime < holdThreshHold && dragDistance < dragDistanceThreshHold)
            return;

        if (!isStartLeftMouseDrag)
        {
            currentDragClickable.StartLeftDrag();
            isStartLeftMouseDrag = true;
        }

        currentDragClickable.LeftDrag(mouseWorldPosition, mouseWorldPosition + targetRelativePosition);
    }
}
