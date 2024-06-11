using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(EdgeCollider2D))]
[DisallowMultipleComponent]
public class Boundaries : MonoBehaviour
{
    private EdgeCollider2D boundsCollider;

    private Vector2 workingSpaceLowerBoundPosition;
    private Vector2 workingSpaceUpperBoundPosition;

    private Vector2 screenLowerBound;
    private Vector2 screenUpperBound;

    [SerializeField] private BoxCollider2D rightVisibleCollider;
    [SerializeField] private BoxCollider2D leftVisibleCollider;


    private void Start()
    {
        Invoke("SetUpBoundaries", .01f);
    }

    private void SetUpBoundaries()
    {
        workingSpaceLowerBoundPosition = WindowFormsDllUtils.GetWorkingSpaceLowerBoundPosition() - (Vector2)Camera.main.transform.position;
        workingSpaceUpperBoundPosition = WindowFormsDllUtils.GetWorkingSpaceUpperBoundPosition() - (Vector2)Camera.main.transform.position;

        boundsCollider = GetComponent<EdgeCollider2D>();
        boundsCollider.points = new Vector2[5] {
            workingSpaceLowerBoundPosition,
            new Vector2(workingSpaceUpperBoundPosition.x, workingSpaceLowerBoundPosition.y),
            workingSpaceUpperBoundPosition,
            new Vector2(workingSpaceLowerBoundPosition.x, workingSpaceUpperBoundPosition.y),
            workingSpaceLowerBoundPosition};

        float visibleColliderWidth = 2;

        rightVisibleCollider.size = new Vector2(visibleColliderWidth, workingSpaceUpperBoundPosition.y - workingSpaceLowerBoundPosition.y);
        rightVisibleCollider.transform.position = new Vector2(workingSpaceUpperBoundPosition.x + visibleColliderWidth / 2, workingSpaceUpperBoundPosition.y / 2 + workingSpaceLowerBoundPosition.y / 2);

        leftVisibleCollider.size = new Vector2(visibleColliderWidth, workingSpaceUpperBoundPosition.y - workingSpaceLowerBoundPosition.y);
        leftVisibleCollider.transform.position = new Vector2(workingSpaceLowerBoundPosition.x - visibleColliderWidth / 2, workingSpaceUpperBoundPosition.y / 2 + workingSpaceLowerBoundPosition.y / 2);

#if UNITY_EDITOR

        screenLowerBound = Camera.main.ScreenToWorldPoint(Vector2.zero);
        screenUpperBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        boundsCollider.points = new Vector2[5] {
            screenLowerBound,
            new Vector2(screenUpperBound.x, screenLowerBound.y),
            screenUpperBound,
            new Vector2(screenLowerBound.x, screenUpperBound.y),
            screenLowerBound};

        rightVisibleCollider.size = new Vector2(visibleColliderWidth, screenUpperBound.y - screenLowerBound.y);
        rightVisibleCollider.transform.position = new Vector2(screenUpperBound.x + visibleColliderWidth / 2, screenUpperBound.y / 2 + screenLowerBound.y / 2);

        leftVisibleCollider.size = new Vector2(visibleColliderWidth, screenUpperBound.y - screenLowerBound.y);
        leftVisibleCollider.transform.position = new Vector2(screenLowerBound.x - visibleColliderWidth / 2, screenUpperBound.y / 2 + screenLowerBound.y / 2);

#endif
    }

    public Vector2 GetWorkingSpaceLowerBoundPosition()
    {
#if UNITY_EDITOR
        return screenLowerBound + (Vector2)Camera.main.transform.position;
#endif

        return workingSpaceLowerBoundPosition + (Vector2)Camera.main.transform.position;
    }

    public Vector2 GetWorkingSpaceUpperBoundPosition()
    {
#if UNITY_EDITOR
        return screenUpperBound + (Vector2)Camera.main.transform.position;
#endif

        return workingSpaceUpperBoundPosition + (Vector2)Camera.main.transform.position;
    }
}
