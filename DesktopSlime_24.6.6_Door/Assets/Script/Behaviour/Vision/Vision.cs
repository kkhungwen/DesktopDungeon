using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider2D))]
public class Vision : MonoBehaviour
{
    // This component should be place with colider in visible layer
    private BoxCollider2D visibleCollider;
    public event Action<UpdateVisionEventArgs> OnUpdateVision;

    [Space(10f)]
    [Header("BOX CAST")]
    [SerializeField] private LayerMask visionLayerMask;
    private Vector2 boxCastSize = new Vector2(0.1f, 10f);
    private float boxCastDistance = 20f;
    private Vector2 boxCastOffset = new Vector2(0f, 0.5f);

    private List<VisibleObject> right_VisibleObjectList = new();
    private List<VisibleObject> left_VisibleObjectList = new();

    private float lastCheckVisionTime = 0;

    private void Awake()
    {
        visibleCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Time.time < lastCheckVisionTime + Settings.checkVisionTime)
            return;
        lastCheckVisionTime = Time.time;

        CastVision();

        OnUpdateVision?.Invoke(new UpdateVisionEventArgs() { right_VisibleObjectList = right_VisibleObjectList, left_VisibleObjectList = left_VisibleObjectList, visionDistance = boxCastDistance });
    }

    private void CastVision()
    {
        // Raycast right direction
        RaycastHit2D[] right_RaycastHitArray = Physics2D.BoxCastAll((Vector2)transform.position + boxCastOffset, boxCastSize, 0, Vector2.right, boxCastDistance, visionLayerMask);

        // Raycast left direction
        RaycastHit2D[] left_RaycastHitArray = Physics2D.BoxCastAll((Vector2)transform.position + boxCastOffset, boxCastSize, 0, Vector2.left, boxCastDistance, visionLayerMask);

        // Remove raycast hit by both direction
        for (int i = 0; i < right_RaycastHitArray.Length; i++)
        {
            for (int j = 0; j < left_RaycastHitArray.Length; j++)
            {
                if (right_RaycastHitArray[i].transform == null || left_RaycastHitArray[j].transform == null)
                    continue;

                if (right_RaycastHitArray[i].collider != left_RaycastHitArray[j].collider)
                    continue;

                if (right_RaycastHitArray[i].transform.position.x > transform.position.x)
                    left_RaycastHitArray[j] = default;
                else
                    right_RaycastHitArray[i] = default;
            }
        }

        // convert raycastHit array to visableObject list
        right_VisibleObjectList.Clear();
        foreach (VisibleObject visibleObject in ConvertRaycastHitToVisibleObject(right_RaycastHitArray))
            right_VisibleObjectList.Add(visibleObject);

        left_VisibleObjectList.Clear();
        foreach (VisibleObject visibleObject in ConvertRaycastHitToVisibleObject(left_RaycastHitArray))
            left_VisibleObjectList.Add(visibleObject);
    }

    private IEnumerable ConvertRaycastHitToVisibleObject(RaycastHit2D[] raycastHitArray)
    {
        for (int i = 0; i < raycastHitArray.Length; i++)
        {
            if (raycastHitArray[i].collider == visibleCollider)
                continue;

            if (raycastHitArray[i].collider == null)
                continue;

            if (!raycastHitArray[i].collider.gameObject.activeSelf)
                continue;

            Tags tags = raycastHitArray[i].collider.GetComponent<Tags>();

            if (tags == null)
                Debug.Log(raycastHitArray[i].collider.transform + " : Visible collider should contains tags component");

            VisibleObject visibleObject = new() { transform = raycastHitArray[i].collider.transform, point = raycastHitArray[i].point, distance = raycastHitArray[i].distance, tags = tags };

            yield return visibleObject;
        }
    }
}
public class VisibleObject
{
    public Transform transform;
    public Vector2 point;
    public float distance;
    public Tags tags;
}

public class UpdateVisionEventArgs
{
    public List<VisibleObject> right_VisibleObjectList;
    public List<VisibleObject> left_VisibleObjectList;
    public float visionDistance;
}
